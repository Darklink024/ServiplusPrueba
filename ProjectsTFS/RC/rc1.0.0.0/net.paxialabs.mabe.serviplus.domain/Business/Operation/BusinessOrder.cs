using net.paxialabs.mabe.serviplus.data.Model;
using net.paxialabs.mabe.serviplus.data.Repository.Operation;
using net.paxialabs.mabe.serviplus.domain.Business.Notification;
using net.paxialabs.mabe.serviplus.domain.Business.Users;
using net.paxialabs.mabe.serviplus.domain.Facade.Interface;
using net.paxialabs.mabe.serviplus.domain.Facade.Users;
using net.paxialabs.mabe.serviplus.entities.Entity.Interface;
using net.paxialabs.mabe.serviplus.entities.Entity.Operation;
using net.paxialabs.mabe.serviplus.entities.ModelView.Operation;
using net.paxialabs.mabe.serviplus.entities.ModelView.Users;
using net.paxialabs.mabe.serviplus.security;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GemBox.Spreadsheet;
using System.IO;
using net.paxialabs.mabe.serviplus.domain.Business.Security;
using System.Net;
using net.paxialabs.mabe.serviplus.domain.Business.Interface;
using net.paxialabs.mabe.serviplus.entities.Entity.Security;
using System.Device.Location;
using net.paxialabs.mabe.serviplus.entities.Entity.SAP;

namespace net.paxialabs.mabe.serviplus.domain.Business.Operation
{
    internal class BusinessOrder
    {
        public void UpdateStatusOrderWS(string orderID, string EstatusEsq, string EstatusCabecera, string tokenApp, string user, string password)
        {
            //try
            //{
            if (tokenApp != GlobalConfiguration.TokenWS)
                throw new Exception("TokenInvalid");
            var dataUser = new BusinessSecurity().Authenticate(user, password);
            if (dataUser == null) throw new Exception("UserPasswordInvalid");
            string msgLog = "";

            msgLog += "-----------WS DELTA INICIO UTC " + DateTime.UtcNow.ToString() + Environment.NewLine;

            // actualizar ODS o recuperar del sftp
            var order = GetByOrderID(orderID);
            if (order != null)
            {
                var estatusesquema = new BusinessStatusScheme().GetStatusScheme(EstatusEsq, EstatusCabecera);
                order.FK_StatusSchemeID = estatusesquema.PK_StatusSchemeID;
                order.SendCRM = "OK";
                var causeOrder = new EntityStatusOrder();
                try
                {
                    causeOrder = new BusinessStatusCauseOrder().GetAll().Where(p => p.StatusOrder1 == estatusesquema.Description.ToUpper()).First();
                    var monitor = new BusinessMonitor().GetAll().Where(p => p.OrderID == order.PK_OrderID).First();
                    monitor.StatusOrderID = causeOrder.PK_StatusOrderID;
                    //monitor.CauseOrderID = causeOrder.PK_CauseOrderID;
                    switch (estatusesquema.Description)
                    {
                        case "Pendiente":
                            monitor.StatusVisitID = 2;
                            break;
                        case "Cancelada":
                            monitor.StatusVisitID = 5;
                            break;
                        case "Terminada":
                            monitor.StatusVisitID = 4;
                            break;
                        default:
                            monitor.StatusVisitID = 1;
                            break;
                    }
                    new BusinessMonitor().Update(monitor);
                }
                catch
                {
                    throw new Exception("StatusNoFound");
                }
                Update(order);

                msgLog += "Actualizando ODS " + orderID + Environment.NewLine;
                // Notificar usuario

                string title = string.Format("Actualización ODS: {0}, Estatus: {1}", orderID, EstatusEsq);
                string body = "Favor de sincronizar <<<para actualizar tu ruta. En caso de estar atendiendo una orden, sincronizar al cierre.";

                var empleado = new BusinessEmployee().GetID(order.FK_EmployeeID.Value);
                var usuario = new BusinessUsers().Get(empleado.FK_UserID.Value);
                List<int> usuarios = new List<int>();
                usuarios.Add(usuario.UserID);

                msgLog += "Notificando Técnico(s) " + empleado.EmployeeID + Environment.NewLine;

                new BusinessNotification().Insert(new ModelViewNotification()
                {
                    CreateDate = DateTime.UtcNow,
                    Message = body,
                    MessageID = 0,
                    MessageRead = false,
                    ModifyDate = DateTime.UtcNow,
                    Status = true,
                    Title = title,
                    Url = "",
                    UserID = usuario.UserID
                });

                new BusinessNotification().SendPush(usuarios, title, body);
            }

            // No aplica platicando con Paco solo es cambio de Estatus
            //else
            //{
            //    msgLog += "Descarga FTPS ODS " + Environment.NewLine;

            //    //Descargar FTP
            //    string DownloadFolder = "";
            //    List<string> arrFiles = new List<string>();
            //    List<string> arrFilesOK = new List<string>();
            //    FacadeInterface.Download(true, out arrFiles, out arrFilesOK, out DownloadFolder);

            //    msgLog += "Procesando ODS " + orderID + Environment.NewLine;

            //    FacadeInterface.Process(DownloadFolder);

            //}



            msgLog += "-----------WS DELTA FIN UTC " + DateTime.UtcNow.ToString() + Environment.NewLine;

            new BusinessWSLogger().WriteEntry(msgLog);
            //}
            //catch(Exception ex)
            //{
            //    new BusinessWSLogger().WriteError(ex, "BusinessOrder - UpdateStatusOrderWS");
            //}
        }

        public void UpdateStatusOrder(ModelViewUpdateStatusOrder data)
        {
            if (data.TokenApp != GlobalConfiguration.TokenWEB)
                if (data.TokenApp != GlobalConfiguration.TokenMobile)
                    throw new Exception("TokenInvalid");

            var dataODS = GetByOrderID(data.ODS);
            
            if (data.Latitude == "" && data.Logitude == "")
            {
                var lstRefEnviadas = data.SpareParts.Select(p => p.SparePartsCode).ToList();

                var dataSparePart = new BusinessSparePart().GetByOrder(dataODS.OrderID).Where(p => p.FK_WorkforceID == null && p.SparePartStatus != "E0003" && !lstRefEnviadas.Contains(p.RefManID));
                foreach (var itemSP in dataSparePart)
                {
                    itemSP.SparePartStatus = "E0003";
                    new BusinessSparePart().Update(itemSP);
                }


                // lista de ref desde bd
                var dataSparePartsList = new BusinessSparePart().GetByOrderID(dataODS.PK_OrderID);

                //SpareParts                
                foreach (var itemSP in data.SpareParts)
                {

                    if (new BusinessSparePart().GetListByRefManID(dataODS.PK_OrderID, itemSP.SparePartsCode).Count() > 0)
                    {
                        foreach (var dataSpareParts in new BusinessSparePart().GetListByRefManID(dataODS.PK_OrderID, itemSP.SparePartsCode))
                        {
                            
                            if (dataSpareParts.SparePartStatus == "E0003")
                            {
                                if (dataSparePartsList.Where(p => p.RefManID == itemSP.SparePartsCode && p.SparePartStatus != "E0003").Count() > 0)
                                {
                                    dataSpareParts.Quantity = itemSP.SparePartsQuantity;
                                    dataSpareParts.Price = itemSP.SparePartsPrice;
                                    dataSpareParts.SparePartStatus = itemSP.SparePartsStatus;
                                    new BusinessSparePart().Update(dataSpareParts);
                                }
                                else
                                {
                                    var produc = new BusinessBuildOfMaterial().GetByID(itemSP.BuildOfMaterialsID);
                                    new BusinessSparePart().Insert(dataODS.PK_OrderID, itemSP.BuildOfMaterialsID, produc.FK_ProductID, null, itemSP.SparePartsQuantity, "0", itemSP.SparePartsStatus, "ZMX00003", itemSP.SparePartsDescription, itemSP.SparePartsCode, float.Parse(itemSP.SparePartsPrice.ToString()));
                                }
                            }
                            else
                            {
                                if (dataSpareParts.SparePartStatus != "E0004")
                                {
                                    dataSpareParts.Quantity = itemSP.SparePartsQuantity;
                                    dataSpareParts.Price = itemSP.SparePartsPrice;
                                    dataSpareParts.SparePartStatus = itemSP.SparePartsStatus;
                                    new BusinessSparePart().Update(dataSpareParts);
                                }
                            }
                                                        
                        }
                    }
                    else
                    {
                        var produc = new BusinessBuildOfMaterial().GetByID(itemSP.BuildOfMaterialsID);
                        new BusinessSparePart().Insert(dataODS.PK_OrderID, itemSP.BuildOfMaterialsID, produc.FK_ProductID, null, itemSP.SparePartsQuantity, "0", itemSP.SparePartsStatus, "ZMX00003", itemSP.SparePartsDescription, itemSP.SparePartsCode, float.Parse(itemSP.SparePartsPrice.ToString()));
                    }
                }

                // actualización de refacciones 
                List<srUpdateRefMan.DT_ActRefMan_OutItem> itemsRefMan = new List<srUpdateRefMan.DT_ActRefMan_OutItem>();
                srUpdateRefMan.DT_ActRefMan_OutItem itemRefManHeader = new srUpdateRefMan.DT_ActRefMan_OutItem();
                List<srUpdateRefMan.DT_ActRefMan_OutItemRefMan> itemRefManDetail = new List<srUpdateRefMan.DT_ActRefMan_OutItemRefMan>();

                dataSparePart = new BusinessSparePart().GetByOrder(dataODS.OrderID).Where(p => p.FK_WorkforceID == null);

                foreach (var itemSP in dataSparePart)
                {
                    itemRefManDetail.Add(new srUpdateRefMan.DT_ActRefMan_OutItemRefMan()
                    {
                        Cantidad = itemSP.Quantity.ToString(),
                        Item_Ref_Man = itemSP.RefManID,
                        NumeroPosicion = itemSP.PosicionItem == "0" ? "" : itemSP.PosicionItem,
                        EstatusItem_RefMan = itemSP.SparePartStatus,
                        MotivoRechazo = itemSP.SparePartStatus == "E0003" ? "04" : "",
                        NumeroFactura = "",
                        Proveedor = ""
                    });
                }

                itemRefManHeader.IDOrden = dataODS.OrderID;
                itemRefManHeader.RefMan = itemRefManDetail.ToArray();
                itemsRefMan.Add(itemRefManHeader);
                new BusinessMabe().UpdateRefMan(itemsRefMan);

            }
            else
            {
                var dataMonitor = new BusinessMonitor().GetByOrderID(dataODS.PK_OrderID); 

                dataMonitor.SequenceVisit = data.Secuence;
                dataMonitor.StatusVisitID = new BusinessStatusCauseVisit().GetAll().Where(p => p.StatusVisit1 == data.Status).Single().PK_StatusVisitID;
                if (data.Status == "Progress")
                {
                    dataMonitor.StartVisitDate = data.StartVisitDate;  
                    dataMonitor.StatusVisitID = 3; // Progress
                    if (!dataMonitor.LatitudeAddress.HasValue && !dataMonitor.LogitudeAddress.HasValue)
                    {
                        dataMonitor.LatitudeAddress = float.Parse(data.Latitude);
                        dataMonitor.LogitudeAddress = float.Parse(data.Logitude);
                    }
                    dataMonitor.LatitudeStartVisit = float.Parse(data.Latitude);
                    dataMonitor.LogitudeStartVisit = float.Parse(data.Logitude);
                    dataMonitor.ModifyDate = DateTime.UtcNow;
                }
                new BusinessMonitor().Update(dataMonitor);
            }


        }

        public string Complete(ModelViewODSMovilUploadList data)
        {
            List<string> ODS = new List<string>();
            foreach (var item in data.Visits)
            {
                int UserModuleID = 0;
                int UserID = 0;

                try
                {

                    var orden = GetByOrderID(item.Detail.ODS);
                    var dataEmpleado = new BusinessEmployee().GetID(orden.FK_EmployeeID.Value);
                    var dataUsuario = new BusinessUsers().Get(dataEmpleado.FK_UserID.Value);
                    UserModuleID = dataUsuario.FK_ModuleID.HasValue ? dataUsuario.FK_ModuleID.Value : 0;
                    UserID = dataUsuario.UserID;

                    EntityMobileSync Mobile = new EntityMobileSync();
                    Mobile.PK_MobileSyncID = 0;
                    Mobile.FK_UserID = UserID;
                    Mobile.UserName = dataUsuario.UserName;
                    Mobile.Name = dataUsuario.Name;
                    Mobile.ODS = item.Detail.ODS;
                    Mobile.Type = "Subida";
                    Mobile.SyncDate = DateTime.UtcNow.ToLocalTime();
                    Mobile.Status = true;
                    Mobile.CreateDate = DateTime.UtcNow;
                    Mobile.ModifyDate = DateTime.UtcNow;
                    new BusinessMobileSync().Insert(Mobile);
                }
                catch(Exception ex)
                {
                    new BusinessMobileLogger().WriteEntry("Error MobileSync Subida - " + ex.Message);
                }


                var dataODS = GetByOrderID(item.Detail.ODS);

                var dataMonitor = new BusinessMonitor().GetByOrderID(dataODS.PK_OrderID); //.GetAll().Where(p => p.OrderID == dataODS.PK_OrderID).First();

                dataMonitor.CauseOrderID = item.CauseOrderID;
                dataMonitor.StatusOrderID = item.StatusOrderID;
                dataMonitor.StatusVisitID = item.StatusVisitID;
                //dataMonitor.SequenceVisit = item.SequenceVisit;

                dataMonitor.StartVisitDate = item.StartVisitDate;
                dataMonitor.EndVisitDate = item.EndVisitDate;
                dataMonitor.StartOrderDate = item.StartOrderDate;
                dataMonitor.EndOrderDate = item.EndOrderDate;
                dataMonitor.StartServiceDate = item.StartServiceDate;
                dataMonitor.EndServiceDate = item.EndServiceDate;
                dataMonitor.LatitudeStartVisit = item.LatitudeStartVisit;
                dataMonitor.LogitudeStartVisit = item.LogitudeStartVisit;
                dataMonitor.LatitudeEndVisit = item.LatitudeEndVisit;
                dataMonitor.LogitudeEndVisit = item.LogitudeEndVisit;
                dataMonitor.LatitudeStartOrder = item.LatitudeStartOrder;
                dataMonitor.LogitudeStartOrder = item.LogitudeStartOrder;
                dataMonitor.LatitudeEndOrder = item.LatitudeEndOrder;
                dataMonitor.LogitudeEndOrder = item.LogitudeEndOrder;
                          

                dataMonitor.DurationVisit = item.DurationVisit;
                dataMonitor.DurationOrder = item.DurationOrder;
                dataMonitor.DurationExecute = item.DurationExecute;
                //dataMonitor.DurationTransport = item.DurationTransport;

                dataMonitor.NoteVisit = item.NoteVisit;
                dataMonitor.NoteOrder = item.NoteOrder;

                dataMonitor.Status = item.Status;
                //dataMonitor.CreateDate = item.CreateDate;
                dataMonitor.ModifyDate = DateTime.UtcNow;
                dataMonitor.ExtraKilometrer = 0;

                new RepositoryMonitorOrder().Update(dataMonitor);

                 
                
                //Detail                
                //var NegocioBaseInstalada = ;
                var dataInstallBase = new BusinessInstalledBase().GetByID(dataODS.FK_InstalledBaseID);
                

                DateTime ShopDate;
                //dataInstallBase.ShopDate = null;

                if (DateTime.TryParse(item.Detail.PurchaseDate, out ShopDate))
                {
                    dataInstallBase.ShopDateFlag = dataInstallBase.ShopDate != ShopDate ? true : false;
                    dataInstallBase.ShopDate = ShopDate;
                }
                dataInstallBase.SerialNumberFlag = dataInstallBase.SerialNumber != item.Detail.SerialNumber ? true : false;
                dataInstallBase.SerialNumber = item.Detail.SerialNumber;

                if (item.Detail.PurchasePlace.Contains("-"))
                {
                    dataInstallBase.ShopPlaceIDFlag = dataInstallBase.FK_ShopPlaceID != new BusinessShopPlace().GetByShopPlace(item.Detail.PurchasePlace.Split('-')[0].Trim()).PK_ShopPlaceID ? true : false;
                    dataInstallBase.FK_ShopPlaceID = new BusinessShopPlace().GetByShopPlace(item.Detail.PurchasePlace.Split('-')[0].Trim()).PK_ShopPlaceID;
                }
                dataInstallBase.ProductIDFlag = dataInstallBase.FK_ProductID != new BusinessProduct().GetByModel(item.Detail.ProductModel).PK_ProductID ? true : false;
                dataInstallBase.FK_ProductID = new BusinessProduct().GetByModel(item.Detail.ProductModel).PK_ProductID;

                new BusinessInstalledBase().Update(dataInstallBase);


                // Order
                try
                {
                    //SymptomsODS
                    int countFaults = 1;
                    foreach (var itemS in item.SymptomsODS.FaultsODS)
                    {
                        switch (countFaults)
                        {
                            case 1:
                                dataODS.Failure1 = itemS.FaultCode;
                                break;
                            case 2:
                                dataODS.Failure2 = itemS.FaultCode;
                                break;
                            case 3:
                                dataODS.Failure3 = itemS.FaultCode;
                                break;
                            case 4:
                                dataODS.Failure4 = itemS.FaultCode;
                                break;
                            case 5:
                                dataODS.Failure5 = itemS.FaultCode;
                                break;
                        }
                        countFaults++;
                    }

                    var garantia = new BusinessGuaranty().GetByGuarantyID(item.Detail.ServiceType.Substring(0, 4));
                    var CausaOrden = new BusinessCauseOrder().GetAll().Where(p => p.PK_CauseOrderID == item.CauseOrderID).FirstOrDefault();
                    var estatusesquema = new BusinessStatusScheme().GetAll().Where(p => p.Description.ToUpper() == CausaOrden.CauseOrder1 && p.StatusHeadboard == "ZMX00001").FirstOrDefault();

                    if (estatusesquema != null) dataODS.FK_StatusSchemeID = estatusesquema.PK_StatusSchemeID;
                    dataODS.FK_GuarantyID = garantia.PK_GuarantyID;
                    Update(dataODS);
                }
                catch (Exception ex)
                {
                    // Write LOG
                    new BusinessMobileLogger().WriteEntry("Error MobileSync Subida - " + ex.Message);
                }



                //SpareParts

                var lstMO = new BusinessWorkforce().GetAll();

                var moduloService = new BusinessModuleService().Get(UserModuleID);

                
                // lista de garantias
                var lstGuaranty = new BusinessGuaranty().GetAll().Where(p => p.FK_GuarantyTypeID == 1 || p.FK_GuarantyTypeID == 3).Select(p => p.PK_GuarantyID).ToList();
                lstGuaranty.Remove(2); // quitar 0020	GARANTÍA DE SERVICIO
                lstGuaranty.Remove(11); // quitar 0130	GARANTÍA POR COMPONENTE

                foreach (var itemSP in item.SpareParts.OrderByDescending(p => p.SparePartsCode)) // ordenamiento se busca que 8011161600000025 kiloextra se inserte al final
                {

                    if (lstMO.Where(p => p.WorkforceID == itemSP.SparePartsCode).Count() > 0)
                    {
                        var dataSpareParts = new BusinessSparePart().GetByRefManID(dataODS.PK_OrderID, itemSP.SparePartsCode);

                        if (dataSpareParts != null)
                        {
                            dataSpareParts.Quantity = itemSP.SparePartsQuantity;
                            dataSpareParts.Price = itemSP.SparePartsPrice;
                            dataSpareParts.SparePartStatus = itemSP.SparePartsStatus;
                            new BusinessSparePart().Update(dataSpareParts);


                        }
                        else
                        {
                            if (itemSP.SparePartsCode == "8011161600000025" && lstGuaranty.Contains(dataODS.FK_GuarantyID.Value) && dataODS.OrderFahter == "X" && (dataODS.FK_StatusSchemeID == 58 || dataODS.FK_StatusSchemeID == 65))
                            {
                                var mano = new BusinessWorkforce().GetAll().Where(p => p.WorkforceID == itemSP.SparePartsCode).First();
                                new BusinessSparePart().Insert(dataODS.PK_OrderID, null, null, mano.PK_WorkforceID, itemSP.SparePartsQuantity, "0", itemSP.SparePartsStatus, "ZMX00003", itemSP.SparePartsDescription, itemSP.SparePartsCode, float.Parse(itemSP.SparePartsPrice.ToString()));
                            }
                            if (itemSP.SparePartsCode != "8011161600000025")
                            {
                                var mano = new BusinessWorkforce().GetAll().Where(p => p.WorkforceID == itemSP.SparePartsCode).First();
                                new BusinessSparePart().Insert(dataODS.PK_OrderID, null, null, mano.PK_WorkforceID, itemSP.SparePartsQuantity, "0", itemSP.SparePartsStatus, "ZMX00003", itemSP.SparePartsDescription, itemSP.SparePartsCode, float.Parse(itemSP.SparePartsPrice.ToString()));
                            }
                        }
                        //if (itemSP.AlmacenSolicitador != "" && itemSP.CentroSolicitador != "" && itemSP.CentroSuministrador != "")
                        //{

                        //    List<entities.Entity.SAP.EntitySAPADRReserveSPRequestItem> lst = new List<entities.Entity.SAP.EntitySAPADRReserveSPRequestItem>();

                        //    lst.Add(new entities.Entity.SAP.EntitySAPADRReserveSPRequestItem()
                        //    {
                        //        IDOrden = itemSP.OrderID,
                        //        Material = itemSP.SparePartsCode,
                        //        Cantidad = itemSP.SparePartsQuantity,
                        //        Sociedad = "3001",
                        //        CentroSuministrador = itemSP.CentroSuministrador,
                        //        CentroSolicitador = itemSP.CentroSolicitador,
                        //        AlmacenSolicitador = itemSP.CentroSolicitador
                        //    });

                        //    entities.Entity.SAP.EntitySAPADRReserveSPRequest datae = new entities.Entity.SAP.EntitySAPADRReserveSPRequest()
                        //    {
                        //        Idioma = "ES",
                        //        Mandante = "700",
                        //        Satelite = "MOBILE",
                        //        Items = lst
                        //    };

                        //    var OrdenVenta = new BusinessWSADRReserveSP().Send(datae);
                        //    if (OrdenVenta.Return[0].Descripcion.Contains("TR Faltante Inv creado"))
                        //    {
                        //        dataMonitor.NoteVisit = dataMonitor.NoteVisit + "/" + OrdenVenta.Return[0].Descripcion;

                        //        new RepositoryMonitorOrder().Update(dataMonitor);
                        //    }
                        //}
                    }
                    else
                    {

                        var dataSpareParts = new BusinessSparePart().GetByOrder(item.Detail.ODS).Where(p => p.FK_WorkforceID == null && p.SparePartStatus != "E0003" && p.RefManID == itemSP.SparePartsCode).FirstOrDefault();

                        if (dataSpareParts != null)
                        {
                            dataSpareParts.Quantity = itemSP.SparePartsQuantity;
                            dataSpareParts.Price = itemSP.SparePartsPrice;
                            dataSpareParts.SparePartStatus = itemSP.SparePartsStatus;
                            new BusinessSparePart().Update(dataSpareParts);
                        }
                        else
                        {
                            var bomsparepart = new BusinessBuildOfMaterial().GetByID(itemSP.BuildOfMaterialsID);
                            new BusinessSparePart().Insert(dataODS.PK_OrderID, bomsparepart.PK_BuildOfMaterialsID, bomsparepart.FK_ProductID, null, itemSP.SparePartsQuantity, "0", itemSP.SparePartsStatus, "ZMX00003", itemSP.SparePartsDescription, itemSP.SparePartsCode, float.Parse(itemSP.SparePartsPrice.ToString()));
                        }
                        //if (itemSP.AlmacenSolicitador != "" && itemSP.CentroSolicitador != "" && itemSP.CentroSuministrador != "")
                        //{

                        //    List<entities.Entity.SAP.EntitySAPADRReserveSPRequestItem> lst = new List<entities.Entity.SAP.EntitySAPADRReserveSPRequestItem>();

                        //    lst.Add(new entities.Entity.SAP.EntitySAPADRReserveSPRequestItem()
                        //    {
                        //        IDOrden = itemSP.OrderID,
                        //        Material = itemSP.SparePartsCode,
                        //        Cantidad = itemSP.SparePartsQuantity,
                        //        Sociedad = "3001",
                        //        CentroSuministrador = itemSP.CentroSuministrador,
                        //        CentroSolicitador = itemSP.CentroSolicitador,
                        //        AlmacenSolicitador = itemSP.AlmacenSolicitador
                        //    });

                        //    entities.Entity.SAP.EntitySAPADRReserveSPRequest datae = new entities.Entity.SAP.EntitySAPADRReserveSPRequest()
                        //    {
                        //        Idioma = "ES",
                        //        Mandante = "700",
                        //        Satelite = "MOBILE",
                        //        Items = lst
                        //    };

                        //    var OrdenVenta = new BusinessWSADRReserveSP().Send(datae);
                        //    if (OrdenVenta.Return[0].Descripcion.Contains("TR Faltante Inv creado"))
                        //    {
                        //        dataMonitor.NoteVisit = dataMonitor.NoteVisit + "/" + OrdenVenta.Return[0].Descripcion;

                        //        new RepositoryMonitorOrder().Update(dataMonitor);
                        //    }


                        //}
                    }

                    if (moduloService != null)
                    {
                        if (moduloService.AutorizedWorkshop.HasValue ? moduloService.AutorizedWorkshop.Value : false)
                        {
                            if (itemSP.SparePartsCode == "8011161600000025" && lstGuaranty.Contains(dataODS.FK_GuarantyID.Value) && dataODS.OrderFahter == "X" && (dataODS.FK_StatusSchemeID == 58 || dataODS.FK_StatusSchemeID == 65)) // Debe ser Padre y completado o cambio fisico
                            {
                                dataMonitor.ExtraKilometrer = itemSP.SparePartsQuantity;
                                dataMonitor.ModifyDate = DateTime.Now;
                                new RepositoryMonitorOrder().Update(dataMonitor);
                                var lstOrders = new BusinessMonitor().GetByUserID(UserID, dataODS.OrderExecuteDate);

                                foreach (var itemOR in lstOrders)
                                {
                                    var odsRuta = GetByID(itemOR.OrderID);
                                    if (odsRuta.SendCRM == "PendienteKM") odsRuta.SendCRM = "Pendiente";
                                    odsRuta.ExtraKilometres = false;
                                    odsRuta.ModifyDate = DateTime.UtcNow;
                                    Update(odsRuta);
                                }


                                var extraOds = lstOrders.Select(p => p.ExtraKilometrer).Max();
                                var itemExtraOds = lstOrders.Where(p => p.ExtraKilometrer == extraOds).FirstOrDefault();

                                if (itemExtraOds != null)
                                {
                                    var odsRuta = GetByID(itemExtraOds.OrderID);
                                    odsRuta.ExtraKilometres = true;
                                    odsRuta.ModifyDate = DateTime.UtcNow;
                                    Update(odsRuta);
                                }

                            }
                        }
                    }

                }                

                ODS.Add(item.Detail.ODS);
            }
            string Ordens = string.Join(",", ODS[0]);

            return Ordens;
        }

        public List<EntityOrder> GetByRange(DateTime Inicio, DateTime Fin)
        {
            return new RepositoryOrder().GetByRange(Inicio, Fin).Select(p => new EntityOrder()
            {
                PK_OrderID = p.PK_OrderID,
                FK_InstalledBaseID = p.FK_InstalledBaseID,
                FK_ClientID = p.FK_ClientID,
                FK_EmployeeID = p.FK_EmployeeID,
                FK_ModuleID = p.FK_ModuleID,
                FK_GuarantyID = p.FK_GuarantyID,
                FK_StatusSchemeID = p.FK_StatusSchemeID,
                OrderID = p.OrderID,
                TechnicalID = p.TechnicalID,
                Failure1 = p.Failure1,
                Failure2 = p.Failure2,
                Failure3 = p.Failure3,
                Failure4 = p.Failure4,
                Failure5 = p.Failure5,
                Symptom = p.Symptom,
                PreOrder = p.PreOrder,
                URLPreOrder = p.URLPreOrder,
                Note = p.Note,
                OrderFahter = p.OrderFahter,
                SendCRM = p.SendCRM,
                Status = p.Status,
                OrderCreateDate = p.OrderCreateDate,
                OrderExecuteDate = p.OrderExecuteDate,
                CreateDate = p.CreateDate,
                ModifyDate = p.ModifyDate
            }).ToList<EntityOrder>();
        }

        public void SetOrderSecuence(ModelViewUserSecuence data)
        {
            var NegocioUsuario = new BusinessUsers();
            var NegocioEmpleado = new BusinessEmployee();
            var NegocioVisita = new BusinessVisit();
            var dataUsuario = NegocioUsuario.GetUserByToken(data.TokenUser);
            var empleado = NegocioEmpleado.GetByUserID(dataUsuario.UserID);
            if (data.TokenApp != GlobalConfiguration.TokenMobile)
                throw new Exception("TokenInvalid");
            foreach (var item in data.Secuence)
            {
                var Ods = GetByOrderID(item.OrderID);
                var visit = Ods != null ? NegocioVisita.GetByOrderID(Ods.PK_OrderID) : null;
                if (visit != null)
                {
                    visit.SequenceVisit = item.Secuence;
                    NegocioVisita.Update(visit);
                }
            }
        }


        public void Complete(ModelViewPreODS data)
        {
            var NegocioCF = new BusinessCodeFailure();
            var dataODS = GetByOrderID(data.OrderID);
            var dataMonitor = new BusinessMonitor().GetAll().Where(p => p.OrderID == dataODS.PK_OrderID).First();

            dataMonitor.CauseOrderID = data.CauseOrderID;
            dataMonitor.StatusOrderID = data.StatusOrderID;
            dataMonitor.StatusVisitID = data.StatusVisitID;
            //dataMonitor.SequenceVisit = data.SequenceVisit;

            dataMonitor.StartVisitDate = data.StartVisitDate;
            dataMonitor.EndVisitDate = data.EndVisitDate;
            dataMonitor.StartOrderDate = data.StartOrderDate;
            dataMonitor.EndOrderDate = data.EndOrderDate;
            dataMonitor.StartServiceDate = data.StartServiceDate;
            dataMonitor.EndServiceDate = data.EndServiceDate;
            dataMonitor.LatitudeStartVisit = data.LatitudeStartVisit;
            dataMonitor.LogitudeStartVisit = data.LogitudeStartVisit;
            dataMonitor.LatitudeEndVisit = data.LatitudeEndVisit;
            dataMonitor.LogitudeEndVisit = data.LogitudeEndVisit;
            dataMonitor.LatitudeStartOrder = data.LatitudeStartOrder;
            dataMonitor.LogitudeStartOrder = data.LogitudeStartOrder;
            dataMonitor.LatitudeEndOrder = data.LatitudeEndOrder;
            dataMonitor.LogitudeEndOrder = data.LogitudeEndOrder;

            dataMonitor.DurationVisit = data.DurationVisit;
            dataMonitor.DurationOrder = data.DurationOrder;
            dataMonitor.DurationExecute = data.DurationExecute;
            //dataMonitor.DurationTransport = data.DurationTransport;

            //dataMonitor.NoteVisit = data.NoteVisit;
            //dataMonitor.NoteOrder = data.NoteOrder;

            dataMonitor.Status = true;
            dataMonitor.CreateDate = DateTime.UtcNow;
            dataMonitor.ModifyDate = DateTime.UtcNow;

            new RepositoryMonitorOrder().Update(dataMonitor);

            //Detail                

            var dataInstallBase = new BusinessInstalledBase().GetByID(dataODS.FK_InstalledBaseID);

            DateTime ShopDate;
            dataInstallBase.ShopDate = null;
            if (DateTime.TryParse(data.ShopDate, out ShopDate)) dataInstallBase.ShopDate = ShopDate;
            dataInstallBase.SerialNumber = data.SerialNumber;

            dataInstallBase.FK_ShopPlaceID = new BusinessShopPlace().GetByShopPlaceID(data.FK_ShopPlaceID).PK_ShopPlaceID;

            dataInstallBase.FK_ProductID = new BusinessProduct().GetByID(data.FK_ProductID).PK_ProductID;

            new BusinessInstalledBase().Update(dataInstallBase);

            //SpareParts
            foreach (var itemSP in data.SparePartDetails)
            {
                var dataSpareParts = new BusinessSparePart().GetAll().Where(p => p.FK_BuildOfMaterialsID == itemSP.BuildOfMaterialID && p.FK_ProductID == data.FK_ProductID).FirstOrDefault();
                if (dataSpareParts != null)
                {
                    dataSpareParts.Status = true;
                    dataSpareParts.Quantity = itemSP.Quantity;
                    dataSpareParts.Price = float.Parse(itemSP.Price);

                }
                else
                {
                    new BusinessSparePart().Insert(dataODS.PK_OrderID, itemSP.BuildOfMaterialID, dataInstallBase.FK_ProductID, null, itemSP.Quantity, "", "", "", "", "", float.Parse(itemSP.Price));
                }
            }

            int countFaults = 1;
            foreach (var itemS in data.Failures)
            {
                var falla = NegocioCF.GetByID(itemS.FailureID);
                switch (countFaults)
                {
                    case 1:
                        dataODS.Failure1 = falla.Failure;
                        break;
                    case 2:
                        dataODS.Failure2 = falla.Failure;
                        break;
                    case 3:
                        dataODS.Failure3 = falla.Failure;
                        break;
                    case 4:
                        dataODS.Failure4 = falla.Failure;
                        break;
                    case 5:
                        dataODS.Failure5 = falla.Failure;
                        break;
                }
                countFaults++;
            }

            //var garantia = new BusinessGuaranty().GetByGuarantyID(item.Detail.ServiceType);
            dataODS.Note = data.Note;
            dataODS.FK_GuarantyID = data.ServiceType == "Fuera de Garantía" ? 7 : 1;
            Update(dataODS);
        }
        
        public List<ModelViewListODS> GetListODS(ModelViewUserVisits objCred)
        {

            var NegocioCliente = new BusinessClient();
            var NegocioBase = new BusinessInstalledBase();
            var NegocioProducto = new BusinessProduct();
            var NegocioUsuario = new BusinessUsers();
            var NegocioEmpleado = new BusinessEmployee();

            var dataUsuario = NegocioUsuario.GetUserByToken(objCred.TokenUser);
            var empleado = NegocioEmpleado.GetByUserID(dataUsuario.UserID);
            if (objCred.TokenApp != GlobalConfiguration.TokenWEB)
                if (objCred.TokenApp != GlobalConfiguration.TokenMobile)
                    throw new Exception("TokenInvalid");

            if (dataUsuario == null) throw new Exception("UserPasswordInvalid");

            var ordenes = GetAllListODS(empleado.Select(q => q.PK_EmployeeID).ToList(), objCred.Date.Date, objCred.Date.Date.AddDays(7), false);
            var ODS = new List<ModelViewListODS>();
            foreach (var item in ordenes)
            {
                var cliente = NegocioCliente.GetByID(item.FK_ClientID);
                var baseInt = NegocioBase.GetByID(item.FK_InstalledBaseID);
                var producto = baseInt.FK_ProductID != null ? NegocioProducto.GetByID(baseInt.FK_ProductID.Value) : null;
                ODS.Add(new ModelViewListODS
                {

                    ODS = item.OrderID,
                    ProductModel = producto != null ? producto.Model : baseInt.Model,
                    BoroghtName = cliente.BoroughAddress,
                    PostalCode = cliente.PostalCodeAddress,
                    Municipality = cliente.MunicipalityAddress,
                    Date = item.OrderExecuteDate.ToString("yyyy-MM-dd")

                });
            }

            return ODS;

        }
        
        public List<ModelViewODSMovil> GetListOrden(ModelViewUserVisits objCred)
        {

            var NegocioCliente = new BusinessClient();
            var NegocioBase = new BusinessInstalledBase();
            var NegocioProducto = new BusinessProduct();
            var NegocioHistorico = new BusinessHistory();
            var NegocioCFP = new BusinessCodeFailureByProduct();
            var NegocioCF = new BusinessCodeFailure();
            var NegocioRefaccion = new BusinessSparePart();
            var NegocioBOM = new BusinessBuildOfMaterial();
            var NegocioVisita = new BusinessVisit();
            var NegocioUsuario = new BusinessUsers();
            var NegocioLugarCompra = new BusinessShopPlace();
            var NegocioEmpleado = new BusinessEmployee();
            var NegocioGarantia = new BusinessGuaranty();
            var NegocioEstatus = new BusinessStatusScheme();
            
            var dataUsuario = NegocioUsuario.GetUserByToken(objCred.TokenUser);
            var empleado = NegocioEmpleado.GetByUserID(dataUsuario.UserID);
            if (objCred.TokenApp != GlobalConfiguration.TokenWEB)
                if (objCred.TokenApp != GlobalConfiguration.TokenMobile)
                    throw new Exception("TokenInvalid");

            if (dataUsuario == null) throw new Exception("UserPasswordInvalid");
            List<EntityOrder> ordenes = new List<EntityOrder>();
            if (objCred.ODS == null)
            { ordenes = GetAllFutures(empleado.Select(q => q.PK_EmployeeID).ToList(),  objCred.Date.Date, false); }
            else
            {
                var order = GetByOrderID(objCred.ODS, objCred.Date);
                ordenes.Add(order);
            }
            var ODS = new List<ModelViewODSMovil>();
            int arrivo = 13;
            foreach (var item in ordenes)
            {
                var DC = new DetailClient();
                var HP = new List<HistoricProduct>();
                var HP2 = new List<HistoricProduct>();
                var HC = new List<HistoricClient>();
                var HC2 = new List<HistoricClient>();
                var DO = new DetailODS();
                var FaultsODS = new List<FaultsODS>();
                var Symptoms = new List<SymptomsODS>();
                var SparePart = new List<SparePart>();
                int StatusVisitID = 1;
                var lst = NegocioVisita.GetByOrderID(item.PK_OrderID, StatusVisitID);
                
                

                if (lst != null)
                {
                    var cliente = NegocioCliente.GetByID(item.FK_ClientID);
                    var baseInt = NegocioBase.GetByID(item.FK_InstalledBaseID);
                    var producto = baseInt.FK_ProductID != null ? NegocioProducto.GetByID(baseInt.FK_ProductID.Value) : null;
                    var lugarcompra = baseInt.FK_ShopPlaceID != null ? NegocioLugarCompra.GetByShopPlaceID(baseInt.FK_ShopPlaceID.Value) : null;
                    var garantia = new BusinessGuaranty().Get(item.FK_GuarantyID.Value);
                    var Empleado = new BusinessEmployee().GetID(item.FK_EmployeeID.Value);
                    try
                    {
                        DC.ClientID = cliente.ClientID;
                        DC.Address = cliente.StreetAddress + " " + cliente.NumberExternalAddress + " " + cliente.BoroughAddress + " " + cliente.MunicipalityAddress;
                        DC.NumberInternalAddress = cliente.NumberInternalAddress;
                        DC.Address2 = (cliente.AdditionalInfoAddress1 == " " ? "" : cliente.AdditionalInfoAddress1) +
                                      (cliente.AdditionalInfoAddress2 == " " ? "" : "-" + cliente.AdditionalInfoAddress2) +
                                      (cliente.AdditionalInfoAddress3 == " " ? "" : "-" + cliente.AdditionalInfoAddress3) +
                                      (cliente.AdditionalInfoAddress4 == " " ? "" : "-" + cliente.AdditionalInfoAddress4) +
                                      (cliente.AdditionalInfoAddress5 == " " ? "" : "-" + cliente.AdditionalInfoAddress5);
                        DC.Address2 = DC.Address2 == "" ? "" : DC.Address2.Substring(0, 1) == "-" ? DC.Address2.Substring(1) : DC.Address2;
                        DC.LatitudeAddress = lst.LatitudeAddress.ToString();
                        DC.LogitudeAddress = lst.LogitudeAddress.ToString();
                        DC.ODS = item.OrderID;
                        DC.ProductModel = producto != null ? producto.Model : baseInt.Model;
                        DC.ClientName = cliente.FirstName + " " + cliente.LastName;
                        DC.Phone1 = cliente.PhoneNumber1;
                        DC.Phone2 = cliente.PhoneNumber2;
                        DC.Phone3 = cliente.PhoneNumber3;
                        DC.Extension = cliente.PhoneExtension1;
                        DC.Email = cliente.Email;
                        DC.RFC = string.IsNullOrEmpty(cliente.RCF) ? " " : cliente.RCF;


                        DO.ODS = item.OrderID;
                        DO.ProductModel = producto != null ? producto.Model : baseInt.Model;
                        DO.ProductDescription = producto != null ? producto.ProductName : baseInt.ProductName;
                        DO.SerialNumber = baseInt.SerialNumber;
                        DO.Fault = item.Symptom;
                        DO.ServiceType = garantia.GuarantyID + " " + garantia.Guaranty1;
                        DO.Notes = item.Note;
                        DO.PurchaseDate = baseInt.ShopDate.HasValue ? baseInt.ShopDate.Value.ToString("yyyy-MM-dd") : "";
                        DO.PurchasePlace = lugarcompra != null ? lugarcompra.ShopPlaceID + " - " + lugarcompra.ShopPlace1 : "";
                        DO.OrderFather = string.IsNullOrEmpty(item.OrderFahter) ? "" : item.OrderFahter;
                        DO.EmployeeID = Empleado.EmployeeID;
                        
                    }

                    catch (Exception e)
                    {
                        throw new Exception("bloque 1");
                    }
                    try
                    {
                         List<EntityHistory> s = NegocioHistorico.GetByOrderID(item.PK_OrderID);
                        var arrHP = s.Select(p => p.Guaranty).Distinct().ToList();
                        var arrST = s.Select(q => q.OrderStatus).Distinct().ToList();
                        List<EntityGuaranty> o = NegocioGarantia.GuarantyList(arrHP);
                        List<EntityStatusScheme> r = NegocioEstatus.GetStatusSchemeID(arrST, "ZMX00001");


                        HP = (from q in s
                              join d in o on q.Guaranty equals d.GuarantyID
                              join c in r on q.OrderStatus equals c.StatusScheme1
                              select new HistoricProduct
                              {
                                  ODSID = item.OrderID,
                                  ODS = q.OrderID,
                                 Status=c.Description,
                                  CloseDate = q.CloseDate.ToString("yyyy-MM-dd"),
                                  Faults = q.Failure1 + ' ' + q.Failure2 + ' ' + q.Failure3
                              }).Take(10).ToList();


                        //HP2 =
                        //(from a in NegocioHistorico.GetByOrderID(item.PK_OrderID)
                        // join b in NegocioGarantia.GetAll() on a.Guaranty equals b.GuarantyID
                        // join c in NegocioEstatus.GetAll() on a.OrderStatus equals c.StatusScheme1
                        // where c.StatusHeadboard == "ZMX00001"
                        // select new HistoricProduct
                        // {
                        //     ODSID = item.OrderID,
                        //     ODS = a.OrderID,
                        //     Status = c.Description,
                        //     CloseDate = a.CloseDate.ToString("yyyy-MM-dd"),
                        //     Faults = a.Failure1 + ' ' + a.Failure2 + ' ' + a.Failure3
                        // })
                        //  .Take(10).ToList();

                        List<EntityInstalledBase> x = NegocioBase.GetByClient(item.FK_ClientID);
                        var ArrProd = x.Select(p => p.Model).Distinct().ToList();
                        List<EntityProduct> v = NegocioProducto.GetByIDs(ArrProd);

                        HC = (from a in x
                              join b in v on a.FK_ProductID equals b.PK_ProductID
                              select new HistoricClient
                              {
                                  ODSID = item.OrderID,
                                  Product = b.ProductName,
                                  ShopDate = a.ShopDate.HasValue ? a.ShopDate.Value.ToString("yyyy-MM-dd") : "--"
                              }).Take(10).ToList();

                        //HC2 = (from a in NegocioBase.GetByClient(item.FK_ClientID)
                        //      join b in NegocioProducto.GetAll() on a.FK_ProductID equals b.PK_ProductID
                        //      select new HistoricClient
                        //      {
                        //          ODSID = item.OrderID,
                        //          Product = b.ProductName,
                        //          ShopDate = a.ShopDate.HasValue ? a.ShopDate.Value.ToString("yyyy-MM-dd") : "--"
                        //      }).Take(10).ToList();

                        Symptoms.Add(new SymptomsODS
                        {
                            SymptomDescription = item.Symptom,
                            FaultsODS = FaultsODS
                        });
                    }

                    catch (Exception e)
                    {
                        throw new Exception("bloque 2");
                    }
                    try
                    {
                        var refaccion = NegocioRefaccion.GetAllByOrderID(item.PK_OrderID);
                        if (refaccion != null)
                        {
                            string statusRef = null;
                            foreach (var x in refaccion)
                            {
                                if(x.SparePartStatus == "E0003") continue;  // No se deben descargar Refacciones no ultilizadas

                                switch (x.SparePartStatus)
                                {
                                    case "E0001":
                                        statusRef = "Util";
                                        break;
                                    case "E0003":
                                        statusRef = "NU";                                        
                                        break;
                                    case "E0004":
                                        statusRef = "FR";
                                        break;
                                    case "E0006":
                                        statusRef = "Prog";
                                        break;
                                }

                                if (x.FK_BuildOfMaterialsID != null)
                                {
                                    var refc = NegocioBOM.GetByID(x.FK_BuildOfMaterialsID.Value);
                                    SparePart.Add(new SparePart
                                    {
                                        PK_SparePartsID = x.PK_SparePartsID,
                                        BuildOfMaterialsID = refc.PK_BuildOfMaterialsID,
                                        WorkForceID = 0,
                                        SparePartsDescription = refc.SparePartDescription,
                                        SparePartsCode = refc.SparePartsID,
                                        SparePartsPrice = x.Price.Value,
                                        SparePartsStatus = x.SparePartStatus,
                                        sparePartNotes = statusRef,
                                        SparePartsQuantity = x.Quantity
                                    });
                                }
                                else if (x.FK_WorkforceID != null)
                                {
                                    var refc = NegocioRefaccion.Get(x.PK_SparePartsID);
                                    SparePart.Add(new SparePart
                                    {
                                        PK_SparePartsID = refc.PK_SparePartsID,
                                        BuildOfMaterialsID = 0,
                                        WorkForceID = refc.FK_WorkforceID.Value,
                                        SparePartsDescription = refc.ItemRefMan,
                                        SparePartsCode = refc.RefManID,
                                        SparePartsPrice = refc.Price.Value,
                                        SparePartsStatus = refc.SparePartStatus,
                                        sparePartNotes = statusRef,
                                        SparePartsQuantity = refc.Quantity
                                    });
                                }
                                else
                                {
                                    SparePart.Add(new SparePart
                                    {
                                        PK_SparePartsID = x.PK_SparePartsID,
                                        BuildOfMaterialsID = 0,
                                        WorkForceID = 0,
                                        SparePartsDescription = x.ItemRefMan,
                                        SparePartsCode = x.RefManID,
                                        SparePartsPrice = x.Price.Value,
                                        SparePartsStatus = x.SparePartStatus,
                                        sparePartNotes = statusRef,
                                        SparePartsQuantity = x.Quantity
                                    });
                                }
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        throw new Exception("bloque 3");
                    }
                    EntityGoogleMapsDistanceMatrixResponse data = null;
                    int secuence = 0;
                    string arrival = "";


                    var dataRequest = new ModelViewGoogleMapsDistanceMatrixRequest()
                    {
                        origins = objCred.Longitude.ToString() + ", " + objCred.Latitude.ToString(),
                        destinations = lst.LatitudeAddress.ToString() + ", " + lst.LogitudeAddress.ToString(),
                        TokenUser = objCred.TokenUser,
                        TokenApp = objCred.TokenApp
                    };

                    try
                    {
                        data = FacadeGoogle.GetDistanceMatrix(dataRequest);
                        secuence = Convert.ToInt32(data.rows[0].elements[0].duration_in_traffic.value);
                        arrival = data.rows[0].elements[0].duration_in_traffic.text;
                        var separada = data.destination_addresses[0].Split(',');

                        DC.Town = separada[3] + " " + separada[4];
                       

                        

                    }
                    catch(Exception ex)
                    {
                        arrival = dataRequest.origins + "|" + dataRequest.destinations + "|" + data.status + "|" + data.origin_addresses.Count.ToString() + "|" + data.destination_addresses.Count.ToString() + "|" + data.rows.Count.ToString();
                        DC.Town = "Ciudad de Mexico";
                        
                    }
                    var futureOrder = new ModelViewODSMovil().FutureService;

                    
                    var FechaOrden = item.OrderExecuteDate.ToString("yyyy/MM/dd");
                    //var FechaAct = "2018/09/26";
                    var FechaAct = DateTime.Today.ToString("yyyy/MM/dd");
                    if (FechaOrden.Contains(FechaAct))
                    {
                        futureOrder = false;
                    }
                    else
                    {
                        futureOrder = true;
                    }

                    ODS.Add(new ModelViewODSMovil
                    {
                        VisitID = lst.PK_VisitID,
                        Secuence = secuence,
                        BoroghtName = cliente.BoroughAddress,
                        PostalCode = cliente.PostalCodeAddress,
                        Municipality = cliente.MunicipalityAddress,
                        ArrivalTime = arrival,
                        ODS = item.OrderID,
                        ProductModel = producto != null ? producto.Model : baseInt.Model,
                        HistoricClient = HC,
                        HistoricProduct = HP,
                        DetailClient = DC,
                        DetailODS = DO,
                        SymptomsODS = Symptoms,
                        SpareParts = SparePart,
                        OrderExecuteDate=item.OrderExecuteDate.Date.ToString("yyy/MM/dd"),
                        FutureService= futureOrder



                        });




                }
                arrivo++;
            }

            int i = 1;

            foreach (var item in ODS.OrderBy(p => p.Secuence))
            {
                TimeSpan ts = TimeSpan.FromSeconds(item.Secuence);
                item.Secuence = i;
                var visita = NegocioVisita.GetByID(item.VisitID);
                visita.DurationTransport = ts;
                visita.SequenceVisit = i;
                try
                { NegocioVisita.Update(visita); }
                catch
                {
                    throw new Exception("Actualizacón Secuencia");
                }
                i++;

            }


            try
            {
                EntityMobileSync Mobile = new EntityMobileSync();
                Mobile.PK_MobileSyncID = 0;
                Mobile.FK_UserID = dataUsuario.UserID;
                Mobile.UserName = dataUsuario.UserName;
                Mobile.Name = dataUsuario.Name;
                Mobile.ODS = ODS.Count().ToString();
                Mobile.Type = "Bajada";
                Mobile.SyncDate = DateTime.UtcNow.ToLocalTime();         
                Mobile.Status = true;
                Mobile.CreateDate = DateTime.UtcNow;
                Mobile.ModifyDate = DateTime.UtcNow;
                new BusinessMobileSync().Insert(Mobile);
            }

            catch
            {
                new BusinessMobileLogger().WriteEntry("Error MobileSync Bajada");
            }
             return ODS.OrderBy(p => p.Secuence).ToList();
            //return ordByArrivalTime;
        }
       
        public EntitySAPReprogramacionODSResult GetReprograminODS(ModelViewUserVisits objCred)
        {         
            return new BusinessMabe().ReprogramingODS(objCred.ODS);
        }




        public EntityOrder Insert(entities.Entity.Interface.ODS model, int PK_ClientID, int FK_InstalledBaseID, int FK_GuarantyID)
        {
            var objRepository = new RepositoryOrder();
            DateTime creacion = DateTime.Parse(model.FechaCreacionODS);
            DateTime ejecucion = DateTime.Parse(model.FechaProgramacion);
            var NegocioEmpleado = new BusinessEmployee();

            var empleado = NegocioEmpleado.GetAll().Where(p => p.EmployeeID == model.IDTecnico).FirstOrDefault();

            EntityOrder data = new EntityOrder()
            {
                PK_OrderID = 0,
                FK_InstalledBaseID = FK_InstalledBaseID,
                FK_ClientID = PK_ClientID,
                FK_EmployeeID = empleado == null ? 248 : empleado.PK_EmployeeID,
                FK_ModuleID = empleado == null ? 1 : empleado.FK_ModuleID,
                FK_GuarantyID = FK_GuarantyID,
                FK_StatusSchemeID = 44,
                OrderID = model.IDOrden,
                TechnicalID = model.IDTecnico,
                Symptom = model.SintomaFalla,
                Failure1 = null,
                Failure2 = null,
                Failure3 = null,
                Failure4 = null,
                Failure5 = null,
                Note = model.Notas,
                PreOrder = false,
                URLPreOrder = null,
                OrderFahter = string.IsNullOrEmpty(model.ODS_PADRE) ? "" : model.ODS_PADRE,
                SendCRM = "Pendiente",
                OrderCreateDate = creacion,
                OrderExecuteDate = ejecucion,
                Status = true,
                CreateDate = DateTime.UtcNow,
                ModifyDate = DateTime.UtcNow
            };
            data = objRepository.Insert(data);
            return data;
        }

        public EntityOrder InsertPre(EntityOrder NewOrder)
        {
            var objRepository = new RepositoryOrder();
            EntityOrder data = new EntityOrder()
            {
                PK_OrderID = 0,
                FK_InstalledBaseID = NewOrder.FK_InstalledBaseID,
                FK_ClientID = NewOrder.FK_ClientID,
                FK_EmployeeID = NewOrder.FK_EmployeeID,
                FK_ModuleID = NewOrder.FK_ModuleID,
                FK_GuarantyID = NewOrder.FK_GuarantyID,
                FK_StatusSchemeID = 44,
                OrderID = NewOrder.OrderID,
                TechnicalID = NewOrder.TechnicalID,
                Symptom = null,
                Failure1 = NewOrder.Failure1,
                Failure2 = NewOrder.Failure2,
                Failure3 = NewOrder.Failure3,
                Failure4 = NewOrder.Failure4,
                Failure5 = NewOrder.Failure5,
                Note = null,
                PreOrder = true,
                OrderFahter = "",
                SendCRM = "PreOrden",
                URLPreOrder = NewOrder.URLPreOrder,
                OrderCreateDate = DateTime.UtcNow.ToLocalTime(),
                OrderExecuteDate = DateTime.UtcNow.ToLocalTime().Date,
                Status = true,
                CreateDate = DateTime.UtcNow.ToLocalTime(),
                ModifyDate = DateTime.UtcNow.ToLocalTime()
            };
            data = objRepository.Insert(data);
            return data;
        }

        public EntityOrder Update(entities.Entity.Interface.ODS model, EntityOrder ord)
        {
            var objRepository = new RepositoryOrder();
            DateTime creacion = DateTime.Parse(model.FechaCreacionODS);
            DateTime ejecucion = DateTime.Parse(model.FechaProgramacion);

            EntityOrder data = new EntityOrder()
            {
                PK_OrderID = ord.PK_OrderID,
                FK_InstalledBaseID = ord.FK_InstalledBaseID,
                FK_ClientID = ord.FK_ClientID,
                FK_EmployeeID = ord.FK_EmployeeID,
                FK_ModuleID = ord.FK_ModuleID,
                FK_GuarantyID = ord.FK_GuarantyID,
                FK_StatusSchemeID = ord.FK_StatusSchemeID,
                OrderID = model.IDOrden,
                TechnicalID = model.IDTecnico,
                Symptom = model.SintomaFalla,
                Failure1 = ord.Failure1,
                Failure2 = ord.Failure2,
                Failure3 = ord.Failure3,
                Failure4 = ord.Failure4,
                Failure5 = ord.Failure5,
                Note = model.Notas,
                PreOrder = false,
                SendCRM = ord.SendCRM,
                OrderFahter = string.IsNullOrEmpty(model.ODS_PADRE) ? "" : model.ODS_PADRE,
                OrderCreateDate = creacion,
                OrderExecuteDate = ejecucion,
                Status = true,
                CreateDate = ord.CreateDate,
                ModifyDate = DateTime.UtcNow
            };
            data = objRepository.Update(data);
            return data;
        }
       public EntityOrder Update(EntityOrder data)
        {
            return new RepositoryOrder().Update(data);
        }

        public List<EntityOrder> GetAll(string SendCRM, DateTime syncDate)
        {
            return new RepositoryOrder().GetAll(SendCRM, syncDate);
        }

        public List<EntityOrder> GetListByUser(int UserID)
        {
            return new RepositoryOrder().GetListByUser(UserID);
        }

        public List<int> GetListByProductID(int UserID, DateTime fh)
        {
            return new RepositoryOrder().GetListByProductID(UserID, fh);
        }


        public List<EntityOrder> GetAll()
        {
            return new RepositoryOrder().GetAll().Select(p => new EntityOrder()
            {
                PK_OrderID = p.PK_OrderID,
                FK_InstalledBaseID = p.FK_InstalledBaseID,
                FK_ClientID = p.FK_ClientID,
                FK_EmployeeID = p.FK_EmployeeID,
                FK_ModuleID = p.FK_ModuleID,
                FK_GuarantyID = p.FK_GuarantyID,
                FK_StatusSchemeID = p.FK_StatusSchemeID,
                OrderID = p.OrderID,
                TechnicalID = p.TechnicalID,
                Failure1 = p.Failure1,
                Failure2 = p.Failure2,
                Failure3 = p.Failure3,
                Failure4 = p.Failure4,
                Failure5 = p.Failure5,
                Symptom = p.Symptom,
                PreOrder = p.PreOrder,
                URLPreOrder = p.URLPreOrder,
                Note = p.Note,
                OrderFahter = p.OrderFahter,
                SendCRM = p.SendCRM,
                Status = p.Status,
                OrderCreateDate = p.OrderCreateDate,
                OrderExecuteDate = p.OrderExecuteDate,
                CreateDate = p.CreateDate,
                ModifyDate = p.ModifyDate
            }).ToList<EntityOrder>();
        }

        public List<EntityOrder> GetByEmployee(int employee, DateTime fecha )
        {
            return new RepositoryOrder().GetByEmployee(employee,fecha).Select(p => new EntityOrder()
            {
                PK_OrderID = p.PK_OrderID,
                FK_InstalledBaseID = p.FK_InstalledBaseID,
                FK_ClientID = p.FK_ClientID,
                FK_EmployeeID = p.FK_EmployeeID,
                FK_ModuleID = p.FK_ModuleID,
                FK_GuarantyID = p.FK_GuarantyID,
                FK_StatusSchemeID = p.FK_StatusSchemeID,
                OrderID = p.OrderID,
                TechnicalID = p.TechnicalID,
                Failure1 = p.Failure1,
                Failure2 = p.Failure2,
                Failure3 = p.Failure3,
                Failure4 = p.Failure4,
                Failure5 = p.Failure5,
                Symptom = p.Symptom,
                PreOrder = p.PreOrder,
                URLPreOrder = p.URLPreOrder,
                Note = p.Note,
                OrderFahter = p.OrderFahter,
                SendCRM = p.SendCRM,
                Status = p.Status,
                OrderCreateDate = p.OrderCreateDate,
                OrderExecuteDate = p.OrderExecuteDate,
                CreateDate = p.CreateDate,
                ModifyDate = p.ModifyDate
            }).ToList<EntityOrder>();
        }

        //public List<EntityOrder> GetByRange(DateTime Inicio, DateTime Fin)
        //{
        //    return new RepositoryOrder().GetByRange(Inicio, Fin).Select(p => new EntityOrder()
        //    {
        //        PK_OrderID = p.PK_OrderID,
        //        FK_InstalledBaseID = p.FK_InstalledBaseID,
        //        FK_ClientID = p.FK_ClientID,
        //        FK_EmployeeID = p.FK_EmployeeID,
        //        FK_ModuleID = p.FK_ModuleID,
        //        FK_GuarantyID = p.FK_GuarantyID,
        //        FK_StatusSchemeID = p.FK_StatusSchemeID,
        //        OrderID = p.OrderID,
        //        TechnicalID = p.TechnicalID,
        //        Failure1 = p.Failure1,
        //        Failure2 = p.Failure2,
        //        Failure3 = p.Failure3,
        //        Failure4 = p.Failure4,
        //        Failure5 = p.Failure5,
        //        Symptom = p.Symptom,
        //        PreOrder = p.PreOrder,
        //        URLPreOrder = p.URLPreOrder,
        //        Note = p.Note,
        //        OrderFahter = p.OrderFahter,
        //        SendCRM = p.SendCRM,
        //        Status = p.Status,
        //        OrderCreateDate = p.OrderCreateDate,
        //        OrderExecuteDate = p.OrderExecuteDate,
        //        CreateDate = p.CreateDate,
        //        ModifyDate = p.ModifyDate
        //    }).ToList<EntityOrder>();
        //}

        public List<EntityOrder> GetAll(List<int> EmployeeID, DateTime fhExecute, bool PreODS)
        {
            return new RepositoryOrder().GetAll(EmployeeID, fhExecute, PreODS).Select(p => new EntityOrder()
            {
                PK_OrderID = p.PK_OrderID,
                FK_InstalledBaseID = p.FK_InstalledBaseID,
                FK_ClientID = p.FK_ClientID,
                FK_EmployeeID = p.FK_EmployeeID,
                FK_ModuleID = p.FK_ModuleID,
                FK_GuarantyID = p.FK_GuarantyID,
                FK_StatusSchemeID = p.FK_StatusSchemeID,
                OrderID = p.OrderID,
                TechnicalID = p.TechnicalID,
                Failure1 = p.Failure1,
                Failure2 = p.Failure2,
                Failure3 = p.Failure3,
                Failure4 = p.Failure4,
                Failure5 = p.Failure5,
                Symptom = p.Symptom,
                PreOrder = p.PreOrder,
                URLPreOrder = p.URLPreOrder,
                Note = p.Note,
                OrderFahter = p.OrderFahter,
                SendCRM = p.SendCRM,
                Status = p.Status,
                OrderCreateDate = p.OrderCreateDate,
                OrderExecuteDate = p.OrderExecuteDate,
                CreateDate = p.CreateDate,
                ModifyDate = p.ModifyDate
            }).ToList<EntityOrder>();
        }
        public List<EntityOrder> GetAllFutures(List<int> EmployeeID, DateTime fhExecute, bool PreODS)
        {
            return new RepositoryOrder().GetAllFutures(EmployeeID, fhExecute, PreODS).Select(p => new EntityOrder()
            {
                PK_OrderID = p.PK_OrderID,
                FK_InstalledBaseID = p.FK_InstalledBaseID,
                FK_ClientID = p.FK_ClientID,
                FK_EmployeeID = p.FK_EmployeeID,
                FK_ModuleID = p.FK_ModuleID,
                FK_GuarantyID = p.FK_GuarantyID,
                FK_StatusSchemeID = p.FK_StatusSchemeID,
                OrderID = p.OrderID,
                TechnicalID = p.TechnicalID,
                Failure1 = p.Failure1,
                Failure2 = p.Failure2,
                Failure3 = p.Failure3,
                Failure4 = p.Failure4,
                Failure5 = p.Failure5,
                Symptom = p.Symptom,
                PreOrder = p.PreOrder,
                URLPreOrder = p.URLPreOrder,
                Note = p.Note,
                OrderFahter = p.OrderFahter,
                SendCRM = p.SendCRM,
                Status = p.Status,
                OrderCreateDate = p.OrderCreateDate,
                OrderExecuteDate = p.OrderExecuteDate,
                CreateDate = p.CreateDate,
                ModifyDate = p.ModifyDate
            }).ToList<EntityOrder>();
        }

        public List<EntityOrder> GetAllListODS(List<int> EmployeeID, DateTime fhExecute, DateTime MaxfhExecute, bool PreODS)
        {
            return new RepositoryOrder().GetAllListODS(EmployeeID, fhExecute, MaxfhExecute, PreODS).Select(p => new EntityOrder()
            {
                PK_OrderID = p.PK_OrderID,
                FK_InstalledBaseID = p.FK_InstalledBaseID,
                FK_ClientID = p.FK_ClientID,
                FK_EmployeeID = p.FK_EmployeeID,
                FK_ModuleID = p.FK_ModuleID,
                FK_GuarantyID = p.FK_GuarantyID,
                FK_StatusSchemeID = p.FK_StatusSchemeID,
                OrderID = p.OrderID,
                TechnicalID = p.TechnicalID,
                Failure1 = p.Failure1,
                Failure2 = p.Failure2,
                Failure3 = p.Failure3,
                Failure4 = p.Failure4,
                Failure5 = p.Failure5,
                Symptom = p.Symptom,
                PreOrder = p.PreOrder,
                URLPreOrder = p.URLPreOrder,
                Note = p.Note,
                OrderFahter = p.OrderFahter,
                SendCRM = p.SendCRM,
                Status = p.Status,
                OrderCreateDate = p.OrderCreateDate,
                OrderExecuteDate = p.OrderExecuteDate,
                CreateDate = p.CreateDate,
                ModifyDate = p.ModifyDate
            }).ToList<EntityOrder>();
        }

        public List<EntityOrder> GetAllListODSJourney(List<int> EmployeeID, DateTime MinfhExecute, DateTime MaxfhExecute, bool PreODS)
        {
            return new RepositoryOrder().GetAllListODSJourney(EmployeeID, MinfhExecute, MaxfhExecute, PreODS).Select(p => new EntityOrder()
            {
                PK_OrderID = p.PK_OrderID,
                FK_InstalledBaseID = p.FK_InstalledBaseID,
                FK_ClientID = p.FK_ClientID,
                FK_EmployeeID = p.FK_EmployeeID,
                FK_ModuleID = p.FK_ModuleID,
                FK_GuarantyID = p.FK_GuarantyID,
                FK_StatusSchemeID = p.FK_StatusSchemeID,
                OrderID = p.OrderID,
                TechnicalID = p.TechnicalID,
                Failure1 = p.Failure1,
                Failure2 = p.Failure2,
                Failure3 = p.Failure3,
                Failure4 = p.Failure4,
                Failure5 = p.Failure5,
                Symptom = p.Symptom,
                PreOrder = p.PreOrder,
                URLPreOrder = p.URLPreOrder,
                Note = p.Note,
                OrderFahter = p.OrderFahter,
                SendCRM = p.SendCRM,
                Status = p.Status,
                OrderCreateDate = p.OrderCreateDate,
                OrderExecuteDate = p.OrderExecuteDate,
                CreateDate = p.CreateDate,
                ModifyDate = p.ModifyDate
            }).ToList<EntityOrder>();
        }

        public EntityOrder GetByOrderID(string OrderID)
        {
            return new RepositoryOrder().GetByOrderID(OrderID);
        }
        
        public EntityOrder GetByOrderID(string OrderID, DateTime OrderExecuteDate)
        {
            var objRepository = new RepositoryOrder();
            return objRepository.GetByOrderID(OrderID, OrderExecuteDate);
        }
        
        public EntityOrder GetByID(int ID)
        {
            var objRepository = new RepositoryOrder();
            return objRepository.Get(ID);
        }

        public string SetQuotation(ModelViewUserG objCred, ModelViewBilling obj)
        {
            var objRepository = new RepositoryOrder();
            var objAlerta = new BusinessNotification();
            var NegocioUsuario = new BusinessUsers();
            var NegocioCliente = new BusinessClient();
            var NegocioBase = new BusinessInstalledBase();
            var NegocioProducto = new BusinessProduct();
            var NegocioFallas = new BusinessCodeFailure();
            var NegocioBOM = new BusinessBuildOfMaterial();
            var NegocioCotizacion = new BusinessQuotation();
            var NegocioCotizacionDetalle = new BusinessQuotationDetail();
            var NegocioCFP = new BusinessCodeFailureByProduct();
            var NegocioCF = new BusinessCodeFailure();
            //revisar
            var NegocioWF = new BusinessWorkforce();
            var NegocioLugarCompra = new BusinessShopPlace();
            var NegocioEmpleado = new BusinessEmployee();
            var NegocioMonitor = new BusinessVisit();
            var NegocioHistorico = new BusinessHistory();
            var NegocioGarantia = new BusinessGuaranty();
            var dataUsuario = NegocioUsuario.GetUserByToken(objCred.TokenUser);
            if (objCred.TokenApp != GlobalConfiguration.TokenWEB)
                if (objCred.TokenApp != GlobalConfiguration.TokenMobile)
                    throw new Exception("TokenInvalid");
            if (dataUsuario == null) throw new Exception("UserPasswordInvalid");

            if (obj.PreOrden == 1)
            {
                bool enviado;
                
                var orden = GetByOrderID(obj.OrderID);
                var cliente = NegocioCliente.GetByID(orden.FK_ClientID);
                var empleado = NegocioEmpleado.GetByUserID(dataUsuario.UserID);
                //DateTime? fecha = null;
                //fecha = (String.IsNullOrEmpty(obj.ShopDate) ?
                //    null : (DateTime?)Convert.ToDateTime(obj.ShopDate));
                //Insertar Nueva BaseInstalada
                var baseIns = new EntityInstalledBase();
                var baseInsHis = NegocioBase.GetAll().Where(p => p.FK_ClientID == cliente.PK_ClientID && p.FK_ProductID == obj.FK_ProductID).FirstOrDefault();
                if (baseInsHis == null)
                {
                    string BsIns = "I-" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                    baseIns = NegocioBase.Insert(cliente.PK_ClientID, obj.FK_ProductID.Value, obj.FK_ShopPlaceID.Value, BsIns, obj.SerialNumber, obj.ShopDate, null, null);
                }
                else
                {
                    baseIns = baseInsHis;
                }
                //
                var lugarcompra = NegocioLugarCompra.GetByShopPlaceID(obj.FK_ShopPlaceID.Value);
                var pro = NegocioProducto.GetByID(baseIns.FK_ProductID.Value);
                SpreadsheetInfo.SetLicense("EQU2-3K5L-UZDC-SDYN");
                string OriginPre = GlobalConfiguration.MabeAttachmentsLocal + "PreODS.xlsx";
                ExcelFile pre = ExcelFile.Load(OriginPre);
                ExcelWorksheet wspre = pre.Worksheets[0];
                var lspres = GetByOrderID(obj.ODS);
                if (lspres != null)
                {
                    if (lspres.OrderID == obj.ODS)
                    {

                        enviado = true;
                    }
                    else
                    {
                        enviado = false;
                    }
                }
                else
                {
                    enviado = false;
                }
                string OrderID = obj.ODS;
                //Excel PreODS
                wspre.Cells["L7"].Value = OrderID;
                wspre.Cells["L85"].Value = OrderID;
                wspre.Cells["L9"].Value = cliente.ClientID;
                wspre.Cells["L11"].Value = cliente.FirstName.ToUpper() + " " + cliente.LastName.ToUpper();
                wspre.Cells["L13"].Value = pro.Model + " / " + pro.ProductName.ToUpper();
                wspre.Cells["U7"].Value = DateTime.Today.ToString("dd-MM-yyyy");
                wspre.Cells["U9"].Value = lugarcompra.ShopPlaceID + " - " + lugarcompra.ShopPlace1;
                wspre.Cells["U10"].Value = baseIns.ShopDate.Value.ToString("dd-MM-yyyy");
                wspre.Cells["U11"].Value = NegocioGarantia.GetByGuarantyID(obj.ServiceType).Guaranty1;
                wspre.Cells["U13"].Value = baseIns.SerialNumber;
                var NewOrder = new EntityOrder();
                int failPre = 15;
                int countFaults = 1;
                foreach (var jko in obj.Failures)
                {
                    var falla = NegocioCF.GetByID(jko.FailureID);
                    wspre.Cells["L" + failPre.ToString()].Value = falla.Failure;
                    switch (countFaults)
                    {
                        case 1:
                            NewOrder.Failure1 = falla.CodeFailure1;
                            break;
                        case 2:
                            NewOrder.Failure2 = falla.CodeFailure1;
                            break;
                        case 3:
                            NewOrder.Failure3 = falla.CodeFailure1;
                            break;
                        case 4:
                            NewOrder.Failure4 = falla.CodeFailure1;
                            break;
                        case 5:
                            NewOrder.Failure5 = falla.CodeFailure1;
                            break;
                    }
                    countFaults++;
                    failPre++;
                }

                int canPre = 27;
                foreach (var jko in obj.BillingDetails)
                {
                    if (canPre == 69)
                    {
                        canPre = 91;
                    }
                    var bom = NegocioBOM.GetByID(jko.ProductID);

                    if (bom != null)
                    {
                        wspre.Cells["T" + canPre.ToString()].Value = bom.SparePartsID;
                        wspre.Cells["L" + canPre.ToString()].Value = bom.SparePartDescription;
                    }
                    else
                    {
                        wspre.Cells["T" + canPre.ToString()].Value = jko.RefManID;
                        wspre.Cells["L" + canPre.ToString()].Value = jko.SparePartsDescription;
                    }
                    wspre.Cells["J" + canPre.ToString()].Value = jko.Quantity;
                    wspre.Cells["V" + canPre.ToString()].Value = "E0001";
                    canPre = canPre + 3;
                }

                string filePre = "PreODS_" + OrderID + ".pdf";
                string PreODS = "PreODS_" + DateTime.Today.ToString("yyyyMMdd");
                string DestinyPre = GlobalConfiguration.MabeAttachmentsLocal + PreODS + "/" + filePre;
                string PreOrdendes = new DirectoryInfo(GlobalConfiguration.MabeAttachmentsLocal).ToString() + PreODS;
                if (!(Directory.Exists(PreOrdendes)))
                {
                    Directory.CreateDirectory(PreOrdendes);
                }

                if (obj.BillingDetails.Count < 15)
                { wspre.NamedRanges.SetPrintArea(wspre.Cells.GetSubrange("J1", "W81")); }
                pre.Save(DestinyPre);
                string URLPre = GlobalConfiguration.urlRequest + "Content/Attachments/" + PreODS + "/" + filePre;
                // Termina Excel PreODS

                var NuevaOrden = new EntityOrder();
                if (enviado == false)
                {
                    NewOrder.FK_InstalledBaseID = baseIns.PK_InstalledBaseID;
                    NewOrder.FK_ClientID = cliente.PK_ClientID;
                    NewOrder.FK_EmployeeID = orden.FK_EmployeeID.Value;
                    NewOrder.FK_ModuleID = orden.FK_ModuleID.Value;
                    NewOrder.OrderID = OrderID;
                    NewOrder.TechnicalID = orden.TechnicalID;
                    NewOrder.URLPreOrder = URLPre;
                    NewOrder.FK_GuarantyID = NegocioGarantia.GetByGuarantyID(obj.ServiceType).PK_GuarantyID;
                    //Insertar PreOrden
                    NuevaOrden = InsertPre(NewOrder);

                    
                    //Insertar Monitor
                    var OrdenVisita = NegocioMonitor.GetByOrderID(orden.PK_OrderID); 
                    NegocioMonitor.Insert(NuevaOrden.PK_OrderID, 1, 1, OrdenVisita.LatitudeAddress, OrdenVisita.LogitudeAddress);

                    lspres = GetByOrderID(obj.ODS);
                }
                else { NuevaOrden = lspres; }

                //Insertar Historico
                //string serviceID = obj.ServiceType == "Fuera de Garantía" ? "0070" : "0010";
                //NegocioHistorico.Insert(baseIns.PK_InstalledBaseID, cliente.PK_ClientID, NuevaOrden.PK_OrderID, NuevaOrden.OrderID, baseIns.ShopDate.ToString(), serviceID, "");
                ////
                var cotizacionPreODS = NegocioCotizacion.GetByOrder(lspres.PK_OrderID);
                var ordenes2 = GetByEmployee(orden.FK_EmployeeID.Value, DateTime.Now.Date);
                //var ordenes = GetAll().Where(p => empleado.Select(q => q.PK_EmployeeID).ToArray<int>().Contains(p.FK_EmployeeID.Value) && p.OrderExecuteDate >= DateTime.Now.Date).ToList();
                var coti = NegocioCotizacion.GetAll().Where(p => p.CreateDate.Value.Date == DateTime.Now.Date).ToList();

                var ls = (from a in coti
                          join b in ordenes2 on a.FK_OrdenID equals b.PK_OrderID
                          select a).ToList();
                int tam_var = orden.TechnicalID.Length;
                string emple = orden.TechnicalID.Substring((tam_var - 4), 4);
                string consecutivo;
                int contador = 0;
                contador = ls.Count + 1;
                consecutivo = null;
                consecutivo = contador.ToString("00");
                string Folio = cotizacionPreODS == null ? emple + "-" + DateTime.Now.Day.ToString("00") + DateTime.Now.Month.ToString("00") + DateTime.Now.Year.ToString().Substring(2) + "-" + consecutivo : cotizacionPreODS.Folio;

                string Origin = GlobalConfiguration.MabeAttachmentsLocal + "FormatoCotización.xlsx";
                List<string> result = new List<string>();

                ExcelFile ef = ExcelFile.Load(Origin);
                ExcelWorksheet ws = ef.Worksheets[0];
                ws.Cells["L6"].Value = Folio;
                ws.Cells["L7"].Value = OrderID;
                ws.Cells["L9"].Value = cliente.ClientID;
                ws.Cells["L11"].Value = cliente.FirstName.ToUpper() + " " + cliente.LastName.ToUpper();
                ws.Cells["L13"].Value = pro.Model + "/" + pro.ProductName.ToUpper();
                ws.Cells["U13"].Value = DateTime.Today.ToString("dd-MM-yyyy");
                ws.Cells["L79"].Value = obj.ODS;
                ws.Cells["L80"].Value = OrderID;
                int fail = 21;

                foreach (var jko in obj.Failures)
                {
                    var falla = NegocioCF.GetByID(jko.FailureID);
                    ws.Cells["L" + fail.ToString()].Value = falla.Failure;
                    fail++;
                }
                int cantidad = 30;
                int cantidad1 = 31;

                foreach (var jko in obj.BillingDetails)
                {
                    if (cantidad == 58)
                    {
                        cantidad = 85;
                        cantidad1 = 86;
                    }
                    var bom = NegocioBOM.GetByID(jko.ProductID);
                    if (bom != null)
                    {
                        ws.Cells["L" + cantidad1.ToString()].Value = bom.SparePartsID;
                        ws.Cells["L" + cantidad.ToString()].Value = bom.SparePartDescription;
                    }
                    else
                    {
                        ws.Cells["L" + cantidad1.ToString()].Value = jko.RefManID;
                        ws.Cells["L" + cantidad.ToString()].Value = jko.SparePartsDescription;
                    }
                    ws.Cells["J" + cantidad.ToString()].Value = jko.Quantity;
                    ws.Cells["T" + cantidad.ToString()].Value = Convert.ToDouble(jko.Price);
                    ws.Cells["V" + cantidad.ToString()].Value = Convert.ToDouble(jko.Totals);
                    cantidad = cantidad + 3;
                    cantidad1 = cantidad1 + 3;


                    var dataSpareParts = new BusinessSparePart().GetByRefManID(NuevaOrden.PK_OrderID, jko.RefManID);
                    if (dataSpareParts != null)
                    {
                        dataSpareParts.Quantity = jko.Quantity;
                        dataSpareParts.Price = float.Parse(jko.Price);
                        dataSpareParts.SparePartStatus = "E0001";
                        new BusinessSparePart().Update(dataSpareParts);
                    }
                    else
                    {
                        Int64 x = 0;
                        bool rest = Int64.TryParse(jko.RefManID, out x);
                        if (!rest)
                        {
                            var bomsparepart = new BusinessBuildOfMaterial().GetMaterialBySparePart(jko.RefManID, baseIns.FK_ProductID.Value);
                            new BusinessSparePart().Insert(NuevaOrden.PK_OrderID, bomsparepart.PK_BuildOfMaterialsID, bomsparepart.FK_ProductID, null, jko.Quantity, "0", "E0001", "ZMX00003", jko.SparePartsDescription, jko.RefManID, float.Parse(jko.Price.ToString()));
                        }
                        else
                        {
                            var mano = new BusinessWorkforce().GetAll().Where(p => p.WorkforceID == jko.RefManID).First();
                            new BusinessSparePart().Insert(NuevaOrden.PK_OrderID, null, null, mano.PK_WorkforceID, jko.Quantity, "0", "E0001", "ZMX00003", jko.SparePartsDescription, jko.RefManID, float.Parse(jko.Price.ToString()));
                        }
                    }
                }
                double manodeobra = 0;
                if (obj.WorkForce != null)
                {
                    manodeobra = Convert.ToDouble(obj.WorkForce.Substring(1));
                }
                double subtotal = Convert.ToDouble(obj.SubTotal.Substring(1));
                double iva = Convert.ToDouble(obj.IVA.Substring(1));
                double total = Convert.ToDouble(obj.Total.Substring(1));
                double subtotalref = subtotal - manodeobra;
                string totalletras = enletras(total.ToString());
                ws.Cells["M15"].Value = subtotalref;
                ws.Cells["M16"].Value = manodeobra;
                ws.Cells["M17"].Value = subtotal;
                ws.Cells["M18"].Value = iva;
                ws.Cells["U16"].Value = total;
                ws.Cells["M19"].Value = totalletras;

                string file = "Cotización_" + obj.ODS + ".pdf";
                string quotion = "Cotizaciones_" + DateTime.Today.ToString("yyyyMMdd");
                string Destiny = GlobalConfiguration.MabeAttachmentsLocal + quotion + "/" + file;

                string Cotizaciones = new DirectoryInfo(GlobalConfiguration.MabeAttachmentsLocal).ToString() + quotion;
                if (!(Directory.Exists(Cotizaciones)))
                {
                    Directory.CreateDirectory(Cotizaciones);
                }
                // Set print area
                if (obj.BillingDetails.Count < 11)
                { ws.NamedRanges.SetPrintArea(ws.Cells.GetSubrange("J1", "W74")); }
                ef.Save(Destiny);
                if (obj.EMails != "")
                {
                    result = obj.EMails.Split(new char[] { ';' }).ToList();
                    string URL = GlobalConfiguration.urlRequest + "Content/Attachments/" + quotion + "/" + file;

                    if (cotizacionPreODS == null)
                    {
                        cotizacionPreODS = NegocioCotizacion.Insert(NuevaOrden.PK_OrderID, subtotal.ToString(), iva.ToString(), total.ToString(), obj.ODS, URL, obj.EstimatedTipe, empleado[0].PK_EmployeeID);
                    }
                    else
                    {
                        cotizacionPreODS.Folio = Folio;
                        cotizacionPreODS.IVA = iva.ToString();
                        cotizacionPreODS.SubTotal = subtotal.ToString();
                        cotizacionPreODS.Total = total.ToString();
                        cotizacionPreODS.URL = URL;
                        cotizacionPreODS.ModifyDate = DateTime.UtcNow;
                        cotizacionPreODS.FK_EmployeeID = empleado[0].PK_EmployeeID;
                        NegocioCotizacion.Update(cotizacionPreODS);
                    }
                    foreach (var jko in obj.BillingDetails)
                    {
                        var newDetailquotion = NegocioCotizacionDetalle.GetDetail(cotizacionPreODS.PK_QuotationID, jko.ProductID);
                        if (newDetailquotion == null)
                        { NegocioCotizacionDetalle.Insert(cotizacionPreODS.PK_QuotationID, jko.ProductID); }
                    }

                    string sb = File.ReadAllText(GlobalConfiguration.LocateBodyMail + "NotificationCotizacion.txt");
                    string lugCom = baseIns.ShopDate != null ? baseIns.ShopDate.Value.ToString("yyyy-MM-dd") : "";
                    sb = sb.Replace("#%Nombre%#", cliente.FirstName + " " + cliente.LastName);
                    sb = sb.Replace("#%OrderID%#", NuevaOrden.OrderID);
                    sb = sb.Replace("#%Folio%#", Folio);
                    sb = sb.Replace("#%Modelo%#", pro.Model);
                    sb = sb.Replace("#%Descripcion%#", pro.ProductName.ToUpper());
                    sb = sb.Replace("#%Serie%#", baseIns.SerialNumber);
                    sb = sb.Replace("#%Fecha%#", lugCom);
                    sb = sb.Replace("#%Lugar%#", lugarcompra.ShopPlace1);
                    //objAlerta.SendMails(result, "Cotización ServiPlus", sb.ToString(), Destiny);
                    objAlerta.SendMailExchange(GlobalConfiguration.exchangeUser, GlobalConfiguration.exchangePwd, result, "Cotización ServiPlus", sb.ToString(), Destiny);
                }

                return NuevaOrden.OrderID;
                //bool enviado;
                //if (obj.ODS.Contains("-"))
                //{
                //    enviado = true;
                //}
                //else
                //{
                //    enviado = false;
                //}
                //var orden = GetByOrderID(obj.ODS);
                //var cliente = NegocioCliente.GetByID(orden.FK_ClientID);
                //var empleado = NegocioEmpleado.GetByUserID(dataUsuario.UserID);
                ////DateTime? fecha = null;
                ////fecha = (String.IsNullOrEmpty(obj.ShopDate) ?
                ////    null : (DateTime?)Convert.ToDateTime(obj.ShopDate));
                ////Insertar Nueva BaseInstalada
                //var baseIns = new EntityInstalledBase();
                //var baseInsHis = NegocioBase.GetAll().Where(p => p.FK_ClientID == cliente.PK_ClientID && p.FK_ProductID == obj.FK_ProductID).FirstOrDefault();
                //if (baseInsHis == null)
                //{
                //    string BsIns = "I-" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                //    baseIns = NegocioBase.Insert(cliente.PK_ClientID, obj.FK_ProductID.Value, obj.FK_ShopPlaceID.Value, BsIns, obj.SerialNumber, obj.ShopDate, null, null);
                //}
                //else
                //{
                //    baseIns = baseInsHis;
                //}
                ////
                //var lugarcompra = NegocioLugarCompra.GetByShopPlaceID(obj.FK_ShopPlaceID.Value);
                //var pro = NegocioProducto.GetByID(baseIns.FK_ProductID.Value);
                //SpreadsheetInfo.SetLicense("EQU2-3K5L-UZDC-SDYN");
                //string OriginPre = GlobalConfiguration.MabeAttachmentsLocal + "PreODS.xlsx";
                //ExcelFile pre = ExcelFile.Load(OriginPre);
                //ExcelWorksheet wspre = pre.Worksheets[0];
                //var lspre = GetAll().Where(p => empleado.Select(q => q.PK_EmployeeID).ToArray<int>().Contains(p.FK_EmployeeID.Value) && p.OrderExecuteDate >= DateTime.Now.Date && p.PreOrder == true).ToList();
                //int tam_var = orden.TechnicalID.Length;
                //string emple = orden.TechnicalID.Substring((tam_var - 4), 4);
                //string consecutivo;
                //int contador = 0;
                //contador = lspre.Count + 1;
                //consecutivo = contador.ToString("00");
                //string OrderID = enviado == false ? emple + "-" + DateTime.Now.Day.ToString("00") + DateTime.Now.Month.ToString("00") + DateTime.Now.Year.ToString().Substring(2) + "-" + consecutivo : orden.OrderID;
                ////Excel PreODS
                //wspre.Cells["L7"].Value = OrderID;
                //wspre.Cells["L85"].Value = OrderID;
                //wspre.Cells["L9"].Value = cliente.ClientID;
                //wspre.Cells["L11"].Value = cliente.FirstName.ToUpper() + " " + cliente.LastName.ToUpper();
                //wspre.Cells["L13"].Value = pro.Model + " / " + pro.ProductName.ToUpper();
                //wspre.Cells["U7"].Value = DateTime.Today.ToString("dd-MM-yyyy");
                //wspre.Cells["U9"].Value = lugarcompra.ShopPlaceID + " - " + lugarcompra.ShopPlace1;
                //wspre.Cells["U10"].Value = baseIns.ShopDate.Value.ToString("dd-MM-yyyy");
                //wspre.Cells["U11"].Value = NegocioGarantia.GetByGuarantyID(obj.ServiceType).Guaranty1;
                //wspre.Cells["U13"].Value = baseIns.SerialNumber;
                //var NewOrder = new EntityOrder();
                //int failPre = 15;
                //int countFaults = 1;
                //foreach (var jko in obj.Failures)
                //{
                //    var falla = NegocioCF.GetByID(jko.FailureID);
                //    wspre.Cells["L" + failPre.ToString()].Value = falla.Failure;
                //    switch (countFaults)
                //    {
                //        case 1:
                //            NewOrder.Failure1 = falla.CodeFailure1;
                //            break;
                //        case 2:
                //            NewOrder.Failure2 = falla.CodeFailure1;
                //            break;
                //        case 3:
                //            NewOrder.Failure3 = falla.CodeFailure1;
                //            break;
                //        case 4:
                //            NewOrder.Failure4 = falla.CodeFailure1;
                //            break;
                //        case 5:
                //            NewOrder.Failure5 = falla.CodeFailure1;
                //            break;
                //    }
                //    countFaults++;
                //    failPre++;
                //}

                //int canPre = 27;
                //foreach (var jko in obj.BillingDetails)
                //{
                //    if (canPre == 69)
                //    {
                //        canPre = 91;
                //    }
                //    var bom = NegocioBOM.GetByID(jko.ProductID);

                //    if (bom != null)
                //    {
                //        wspre.Cells["T" + canPre.ToString()].Value = bom.SparePartsID;
                //        wspre.Cells["L" + canPre.ToString()].Value = bom.SparePartDescription;
                //    }
                //    else
                //    {
                //        wspre.Cells["T" + canPre.ToString()].Value = jko.RefManID;
                //        wspre.Cells["L" + canPre.ToString()].Value = jko.SparePartsDescription;
                //    }
                //    wspre.Cells["J" + canPre.ToString()].Value = jko.Quantity;
                //    wspre.Cells["V" + canPre.ToString()].Value = "E0001";
                //    canPre = canPre + 3;
                //}

                //string filePre = "PreODS_" + OrderID + ".pdf";
                //string PreODS = "PreODS_" + DateTime.Today.ToString("yyyyMMdd");
                //string DestinyPre = GlobalConfiguration.MabeAttachmentsLocal + PreODS + "/" + filePre;
                //string PreOrdendes = new DirectoryInfo(GlobalConfiguration.MabeAttachmentsLocal).ToString() + PreODS;
                //if (!(Directory.Exists(PreOrdendes)))
                //{
                //    Directory.CreateDirectory(PreOrdendes);
                //}

                //if (obj.BillingDetails.Count < 15)
                //{ wspre.NamedRanges.SetPrintArea(wspre.Cells.GetSubrange("J1", "W81")); }
                //pre.Save(DestinyPre);
                //string URLPre = GlobalConfiguration.urlRequest + "Content/Attachments/" + PreODS + "/" + filePre;
                //// Termina Excel PreODS

                //var NuevaOrden = new EntityOrder();
                //if (enviado == false)
                //{
                //    NewOrder.FK_InstalledBaseID = baseIns.PK_InstalledBaseID;
                //    NewOrder.FK_ClientID = cliente.PK_ClientID;
                //    NewOrder.FK_EmployeeID = orden.FK_EmployeeID.Value;
                //    NewOrder.FK_ModuleID = orden.FK_ModuleID.Value;
                //    NewOrder.OrderID = OrderID;
                //    NewOrder.TechnicalID = orden.TechnicalID;
                //    NewOrder.URLPreOrder = URLPre;
                //    NewOrder.FK_GuarantyID = NegocioGarantia.GetByGuarantyID(obj.ServiceType).PK_GuarantyID;
                //    //Insertar PreOrden
                //    NuevaOrden = InsertPre(NewOrder);

                //    //

                //    //Insertar Monitor
                //    var OrdenVisita = NegocioMonitor.GetAll().Where(p => p.FK_OrderID == orden.PK_OrderID).FirstOrDefault();
                //    NegocioMonitor.Insert(NuevaOrden.PK_OrderID, 1, 1, OrdenVisita.LatitudeAddress, OrdenVisita.LogitudeAddress);
                //    //
                //}
                //else { NuevaOrden = orden; }

                ////Insertar Historico
                ////string serviceID = obj.ServiceType == "Fuera de Garantía" ? "0070" : "0010";
                ////NegocioHistorico.Insert(baseIns.PK_InstalledBaseID, cliente.PK_ClientID, NuevaOrden.PK_OrderID, NuevaOrden.OrderID, baseIns.ShopDate.ToString(), serviceID, "");
                //////
                //var cotizacionPreODS = NegocioCotizacion.GetByOrder(orden.PK_OrderID);
                //var ordenes = GetAll().Where(p => empleado.Select(q => q.PK_EmployeeID).ToArray<int>().Contains(p.FK_EmployeeID.Value) && p.OrderExecuteDate >= DateTime.Now.Date).ToList();
                //var coti = NegocioCotizacion.GetAll().Where(p => p.CreateDate.Value.Date == DateTime.Now.Date).ToList();

                //var ls = (from a in coti
                //          join b in ordenes on a.FK_OrdenID equals b.PK_OrderID
                //          select a).ToList();

                //contador = 0;
                //contador = ls.Count + 1;
                //consecutivo = null;
                //consecutivo = contador.ToString("00");
                //string Folio = cotizacionPreODS == null ? emple + "-" + DateTime.Now.Day.ToString("00") + DateTime.Now.Month.ToString("00") + DateTime.Now.Year.ToString().Substring(2) + "-" + consecutivo : cotizacionPreODS.Folio;

                //string Origin = GlobalConfiguration.MabeAttachmentsLocal + "FormatoCotización.xlsx";
                //List<string> result = new List<string>();

                //ExcelFile ef = ExcelFile.Load(Origin);
                //ExcelWorksheet ws = ef.Worksheets[0];
                //ws.Cells["L6"].Value = Folio;
                //ws.Cells["L7"].Value = OrderID;
                //ws.Cells["L9"].Value = cliente.ClientID;
                //ws.Cells["L11"].Value = cliente.FirstName.ToUpper() + " " + cliente.LastName.ToUpper();
                //ws.Cells["L13"].Value = pro.Model + "/" + pro.ProductName.ToUpper();
                //ws.Cells["U13"].Value = DateTime.Today.ToString("dd-MM-yyyy");
                //ws.Cells["L79"].Value = Folio;
                //ws.Cells["L80"].Value = OrderID;
                //int fail = 21;

                //foreach (var jko in obj.Failures)
                //{
                //    var falla = NegocioCF.GetByID(jko.FailureID);
                //    ws.Cells["L" + fail.ToString()].Value = falla.Failure;
                //    fail++;
                //}
                //int cantidad = 30;
                //int cantidad1 = 31;

                //foreach (var jko in obj.BillingDetails)
                //{
                //    if (cantidad == 58)
                //    {
                //        cantidad = 85;
                //        cantidad1 = 86;
                //    }
                //    var bom = NegocioBOM.GetByID(jko.ProductID);
                //    if (bom != null)
                //    {
                //        ws.Cells["L" + cantidad1.ToString()].Value = bom.SparePartsID;
                //        ws.Cells["L" + cantidad.ToString()].Value = bom.SparePartDescription;
                //    }
                //    else
                //    {
                //        ws.Cells["L" + cantidad1.ToString()].Value = jko.RefManID;
                //        ws.Cells["L" + cantidad.ToString()].Value = jko.SparePartsDescription;
                //    }
                //    ws.Cells["J" + cantidad.ToString()].Value = jko.Quantity;
                //    ws.Cells["T" + cantidad.ToString()].Value = Convert.ToDouble(jko.Price);
                //    ws.Cells["V" + cantidad.ToString()].Value = Convert.ToDouble(jko.Totals);
                //    cantidad = cantidad + 3;
                //    cantidad1 = cantidad1 + 3;


                //    var dataSpareParts = new BusinessSparePart().GetByRefManID(NuevaOrden.PK_OrderID, jko.RefManID);
                //    if (dataSpareParts != null)
                //    {
                //        dataSpareParts.Quantity = jko.Quantity;
                //        dataSpareParts.Price = float.Parse(jko.Price);
                //        dataSpareParts.SparePartStatus = "E0001";
                //        new BusinessSparePart().Update(dataSpareParts);
                //    }
                //    else
                //    {
                //        Int64 x = 0;
                //        bool rest = Int64.TryParse(jko.RefManID, out x);
                //        if (!rest)
                //        {
                //            var bomsparepart = new BusinessBuildOfMaterial().GetMaterialBySparePart(jko.RefManID, baseIns.FK_ProductID.Value);
                //            new BusinessSparePart().Insert(NuevaOrden.PK_OrderID, bomsparepart.PK_BuildOfMaterialsID, bomsparepart.FK_ProductID, null, jko.Quantity, "0", "E0001", "ZMX00003", jko.SparePartsDescription, jko.RefManID, float.Parse(jko.Price.ToString()));
                //        }
                //        else
                //        {
                //            var mano = new BusinessWorkforce().GetAll().Where(p => p.WorkforceID == jko.RefManID).First();
                //            new BusinessSparePart().Insert(NuevaOrden.PK_OrderID, null, null, mano.PK_WorkforceID, jko.Quantity, "0", "E0001", "ZMX00003", jko.SparePartsDescription, jko.RefManID, float.Parse(jko.Price.ToString()));
                //        }
                //    }
                //}
                //double manodeobra = 0;
                //if (obj.WorkForce != null)
                //{
                //    manodeobra = Convert.ToDouble(obj.WorkForce.Substring(1));
                //}
                //double subtotal = Convert.ToDouble(obj.SubTotal.Substring(1));
                //double iva = Convert.ToDouble(obj.IVA.Substring(1));
                //double total = Convert.ToDouble(obj.Total.Substring(1));
                //double subtotalref = subtotal - manodeobra;
                //string totalletras = enletras(total.ToString());
                //ws.Cells["M15"].Value = subtotalref;
                //ws.Cells["M16"].Value = manodeobra;
                //ws.Cells["M17"].Value = subtotal;
                //ws.Cells["M18"].Value = iva;
                //ws.Cells["U16"].Value = total;
                //ws.Cells["M19"].Value = totalletras;

                //string file = "Cotización_" + Folio + ".pdf";
                //string quotion = "Cotizaciones_" + DateTime.Today.ToString("yyyyMMdd");
                //string Destiny = GlobalConfiguration.MabeAttachmentsLocal + quotion + "/" + file;

                //string Cotizaciones = new DirectoryInfo(GlobalConfiguration.MabeAttachmentsLocal).ToString() + quotion;
                //if (!(Directory.Exists(Cotizaciones)))
                //{
                //    Directory.CreateDirectory(Cotizaciones);
                //}
                //// Set print area
                //if (obj.BillingDetails.Count < 11)
                //{ ws.NamedRanges.SetPrintArea(ws.Cells.GetSubrange("J1", "W74")); }
                //ef.Save(Destiny);
                //if (obj.EMails != "")
                //{
                //    result = obj.EMails.Split(new char[] { ';' }).ToList();
                //    string URL = GlobalConfiguration.urlRequest + "Content/Attachments/" + quotion + "/" + file;

                //    if (cotizacionPreODS == null)
                //    { cotizacionPreODS = NegocioCotizacion.Insert(NuevaOrden.PK_OrderID, subtotal.ToString(), iva.ToString(), total.ToString(), Folio, URL); }
                //    else
                //    {
                //        cotizacionPreODS.Folio = Folio;
                //        cotizacionPreODS.IVA = iva.ToString();
                //        cotizacionPreODS.SubTotal = subtotal.ToString();
                //        cotizacionPreODS.Total = total.ToString();
                //        cotizacionPreODS.URL = URL;
                //        cotizacionPreODS.ModifyDate = DateTime.UtcNow;
                //        NegocioCotizacion.Update(cotizacionPreODS);
                //    }
                //    foreach (var jko in obj.BillingDetails)
                //    {
                //        var newDetailquotion = NegocioCotizacionDetalle.GetDetail(cotizacionPreODS.PK_QuotationID, jko.ProductID);
                //        if (newDetailquotion == null)
                //        { NegocioCotizacionDetalle.Insert(cotizacionPreODS.PK_QuotationID, jko.ProductID); }
                //    }

                //    string sb = File.ReadAllText(GlobalConfiguration.LocateBodyMail + "NotificationCotizacion.txt");
                //    string lugCom = baseIns.ShopDate != null ? baseIns.ShopDate.Value.ToString("yyyy-MM-dd") : "";
                //    sb = sb.Replace("#%Nombre%#", cliente.FirstName + " " + cliente.LastName);
                //    sb = sb.Replace("#%OrderID%#", NuevaOrden.OrderID);
                //    sb = sb.Replace("#%Folio%#", Folio);
                //    sb = sb.Replace("#%Modelo%#", pro.Model);
                //    sb = sb.Replace("#%Descripcion%#", pro.ProductName.ToUpper());
                //    sb = sb.Replace("#%Serie%#", baseIns.SerialNumber);
                //    sb = sb.Replace("#%Fecha%#", lugCom);
                //    sb = sb.Replace("#%Lugar%#", lugarcompra.ShopPlace1);
                //    //objAlerta.SendMails(result, "Cotización ServiPlus", sb.ToString(), Destiny);
                //    objAlerta.SendMailExchange(GlobalConfiguration.exchangeUser, GlobalConfiguration.exchangePwd, result, "Cotización ServiPlus", sb.ToString(), Destiny);
                //}

                //return NuevaOrden.OrderID;
            }
            else
            {
                
                var empleado = NegocioEmpleado.GetByUserID(dataUsuario.UserID);
                var orden = GetByOrderID(obj.ODS);
                var cliente = NegocioCliente.GetByID(orden.FK_ClientID);
                //var baseIns = NegocioBase.GetByID(orden.FK_InstalledBaseID);
                var lugarcompra = NegocioLugarCompra.GetByShopPlaceID(obj.FK_ShopPlaceID.Value);
                var pro = NegocioProducto.GetByID(obj.FK_ProductID.Value);
                var coti = NegocioCotizacion.GetAll().Where(p => p.CreateDate.Value.Date == DateTime.Now.Date).ToList();
                var ordenes2 = GetByEmployee(orden.FK_EmployeeID.Value,DateTime.Now.Date);
                //var ordenes = GetAll().Where(p => empleado.Select(q => q.PK_EmployeeID).ToArray<int>().Contains(p.FK_EmployeeID.Value) && p.OrderExecuteDate >= DateTime.Now.Date).ToList();
                var ls = (from a in coti
                          join b in ordenes2 on a.FK_OrdenID equals b.PK_OrderID
                          select a).ToList();
                int tam_var = orden.TechnicalID.Length;
                string emple = orden.TechnicalID.Substring((tam_var - 4), 4);
                string consecutivo;
                int contador = 0;
                contador = ls.Count + 1;
                consecutivo = contador.ToString("00");
                var cotizacion = NegocioCotizacion.GetByOrderType(orden.PK_OrderID,obj.EstimatedTipe);
                string Folio = "";
                Folio = cotizacion == null ? emple + "-" + DateTime.Now.Day.ToString("00") + DateTime.Now.Month.ToString("00") + DateTime.Now.Year.ToString().Substring(2) + "-" + consecutivo : cotizacion.Folio;
                SpreadsheetInfo.SetLicense("EQU2-3K5L-UZDC-SDYN");
                string Origin = GlobalConfiguration.MabeAttachmentsLocal + "FormatoCotización1.xlsx";
                List<string> result = obj.EMails.Split(new char[] { ';' }).ToList();
                ExcelFile ef = ExcelFile.Load(Origin);
                ExcelWorksheet ws = ef.Worksheets[0];

                ws.Cells["Q2"].Value = DateTime.Today.ToString("dd-MM-yyyy");
                ws.Cells["L5"].Value = cliente.FirstName.ToUpper() + " " + cliente.LastName.ToUpper();
                ws.Cells["Q5"].Value = cliente.ClientID;
                ws.Cells["S5"].Value = obj.ODS;
                ws.Cells["W5"].Value = Folio;
                ws.Cells["L6"].Value = pro.Model + " / " + pro.ProductName.ToUpper();
                ws.Cells["V6"].Value = obj.SerialNumber;

                //se mapean fallas 
                int fail = 10;
                foreach (var jko in obj.Failures)
                {
                    var falla = NegocioCF.GetByID(jko.FailureID);
                    ws.Cells["J"+fail.ToString()].Value = falla.CodeFailure1;
                    string[] descripcion = falla.Failure.Split('-');
                    ws.Cells["L" + fail.ToString()].Value = descripcion[1];
                    fail++;
                }

              
                //empieza revision diagnostico
                int cantidad = 26;
                int cantidad1 = 29;
                int rev = 16;
                int rev1 = 16;
                int costRev = 16;
                int manoVenta = 23;
                int refa = 26;
                int refafin = refa;
                int costRefa = 26;
                int kilometros = 0;
                int pruebagit=0;
                

                foreach (var jko in obj.BillingDetails)
                {
                    if (cantidad == 60)
                    {
                        cantidad = 85;
                        cantidad1 = 86;
                    }
                    //DE MANO DE OBRA DE VENTA
                    int wfk = 1;
                    var bom = NegocioBOM.GetByID(jko.ProductID);
                    var wfitem = NegocioWF.GetByID(wfk);
                    //revisar

                    if (bom != null)
                    {
                        //refaccion linea 26
                        ws.Cells["J" + refa.ToString()].Value = bom.SparePartsID;
                        ws.Cells["M" + refa.ToString()].Value = bom.SparePartDescription;
                        ws.Cells["T" + refa.ToString()].Value = bom.Quantity;
                        ws.Cells["U" + refa.ToString()].Value = jko.Price;
                        ws.Cells["W" + refa.ToString()].Value = jko.Totals;
                        refa = refa + 1;
                        refafin = refa;


                        //MANO DE OBRA VENTA 22-24
                        ws.Cells["J" + manoVenta.ToString()].Value = wfitem.WorkforceID;
                        ws.Cells["M" + manoVenta.ToString()].Value = wfitem.Description;
                        ws.Cells["U" + manoVenta.ToString()].Value = obj.WorkForce;


                    }
                    
                    else
                    {
                        if (jko.SparePartsDescription == "Kilometraje")
                        {
                           
                            refafin = refa;
                            ws.Cells["J" + refafin.ToString()].Value = jko.RefManID;
                            ws.Cells["M" + refafin.ToString()].Value = "Costo de servicio fuera de cobertura.";
                            //ws.Cells["J44"].Value = "En caso de requerir factura esta se emitirá por parte del Centro de Servicio que lo atendió";
                        }

                        //MANO DE OBRA REVISION 15-17
                        else
                        {
                            ws.Cells["J" + rev.ToString()].Value = jko.RefManID;
                            ws.Cells["M" + rev1.ToString()].Value = jko.SparePartsDescription;
                            ws.Cells["U" + rev1.ToString()].Value = jko.Price;

                        }
                        //REFACCON
                        //ws.Cells["T" + refa.ToString()].Value = jko.Quantity;
                        //ws.Cells["U" + refa.ToString()].Value = Convert.ToDouble(jko.Price);
                        //ws.Cells["W" + refa.ToString()].Value = Convert.ToDouble(jko.Totals);

                        //cantidad = cantidad + 1;
                        //cantidad1 = cantidad1 + 1;
                    }
                }


                double manodeobra = 0;
                if (obj.WorkForce != null)
                {
                  
                    manodeobra = Convert.ToDouble(obj.WorkForce.Substring(1));
                }
                double subtotal = Convert.ToDouble(obj.SubTotal.Substring(1));
                double iva = Convert.ToDouble(obj.IVA.Substring(1));
                double total = Convert.ToDouble(obj.Total.Substring(1));
                double subtotalref = subtotal - manodeobra;
                string totalletras = enletras(total.ToString());
                //ws.Cells["W31"].Value = subtotalref;
               // ws.Cells["W35"].Value = manodeobra;
                ws.Cells["W31"].Value = subtotal;
                ws.Cells["W32"].Value = iva;
                ws.Cells["W33"].Value = total;
                //ws.Cells["W34"].Value = totalletras;
                string file = "Cotización_" + Folio + ".pdf";
                string quotion = "Cotizaciones_" + DateTime.Today.ToString("yyyyMMdd");
                string Destiny = GlobalConfiguration.MabeAttachmentsLocal + quotion + "/" + file;
                if (obj.BillingDetails.Count < 11)
                { ws.NamedRanges.SetPrintArea(ws.Cells.GetSubrange("J1", "W74")); }
                string Cotizaciones = new DirectoryInfo(GlobalConfiguration.MabeAttachmentsLocal).ToString() + quotion;
                if (!(Directory.Exists(Cotizaciones)))
                {
                    Directory.CreateDirectory(Cotizaciones);
                }
                ef.Save(Destiny);
                string URL = GlobalConfiguration.urlRequest + "Content/Attachments/" + quotion + "/" + file;

                if (obj.EMails != "")
                {
                    string sb = File.ReadAllText(GlobalConfiguration.LocateBodyMail + "NotificationCotizacion.txt");
                    string lugCom = obj.ShopDate != null ? obj.ShopDate : "";
                    sb = sb.Replace("#%Nombre%#", cliente.FirstName + " " + cliente.LastName);
                    sb = sb.Replace("#%OrderID%#", obj.ODS);
                    sb = sb.Replace("#%Folio%#", Folio);
                    sb = sb.Replace("#%Modelo%#", pro.Model);
                    sb = sb.Replace("#%Descripcion%#", pro.ProductName.ToUpper());
                    sb = sb.Replace("#%Serie%#", obj.SerialNumber);
                    sb = sb.Replace("#%Fecha%#", lugCom);
                    sb = sb.Replace("#%Lugar%#", lugarcompra.ShopPlace1);
                    //objAlerta.SendMails(result, "Cotización ServiPlus", sb.ToString(), Destiny);
                    objAlerta.SendMailExchange(GlobalConfiguration.exchangeUserCotiza, GlobalConfiguration.exchangePwdCotiza, result, "Cotización ServiPlus", sb.ToString(), Destiny);

                    if (cotizacion == null)
                    { cotizacion = NegocioCotizacion.Insert(orden.PK_OrderID, subtotal.ToString(), iva.ToString(), total.ToString(), Folio, URL, obj.EstimatedTipe, empleado[0].PK_EmployeeID); }
                    else
                    {
                        cotizacion.Folio = Folio;
                        cotizacion.IVA = iva.ToString();
                        cotizacion.SubTotal = subtotal.ToString();
                        cotizacion.Total = total.ToString();
                        cotizacion.URL = URL;
                        cotizacion.ModifyDate = DateTime.UtcNow;
                        cotizacion.TypeQuotation = obj.EstimatedTipe;               
                        cotizacion.FK_EmployeeID = empleado[0].PK_EmployeeID;
                        NegocioCotizacion.Update(cotizacion);
                    }

                }
                else
                {
                    if (cotizacion == null)
                    { cotizacion = NegocioCotizacion.Insert(orden.PK_OrderID, subtotal.ToString(), iva.ToString(), total.ToString(), Folio, URL, obj.EstimatedTipe, empleado[0].PK_EmployeeID); }
                    else
                    {
                        cotizacion.Folio = Folio;
                        cotizacion.IVA = iva.ToString();
                        cotizacion.SubTotal = subtotal.ToString();
                        cotizacion.Total = total.ToString();
                        cotizacion.URL = URL;
                        cotizacion.ModifyDate = DateTime.UtcNow;
                        cotizacion.TypeQuotation = obj.EstimatedTipe;
                        cotizacion.FK_EmployeeID = empleado[0].PK_EmployeeID;
                        NegocioCotizacion.Update(cotizacion);
                    }
                    foreach (var jko in obj.BillingDetails)
                    {
                        var newDetailquotion = NegocioCotizacionDetalle.GetDetail(cotizacion.PK_QuotationID, jko.ProductID);
                        if (newDetailquotion == null)
                        { NegocioCotizacionDetalle.Insert(cotizacion.PK_QuotationID, jko.ProductID); }
                    }
                }
                
                return obj.ODS;
            }
        }

        public string enletras(string num)
        {
            string res, dec = "";
            Int64 entero;
            int decimales;
            double nro;

            try

            {
                nro = Convert.ToDouble(num);
            }
            catch
            {
                return "";
            }

            entero = Convert.ToInt64(Math.Truncate(nro));
            decimales = Convert.ToInt32(Math.Round((nro - entero) * 100, 2));
            if (decimales > 0)
            {
                dec = " PESOS CON " + decimales.ToString() + "/100" + " M.N.";
            }

            res = toText(Convert.ToDouble(entero)) + dec;
            return res;
        }

        private string toText(double value)
        {
            string Num2Text = "";
            value = Math.Truncate(value);
            if (value == 0) Num2Text = "CERO";
            else if (value == 1) Num2Text = "UNO";
            else if (value == 2) Num2Text = "DOS";
            else if (value == 3) Num2Text = "TRES";
            else if (value == 4) Num2Text = "CUATRO";
            else if (value == 5) Num2Text = "CINCO";
            else if (value == 6) Num2Text = "SEIS";
            else if (value == 7) Num2Text = "SIETE";
            else if (value == 8) Num2Text = "OCHO";
            else if (value == 9) Num2Text = "NUEVE";
            else if (value == 10) Num2Text = "DIEZ";
            else if (value == 11) Num2Text = "ONCE";
            else if (value == 12) Num2Text = "DOCE";
            else if (value == 13) Num2Text = "TRECE";
            else if (value == 14) Num2Text = "CATORCE";
            else if (value == 15) Num2Text = "QUINCE";
            else if (value < 20) Num2Text = "DIECI" + toText(value - 10);
            else if (value == 20) Num2Text = "VEINTE";
            else if (value < 30) Num2Text = "VEINTI" + toText(value - 20);
            else if (value == 30) Num2Text = "TREINTA";
            else if (value == 40) Num2Text = "CUARENTA";
            else if (value == 50) Num2Text = "CINCUENTA";
            else if (value == 60) Num2Text = "SESENTA";
            else if (value == 70) Num2Text = "SETENTA";
            else if (value == 80) Num2Text = "OCHENTA";
            else if (value == 90) Num2Text = "NOVENTA";
            else if (value < 100) Num2Text = toText(Math.Truncate(value / 10) * 10) + " Y " + toText(value % 10);
            else if (value == 100) Num2Text = "CIEN";
            else if (value < 200) Num2Text = "CIENTO " + toText(value - 100);
            else if ((value == 200) || (value == 300) || (value == 400) || (value == 600) || (value == 800)) Num2Text = toText(Math.Truncate(value / 100)) + "CIENTOS";
            else if (value == 500) Num2Text = "QUINIENTOS";
            else if (value == 700) Num2Text = "SETECIENTOS";
            else if (value == 900) Num2Text = "NOVECIENTOS";
            else if (value < 1000) Num2Text = toText(Math.Truncate(value / 100) * 100) + " " + toText(value % 100);
            else if (value == 1000) Num2Text = "MIL";
            else if (value < 2000) Num2Text = "MIL " + toText(value % 1000);
            else if (value < 1000000)
            {
                Num2Text = toText(Math.Truncate(value / 1000)) + " MIL";
                if ((value % 1000) > 0) Num2Text = Num2Text + " " + toText(value % 1000);
            }

            else if (value == 1000000) Num2Text = "UN MILLON";
            else if (value < 2000000) Num2Text = "UN MILLON " + toText(value % 1000000);
            else if (value < 1000000000000)
            {
                Num2Text = toText(Math.Truncate(value / 1000000)) + " MILLONES ";
                if ((value - Math.Truncate(value / 1000000) * 1000000) > 0) Num2Text = Num2Text + " " + toText(value - Math.Truncate(value / 1000000) * 1000000);
            }

            else if (value == 1000000000000) Num2Text = "UN BILLON";
            else if (value < 2000000000000) Num2Text = "UN BILLON " + toText(value - Math.Truncate(value / 1000000000000) * 1000000000000);

            else
            {
                Num2Text = toText(Math.Truncate(value / 1000000000000)) + " BILLONES";
                if ((value - Math.Truncate(value / 1000000000000) * 1000000000000) > 0) Num2Text = Num2Text + " " + toText(value - Math.Truncate(value / 1000000000000) * 1000000000000);
            }
            return Num2Text;

        }
        
        public void SetStatus(string OrderID)
        {
            var order = GetByOrderID(OrderID);
            order.SendCRM = "OK";
            Update(order);
        }

        public List<AddressV> GetList_Address(string OrderID)
        {

            var NegocioEmpleado = new BusinessEmployee();
            var NegocioOrdenes = new BusinessOrder();
            var NegocioEstatusOrden = new BusinessStatusCauseOrder();
            var NegocioGarantia = new BusinessGuaranty();
            var NegocioModulosMabe = new BusinessModuleService();
            var NegocioEvidencia = new BusinessOrderEvidence();
            var NegocioVisitas = new BusinessMonitor();
            var NegocioUsuario = new BusinessUsers();
            var NegocioPrioridad = new BusinessPriority();
            var NegocioSecuencia = new BusinessSchedule();
            var orden = GetByOrderID(OrderID);
            var dataEmpleado = NegocioEmpleado.GetID(orden.FK_EmployeeID.Value);
            var dataUsuario = NegocioUsuario.Get(dataEmpleado.FK_UserID.Value);
            var empleado = NegocioEmpleado.GetByUserID(dataUsuario.UserID);
            var ordenes = GetAll(empleado.Select(q => q.PK_EmployeeID).ToList(), orden.OrderExecuteDate.Date, false);
            var visitas = NegocioVisitas.GeListByOrder(ordenes.Select(p => p.PK_OrderID).ToList());
            var estatus = NegocioEstatusOrden.GetAll();
            if (visitas.Where(p => p.StatusVisitID == 3).Count() > 0)
            {
                int inicial = 0;
                int final = 0;
                string ini;
                string fin;
                DateTime StarVisit = visitas.Where(p => p.StatusVisitID == 3).First().StartVisitDate.Value;
                double totalVisitaDir = 0;
                double totalTrasladoDir = 0;
                double totalVisitaSem = 0;
                double totalTrasladoSem = 0;
                double TotalVisitaGral = 0;
                double TotalTrasladoGral = 0;
                TimeSpan inicio;
                var visitasDiarias = visitas.Where(p => p.StatusOrderID == 4).ToList();
                if (visitasDiarias.Count > 0)
                {
                    totalVisitaDir = visitasDiarias.Sum(s => s.DurationVisit.Value.TotalMinutes) / visitasDiarias.Count();
                    totalTrasladoDir = GetAverageTransportDir(empleado.Select(q => q.PK_EmployeeID).ToList(), orden.OrderExecuteDate);
                    totalVisitaSem = GetAverageVisitSem(empleado.Select(q => q.PK_EmployeeID).ToList(), orden.OrderExecuteDate);
                    totalTrasladoSem = GetAverageTransportSem(empleado.Select(q => q.PK_EmployeeID).ToList(), orden.OrderExecuteDate);
                    TotalVisitaGral = (totalVisitaDir + totalVisitaSem) / 2;
                    TotalTrasladoGral = (totalTrasladoDir + totalTrasladoSem) / 2;
                }
                else
                {
                    TotalVisitaGral = GetAverageVisitSem(empleado.Select(q => q.PK_EmployeeID).ToList(), orden.OrderExecuteDate);
                    TotalTrasladoGral = GetAverageTransportSem(empleado.Select(q => q.PK_EmployeeID).ToList(), orden.OrderExecuteDate);
                }
                foreach (var item in visitas.OrderBy(p => p.SequenceVisit))
                {
                    switch (item.StatusVisitID)
                    {
                        case 1:
                            inicial = (int)(final + TotalTrasladoGral);
                            final = (int)(inicial + TotalVisitaGral);
                            ini = TimeSpan.FromMinutes(inicial).ToString();
                            fin = TimeSpan.FromMinutes(final).ToString();
                            item.Time = ini + " - " + fin;
                            break;
                        case 3:
                            inicio = item.StartVisitDate.Value.TimeOfDay;
                            inicial = (int)inicio.TotalMinutes;
                            final = (int)(inicial + TotalVisitaGral);
                            ini = TimeSpan.FromMinutes(inicial).ToString();
                            fin = TimeSpan.FromMinutes(final).ToString();
                            item.Time = ini + " - " + fin;
                            break;
                    }
                }
            }
            else
            {
                if(visitas.Where(p => p.StatusVisitID == 4).Count() > 0)
                {

                    int inicial = 0;
                    int final = 0;
                    string ini;
                    string fin;
                    DateTime StarVisit = visitas.Where(p => p.StatusVisitID == 4).Last().StartVisitDate.Value;
                    double totalVisitaDir = 0;
                    double totalTrasladoDir = 0;
                    double totalVisitaSem = 0;
                    double totalTrasladoSem = 0;
                    double TotalVisitaGral = 0;
                    double TotalTrasladoGral = 0;
                    TimeSpan inicio;
                    var visitasDiarias = visitas.Where(p => p.StatusOrderID == 4).ToList();
                    if (visitasDiarias.Count > 0)
                    {
                        totalVisitaDir = visitasDiarias.Sum(s => s.DurationVisit.Value.TotalMinutes) / visitasDiarias.Count();
                        totalTrasladoDir = GetAverageTransportDir(empleado.Select(q => q.PK_EmployeeID).ToList(), orden.OrderExecuteDate);
                        totalVisitaSem = GetAverageVisitSem(empleado.Select(q => q.PK_EmployeeID).ToList(), orden.OrderExecuteDate);
                        totalTrasladoSem = GetAverageTransportSem(empleado.Select(q => q.PK_EmployeeID).ToList(), orden.OrderExecuteDate);
                        TotalVisitaGral = (totalVisitaDir + totalVisitaSem) / 2;
                        TotalTrasladoGral = (totalTrasladoDir + totalTrasladoSem) / 2;
                    }
                    else
                    {
                        TotalVisitaGral = GetAverageVisitSem(empleado.Select(q => q.PK_EmployeeID).ToList(), orden.OrderExecuteDate);
                        TotalTrasladoGral = GetAverageTransportSem(empleado.Select(q => q.PK_EmployeeID).ToList(), orden.OrderExecuteDate);
                    }
                    foreach (var item in visitas.OrderBy(p => p.SequenceVisit))
                    {
                        switch (item.StatusVisitID)
                        {
                            case 1:
                                inicial = (int)(final + TotalTrasladoGral);
                                final = (int)(inicial + TotalVisitaGral);
                                ini = TimeSpan.FromMinutes(inicial).ToString();
                                fin = TimeSpan.FromMinutes(final).ToString();
                                item.Time = ini + " - " + fin;
                                break;
                            case 4:
                                inicio = item.StartVisitDate.Value.TimeOfDay;
                                inicial = (int)inicio.TotalMinutes;
                                final = (int)(inicial + TotalVisitaGral);
                                ini = TimeSpan.FromMinutes(inicial).ToString();
                                fin = TimeSpan.FromMinutes(final).ToString();
                                item.Time = ini + " - " + fin;
                                break;
                        }
                    }


                }
                else
                {
                    int sec = 10;
                    foreach (var item in visitas.OrderBy(p => p.SequenceVisit))
                    {
                        if (item.SequenceVisit < 10)
                        { sec = item.SequenceVisit.Value; }
                        var secuencia = NegocioSecuencia.GetListAll().Where(p => p.Priority == sec).First();
                        item.Time = secuencia.ScheduleStart + " - " + secuencia.ScheduleEnd;

                    }
                }
               
            }
        

            var lt = (from a in visitas
                      join b in estatus on a.StatusOrderID equals b.PK_StatusOrderID
                      join c in ordenes on a.OrderID equals c.PK_OrderID
                      select new AddressV
                      {
                          StatusOrderID = a.StatusVisitID.Value,
                          LatitudeStartVisit = a.LatitudeStartVisit,
                          LogitudeStartVisit = a.LogitudeStartVisit,
                          LatitudeEndVisit = a.LatitudeEndVisit,
                          LogitudeEndVisit = a.LogitudeEndVisit,
                          LatitudeAddress = a.LatitudeAddress == null ? 0 : a.LatitudeAddress,
                          LogitudeAddress = a.LogitudeAddress == null ? 0 : a.LogitudeAddress,
                          Status = b.StatusOrder1,
                          OrderID = c.OrderID,
                          StarTime = a.StartVisitDate.ToString(),
                          EndTime = a.EndVisitDate.ToString(),
                          Secuence = a.SequenceVisit.HasValue ? a.SequenceVisit.Value : 0,
                          Rango = a.Time,
                      }).ToList();

            return lt.OrderBy(p => p.Secuence).ToList();
        }

        public double GetAverageVisitSem (List<int> empleado, DateTime OrderExecuteDate)
        {
            double Promedio = 0;
            var OrdenesJornada = 
                GetAllListODSJourney(empleado, OrderExecuteDate.AddDays(-7).Date, OrderExecuteDate.AddDays(-1).Date, false);
            var visitasJornada = new BusinessMonitor().GeListByOrderAverage(OrdenesJornada.Select(p => p.PK_OrderID).ToList());
            Promedio =  visitasJornada.Sum(s => s.DurationVisit.Value.TotalMinutes) / visitasJornada.Count();
            return Promedio;
        }

        public double GetAverageTransportSem(List<int> empleado, DateTime OrderExecuteDate)
        {
            double Promedio = 0;
            List<double> PromedioSemanal = new List<double>();
            List<int> PromedioDiario = new List<int>();
            double cie = 0;
            double ini = 0;
            int res = 0;
            for (int i = 1; i < 8; i++)
            {
                var OrdenesJornada = GetAll(empleado, OrderExecuteDate.AddDays(-i).Date, false);
                var visitasJornada = new BusinessMonitor().GeListByOrderAverage(OrdenesJornada.Select(p => p.PK_OrderID).ToList());
                foreach(var item in visitasJornada.OrderBy(p=> p.StartVisitDate))
                {
                    TimeSpan inicio = item.StartVisitDate.Value.TimeOfDay;
                    ini = inicio.TotalMinutes;
                    if (item.SequenceVisit != 1)
                    {
                        res = (int)(ini - cie);
                        PromedioDiario.Add(res);
                    }
                    TimeSpan cierre = item.EndVisitDate.Value.TimeOfDay;
                    cie = cierre.TotalMinutes;
                }
                if (PromedioDiario.Count() > 0)
                { PromedioSemanal.Add(PromedioDiario.Sum() / PromedioDiario.Count()); }
                PromedioDiario.Clear();
            }
            if (PromedioSemanal.Count() > 0)
            { Promedio = PromedioSemanal.Sum() / PromedioSemanal.Count(); }
            return Promedio;
        }

        public double GetAverageTransportDir(List<int> empleado, DateTime OrderExecuteDate)
        {
            double Promedio = 0;
            List<int> PromedioDiario = new List<int>();
            double cie = 0;
            double ini = 0;
            int res = 0;
                var OrdenesJornada = GetAll(empleado, OrderExecuteDate.Date, false);
                var visitasJornada = new BusinessMonitor().GeListByOrderAverage(OrdenesJornada.Select(p => p.PK_OrderID).ToList());
                foreach (var item in visitasJornada.OrderBy(p => p.StartVisitDate))
                {
                    TimeSpan inicio = item.StartVisitDate.Value.TimeOfDay;
                    ini = inicio.TotalMinutes;
                    if (item.SequenceVisit != 1)
                    {
                        res = (int)(ini - cie);
                        PromedioDiario.Add(res);
                    }
                    TimeSpan cierre = item.EndVisitDate.Value.TimeOfDay;
                    cie = cierre.TotalMinutes;
                }
                if (PromedioDiario.Count() > 0)
                { Promedio = PromedioDiario.Sum() / PromedioDiario.Count(); }
            return Promedio;

        }
        // public void SetKilometerExtra(int OrderID)
        //{

        //    var dataMonitor = new BusinessMonitor().GetByOrderID(OrderID);
        //    var dataODS = new BusinessOrder().GetByID(dataMonitor.OrderID);
        //    var dataModule = new BusinessModuleService().GetAllBYModule(dataODS.FK_ModuleID.Value);
                

        //    double latStar = System.Convert.ToDouble(dataMonitor.LatitudeStartVisit);
        //    double longstat = System.Convert.ToDouble(dataMonitor.LogitudeStartVisit);
        //    double Modulelat = System.Convert.ToDouble(dataModule.LatWorkshop);
        //    double Modulelong = System.Convert.ToDouble(dataModule.LongWorkshop);

        //    if (dataMonitor.StatusVisitID == 4)
        //    {
        //        var Checkincoord = new GeoCoordinate(latStar, longstat);
        //        var ModuleCoord = new GeoCoordinate(Modulelat, Modulelong);
        //        var Distance = (ModuleCoord.GetDistanceTo(Checkincoord) / 1000).ToString("0.000");
        //        decimal Kilometre = System.Convert.ToDecimal(Distance);

        //        if (Kilometre >= 30)
        //        {
        //            decimal ExtraKilometre = Kilometre - 30;

        //        }




        //    }


        //    return ;

        //}
        public  bool SetUser(int OrderID, int EmployeeID)
        {
            bool result = true;
            try
            {
               var userorigin= new BusinessEmployee().GetID(EmployeeID);
                int useragrega = userorigin.FK_UserID.Value;

                var ods = new BusinessOrder().GetByID(OrderID);
                var userdestiny = new BusinessEmployee().GetID(ods.FK_EmployeeID.Value);
                int userquita = userdestiny.FK_UserID.Value;
                if (ods == null) throw new Exception("No existe order ID " + OrderID.ToString());

                
                List<int> userChange = new List<int>();

                userChange.Add(userquita); //usuario quita ods.FK_EmployeeID
                userChange.Add(useragrega); // usuarioa agrega EmployeeID

                ods.FK_EmployeeID = EmployeeID;
                ods.ModifyDate = DateTime.UtcNow;

                new BusinessOrder().Update(ods);
                          

                // quita
                new BusinessNotification().Insert(new ModelViewNotification() {UserID= userChange[0], Title = "Reasignación de ODS", Message = "Algunas Ordenes de Servicio se  han reasignado a otro usuario. Por favor sincroniza.", Url="", Status = true });

                // agrega
                new BusinessNotification().Insert(new ModelViewNotification() { UserID = userChange[1], Title= "Reasignación de ODS", Message = "Se te han asignado nuevas Ordenes de Servicio por Reasignación. Por favor sincroniza.", Url = "", Status = true });


                new BusinessNotification().SendPush(userChange, "se va ha reasignar una ods", "la orden de servicio se reasigno");
            }
            catch(Exception ex)
            {
                // log ex
                result = false;
            }
            return result;
        }

        public string SetQuotation2(ModelViewUserG objCred, ModelViewBilling obj,DateTime date)
        {
            var objRepository = new RepositoryOrder();
            var objAlerta = new BusinessNotification();
            var NegocioUsuario = new BusinessUsers();
            var NegocioCliente = new BusinessClient();
            var NegocioBase = new BusinessInstalledBase();
            var NegocioProducto = new BusinessProduct();
            var NegocioFallas = new BusinessCodeFailure();
            var NegocioBOM = new BusinessBuildOfMaterial();
            var NegocioCotizacion = new BusinessQuotation();
            var NegocioCotizacionDetalle = new BusinessQuotationDetail();
            var NegocioCFP = new BusinessCodeFailureByProduct();
            var NegocioCF = new BusinessCodeFailure();
            var NegocioLugarCompra = new BusinessShopPlace();
            var NegocioEmpleado = new BusinessEmployee();
            var NegocioMonitor = new BusinessVisit();
            var NegocioHistorico = new BusinessHistory();
            var NegocioGarantia = new BusinessGuaranty();
            var dataUsuario = NegocioUsuario.GetUserByToken(objCred.TokenUser);
            if (objCred.TokenApp != GlobalConfiguration.TokenWEB)
                if (objCred.TokenApp != GlobalConfiguration.TokenMobile)
                    throw new Exception("TokenInvalid");
            if (dataUsuario == null) throw new Exception("UserPasswordInvalid");

            if (obj.PreOrden == 1)
            {
                bool enviado;

                var orden = GetByOrderID(obj.OrderID);
                var cliente = NegocioCliente.GetByID(orden.FK_ClientID);
                var empleado = NegocioEmpleado.GetByUserID(dataUsuario.UserID);
                //DateTime? fecha = null;
                //fecha = (String.IsNullOrEmpty(obj.ShopDate) ?
                //    null : (DateTime?)Convert.ToDateTime(obj.ShopDate));
                //Insertar Nueva BaseInstalada
                var baseIns = new EntityInstalledBase();
                var baseInsHis = NegocioBase.GetAll().Where(p => p.FK_ClientID == cliente.PK_ClientID && p.FK_ProductID == obj.FK_ProductID).FirstOrDefault();
                if (baseInsHis == null)
                {
                    string BsIns = "I-" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                    baseIns = NegocioBase.Insert(cliente.PK_ClientID, obj.FK_ProductID.Value, obj.FK_ShopPlaceID.Value, BsIns, obj.SerialNumber, obj.ShopDate, null, null);
                }
                else
                {
                    baseIns = baseInsHis;
                }
                //
                var lugarcompra = NegocioLugarCompra.GetByShopPlaceID(obj.FK_ShopPlaceID.Value);
                var pro = NegocioProducto.GetByID(baseIns.FK_ProductID.Value);
                SpreadsheetInfo.SetLicense("EQU2-3K5L-UZDC-SDYN");
                string OriginPre = GlobalConfiguration.MabeAttachmentsLocal + "PreODS.xlsx";
                ExcelFile pre = ExcelFile.Load(OriginPre);
                ExcelWorksheet wspre = pre.Worksheets[0];
                var lspres = GetByOrderID(obj.ODS);
                if (lspres != null)
                {
                    if (lspres.OrderID == obj.ODS)
                    {

                        enviado = true;
                    }
                    else
                    {
                        enviado = false;
                    }
                }
                else
                {
                    enviado = false;
                }
                string OrderID = obj.ODS;
                //Excel PreODS
                wspre.Cells["L7"].Value = OrderID;
                wspre.Cells["L85"].Value = OrderID;
                wspre.Cells["L9"].Value = cliente.ClientID;
                wspre.Cells["L11"].Value = cliente.FirstName.ToUpper() + " " + cliente.LastName.ToUpper();
                wspre.Cells["L13"].Value = pro.Model + " / " + pro.ProductName.ToUpper();
                wspre.Cells["U7"].Value = DateTime.Today.ToString("dd-MM-yyyy");
                wspre.Cells["U9"].Value = lugarcompra.ShopPlaceID + " - " + lugarcompra.ShopPlace1;
                wspre.Cells["U10"].Value = baseIns.ShopDate.Value.ToString("dd-MM-yyyy");
                wspre.Cells["U11"].Value = NegocioGarantia.GetByGuarantyID(obj.ServiceType).Guaranty1;
                wspre.Cells["U13"].Value = baseIns.SerialNumber;
                var NewOrder = new EntityOrder();
                int failPre = 15;
                int countFaults = 1;
                foreach (var jko in obj.Failures)
                {
                    var falla = NegocioCF.GetByID(jko.FailureID);
                    wspre.Cells["L" + failPre.ToString()].Value = falla.Failure;
                    switch (countFaults)
                    {
                        case 1:
                            NewOrder.Failure1 = falla.CodeFailure1;
                            break;
                        case 2:
                            NewOrder.Failure2 = falla.CodeFailure1;
                            break;
                        case 3:
                            NewOrder.Failure3 = falla.CodeFailure1;
                            break;
                        case 4:
                            NewOrder.Failure4 = falla.CodeFailure1;
                            break;
                        case 5:
                            NewOrder.Failure5 = falla.CodeFailure1;
                            break;
                    }
                    countFaults++;
                    failPre++;
                }

                int canPre = 27;
                foreach (var jko in obj.BillingDetails)
                {
                    if (canPre == 69)
                    {
                        canPre = 91;
                    }
                    var bom = NegocioBOM.GetByID(jko.ProductID);

                    if (bom != null)
                    {
                        wspre.Cells["T" + canPre.ToString()].Value = bom.SparePartsID;
                        wspre.Cells["L" + canPre.ToString()].Value = bom.SparePartDescription;
                    }
                    else
                    {
                        wspre.Cells["T" + canPre.ToString()].Value = jko.RefManID;
                        wspre.Cells["L" + canPre.ToString()].Value = jko.SparePartsDescription;
                    }
                    wspre.Cells["J" + canPre.ToString()].Value = jko.Quantity;
                    wspre.Cells["V" + canPre.ToString()].Value = "E0001";
                    canPre = canPre + 3;
                }

                string filePre = "PreODS_" + OrderID + ".pdf";
                string PreODS = "PreODS_" + DateTime.Today.ToString("yyyyMMdd");
                string DestinyPre = GlobalConfiguration.MabeAttachmentsLocal + PreODS + "/" + filePre;
                string PreOrdendes = new DirectoryInfo(GlobalConfiguration.MabeAttachmentsLocal).ToString() + PreODS;
                if (!(Directory.Exists(PreOrdendes)))
                {
                    Directory.CreateDirectory(PreOrdendes);
                }

                if (obj.BillingDetails.Count < 15)
                { wspre.NamedRanges.SetPrintArea(wspre.Cells.GetSubrange("J1", "W81")); }
                pre.Save(DestinyPre);
                string URLPre = GlobalConfiguration.urlRequest + "Content/Attachments/" + PreODS + "/" + filePre;
                // Termina Excel PreODS

                var NuevaOrden = new EntityOrder();
                if (enviado == false)
                {
                    NewOrder.FK_InstalledBaseID = baseIns.PK_InstalledBaseID;
                    NewOrder.FK_ClientID = cliente.PK_ClientID;
                    NewOrder.FK_EmployeeID = orden.FK_EmployeeID.Value;
                    NewOrder.FK_ModuleID = orden.FK_ModuleID.Value;
                    NewOrder.OrderID = OrderID;
                    NewOrder.TechnicalID = orden.TechnicalID;
                    NewOrder.URLPreOrder = URLPre;
                    NewOrder.FK_GuarantyID = NegocioGarantia.GetByGuarantyID(obj.ServiceType).PK_GuarantyID;
                    //Insertar PreOrden
                    NuevaOrden = InsertPre(NewOrder);


                    //Insertar Monitor
                    var OrdenVisita = NegocioMonitor.GetByOrderID(orden.PK_OrderID);
                    NegocioMonitor.Insert(NuevaOrden.PK_OrderID, 1, 1, OrdenVisita.LatitudeAddress, OrdenVisita.LogitudeAddress);

                    lspres = GetByOrderID(obj.ODS);
                }
                else { NuevaOrden = lspres; }

                //Insertar Historico
                //string serviceID = obj.ServiceType == "Fuera de Garantía" ? "0070" : "0010";
                //NegocioHistorico.Insert(baseIns.PK_InstalledBaseID, cliente.PK_ClientID, NuevaOrden.PK_OrderID, NuevaOrden.OrderID, baseIns.ShopDate.ToString(), serviceID, "");
                ////
                var cotizacionPreODS = NegocioCotizacion.GetByOrder(lspres.PK_OrderID);
                var ordenes2 = GetByEmployee(orden.FK_EmployeeID.Value, date);
                //var ordenes = GetAll().Where(p => empleado.Select(q => q.PK_EmployeeID).ToArray<int>().Contains(p.FK_EmployeeID.Value) && p.OrderExecuteDate >= DateTime.Now.Date).ToList();
                var coti = NegocioCotizacion.GetAll().Where(p => p.CreateDate.Value.Date == date).ToList();

                var ls = (from a in coti
                          join b in ordenes2 on a.FK_OrdenID equals b.PK_OrderID
                          select a).ToList();
                int tam_var = orden.TechnicalID.Length;
                string emple = orden.TechnicalID.Substring((tam_var - 4), 4);
                string consecutivo;
                int contador = 0;
                contador = ls.Count + 1;
                consecutivo = null;
                consecutivo = contador.ToString("00");
                string Folio = cotizacionPreODS == null ? emple + "-" + date.Day.ToString("00") + date.Month.ToString("00") + date.Year.ToString().Substring(2) + "-" + consecutivo : cotizacionPreODS.Folio;

                string Origin = GlobalConfiguration.MabeAttachmentsLocal + "FormatoCotización.xlsx";
                List<string> result = new List<string>();

                ExcelFile ef = ExcelFile.Load(Origin);
                ExcelWorksheet ws = ef.Worksheets[0];
                ws.Cells["L6"].Value = Folio;
                ws.Cells["L7"].Value = OrderID;
                ws.Cells["L9"].Value = cliente.ClientID;
                ws.Cells["L11"].Value = cliente.FirstName.ToUpper() + " " + cliente.LastName.ToUpper();
                ws.Cells["L13"].Value = pro.Model + "/" + pro.ProductName.ToUpper();
                ws.Cells["U13"].Value = DateTime.Today.ToString("dd-MM-yyyy");
                ws.Cells["L79"].Value = obj.ODS;
                ws.Cells["L80"].Value = OrderID;
                int fail = 21;

                foreach (var jko in obj.Failures)
                {
                    var falla = NegocioCF.GetByID(jko.FailureID);
                    ws.Cells["L" + fail.ToString()].Value = falla.Failure;
                    fail++;
                }
                int cantidad = 30;
                int cantidad1 = 31;

                foreach (var jko in obj.BillingDetails)
                {
                    if (cantidad == 58)
                    {
                        cantidad = 85;
                        cantidad1 = 86;
                    }
                    var bom = NegocioBOM.GetByID(jko.ProductID);
                    if (bom != null)
                    {
                        ws.Cells["L" + cantidad1.ToString()].Value = bom.SparePartsID;
                        ws.Cells["L" + cantidad.ToString()].Value = bom.SparePartDescription;
                    }
                    else
                    {
                        ws.Cells["L" + cantidad1.ToString()].Value = jko.RefManID;
                        ws.Cells["L" + cantidad.ToString()].Value = jko.SparePartsDescription;
                    }
                    ws.Cells["J" + cantidad.ToString()].Value = jko.Quantity;
                    ws.Cells["T" + cantidad.ToString()].Value = Convert.ToDouble(jko.Price);
                    ws.Cells["V" + cantidad.ToString()].Value = Convert.ToDouble(jko.Totals);
                    cantidad = cantidad + 3;
                    cantidad1 = cantidad1 + 3;


                    var dataSpareParts = new BusinessSparePart().GetByRefManID(NuevaOrden.PK_OrderID, jko.RefManID);
                    if (dataSpareParts != null)
                    {
                        dataSpareParts.Quantity = jko.Quantity;
                        dataSpareParts.Price = float.Parse(jko.Price);
                        dataSpareParts.SparePartStatus = "E0001";
                        new BusinessSparePart().Update(dataSpareParts);
                    }
                    else
                    {
                        Int64 x = 0;
                        bool rest = Int64.TryParse(jko.RefManID, out x);
                        if (!rest)
                        {
                            var bomsparepart = new BusinessBuildOfMaterial().GetMaterialBySparePart(jko.RefManID, baseIns.FK_ProductID.Value);
                            new BusinessSparePart().Insert(NuevaOrden.PK_OrderID, bomsparepart.PK_BuildOfMaterialsID, bomsparepart.FK_ProductID, null, jko.Quantity, "0", "E0001", "ZMX00003", jko.SparePartsDescription, jko.RefManID, float.Parse(jko.Price.ToString()));
                        }
                        else
                        {
                            var mano = new BusinessWorkforce().GetAll().Where(p => p.WorkforceID == jko.RefManID).First();
                            new BusinessSparePart().Insert(NuevaOrden.PK_OrderID, null, null, mano.PK_WorkforceID, jko.Quantity, "0", "E0001", "ZMX00003", jko.SparePartsDescription, jko.RefManID, float.Parse(jko.Price.ToString()));
                        }
                    }
                }
                double manodeobra = 0;
                if (obj.WorkForce != null)
                {
                    manodeobra = Convert.ToDouble(obj.WorkForce.Substring(1));
                }
                double subtotal = Convert.ToDouble(obj.SubTotal.Substring(1));
                double iva = Convert.ToDouble(obj.IVA.Substring(1));
                double total = Convert.ToDouble(obj.Total.Substring(1));
                double subtotalref = subtotal - manodeobra;
                string totalletras = enletras(total.ToString());
                ws.Cells["M15"].Value = subtotalref;
                ws.Cells["M16"].Value = manodeobra;
                ws.Cells["M17"].Value = subtotal;
                ws.Cells["M18"].Value = iva;
                ws.Cells["U16"].Value = total;
                ws.Cells["M19"].Value = totalletras;

                string file = "Cotización_" + obj.ODS + ".pdf";
                string quotion = "Cotizaciones_" + DateTime.Today.ToString("yyyyMMdd");
                string Destiny = GlobalConfiguration.MabeAttachmentsLocal + quotion + "/" + file;

                string Cotizaciones = new DirectoryInfo(GlobalConfiguration.MabeAttachmentsLocal).ToString() + quotion;
                if (!(Directory.Exists(Cotizaciones)))
                {
                    Directory.CreateDirectory(Cotizaciones);
                }
                // Set print area
                if (obj.BillingDetails.Count < 11)
                { ws.NamedRanges.SetPrintArea(ws.Cells.GetSubrange("J1", "W74")); }
                ef.Save(Destiny);
                if (obj.EMails != "")
                {
                    result = obj.EMails.Split(new char[] { ';' }).ToList();
                    string URL = GlobalConfiguration.urlRequest + "Content/Attachments/" + quotion + "/" + file;

                    if (cotizacionPreODS == null)
                    {
                        cotizacionPreODS = NegocioCotizacion.Insert2(NuevaOrden.PK_OrderID, subtotal.ToString(), iva.ToString(), total.ToString(), obj.ODS, URL, obj.EstimatedTipe, empleado[0].PK_EmployeeID,date);
                    }
                    else
                    {
                        cotizacionPreODS.Folio = Folio;
                        cotizacionPreODS.IVA = iva.ToString();
                        cotizacionPreODS.SubTotal = subtotal.ToString();
                        cotizacionPreODS.Total = total.ToString();
                        cotizacionPreODS.URL = URL;
                        cotizacionPreODS.ModifyDate = DateTime.UtcNow;
                        cotizacionPreODS.FK_EmployeeID = empleado[0].PK_EmployeeID;
                        NegocioCotizacion.Update(cotizacionPreODS);
                    }
                    foreach (var jko in obj.BillingDetails)
                    {
                        var newDetailquotion = NegocioCotizacionDetalle.GetDetail(cotizacionPreODS.PK_QuotationID, jko.ProductID);
                        if (newDetailquotion == null)
                        { NegocioCotizacionDetalle.Insert(cotizacionPreODS.PK_QuotationID, jko.ProductID); }
                    }

                    string sb = File.ReadAllText(GlobalConfiguration.LocateBodyMail + "NotificationCotizacion.txt");
                    string lugCom = baseIns.ShopDate != null ? baseIns.ShopDate.Value.ToString("yyyy-MM-dd") : "";
                    sb = sb.Replace("#%Nombre%#", cliente.FirstName + " " + cliente.LastName);
                    sb = sb.Replace("#%OrderID%#", NuevaOrden.OrderID);
                    sb = sb.Replace("#%Folio%#", Folio);
                    sb = sb.Replace("#%Modelo%#", pro.Model);
                    sb = sb.Replace("#%Descripcion%#", pro.ProductName.ToUpper());
                    sb = sb.Replace("#%Serie%#", baseIns.SerialNumber);
                    sb = sb.Replace("#%Fecha%#", lugCom);
                    sb = sb.Replace("#%Lugar%#", lugarcompra.ShopPlace1);
                    //objAlerta.SendMails(result, "Cotización ServiPlus", sb.ToString(), Destiny);
                    objAlerta.SendMailExchange(GlobalConfiguration.exchangeUser, GlobalConfiguration.exchangePwd, result, "Cotización ServiPlus", sb.ToString(), Destiny);
                }

                return NuevaOrden.OrderID;
               
            }
            else
            {

                var empleado = NegocioEmpleado.GetByUserID(dataUsuario.UserID);
                var orden = GetByOrderID(obj.ODS);
                var cliente = NegocioCliente.GetByID(orden.FK_ClientID);
                //var baseIns = NegocioBase.GetByID(orden.FK_InstalledBaseID);
                var lugarcompra = NegocioLugarCompra.GetByShopPlaceID(obj.FK_ShopPlaceID.Value);
                var pro = NegocioProducto.GetByID(obj.FK_ProductID.Value);
                var coti = NegocioCotizacion.GetAll().Where(p => p.CreateDate.Value.Date == date).ToList();
                var ordenes2 = GetByEmployee(orden.FK_EmployeeID.Value, date);
                //var ordenes = GetAll().Where(p => empleado.Select(q => q.PK_EmployeeID).ToArray<int>().Contains(p.FK_EmployeeID.Value) && p.OrderExecuteDate >= DateTime.Now.Date).ToList();
                var ls = (from a in coti
                          join b in ordenes2 on a.FK_OrdenID equals b.PK_OrderID
                          select a).ToList();
                int tam_var = orden.TechnicalID.Length;
                string emple = orden.TechnicalID.Substring((tam_var - 4), 4);
                string consecutivo;
                int contador = 0;
                contador = ls.Count + 1;
                consecutivo = contador.ToString("00");
                var cotizacion = NegocioCotizacion.GetByOrderType(orden.PK_OrderID, obj.EstimatedTipe);
                string Folio = "";
                Folio = cotizacion == null ? emple + "-" + date.Day.ToString("00") + date.Month.ToString("00") + date.Year.ToString().Substring(2) + "-" + consecutivo : cotizacion.Folio;
                SpreadsheetInfo.SetLicense("EQU2-3K5L-UZDC-SDYN");
                string Origin = GlobalConfiguration.MabeAttachmentsLocal + "FormatoCotización.xlsx";
                List<string> result = obj.EMails.Split(new char[] { ';' }).ToList();
                ExcelFile ef = ExcelFile.Load(Origin);
                ExcelWorksheet ws = ef.Worksheets[0];

                ws.Cells["L6"].Value = Folio;
                ws.Cells["L7"].Value = obj.ODS;
                ws.Cells["L9"].Value = cliente.ClientID;
                ws.Cells["L11"].Value = cliente.FirstName.ToUpper() + " " + cliente.LastName.ToUpper();
                ws.Cells["L13"].Value = pro.Model + " / " + pro.ProductName.ToUpper();
                ws.Cells["U13"].Value = DateTime.Today.ToString("dd-MM-yyyy");
                ws.Cells["L79"].Value = Folio;
                ws.Cells["L80"].Value = obj.ODS;
                int fail = 21;
                foreach (var jko in obj.Failures)
                {
                    var falla = NegocioCF.GetByID(jko.FailureID);
                    ws.Cells["L" + fail.ToString()].Value = falla.Failure;
                    fail++;
                }
                int cantidad = 30;
                int cantidad1 = 31;

                foreach (var jko in obj.BillingDetails)
                {
                    if (cantidad == 60)
                    {
                        cantidad = 85;
                        cantidad1 = 86;
                    }
                    var bom = NegocioBOM.GetByID(jko.ProductID);
                    if (bom != null)
                    {
                        ws.Cells["L" + cantidad1.ToString()].Value = bom.SparePartsID;
                        ws.Cells["L" + cantidad.ToString()].Value = bom.SparePartDescription;
                    }
                    else
                    {
                        if (jko.SparePartsDescription == "Kilometraje")
                        {
                            ws.Cells["L" + cantidad1.ToString()].Value = jko.RefManID;
                            ws.Cells["L" + cantidad.ToString()].Value = "Costo de servicio fuera de cobertura.";
                            ws.Cells["L66"].Value = "En caso de requerir factura esta se emitirá por parte del Centro de Servicio que lo atendió";
                        }
                        else
                        {
                            ws.Cells["L" + cantidad1.ToString()].Value = jko.RefManID;
                            ws.Cells["L" + cantidad.ToString()].Value = jko.SparePartsDescription;
                        }
                    }
                    ws.Cells["J" + cantidad.ToString()].Value = jko.Quantity;
                    ws.Cells["T" + cantidad.ToString()].Value = Convert.ToDouble(jko.Price);
                    ws.Cells["V" + cantidad.ToString()].Value = Convert.ToDouble(jko.Totals);
                    cantidad = cantidad + 3;
                    cantidad1 = cantidad1 + 3;
                }


                double manodeobra = 0;
                if (obj.WorkForce != null)
                {
                    manodeobra = Convert.ToDouble(obj.WorkForce.Substring(1));
                }
                double subtotal = Convert.ToDouble(obj.SubTotal.Substring(1));
                double iva = Convert.ToDouble(obj.IVA.Substring(1));
                double total = Convert.ToDouble(obj.Total.Substring(1));
                double subtotalref = subtotal - manodeobra;
                string totalletras = enletras(total.ToString());
                ws.Cells["M15"].Value = subtotalref;
                ws.Cells["M16"].Value = manodeobra;
                ws.Cells["M17"].Value = subtotal;
                ws.Cells["M18"].Value = iva;
                ws.Cells["U16"].Value = total;
                ws.Cells["M19"].Value = totalletras;
                string file = "Cotización_" + Folio + ".pdf";
                string quotion = "Cotizaciones_" + DateTime.Today.ToString("yyyyMMdd");
                string Destiny = GlobalConfiguration.MabeAttachmentsLocal + quotion + "/" + file;
                if (obj.BillingDetails.Count < 11)
                { ws.NamedRanges.SetPrintArea(ws.Cells.GetSubrange("J1", "W74")); }
                string Cotizaciones = new DirectoryInfo(GlobalConfiguration.MabeAttachmentsLocal).ToString() + quotion;
                if (!(Directory.Exists(Cotizaciones)))
                {
                    Directory.CreateDirectory(Cotizaciones);
                }
                ef.Save(Destiny);
                string URL = GlobalConfiguration.urlRequest + "Content/Attachments/" + quotion + "/" + file;

                if (obj.EMails != "")
                {
                    string sb = File.ReadAllText(GlobalConfiguration.LocateBodyMail + "NotificationCotizacion.txt");
                    string lugCom = obj.ShopDate != null ? obj.ShopDate : "";
                    sb = sb.Replace("#%Nombre%#", cliente.FirstName + " " + cliente.LastName);
                    sb = sb.Replace("#%OrderID%#", obj.ODS);
                    sb = sb.Replace("#%Folio%#", Folio);
                    sb = sb.Replace("#%Modelo%#", pro.Model);
                    sb = sb.Replace("#%Descripcion%#", pro.ProductName.ToUpper());
                    sb = sb.Replace("#%Serie%#", obj.SerialNumber);
                    sb = sb.Replace("#%Fecha%#", lugCom);
                    sb = sb.Replace("#%Lugar%#", lugarcompra.ShopPlace1);
                    //objAlerta.SendMails(result, "Cotización ServiPlus", sb.ToString(), Destiny);
                    objAlerta.SendMailExchange(GlobalConfiguration.exchangeUserCotiza, GlobalConfiguration.exchangePwdCotiza, result, "Cotización ServiPlus", sb.ToString(), Destiny);

                    if (cotizacion == null)
                    { cotizacion = NegocioCotizacion.Insert2(orden.PK_OrderID, subtotal.ToString(), iva.ToString(), total.ToString(), Folio, URL, obj.EstimatedTipe, empleado[0].PK_EmployeeID,date); }
                    else
                    {
                        cotizacion.Folio = Folio;
                        cotizacion.IVA = iva.ToString();
                        cotizacion.SubTotal = subtotal.ToString();
                        cotizacion.Total = total.ToString();
                        cotizacion.URL = URL;
                        cotizacion.ModifyDate = DateTime.UtcNow;
                        cotizacion.TypeQuotation = obj.EstimatedTipe;
                        cotizacion.FK_EmployeeID = empleado[0].PK_EmployeeID;
                        NegocioCotizacion.Update(cotizacion);
                    }

                }
                else
                {
                    if (cotizacion == null)
                    { cotizacion = NegocioCotizacion.Insert2(orden.PK_OrderID, subtotal.ToString(), iva.ToString(), total.ToString(), Folio, URL, obj.EstimatedTipe, empleado[0].PK_EmployeeID,date); }
                    else
                    {
                        cotizacion.Folio = Folio;
                        cotizacion.IVA = iva.ToString();
                        cotizacion.SubTotal = subtotal.ToString();
                        cotizacion.Total = total.ToString();
                        cotizacion.URL = URL;
                        cotizacion.ModifyDate = DateTime.UtcNow;
                        cotizacion.TypeQuotation = obj.EstimatedTipe;
                        cotizacion.FK_EmployeeID = empleado[0].PK_EmployeeID;
                        NegocioCotizacion.Update(cotizacion);
                    }
                    foreach (var jko in obj.BillingDetails)
                    {
                        var newDetailquotion = NegocioCotizacionDetalle.GetDetail(cotizacion.PK_QuotationID, jko.ProductID);
                        if (newDetailquotion == null)
                        { NegocioCotizacionDetalle.Insert(cotizacion.PK_QuotationID, jko.ProductID); }
                    }
                }

                return obj.ODS;
            }
        }

    }
}
