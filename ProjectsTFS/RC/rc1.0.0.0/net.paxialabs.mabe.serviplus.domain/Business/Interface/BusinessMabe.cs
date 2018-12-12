using net.paxialabs.mabe.serviplus.domain.Business.Operation;
using net.paxialabs.mabe.serviplus.domain.Business.Security;
using net.paxialabs.mabe.serviplus.domain.Business.Users;
using net.paxialabs.mabe.serviplus.entities.Entity.Operation;
using net.paxialabs.mabe.serviplus.entities.Entity.SAP;
using net.paxialabs.mabe.serviplus.entities.Entity.Security;
using net.paxialabs.mabe.serviplus.entities.ModelView.Operation;
using net.paxialabs.mabe.serviplus.entities.ModelView.Security;
using net.paxialabs.mabe.serviplus.security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.domain.Business.Interface
{
    internal class BusinessMabe
    {
        public bool ProcessExtraKM { get; set; }

        public BusinessMabe()
        {
            ProcessExtraKM = false;
        }

        public BusinessMabe(bool _ProcessExtraKM)
        {
            ProcessExtraKM = _ProcessExtraKM;
        }

        void wait(int x)
        {
            DateTime time = DateTime.Now;
            DateTime timefinal = DateTime.Now.AddSeconds(x);

            while (time < timefinal)
            {
                time = DateTime.Now;
            }
        }

        public void UpdateODS(List<srUpdateODS.DT_ActulizaODS_OutItem> items, string OrderID)
        {
            string sleep = "-------------------------INICIO_SleepSendUpdateODS / OrderID: " + OrderID + "-------------------------" + Environment.NewLine;
            new BusinessLogger().WriteEntry(sleep);
            //Thread.Sleep(60000);
            try
            {
                wait(50);
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                new BusinessLogger().WriteEntry(msg);
            }
            string wakeup = "-------------------------FIN_SleepSendUpdateODS / OrderID: " + OrderID + "-------------------------" + Environment.NewLine;
            new BusinessLogger().WriteEntry(wakeup);

            var NegocioOrder = new BusinessOrder();
            var NegocioLog = new BusinessLogCRM();
            srUpdateODS.DT_ActulizaODS_Out MT_ActulizaODS_Out = new srUpdateODS.DT_ActulizaODS_Out();
            MT_ActulizaODS_Out.Destino = new srUpdateODS.DT_ActulizaODS_OutDestino() { Idioma = "ES", Mandante = "700" };
            MT_ActulizaODS_Out.SessionReference = new srUpdateODS.DT_ActulizaODS_OutSessionReference() { Satelite = "MOBILE", Value = "" };

            MT_ActulizaODS_Out.Item = items.ToArray();

            srUpdateODS.SI_ActualizaODS_OSClient proxy = new srUpdateODS.SI_ActualizaODS_OSClient();

            var eab = new System.ServiceModel.EndpointAddressBuilder(new System.ServiceModel.EndpointAddress(GlobalConfiguration.endPointUpdateODS));

            proxy.ClientCredentials.UserName.UserName = GlobalConfiguration.endPointUser;
            proxy.ClientCredentials.UserName.Password = GlobalConfiguration.endPointPwd;

            proxy.Endpoint.Address = eab.ToEndpointAddress();
            string UpdateODS = null;
            try
            {
                srUpdateODS.DT_ActualizaODS_InItemRETURN[][] result = proxy.SI_ActualizaODS_OS(MT_ActulizaODS_Out);
                foreach (var itemHeader in result)
                {
                    foreach (var item in itemHeader)
                    {
                        string msg = "";
                        UpdateODS += string.Format("DESCRIPCION: {0}", item.DESCRIPCION) + Environment.NewLine;
                        msg += "-------------------------INICIO_UpdateODS "  + OrderID + "-------------------------" + Environment.NewLine;
                        msg += string.Format("TIPO_MENSAJE: {0}", item.TIPO_MENSAJE) + Environment.NewLine;
                        msg += string.Format("CLASE_MENSAJE: {0}", item.CLASE_MENSAJE) + Environment.NewLine;
                        msg += string.Format("DESCRIPCION: {0}", item.DESCRIPCION) + Environment.NewLine;
                        msg += string.Format("MENSAJE: {0}", item.MENSAJE) + Environment.NewLine;
                        msg += "-------------------------FIN_UpdateODS " + OrderID + "-------------------------" + Environment.NewLine;

                        if (item.TIPO_MENSAJE != "I" && item.TIPO_MENSAJE != "E")
                        {
                            if (item.MENSAJE.Contains("005"))
                            {
                                msg += "------- Actualizar EnvioCRM " + OrderID + "-----" + Environment.NewLine;
                                var order = NegocioOrder.GetByOrderID(OrderID);
                                if (item.DESCRIPCION.Contains("Campo Estatus Actualizado"))
                                {
                                    msg += "Actualizo: " + "OK" + Environment.NewLine;
                                    order.SendCRM = "OK";
                                    NegocioOrder.Update(order);
                                    msg += "OrderID: " + OrderID + Environment.NewLine;
                                }
                                msg += "-------------------------FIN Actualizar EnvioCRM " + OrderID + "-------------------------" + Environment.NewLine;
                            }

                            if (item.MENSAJE.Contains("007"))
                            {
                                msg += "------- Actualizar EnvioCRM " + OrderID + " -----" + Environment.NewLine;
                                var order = NegocioOrder.GetByOrderID(OrderID);
                                if (item.DESCRIPCION.Contains("Error al Modificar Estatus"))
                                {
                                    msg += "Actualizo: " + "Error" + Environment.NewLine;
                                    order.SendCRM = "Error";
                                    NegocioOrder.Update(order);
                                    msg += "OrderID: " + OrderID + Environment.NewLine;
                                }
                                msg += "-------------------------FIN Actualizar EnvioCRM  " + OrderID  + " -------------------------" + Environment.NewLine;
                            }
                        }
                        new BusinessLogger().WriteEntry(msg);
                    }
                }

            }
            catch (Exception ex)
            {
                var order = NegocioOrder.GetByOrderID(OrderID);
                order.SendCRM = "Error";
                NegocioOrder.Update(order);

                new BusinessLogger().WriteError(ex, "DT_ActualizaODS_InItemRETURN ODS " + OrderID);
            }

            try
            {
                var order = NegocioOrder.GetByOrderID(OrderID);
                var Log = NegocioLog.GetByOrderID(order.PK_OrderID);
                if (Log == null)
                {
                    EntityLogCRM LogCRM = new EntityLogCRM();
                    LogCRM.LogID = 0;
                    LogCRM.OrderID = order.PK_OrderID;
                    LogCRM.UpdateBase = null;
                    LogCRM.UpdateRefMan = null;
                    LogCRM.UpdateODS = UpdateODS;
                    LogCRM.ModuleReserveSP = null;
                    LogCRM.ADRReserveSP = null;
                    LogCRM.Status = true;
                    LogCRM.CreateDate = DateTime.UtcNow.ToLocalTime();
                    LogCRM.ModifyDate = DateTime.UtcNow.ToLocalTime();
                    new BusinessLogCRM().Insert(LogCRM);
                }
                else
                {
                    Log.UpdateODS = UpdateODS;
                    Log.ModifyDate = DateTime.UtcNow.ToLocalTime();
                    NegocioLog.Update(Log);
                }
            }
            catch (Exception ex)
            {
                new BusinessLogger().WriteError(ex, "UpdateODS LogCRM ODS " + OrderID);
            }
        }

        public void UpdateBase(List<srUpdateBase.DT_ActualizaIB_ReqIT_IB_CHANGEItem> items, int OrderID)
        {
            var NegocioOrder = new BusinessOrder();
            var NegocioLog = new BusinessLogCRM();
            srUpdateBase.DT_ActualizaIB_Req MT_ActualizaIB_Req = new srUpdateBase.DT_ActualizaIB_Req();
            MT_ActualizaIB_Req.DestinoSistema = new srUpdateBase.DT_ActualizaIB_ReqDestinoSistema() { idioma = "ES", Mandante = "700" };
            MT_ActualizaIB_Req.SessionReference = new srUpdateBase.DT_ActualizaIB_ReqSessionReference() { Satelite = "MOBILE" };

            MT_ActualizaIB_Req.IT_IB_CHANGE = items.ToArray();

            srUpdateBase.SI_ActualizaIB_OSClient proxy = new srUpdateBase.SI_ActualizaIB_OSClient();

            var eab = new System.ServiceModel.EndpointAddressBuilder(new System.ServiceModel.EndpointAddress(GlobalConfiguration.endPointUpdateBase));

            proxy.ClientCredentials.UserName.UserName = GlobalConfiguration.endPointUser;
            proxy.ClientCredentials.UserName.Password = GlobalConfiguration.endPointPwd;

            proxy.Endpoint.Address = eab.ToEndpointAddress();

            srUpdateBase.DT_ActualizaIB_Res result = proxy.SI_ActualizaIB_OS(MT_ActualizaIB_Req);
            string UpdateBase = null;
            if (result.ET_IB_RETURN != null)
            {
                foreach (var item in result.ET_IB_RETURN)
                {
                    string msg = "";
                    UpdateBase = item.MESSAGE;
                    msg += "-------------------------INICIO_UpdateBaseReturn " + OrderID + "-------------------------" + Environment.NewLine;
                    msg += string.Format("ID: {0}", item.ID) + Environment.NewLine;
                    msg += string.Format("FIELD: {0}", item.FIELD) + Environment.NewLine;
                    msg += string.Format("LOG_MSG_NO: {0}", item.LOG_MSG_NO) + Environment.NewLine;
                    msg += string.Format("LOG_NO: {0}", item.LOG_NO) + Environment.NewLine;
                    msg += string.Format("MESSAGE: {0}", item.MESSAGE) + Environment.NewLine;
                    msg += string.Format("MESSAGE_V1: {0}", item.MESSAGE_V1) + Environment.NewLine;
                    msg += string.Format("MESSAGE_V2: {0}", item.MESSAGE_V2) + Environment.NewLine;
                    msg += string.Format("MESSAGE_V3: {0}", item.MESSAGE_V3) + Environment.NewLine;
                    msg += string.Format("MESSAGE_V4: {0}", item.MESSAGE_V4) + Environment.NewLine;
                    msg += string.Format("NUMBER: {0}", item.NUMBER) + Environment.NewLine;
                    msg += string.Format("PARAMETER: {0}", item.PARAMETER) + Environment.NewLine;
                    msg += string.Format("ROW: {0}", item.ROW) + Environment.NewLine;
                    msg += string.Format("SYSTEM: {0}", item.SYSTEM) + Environment.NewLine;
                    msg += string.Format("TYPE: {0}", item.TYPE) + Environment.NewLine;
                    msg += "-------------------------FIN_UpdateBaseReturn " + OrderID + "-------------------------" + Environment.NewLine;
                    new BusinessLogger().WriteEntry(msg);
                }
            }
            if (result.ET_IB_CHANGE != null)
            {
                foreach (var item in result.ET_IB_CHANGE)
                {
                    string msg = "";

                    msg += "-------------------------INICIO_UpdateBaseChange " + OrderID + "-------------------------" + Environment.NewLine;
                    msg += string.Format("BRAND: {0}", item.BRAND) + Environment.NewLine;
                    msg += string.Format("PRODUCT_ID: {0}", item.PRODUCT_ID) + Environment.NewLine;
                    msg += string.Format("PURCHASE_DATE: {0}", item.PURCHASE_DATE) + Environment.NewLine;
                    msg += string.Format("PURCHASE_LOC: {0}", item.PURCHASE_LOC) + Environment.NewLine;
                    msg += string.Format("REGISTRY: {0}", item.REGISTRY) + Environment.NewLine;
                    msg += string.Format("SERIAL_NUMBER: {0}", item.SERIAL_NUMBER) + Environment.NewLine;
                    msg += string.Format("WARRANTY_FROM: {0}", item.WARRANTY_FROM) + Environment.NewLine;
                    msg += string.Format("WARRANTY_TO: {0}", item.WARRANTY_TO) + Environment.NewLine;
                    msg += "-------------------------FIN_UpdateBaseChange " + OrderID + "-------------------------" + Environment.NewLine;
                    new BusinessLogger().WriteEntry(msg);
                }
            }
            try
            {
                var Log = NegocioLog.GetByOrderID(OrderID);
                if (Log == null)
                {
                    EntityLogCRM LogCRM = new EntityLogCRM();
                    LogCRM.LogID = 0;
                    LogCRM.OrderID = OrderID;
                    LogCRM.UpdateBase = UpdateBase;
                    LogCRM.UpdateRefMan = null;
                    LogCRM.UpdateODS = null;
                    LogCRM.ModuleReserveSP = null;
                    LogCRM.ADRReserveSP = null;
                    LogCRM.Status = true;
                    LogCRM.CreateDate = DateTime.UtcNow.ToLocalTime();
                    LogCRM.ModifyDate = DateTime.UtcNow.ToLocalTime();
                    new BusinessLogCRM().Insert(LogCRM);
                }
                else
                {
                    Log.UpdateBase = UpdateBase;
                    Log.ModifyDate = DateTime.UtcNow.ToLocalTime();
                    NegocioLog.Update(Log);
                }
            }
            catch (Exception ex)
            {
                new BusinessLogger().WriteError(ex, "UpdateBase Log " + OrderID);
            }
        }

        public bool UpdateRefMan(List<srUpdateRefMan.DT_ActRefMan_OutItem> items)
        {
            bool Send = true;
            var NegocioOrder = new BusinessOrder();
            var NegocioLog = new BusinessLogCRM();
            srUpdateRefMan.DT_ActRefMan_Out MT_ActRefMan_Out = new srUpdateRefMan.DT_ActRefMan_Out();
            MT_ActRefMan_Out.DestinoSistema = new srUpdateRefMan.DT_ActRefMan_OutDestinoSistema() { Idioma = "ES", Mandante = "700", Value = "" };
            MT_ActRefMan_Out.SessionReference = new srUpdateRefMan.DT_ActRefMan_OutSessionReference() { Satelite = "MOBILE", Value = "" };

            MT_ActRefMan_Out.Item = items.ToArray();

            srUpdateRefMan.SI_ActRefMan_OSClient proxy = new srUpdateRefMan.SI_ActRefMan_OSClient();

            var eab = new System.ServiceModel.EndpointAddressBuilder(new System.ServiceModel.EndpointAddress(GlobalConfiguration.endPointUpdateRefMan));

            proxy.ClientCredentials.UserName.UserName = GlobalConfiguration.endPointUser;
            proxy.ClientCredentials.UserName.Password = GlobalConfiguration.endPointPwd;

            proxy.Endpoint.Address = eab.ToEndpointAddress();

            srUpdateRefMan.DT_ActRefMan_InItem[] result = proxy.SI_ActRefMan_OS(MT_ActRefMan_Out);
            string ODS = "";
            string UpdateRefMan = null;
            foreach (var item in result)
            {
                string msg = "";
                msg += "-------------------------INICIO_UpdateRefMan-------------------------" + Environment.NewLine;
                msg += string.Format("IDOrden: {0}", item.IDOrden) + Environment.NewLine;
                msg += string.Format("MensajeCRM: {0}", item.MensajeCRM) + Environment.NewLine;

                UpdateRefMan += string.Format("MensajeCRM: {0}", item.MensajeCRM) + Environment.NewLine;
                UpdateRefMan += string.Format("IDOrden: {0}", item.IDOrden) + Environment.NewLine;

                if (item.MensajeCRM.Contains("correctamente"))
                {
                    try
                    {
                        string posicion = item.IDOrden.Split('|')[0].Split(':')[1].Trim();
                        string RefManID = item.IDOrden.Split('|')[1].Split(':')[1].Trim();

                        ODS = item.MensajeCRM.Substring(item.MensajeCRM.IndexOf("orden") + 5).Trim();

                        try
                        {
                            var NegocioRefaccion = new BusinessSparePart();
                            var order = new BusinessOrder().GetByOrderID(ODS);
                            foreach (var sparepart in NegocioRefaccion.GetListByRefManID(order.PK_OrderID, RefManID))
                            {
                                if (sparepart.PosicionItem == "" || sparepart.PosicionItem == posicion)
                                {
                                    sparepart.PosicionItem = posicion;
                                    NegocioRefaccion.Update(sparepart);
                                    break;
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            msg += "------- Actualizar Refaccion -----" + Environment.NewLine;
                            msg += "ODS" + ODS + Environment.NewLine;
                            msg += "Error: " + ex.Message + Environment.NewLine;
                            msg += "ST: " + ex.StackTrace + Environment.NewLine;
                            msg += "Data: " + item.IDOrden + Environment.NewLine;
                            msg += "CRM: " + item.MensajeCRM + Environment.NewLine;
                        }
                    }
                    catch (Exception ex)
                    {
                        msg += "------- Transformación para actualizar refaccion -----" + Environment.NewLine;
                        msg += "Error: " + ex.Message + Environment.NewLine;
                        msg += "ST: " + ex.StackTrace + Environment.NewLine;
                        msg += "Data: " + item.IDOrden + Environment.NewLine;
                        msg += "CRM: " + item.MensajeCRM + Environment.NewLine;
                    }
                }

                if (item.MensajeCRM.Contains("No se pudo actualizar"))
                {
                    try
                    {
                        string buscar = "orden";
                        int Index = item.MensajeCRM.IndexOf(buscar);
                        ODS = item.MensajeCRM.Substring(Index + 5).Trim();
                        try
                        {
                            msg += "------- Actualizar EnvioCRM -----" + Environment.NewLine;
                            msg += "Actualizo: " + "Error" + Environment.NewLine;
                            Send = false;
                            msg += "OrderID: " + ODS + Environment.NewLine;
                            msg += "-------------------------FIN Actualizar EnvioCRM-------------------------" + Environment.NewLine;
                        }
                        catch (Exception ex)
                        {
                            msg += "------- Actualizar Refaccion -----";
                            msg += "Error: " + ex.Message + Environment.NewLine;
                            msg += "ST: " + ex.StackTrace + Environment.NewLine;
                            msg += "Data: " + item.IDOrden;
                        }
                    }
                    catch (Exception ex)
                    {
                        msg += "------- Transformación para actualizar refaccion -----";
                        msg += "Error: " + ex.Message + Environment.NewLine;
                        msg += "ST: " + ex.StackTrace + Environment.NewLine;
                        msg += "Data: " + item.IDOrden;
                    }
                }

                msg += string.Format("ActualizaODS: {0}", Send) + Environment.NewLine;
                msg += "-------------------------FIN_UpdateRefMan-------------------------" + Environment.NewLine;

                new BusinessLogger().WriteEntry(msg);
            }
            try
            {
                var order = NegocioOrder.GetByOrderID(ODS);
                var Log = NegocioLog.GetByOrderID(order.PK_OrderID);
                if (Log == null)
                {
                    EntityLogCRM LogCRM = new EntityLogCRM();
                    LogCRM.LogID = 0;
                    LogCRM.OrderID = order.PK_OrderID;
                    LogCRM.UpdateBase = null;
                    LogCRM.UpdateRefMan = UpdateRefMan;
                    LogCRM.UpdateODS = null;
                    LogCRM.ModuleReserveSP = null;
                    LogCRM.ADRReserveSP = null;
                    LogCRM.Status = true;
                    LogCRM.CreateDate = DateTime.UtcNow.ToLocalTime();
                    LogCRM.ModifyDate = DateTime.UtcNow.ToLocalTime();
                    new BusinessLogCRM().Insert(LogCRM);
                }
                else
                {

                    Log.UpdateRefMan = UpdateRefMan;
                    Log.ModifyDate = DateTime.UtcNow.ToLocalTime();
                    NegocioLog.Update(Log);
                }
            }
            catch (Exception ex)
            {
                new BusinessLogger().WriteError(ex, "Ln: 409 BusinessMabe");
            }
            try
            {
                if (!Send)
                {
                    var orderUpdate = NegocioOrder.GetByOrderID(ODS);
                    orderUpdate.SendCRM = "Error";
                    NegocioOrder.Update(orderUpdate);
                }
            }
            catch (Exception ex)
            {
                new BusinessLogger().WriteError(ex, "Ln: 422 BusinessMabe");
            }

            return Send;
        }

        public List<srHistoryODS.DT_BusquedaODS_InItem> HistoryODS(string InstalledBaseID)
        {
            srHistoryODS.DT_BusquedaODS_Out MT_BusquedaODS_Out = new srHistoryODS.DT_BusquedaODS_Out();

            MT_BusquedaODS_Out.DestinoSistema = new srHistoryODS.DT_BusquedaODS_OutDestinoSistema() { Idioma = "ES", Mandante = "700", Value = "CPR" };
            MT_BusquedaODS_Out.SessionReference = new srHistoryODS.DT_BusquedaODS_OutSessionReference() { Satelite = "MOBILE", Value = "Interfaz_Base_final_BC" };

            List<srHistoryODS.DT_BusquedaODS_OutItem> items = new List<srHistoryODS.DT_BusquedaODS_OutItem>();
            items.Add(new srHistoryODS.DT_BusquedaODS_OutItem() { IDBaseInstalada = InstalledBaseID });
            MT_BusquedaODS_Out.Item = items.ToArray();

            srHistoryODS.SI_BusquedaODS_OSClient proxy = new srHistoryODS.SI_BusquedaODS_OSClient();

            var eab = new System.ServiceModel.EndpointAddressBuilder(new System.ServiceModel.EndpointAddress(GlobalConfiguration.endPointHistoryODS));

            proxy.ClientCredentials.UserName.UserName = GlobalConfiguration.endPointUser;
            proxy.ClientCredentials.UserName.Password = GlobalConfiguration.endPointPwd;

            proxy.Endpoint.Address = eab.ToEndpointAddress();

            return proxy.SI_BusquedaODS_OS(MT_BusquedaODS_Out).ToList();
        }

        public EntitySAPReprogramacionODSResult ReprogramingODS(string OrderID)
        {


            srReprogramingODS.DT_ReprogramacionODS_SendItems[] MT_ReprogramacionODS_Out = new srReprogramingODS.DT_ReprogramacionODS_SendItems[1];

            MT_ReprogramacionODS_Out[0] = new srReprogramingODS.DT_ReprogramacionODS_SendItems();
            MT_ReprogramacionODS_Out[0].SessionReference = "Interfaz_Mobile";
            MT_ReprogramacionODS_Out[0].Satelite = "Mobile";

            MT_ReprogramacionODS_Out[0].Destino = new srReprogramingODS.DT_ReprogramacionODS_SendItemsDestino() { Idioma = "ES", Mandante = "700", Sistema = "CSN" };

            MT_ReprogramacionODS_Out[0].Item = new srReprogramingODS.DT_ReprogramacionODS_SendItemsItem { ODS = OrderID };



            srReprogramingODS.SI_CONFINAL_ReprogramacionODSClient proxy = new srReprogramingODS.SI_CONFINAL_ReprogramacionODSClient();


            var eab = new System.ServiceModel.EndpointAddressBuilder(new System.ServiceModel.EndpointAddress(GlobalConfiguration.endPointReprogramingODS));

            proxy.ClientCredentials.UserName.UserName = GlobalConfiguration.endPointUser;
            proxy.ClientCredentials.UserName.Password = GlobalConfiguration.endPointPwd;
            //proxy.ClientCredentials.UserName.UserName = "ecommerce";
            //proxy.ClientCredentials.UserName.Password = "mabe.12345";

            proxy.Endpoint.Address = eab.ToEndpointAddress();

            srReprogramingODS.ReprogramacionODSRequest request = new srReprogramingODS.ReprogramacionODSRequest(MT_ReprogramacionODS_Out);
            srReprogramingODS.ReprogramacionODSResponse x = proxy.ReprogramacionODS(request);

            string msg = "------- BussinesWSReprograminODS Send  Inicio -----" + Environment.NewLine;
            msg += "Orden " + OrderID + Environment.NewLine;
            msg += x;
            msg += "------- BussinesWSOBussinesWSReprograminODS Response Fin -----" + Environment.NewLine;

            new Security.BusinessLogger().WriteEntry(msg);
            var mensaje = x.MT_ReprogramacionODS_In[0][0].Descripcion;
            if (mensaje == "ACTUALIZADO CORRECTAMENTE")
            {
                var dataODS = new BusinessOrder().GetByOrderID(OrderID);
                dataODS.OrderExecuteDate = DateTime.Now;
                new BusinessOrder().Update(dataODS);

            }

            return new EntitySAPReprogramacionODSResult() {
                ClaseMensaje = x.MT_ReprogramacionODS_In[0][0].ClaseMensaje,
                Descripcion = x.MT_ReprogramacionODS_In[0][0].Descripcion,
                Mensaje = x.MT_ReprogramacionODS_In[0][0].Mensaje,
                TipoMensaje = x.MT_ReprogramacionODS_In[0][0].TipoMensaje
            };
        }
        public EntitySAPOrdenVentaResult Orden_Venta(string ODS, string TypeService, string folio)
        {
            var NegocioLog = new BusinessLogCRM();
            var NegocioOrder = new BusinessOrder();
            var NegocioCliente = new BusinessClient();
            var NegocioProducto = new BusinessProduct();
            var NegocioCotizacion = new BusinessQuotation();
            var NegocioEmpleado = new BusinessEmployee();
            var NegocioMonitor = new BusinessVisit();
            var NegocioPayment = new BusinessPayment();
            var NegocioInvoice = new BusinessInvoice();
            var NegocioPoliza = new BusinessPolicy();
            var NegocioLugarCompra = new BusinessShopPlace();
            var NegocioRegion = new BusinessStates();
            var NegocioUsuario = new BusinessUsers();
            var NegocioRefSell = new BusinessRefsell();
            var NegocioModulo = new BusinessModuleService();
            var NegocioDetalleCotizacion = new BusinessRefsellDetail();
            var NegocioBOM = new BusinessBuildOfMaterial();
            var NegocioPrecio = new BusinessPrice();
            EntitySAPOrdenVentaRequestClaseCondicion clase = new EntitySAPOrdenVentaRequestClaseCondicion();

            var orden = new EntityOrder();
            var detalleRefsell = new EntityRefsellDetail();
            var poliza = new EntityPolicy();
            var Refaccion = new EntityRefSell();
            var Empleado = new EntityEmployee();
            var Usuario = new EntityUser();
            var Cotizacion = new EntityQuotation();
            var Factura = new ModelViewInvoicing();
            var Cliente = new EntityClient();
            var LugarCompra = new EntityShopPlace();
            var Region = new EntityStates();
            var Producto = new EntityProduct();
            var Regionfac = new EntityStates();
            var Pago = new ModelViewPayment();
            var Modulo = new EntityModuleService();
            var RefaccionDetail = new EntityBuildOfMaterial();
            var result = new EntitySAPOrdenVentaResult();
            var PriceRef = new EntityPrice();
            List<EntityRefsellDetail> Detalleventa = new List<EntityRefsellDetail>();
            string ClaseDocumento = "";
            string Sector = "";
            string MaterialSKU = "";
            string CantPedido = "";
            string Modelo = "";
            string GrupoMateriales4 = "";
            string GrupoCondiciones1 = "";
            string TipoListaPrecios = "";
            string PolizaSN = "";
            string ViaPago = "";
            string Almacen = "";
            string Centro = "";
            string regionfacAv = "";

            EntitySAPOrdenVentaRequest data = new EntitySAPOrdenVentaRequest();
            List<EntitySAPOrdenVentaRequestClaseCondicion> clasev = new List<EntitySAPOrdenVentaRequestClaseCondicion>();

            if (TypeService == "Policy")
            {

                orden = NegocioOrder.GetByOrderID(ODS);
                poliza = NegocioPoliza.GetByFolio(orden.PK_OrderID, folio);
                Cotizacion = NegocioCotizacion.GetByID(poliza.FK_QuotationID);

                Empleado = NegocioEmpleado.GetID(poliza.FK_EmployeeID);
                Usuario = NegocioUsuario.GetByID(Empleado.FK_UserID.Value);
                Factura = NegocioInvoice.GetPolicyInvoice(orden.PK_OrderID, Cotizacion.Folio);
                if (Factura.OrderID == null)
                {
                    Factura.FirstName = "";
                    Factura.LastName = "";
                    Factura.StreetAddress = "";
                    Factura.NumIntAddress = "";
                    Factura.NumExtAddress = "";
                    Factura.CityAddress = "";
                    Factura.CPAddress = "";
                    Factura.MunicipalityAddress = "";
                    Factura.RFC = "";
                    Factura.Email = "";
                    regionfacAv = "";
                }
                else
                {
                    Regionfac = NegocioRegion.GetByState(Factura.StateAddress);
                    regionfacAv = Regionfac.Abbreviation;
                }

                Cliente = NegocioCliente.GetByID(poliza.FK_ClientID);
                Producto = NegocioProducto.GetByID(poliza.FK_ProductID);
                LugarCompra = NegocioLugarCompra.GetByShopPlaceID(poliza.FK_ShopPlace);
                Region = NegocioRegion.GetBYID(LugarCompra.FK_StateID.Value);
                Modulo = NegocioModulo.Get(Empleado.FK_ModuleID);
                Pago = NegocioPayment.GetPolicyPayment(poliza.FK_OrderID, Cotizacion.Folio);
                if (Pago != null)
                {
                    switch (Pago.TypePaymentID)
                    {
                        case "Credit card":
                            ViaPago = "X";

                            break;
                        case "Debit card":
                            ViaPago = "R";

                            break;
                        case "Cash":
                            ViaPago = "W";

                            break;
                    }
                }
                else
                {
                    ViaPago = "";

                }

                MaterialSKU = "008011161600000000";
                CantPedido = "1";
                Modelo = Producto.Model;

                GrupoMateriales4 = poliza.MaterialGroup4;
                GrupoCondiciones1 = poliza.ccGroup;
                TipoListaPrecios = poliza.PriceList;
                PolizaSN = poliza.SerialNumber;

                ClaseDocumento = "ZP21";
                Sector = "13";




                List<EntitySAPOrdenVentaRequestItem> items = new List<EntitySAPOrdenVentaRequestItem>();
                items.Add(new EntitySAPOrdenVentaRequestItem()
                {
                    OrdenWeb = Cotizacion.Folio,
                    AliasNegocio = "",
                    ClaseDocumento = ClaseDocumento,
                    OrganizacionVentas = "MX03",
                    CanalDistribucion = "10",
                    Sector = Sector,
                    NumCliente = Empleado.IDPolicy,
                    FuncionInterlocutorSP = "AG",
                    NumDestinatario = Cliente.ClientID,
                    FuncionInterlocutorSH = "",
                    NombreSH = Cliente.FirstName,
                    NombreSH1 = Cliente.LastName,
                    NombreSH2 = "",
                    NombreSH3 = "",
                    CalleNum = Cliente.StreetAddress,
                    NumInterior = Cliente.NumberInternalAddress,
                    NumExteriorSup = Cliente.NumberExternalAddress,
                    ColoniaSH = Cliente.BoroughAddress,
                    CodigoPostalSH = Cliente.PostalCodeAddress,
                    Delegacion = Cliente.MunicipalityAddress,
                    PaisSH = "MX",
                    RegionSH = Region.Abbreviation,
                    Telefono1 = Cliente.PhoneNumber1,
                    Telefono2 = Cliente.PhoneNumber2,
                    FaxSH = "",
                    RFCSH = "",
                    EmailSH = Cliente.Email,
                    FuncionInterlocutorZR = "",
                    NumEjecutivoVentas = "",
                    NumOrdenCompra = "",
                    PreciosCliente = "",
                    FechaCompra = DateTime.Now.ToString("yyyyMMdd"),
                    FechaEntrega = poliza.GuarantyStart.ToString("yyyyMMdd"),
                    BloqueoEntrega = "",
                    FechaPrecio = DateTime.Now.ToString("yyyyMMdd"),
                    MonedaDoc = poliza.Coin,
                    MotivoPedido = "",
                    Incoterms1 = "",
                    Incoterms2 = "",
                    CondiPago = "",
                    CondiEmbarque = "11",
                    ViaPago = ViaPago,
                    ClasificacionFiscal = "",
                    PosicionPedido = "10",
                    MaterialSKU = MaterialSKU,
                    CantPedido = CantPedido,
                    UniMedida = "ST",
                    FechaEntregaItem = "",
                    Centro = Centro,
                    Almacen = Almacen,
                    Ruta = "",
                    NumEmpleado = Usuario.UserName,
                    NombEmpleado = Modelo,
                    UbicacionEmpleado = "",
                    FormaPago = "",
                    TexCabecera = "",
                    ClaseCondicion = clasev,
                    NombreSHFac = Factura.FirstName,
                    NombreSH1Fac = Factura.LastName,
                    CalleNumFac = Factura.StreetAddress,
                    NumInteriorFac = Factura.NumIntAddress.ToString(),
                    NumExteriorSupFac = Factura.NumExtAddress.ToString(),
                    ColoniaSHFac = Factura.CityAddress,
                    CodigoPostalSHFac = Factura.CPAddress.ToString(),
                    DelegacionFac = Factura.MunicipalityAddress,
                    PaisSHFac = "MX",
                    RegionSHFac = regionfacAv,
                    RFCSHFac = Factura.RFC,
                    EmailSHFac = Factura.Email,
                    Telefono1Fac = "",
                    Telefono2Fac = "",
                    PersonaFisica = "",
                    TipoIdentFiscal = "",
                    GrupoMateriales4 = GrupoMateriales4,
                    GrupoCondiciones1 = GrupoCondiciones1,
                    TipoListaPrecios = TipoListaPrecios,
                    PolizaSN = PolizaSN

                });


                data.Destino = "MEX";
                data.Satelite = "MOB";
                data.Items = items;
                result = new BussinesWSOrdenVenta().Send(data);
                Cotizacion.OrdenVenta = result.Item.PedidoSAP;
                NegocioCotizacion.Update(Cotizacion);

                string msg = "";
                foreach (var item in result.Returns.Return)
                {
                    msg += string.Format("Type: {0}", item.Type) + Environment.NewLine;
                    msg += string.Format("Field: {0}", item.Field) + Environment.NewLine;
                    msg += string.Format("ID: {0}", item.ID) + Environment.NewLine;
                    msg += string.Format("Number: {0}", item.Number) + Environment.NewLine;
                    msg += string.Format("Message: {0}", item.Message) + Environment.NewLine;
                    msg += string.Format("Log_No: {0}", item.Log_No) + Environment.NewLine;
                    msg += string.Format("Log_Msg_No: {0}", item.Log_Msg_No) + Environment.NewLine;
                    msg += string.Format("Message_V1: {0}", item.Message_V1) + Environment.NewLine;
                    msg += string.Format("Message_V2: {0}", item.Message_V2) + Environment.NewLine;
                    msg += string.Format("Message_V3: {0}", item.Message_V3) + Environment.NewLine;
                    msg += string.Format("Message_V4: {0}", item.Message_V4) + Environment.NewLine;
                    msg += string.Format("Parameter: {0}", item.Parameter) + Environment.NewLine;
                    msg += string.Format("Row: {0}", item.Row) + Environment.NewLine;
                    msg += string.Format("System: {0}", item.System) + Environment.NewLine;
                }
                try
                {
                    var Log = NegocioLog.GetByOrderID(orden.PK_OrderID);
                    if (Log == null)
                    {
                        EntityLogCRM LogCRM = new EntityLogCRM();
                        LogCRM.LogID = 0;
                        LogCRM.OrderID = orden.PK_OrderID;
                        LogCRM.UpdateBase = null;
                        LogCRM.UpdateRefMan = null;
                        LogCRM.UpdateODS = null;
                        LogCRM.ModuleReserveSP = msg;
                        LogCRM.ADRReserveSP = null;
                        LogCRM.Status = true;
                        LogCRM.CreateDate = DateTime.UtcNow.ToLocalTime();
                        LogCRM.ModifyDate = DateTime.UtcNow.ToLocalTime();
                        new BusinessLogCRM().Insert(LogCRM);
                    }
                    else
                    {
                        Log.ModuleReserveSP = msg;
                        Log.ModifyDate = DateTime.UtcNow.ToLocalTime();
                        NegocioLog.Update(Log);
                    }
                }
                catch (Exception ex)
                {

                }

            }
            else
            {
              

                orden = NegocioOrder.GetByOrderID(ODS);
                Refaccion = NegocioRefSell.GetByFolio(orden.PK_OrderID,folio);
                Empleado = NegocioEmpleado.GetID(Refaccion.FK_EmployeeID);
                Usuario = NegocioUsuario.GetByID(Empleado.FK_UserID.Value);
                Cotizacion = NegocioCotizacion.GetByID(Refaccion.FK_QuotationID);
                Cliente = NegocioCliente.GetByID(Refaccion.FK_ClientID);
                Factura = NegocioInvoice.GetPolicyInvoice(orden.PK_OrderID, Cotizacion.Folio);
               
                if (Factura.OrderID == null)
                {
                    Factura.FirstName = "";
                    Factura.LastName = "";
                    Factura.StreetAddress = "";
                    Factura.NumIntAddress = "";
                    Factura.NumExtAddress = "";
                    Factura.CityAddress = "";
                    Factura.CPAddress = "";
                    Factura.MunicipalityAddress = "";
                    Factura.RFC = "";
                    Factura.Email = "";
                    regionfacAv = "";
                }
                else
                {
                    Regionfac = NegocioRegion.GetByState(Factura.StateAddress);
                    regionfacAv = Regionfac.Abbreviation;
                }
                Pago = NegocioPayment.GetPolicyPayment(Refaccion.FK_OrderID, Cotizacion.Folio);
                if (Pago != null)
                {
                    switch (Pago.TypePaymentID)
                    {
                        case "Credit card":
                            ViaPago = "X";

                            break;
                        case "Debit card":
                            ViaPago = "R";

                            break;
                        case "Cash":
                            ViaPago = "W";

                            break;
                    }
                }
                else
                {
                    ViaPago = "";

                }
                Producto = NegocioProducto.GetByID(Refaccion.FK_ProductID);
                ClaseDocumento = "ZP18";
                Sector = "12";
                LugarCompra = NegocioLugarCompra.GetByShopPlaceID(Refaccion.FK_ShopPlace);
                Region = NegocioRegion.GetBYID(LugarCompra.FK_StateID.Value);
                Modulo = NegocioModulo.Get(Empleado.FK_ModuleID);
                
                Detalleventa = NegocioDetalleCotizacion.GetList(Refaccion.FK_QuotationID);
                
                foreach (var jko in Detalleventa)
                {
                    if (jko.Fk_ProductID != 0)
                    {
                        clasev.Clear();


                        switch (jko.Origen)
                        {
                            case "T002":
                                Almacen = "4700";
                                Centro = jko.Origen;
                              
                                break;
                            case "T012":
                                Almacen = "4700";
                                Centro = jko.Origen;
                                break;
                            case "T024":
                                Almacen = "4700";
                                Centro = jko.Origen;
                                break;
                            default:
                                Almacen = Empleado.StoreProp;
                                Centro = Modulo.ID.ToString();
                                break;
                        }


                        
                        MaterialSKU = jko.RefMan;
                        CantPedido = jko.Cantidad;
                        if (jko.Flete is true)
                        {
                            clasev.Add(new EntitySAPOrdenVentaRequestClaseCondicion()
                            {
                                Valor = jko.CostoRef,
                                Clase = "ZPR1",
                            });
                            clasev.Add(new EntitySAPOrdenVentaRequestClaseCondicion()
                            {
                                Valor = jko.CostoFlete,
                                Clase = "ZF02",
                            });
                        }                       
                        else
                        {
                            clasev.Add(new EntitySAPOrdenVentaRequestClaseCondicion()
                            {
                                Valor = jko.CostoRef,
                                Clase = "ZPR1",
                            });
                           
                        }
                        
                        
                        List<EntitySAPOrdenVentaRequestItem> items = new List<EntitySAPOrdenVentaRequestItem>();
                        items.Add(new EntitySAPOrdenVentaRequestItem()
                        {
                            OrdenWeb = Cotizacion.Folio,
                            AliasNegocio = "",
                            ClaseDocumento = ClaseDocumento,
                            OrganizacionVentas = "MX03",
                            CanalDistribucion = "10",
                            Sector = Sector,
                            NumCliente = Empleado.IDRefSell,
                            FuncionInterlocutorSP = "AG",
                            NumDestinatario = Empleado.IDRefSell,
                            FuncionInterlocutorSH = "",
                            NombreSH = Cliente.FirstName,
                            NombreSH1 = Cliente.LastName,
                            NombreSH2 = "",
                            NombreSH3 = "",
                            CalleNum = Cliente.StreetAddress,
                            NumInterior = Cliente.NumberInternalAddress,
                            NumExteriorSup = Cliente.NumberExternalAddress,
                            ColoniaSH = Cliente.BoroughAddress,
                            CodigoPostalSH = Cliente.PostalCodeAddress,
                            Delegacion = Cliente.MunicipalityAddress,
                            PaisSH = "MX",
                            RegionSH = Region.Abbreviation,
                            Telefono1 = Cliente.PhoneNumber1,
                            Telefono2 = Cliente.PhoneNumber2,
                            FaxSH = "",
                            RFCSH = "",
                            EmailSH = Cliente.Email,
                            FuncionInterlocutorZR = "",
                            NumEjecutivoVentas = "",
                            NumOrdenCompra = "",
                            PreciosCliente = "",
                            FechaCompra = DateTime.Now.ToString("yyyyMMdd"),
                            FechaEntrega = DateTime.Now.ToString("yyyyMMdd"),
                            BloqueoEntrega = "",
                            FechaPrecio = DateTime.Now.ToString("yyyyMMdd"),
                            MonedaDoc = "MXN",
                            MotivoPedido = "",
                            Incoterms1 = "",
                            Incoterms2 = "",
                            CondiPago = "",
                            CondiEmbarque = "12",
                            ViaPago = ViaPago,
                            ClasificacionFiscal = "1",
                            PosicionPedido = "10",
                            MaterialSKU = MaterialSKU,
                            CantPedido = CantPedido,
                            UniMedida = "ST",
                            FechaEntregaItem = "",
                            Centro = Centro,
                            Almacen = Almacen,
                            Ruta = "",
                            NumEmpleado = Usuario.UserName,
                            NombEmpleado = Modelo,
                            UbicacionEmpleado = "",
                            FormaPago = "",
                            TexCabecera = "",
                            ClaseCondicion = clasev,
                            NombreSHFac = Factura.FirstName,
                            NombreSH1Fac = Factura.LastName,
                            CalleNumFac = Factura.StreetAddress,
                            NumInteriorFac = Factura.NumIntAddress.ToString(),
                            NumExteriorSupFac = Factura.NumExtAddress.ToString(),
                            ColoniaSHFac = Factura.CityAddress,
                            CodigoPostalSHFac = Factura.CPAddress.ToString(),
                            DelegacionFac = Factura.MunicipalityAddress,
                            PaisSHFac = "MX",
                            RegionSHFac = regionfacAv,
                            RFCSHFac = Factura.RFC,
                            EmailSHFac = Factura.Email,
                            Telefono1Fac = "",
                            Telefono2Fac = "",
                            PersonaFisica = "",
                            TipoIdentFiscal = "",
                            GrupoMateriales4 = GrupoMateriales4,
                            GrupoCondiciones1 = GrupoCondiciones1,
                            TipoListaPrecios = TipoListaPrecios,
                            PolizaSN = PolizaSN

                        });


                        data.Destino = "MEX";
                        data.Satelite = "MOB";
                        data.Items = items;


                        result = new BussinesWSOrdenVenta().Send(data);

                        jko.OrdenVenta = result.Item.PedidoSAP;
                        NegocioDetalleCotizacion.Update(jko);
                        string msg = "";
                        msg += "-------------------------INICIO_Orden_venta-------------------------" + Environment.NewLine;
                        msg += string.Format("Refaccion", MaterialSKU) + Environment.NewLine;
                        foreach (var item in result.Returns.Return)
                        {
                            msg += string.Format("Type: {0}", item.Type) + Environment.NewLine;
                            msg += string.Format("Field: {0}", item.Field) + Environment.NewLine;
                            msg += string.Format("ID: {0}", item.ID) + Environment.NewLine;
                            msg += string.Format("Number: {0}", item.Number) + Environment.NewLine;
                            msg += string.Format("Message: {0}", item.Message) + Environment.NewLine;
                            msg += string.Format("Log_No: {0}", item.Log_No) + Environment.NewLine;
                            msg += string.Format("Log_Msg_No: {0}", item.Log_Msg_No) + Environment.NewLine;
                            msg += string.Format("Message_V1: {0}", item.Message_V1) + Environment.NewLine;
                            msg += string.Format("Message_V2: {0}", item.Message_V2) + Environment.NewLine;
                            msg += string.Format("Message_V3: {0}", item.Message_V3) + Environment.NewLine;
                            msg += string.Format("Message_V4: {0}", item.Message_V4) + Environment.NewLine;
                            msg += string.Format("Parameter: {0}", item.Parameter) + Environment.NewLine;
                            msg += string.Format("Row: {0}", item.Row) + Environment.NewLine;
                            msg += string.Format("System: {0}", item.System) + Environment.NewLine;
                        }
                        msg += "-------------------------INICIO_Orden_venta-------------------------" + Environment.NewLine;
                        new BusinessLogger().WriteEntry(msg);
                        
                    }
                }


               
                Detalleventa = NegocioDetalleCotizacion.GetList(Refaccion.FK_QuotationID);
                string OrdenVentaFin="";
                var OrdenVenta = (from x in Detalleventa select x.OrdenVenta).ToList();
               
                    foreach (var item in OrdenVenta)
                    {
                           if (item != "")
                        {
                        if (OrdenVentaFin != "")
                        {
                            OrdenVentaFin = OrdenVentaFin + "," + item;
                        }
                        else
                        {
                            OrdenVentaFin = item;
                        }
                         }

                }
                Cotizacion.OrdenVenta = OrdenVentaFin;
                NegocioCotizacion.Update(Cotizacion);
              

                



            }

            return result;





        }

        private void InnerChannel_UnknownMessageReceived(object sender, System.ServiceModel.UnknownMessageReceivedEventArgs e)
        {
            throw new NotImplementedException();
        }

        public List<srHistoryODS.DT_BusquedaODS_InItem> HistoryODSByClient(string ClientID)
        {
            srHistoryODS.DT_BusquedaODS_Out MT_BusquedaODS_Out = new srHistoryODS.DT_BusquedaODS_Out();

            MT_BusquedaODS_Out.DestinoSistema = new srHistoryODS.DT_BusquedaODS_OutDestinoSistema() { Idioma = "ES", Mandante = "700", Value = "CPR" };
            MT_BusquedaODS_Out.SessionReference = new srHistoryODS.DT_BusquedaODS_OutSessionReference() { Satelite = "MOBILE", Value = "Interfaz_Base_final_BC" };

            List<srHistoryODS.DT_BusquedaODS_OutItem> items = new List<srHistoryODS.DT_BusquedaODS_OutItem>();
            items.Add(new srHistoryODS.DT_BusquedaODS_OutItem() {  IDConsumidor = ClientID });
            MT_BusquedaODS_Out.Item = items.ToArray();

            srHistoryODS.SI_BusquedaODS_OSClient proxy = new srHistoryODS.SI_BusquedaODS_OSClient();

            var eab = new System.ServiceModel.EndpointAddressBuilder(new System.ServiceModel.EndpointAddress(GlobalConfiguration.endPointHistoryODS));

            proxy.ClientCredentials.UserName.UserName = GlobalConfiguration.endPointUser;
            proxy.ClientCredentials.UserName.Password = GlobalConfiguration.endPointPwd;

            proxy.Endpoint.Address = eab.ToEndpointAddress();

            return proxy.SI_BusquedaODS_OS(MT_BusquedaODS_Out).ToList();
        }

        public List<srHistoryODS.DT_BusquedaODS_InItem> HistoryODSByClient(string ClientID, string InstalledBaseID)
        {
            srHistoryODS.DT_BusquedaODS_Out MT_BusquedaODS_Out = new srHistoryODS.DT_BusquedaODS_Out();

            MT_BusquedaODS_Out.DestinoSistema = new srHistoryODS.DT_BusquedaODS_OutDestinoSistema() { Idioma = "ES", Mandante = "700", Value = "CPR" };
            MT_BusquedaODS_Out.SessionReference = new srHistoryODS.DT_BusquedaODS_OutSessionReference() { Satelite = "MOBILE", Value = "Interfaz_Base_final_BC" };

            List<srHistoryODS.DT_BusquedaODS_OutItem> items = new List<srHistoryODS.DT_BusquedaODS_OutItem>();
            items.Add(new srHistoryODS.DT_BusquedaODS_OutItem() { IDConsumidor = ClientID, IDBaseInstalada = InstalledBaseID, IDOperacion = "" });
            MT_BusquedaODS_Out.Item = items.ToArray();

            srHistoryODS.SI_BusquedaODS_OSClient proxy = new srHistoryODS.SI_BusquedaODS_OSClient();

            var eab = new System.ServiceModel.EndpointAddressBuilder(new System.ServiceModel.EndpointAddress(GlobalConfiguration.endPointHistoryODS));

            proxy.ClientCredentials.UserName.UserName = GlobalConfiguration.endPointUser;
            proxy.ClientCredentials.UserName.Password = GlobalConfiguration.endPointPwd;

            proxy.Endpoint.Address = eab.ToEndpointAddress();

           
            return proxy.SI_BusquedaODS_OS(MT_BusquedaODS_Out).ToList();
        }

        public List<srHistoryBase.DT_BaseInstalada_InItem> HistoryBase(string ClientID)
        {
            srHistoryBase.DT_BaseInstalada_Out MT_BaseInstalada_Out = new srHistoryBase.DT_BaseInstalada_Out();
            MT_BaseInstalada_Out.DestinoSistema = new srHistoryBase.DT_BaseInstalada_OutDestinoSistema() { Idioma = "ES", Mandante = "700", Value = "CPR" };
            MT_BaseInstalada_Out.SessionReference = new srHistoryBase.DT_BaseInstalada_OutSessionReference() { Satelite = "MOBILE", Value = "Interfaz_Base_final_BC" };

            List<srHistoryBase.DT_BaseInstalada_OutItem> items = new List<srHistoryBase.DT_BaseInstalada_OutItem>();
            items.Add(new srHistoryBase.DT_BaseInstalada_OutItem() { ID_Cliente = ClientID });
            MT_BaseInstalada_Out.Item = items.ToArray();

            srHistoryBase.SI_BusquedaIB_OSClient proxy = new srHistoryBase.SI_BusquedaIB_OSClient();

            var eab = new System.ServiceModel.EndpointAddressBuilder(new System.ServiceModel.EndpointAddress(GlobalConfiguration.endPointHistoryBase));
           
            proxy.ClientCredentials.UserName.UserName = GlobalConfiguration.endPointUser;
            proxy.ClientCredentials.UserName.Password = GlobalConfiguration.endPointPwd;

            proxy.Endpoint.Address = eab.ToEndpointAddress(); 

            return proxy.SI_BusquedaIB_OS(MT_BaseInstalada_Out).ToList();
        }

        public ModelViewResultREST ADRReserveSP(List<srADRReserveSP.DT_ProcessRPL_ReqTipo_de_operacionItem> items)
        {
                        
            //srADRReserveSP.DT_ProcessRPL_Res SI_Process_RPL_OARequest = new srADRReserveSP.SI_Process_RPL_OARequest();
            
            srADRReserveSP.DT_ProcessRPL_Req MT_ProcessRPL_Req = new srADRReserveSP.DT_ProcessRPL_Req();            
            MT_ProcessRPL_Req.DestinoSistema = new srADRReserveSP.DT_ProcessRPL_ReqDestinoSistema() { idioma = "ES", Mandante = "700" };
            MT_ProcessRPL_Req.SessionReference = new srADRReserveSP.DT_ProcessRPL_ReqSessionReference() { Satelite = "MOBILE" };
                         
            MT_ProcessRPL_Req.Tipo_de_operacion = items.ToArray();

            //SI_Process_RPL_OARequest.MT_ProcessRPL_Req = MT_ProcessRPL_Req;

            srADRReserveSP.SI_Process_RPL_OAClient proxy = new srADRReserveSP.SI_Process_RPL_OAClient();

            var eab = new System.ServiceModel.EndpointAddressBuilder(new System.ServiceModel.EndpointAddress(GlobalConfiguration.endPointADRReserveSP));

            proxy.ClientCredentials.UserName.UserName = GlobalConfiguration.endPointUser;
            proxy.ClientCredentials.UserName.Password = GlobalConfiguration.endPointPwd;

             

            proxy.Endpoint.Address = eab.ToEndpointAddress();

            var s = proxy.SI_Process_RPL_OA(MT_ProcessRPL_Req).ToString();

            //srADRReserveSP.DT_ProcessRPL_Res result = proxy.SI_Process_RPL_OA(MT_ProcessRPL_Req);

            //string msg = "";
            //msg += "-------------------------INICIO_ADRReserveSP-------------------------" + Environment.NewLine;
            //msg += string.Format("item: {0}", result.item) + Environment.NewLine;
            //foreach (var item in result.Return)
            //{
            //    msg += string.Format("ClaseMensaje: {0}", item.ClaseMensaje) + Environment.NewLine;
            //    msg += string.Format("Descripcion: {0}", item.Descripcion) + Environment.NewLine;
            //    msg += string.Format("Mensaje: {0}", item.Mensaje) + Environment.NewLine;
            //    msg += string.Format("TipoMensaje: {0}", item.TipoMensaje) + Environment.NewLine;                
            //}            
            //msg += "-------------------------FIN_ADRReserveSP-------------------------" + Environment.NewLine;
            //new BusinessLogger().WriteEntry(msg);

            ModelViewResultREST resultRest = new ModelViewResultREST();
            resultRest.Result = "Success";
            return resultRest;

        }

        public ModelViewResultREST ModuleReserveSP(List<srModuleReserveSP.DT_CrearReserva_ReqTipo_de_operacionItem> items)
        {
            srModuleReserveSP.DT_CrearReserva_Req MT_CrearReserva_Req = new srModuleReserveSP.DT_CrearReserva_Req();

            MT_CrearReserva_Req.DestinoSistema = new srModuleReserveSP.DT_CrearReserva_ReqDestinoSistema() { idioma = "ES", Mandante = "700" };
            MT_CrearReserva_Req.SessionReference = new srModuleReserveSP.DT_CrearReserva_ReqSessionReference() { Satelite = "MOBILE" };
            
            MT_CrearReserva_Req.Tipo_de_operacion = items.ToArray();

            srModuleReserveSP.SI_CrearReserva_OSClient proxy = new srModuleReserveSP.SI_CrearReserva_OSClient();

            var eab = new System.ServiceModel.EndpointAddressBuilder(new System.ServiceModel.EndpointAddress(GlobalConfiguration.endPointModuleReserveSP));

            proxy.ClientCredentials.UserName.UserName = GlobalConfiguration.endPointUser;
            proxy.ClientCredentials.UserName.Password = GlobalConfiguration.endPointPwd;

            proxy.Endpoint.Address = eab.ToEndpointAddress();

            srModuleReserveSP.DT_CrearReserva_Res result = proxy.SI_CrearReserva_OS(MT_CrearReserva_Req);

            string msg = "";
            msg += "-------------------------INICIO_ModuleReserveSP-------------------------" + Environment.NewLine;
            msg += string.Format("item: {0}", result.item) + Environment.NewLine;
            foreach (var item in result.Return)
            {
                msg += string.Format("ClaseMensaje: {0}", item.ClaseMensaje) + Environment.NewLine;
                msg += string.Format("Descripcion: {0}", item.Descripcion) + Environment.NewLine;
                msg += string.Format("Mensaje: {0}", item.Mensaje) + Environment.NewLine;
                msg += string.Format("TipoMensaje: {0}", item.TipoMensaje) + Environment.NewLine;
            }
            msg += "-------------------------FIN_ModuleReserveSP-------------------------" + Environment.NewLine;
            new BusinessLogger().WriteEntry(msg);

            ModelViewResultREST resultRest = new ModelViewResultREST();
            resultRest.Result = "Success";
            return resultRest;
        }
        
        public void UpdateInvoiceODS(List<srInvoiceODS.DT_Facturacion_OutItem> items, string OrderID)
        {
            //string sleep = "-------------------------INICIO_SleepInvoiceODS / OrderID: " + OrderID + "-------------------------" + Environment.NewLine;
            //new BusinessLogger().WriteEntry(sleep);
            //try
            //{
            //    wait(180);
            //}
            //catch (Exception ex)
            //{
            //    string msg = ex.Message;
            //    new BusinessLogger().WriteEntry(msg);
            //}
            //string wakeup = "-------------------------FIN_SleepInvoiceODS / OrderID: " + OrderID + "-------------------------" + Environment.NewLine;
            //new BusinessLogger().WriteEntry(wakeup);

            var NegocioOrder = new BusinessOrder();
            var NegocioLog = new BusinessLogCRM();
            srInvoiceODS.DT_Facturacion_Out  MT_ActulizaInvoiceODS_Out = new srInvoiceODS.DT_Facturacion_Out();
            MT_ActulizaInvoiceODS_Out.DestinoSistema = new srInvoiceODS.DT_Facturacion_OutDestinoSistema() {  Idioma = "ES", Mandante = "700" };
            MT_ActulizaInvoiceODS_Out.SessionReference = new srInvoiceODS.DT_Facturacion_OutSessionReference() { Satelite = "MOBILE", Value = "" };

            MT_ActulizaInvoiceODS_Out.Item = items.ToArray();

            srInvoiceODS.SI_Facturacion_OSClient proxy = new srInvoiceODS.SI_Facturacion_OSClient();

            // Remove the ClientCredentials behavior. 
           // proxy.ChannelFactory.Endpoint.Behaviors.Remove<System.ServiceModel.Description.ClientCredentials>();

            // Add a custom client credentials instance to the behaviors collection. 
           // proxy.ChannelFactory.Endpoint.Behaviors.Add(new MyClientCredentials());
            var eab = new System.ServiceModel.EndpointAddressBuilder(new System.ServiceModel.EndpointAddress(GlobalConfiguration.endPointInvoiceODS));

            proxy.ClientCredentials.UserName.UserName = GlobalConfiguration.endPointUser;
            proxy.ClientCredentials.UserName.Password = GlobalConfiguration.endPointPwd;

            proxy.Endpoint.Address = eab.ToEndpointAddress();
            //string UpdateODS = null;
            try
            {
                srInvoiceODS.DT_ActualizaODS_InItemRETURN[][] result = proxy.SI_Facturacion_OS(MT_ActulizaInvoiceODS_Out);

                foreach (var itemHeader in result)
                {
                    foreach (var item in itemHeader)
                    {
                        string msg = "";
                        // UpdateODS += string.Format("DESCRIPCION: {0}", item.DESCRIPCION) + Environment.NewLine;
                        msg += "-------------------------INICIO_InvoiceODS-------------------------" + Environment.NewLine;
                        msg += string.Format("TIPO_MENSAJE: {0}", item.TIPO_MENSAJE) + Environment.NewLine;
                        msg += string.Format("CLASE_MENSAJE: {0}", item.CLASE_MENSAJE) + Environment.NewLine;
                        msg += string.Format("DESCRIPCION: {0}", item.DESCRIPCION) + Environment.NewLine;
                        msg += string.Format("MENSAJE: {0}", item.MENSAJE) + Environment.NewLine;
                        msg += "-------------------------FIN_InvoiceODS-------------------------" + Environment.NewLine;

                        //        if (item.MENSAJE.Contains("005"))
                        //        {
                        //            msg += "------- Actualizar EnvioCRM -----" + Environment.NewLine;
                        //            var order = NegocioOrder.GetByOrderID(OrderID);
                        //            if (item.DESCRIPCION.Contains("Campo Estatus Actualizado"))
                        //            {
                        //                msg += "Actualizo: " + "OK" + Environment.NewLine;
                        //                order.SendCRM = "OK";
                        //                NegocioOrder.Update(order);
                        //                msg += "OrderID: " + OrderID + Environment.NewLine;
                        //            }
                        //            msg += "-------------------------FIN Actualizar EnvioCRM-------------------------" + Environment.NewLine;
                        //        }

                        //        if (item.MENSAJE.Contains("007"))
                        //        {
                        //            msg += "------- Actualizar EnvioCRM -----" + Environment.NewLine;
                        //            var order = NegocioOrder.GetByOrderID(OrderID);
                        //            if (item.DESCRIPCION.Contains("Error al Modificar Estatus"))
                        //            {
                        //                msg += "Actualizo: " + "Error" + Environment.NewLine;
                        //                order.SendCRM = "Error";
                        //                NegocioOrder.Update(order);
                        //                msg += "OrderID: " + OrderID + Environment.NewLine;
                        //            }
                        //            msg += "-------------------------FIN Actualizar EnvioCRM-------------------------" + Environment.NewLine;
                        new BusinessLogger().WriteEntry(msg);
                    }       
                }
            

             }
            catch
            {
                var order = NegocioOrder.GetByOrderID(OrderID);
                order.SendCRM = "Error";
                NegocioOrder.Update(order);
            }

            //try
            //{
            //    var order = NegocioOrder.GetByOrderID(OrderID);
            //    var Log = NegocioLog.GetByOrderID(order.PK_OrderID);
            //    if (Log == null)
            //    {
            //        EntityLogCRM LogCRM = new EntityLogCRM();
            //        LogCRM.LogID = 0;
            //        LogCRM.OrderID = order.PK_OrderID;
            //        LogCRM.UpdateBase = null;
            //        LogCRM.UpdateRefMan = null;
            //        LogCRM.UpdateODS = UpdateODS;
            //        LogCRM.ModuleReserveSP = null;
            //        LogCRM.ADRReserveSP = null;
            //        LogCRM.Status = true;
            //        LogCRM.CreateDate = DateTime.UtcNow.ToLocalTime();
            //        LogCRM.ModifyDate = DateTime.UtcNow.ToLocalTime();
            //        new BusinessLogCRM().Insert(LogCRM);
            //    }
            //    else
            //    {
            //        Log.UpdateODS = UpdateODS;
            //        Log.ModifyDate = DateTime.UtcNow.ToLocalTime();
            //        NegocioLog.Update(Log);
            //    }
            //}
            //catch
            //{


            //}
        }
        public void ProcessODS(string ods)
        {
            Console.WriteLine(String.Format("Procesando ODS {0}", ods));
            entities.Entity.Operation.EntityOrder dataODS = null;
            entities.Entity.Operation.EntityInstalledBase dataInstallBase = null;
            entities.Entity.Operation.EntityClient dataClient = null;
            entities.Entity.Operation.EntityVisit dataVisit = null;
            entities.ModelView.Operation.ModelViewInvoicing dataInvoice = null;
            bool StatusRef = true;

            // datos de la orden
            try
            {
                dataODS = new BusinessOrder().GetByOrderID(ods);
                dataInstallBase = new BusinessInstalledBase().GetByID(dataODS.FK_InstalledBaseID);
                dataClient = new BusinessClient().GetByID(dataInstallBase.FK_ClientID);
                dataVisit = new BusinessVisit().GetByOrderID(dataODS.PK_OrderID);

                if (dataODS.ExtraKilometres == true && ProcessExtraKM == false) return;
            }
            catch (Exception ex)
            {
                new BusinessLogger().WriteError(ex, "Obtener información de ODS - " + ods);
            }

            //actualización Facturación
            try
            {
                dataInvoice = new BusinessInvoice().GetByOrderID(dataODS.PK_OrderID);
                
                List<srInvoiceODS.DT_Facturacion_OutItem> itemInvoiceODSList = new List<srInvoiceODS.DT_Facturacion_OutItem>();
                srInvoiceODS.DT_Facturacion_OutItem itemODSInvoiceHeader = new srInvoiceODS.DT_Facturacion_OutItem();
                List<srInvoiceODS.DT_Facturacion_OutItemDireccion_de_Facturas> arrDireccion = new List<srInvoiceODS.DT_Facturacion_OutItemDireccion_de_Facturas>();

                if (dataInvoice.OrderID != null)
                {
                    string PersonaFisica = dataInvoice.PersonType != "Moral" ? "X" : "";

                    arrDireccion.Add(new srInvoiceODS.DT_Facturacion_OutItemDireccion_de_Facturas()
                    {
                        Addrnumber = "",
                        Street = dataInvoice.StreetAddress,
                        House_num1 = dataInvoice.NumExtAddress,
                        House_num2 = dataInvoice.NumIntAddress,
                        Referencias = dataInvoice.Reference,
                        City1 = dataInvoice.MunicipalityAddress,
                        City2 = dataInvoice.CityAddress,
                        Post_code1 = dataInvoice.CPAddress,
                        Region = new BusinessStates().GetAll().Where(p => dataInvoice.StateAddress.Contains(p.StateName)).FirstOrDefault().Abbreviation,
                        Country = "MX",
                        RFC = dataInvoice.RFC,
                        Persona_Fisica = PersonaFisica,
                        Nombre = PersonaFisica == "X" ? dataInvoice.FirstName : (dataInvoice.BusinessName.Length > 40 ? dataInvoice.BusinessName.Substring(0, 39) : dataInvoice.BusinessName),
                        Apellido = PersonaFisica == "X" ? dataInvoice.LastName : (dataInvoice.BusinessName.Length > 40 ? dataInvoice.BusinessName.Substring(30) : ""),
                        Razon_social = PersonaFisica == "" ? dataInvoice.BusinessName : ""

                    });
                    itemODSInvoiceHeader.ID_Operacion = dataODS.OrderID;
                    itemODSInvoiceHeader.Direccion_de_Facturas = arrDireccion.ToArray();
                    itemInvoiceODSList.Add(itemODSInvoiceHeader);

                    string msg = "-----------DATABEGIN-----------" + Environment.NewLine;
                    msg += String.Format("Street: {0}", dataInvoice.StreetAddress) + Environment.NewLine;
                    msg += String.Format("House_num1: {0}", dataInvoice.NumExtAddress) + Environment.NewLine;
                    msg += String.Format("House_num2: {0}", dataInvoice.NumIntAddress) + Environment.NewLine;
                    msg += String.Format("Referencias: {0}", dataInvoice.Reference) + Environment.NewLine;
                    msg += String.Format("City1: ", dataInvoice.MunicipalityAddress) + Environment.NewLine;
                    msg += String.Format("City2: {0}", dataInvoice.CityAddress) + Environment.NewLine;
                    msg += String.Format("Post_code1: {0}", dataInvoice.CPAddress) + Environment.NewLine;
                    msg += String.Format("Region: {0}", new BusinessStates().GetAll().Where(p => dataInvoice.StateAddress.Contains(p.StateName)).FirstOrDefault().Abbreviation) + Environment.NewLine;
                    msg += String.Format("Country: {0}", "MX") + Environment.NewLine;
                    msg += String.Format("RFC: {0}", dataInvoice.RFC) + Environment.NewLine;
                    msg += String.Format("Persona_Fisica: {0}", PersonaFisica) + Environment.NewLine;
                    msg += String.Format("Nombre: {0}", PersonaFisica == "X" ? dataInvoice.FirstName : (dataInvoice.BusinessName.Length > 40 ? dataInvoice.BusinessName.Substring(0, 39) : dataInvoice.BusinessName)) + Environment.NewLine;
                    msg += String.Format("Apellido: {0}", PersonaFisica == "X" ? dataInvoice.LastName : (dataInvoice.BusinessName.Length > 40 ? dataInvoice.BusinessName.Substring(30) : "")) + Environment.NewLine;
                    msg += String.Format("Razon_social: {0}", PersonaFisica == "" ? dataInvoice.BusinessName : "") + Environment.NewLine;
                    msg += "-----------DATAEND-----------" + Environment.NewLine;
                    new BusinessLogger().WriteEntry(msg);

                    new BusinessMabe().UpdateInvoiceODS(itemInvoiceODSList, dataODS.OrderID);
                }
            }
            catch (Exception ex)
            {
                new BusinessLogger().WriteError(ex, "Actualización Facturación - " + ods);
            }
                       

            // actualización de base instalada
            try
            {
                List<srUpdateBase.DT_ActualizaIB_ReqIT_IB_CHANGEItem> items = new List<srUpdateBase.DT_ActualizaIB_ReqIT_IB_CHANGEItem>();
                items.Add(new srUpdateBase.DT_ActualizaIB_ReqIT_IB_CHANGEItem()
                {
                    Idioma = "ES",
                    Fecha_de_Compra = dataInstallBase.ShopDateFlag == true ? dataInstallBase.ShopDate.Value.ToString("yyyyMMdd") : "",
                    ID_de_Cliente = dataClient.ClientID,
                    ID_de_Registro = dataInstallBase.InstalledBaseID,
                    Lugar_de_Compra = dataInstallBase.ShopPlaceIDFlag == true ? new BusinessShopPlace().GetByShopPlaceID(dataInstallBase.FK_ShopPlaceID.Value).ShopPlaceID : "",
                    Marca = "",
                    Modelo = new BusinessProduct().GetByID(dataInstallBase.FK_ProductID.Value).Model,
                    No_Serie = dataInstallBase.SerialNumberFlag == true ? dataInstallBase.SerialNumber : "",
                    
                });
                if ((dataInstallBase.ShopDateFlag == true) || (dataInstallBase.ShopPlaceIDFlag == true) || (dataInstallBase.ProductIDFlag == true) || (dataInstallBase.SerialNumberFlag == true))
                {
                    Console.WriteLine(String.Format("Procesando BI ODS {0}", ods));
                    new BusinessMabe().UpdateBase(items, dataODS.PK_OrderID);
                    try
                    {
                        wait(50);
                    }
                    catch(Exception ex)
                    {
                        new BusinessLogger().WriteError(ex, "Actualización BI Espera ODS " + ods);                        
                    }                  
                }                 
            }
            catch (Exception ex)
            {
                new BusinessLogger().WriteError(ex, "Update BI ODS " + ods);
            }

            try
            {
                // actualización de refacciones 
                List<srUpdateRefMan.DT_ActRefMan_OutItem> itemsRefMan = new List<srUpdateRefMan.DT_ActRefMan_OutItem>();
                srUpdateRefMan.DT_ActRefMan_OutItem itemRefManHeader = new srUpdateRefMan.DT_ActRefMan_OutItem();
                List<srUpdateRefMan.DT_ActRefMan_OutItemRefMan> itemRefManDetail = new List<srUpdateRefMan.DT_ActRefMan_OutItemRefMan>();
                
                var dataSparePart = new BusinessSparePart().GetByOrder(ods);
                                        

                if (dataODS.OrderID.StartsWith("95"))
                {
                    foreach (var itemSP in dataSparePart)
                    {
                        // ordenes de cargo en talleres no envian kilometraje ni mano de obra
                        if (itemSP.RefManID != "8011161600000025" && itemSP.RefManID != "8011161600000030"&& itemSP.RefManID!= "8011161600000029"&& itemSP.RefManID!= "8011161600000020")
                        {
                            string motivoR = "";
                            switch (itemSP.SparePartStatus)
                            {
                                case "E0001":
                                    motivoR = "";
                                    break;
                                case "E0003":
                                    motivoR = "04";
                                    break;
                                case "E0004":
                                    motivoR = (dataVisit.FK_CauseOrderID == 39) || (dataVisit.FK_CauseOrderID == 42) || (dataVisit.FK_CauseOrderID == 40) || (dataVisit.FK_CauseOrderID == 41) ? "04" : "";
                                    break;
                                case "E0006":
                                    motivoR = "";
                                    break;
                            }

                            itemRefManDetail.Add(new srUpdateRefMan.DT_ActRefMan_OutItemRefMan()
                            {
                                Cantidad = itemSP.Quantity.ToString(),
                                Item_Ref_Man = itemSP.RefManID,
                                NumeroPosicion = itemSP.PosicionItem == "0" ? "" : itemSP.PosicionItem,
                                EstatusItem_RefMan = itemSP.SparePartStatus,
                                MotivoRechazo = motivoR,
                                NumeroFactura = "",
                                Proveedor = ""
                            });
                        }
                    }
                }                           
                else
                {
                    var garantia = new BusinessGuaranty().Get(dataODS.FK_GuarantyID.Value);
                    if (garantia.GuarantyID != "0120" && garantia.GuarantyID != "0140" && garantia.GuarantyID != "0150" && dataODS.ExtraKilometres==false)
                    {
                        foreach (var itemSP in dataSparePart)
                        {
                            if (itemSP.RefManID != "8011161600000025" )
                            {
                                string motivoR = "";
                                switch (itemSP.SparePartStatus)
                                {
                                    case "E0001":
                                        motivoR = "";
                                        break;
                                    case "E0003":
                                        motivoR = "04";
                                        break;
                                    case "E0004":
                                        motivoR = (dataVisit.FK_CauseOrderID == 39) || (dataVisit.FK_CauseOrderID == 42) || (dataVisit.FK_CauseOrderID == 40) || (dataVisit.FK_CauseOrderID == 41) ? "04" : "";
                                        break;
                                    case "E0006":
                                        motivoR = "";
                                        break;
                                }

                                itemRefManDetail.Add(new srUpdateRefMan.DT_ActRefMan_OutItemRefMan()
                                {
                                    Cantidad = itemSP.Quantity.ToString(),
                                    Item_Ref_Man = itemSP.RefManID,
                                    NumeroPosicion = itemSP.PosicionItem == "0" ? "" : itemSP.PosicionItem,
                                    EstatusItem_RefMan = itemSP.SparePartStatus,
                                    MotivoRechazo = motivoR,
                                    NumeroFactura = "",
                                    Proveedor = ""
                                });
                            }
                        }
                    }
                    else
                    {
                        foreach (var itemSP in dataSparePart)
                        {
                            string motivoR = "";
                            switch (itemSP.SparePartStatus)
                            {
                                case "E0001":
                                    motivoR = "";
                                    break;
                                case "E0003":
                                    motivoR = "04";
                                    break;
                                case "E0004":
                                    motivoR = (dataVisit.FK_CauseOrderID == 39) || (dataVisit.FK_CauseOrderID == 42) || (dataVisit.FK_CauseOrderID == 40) || (dataVisit.FK_CauseOrderID == 41) ? "04" : "";
                                    break;
                                case "E0006":
                                    motivoR = "";
                                    break;
                            }

                            itemRefManDetail.Add(new srUpdateRefMan.DT_ActRefMan_OutItemRefMan()
                            {
                                Cantidad = itemSP.Quantity.ToString(),
                                Item_Ref_Man = itemSP.RefManID,
                                NumeroPosicion = itemSP.PosicionItem == "0" ? "" : itemSP.PosicionItem,
                                EstatusItem_RefMan = itemSP.SparePartStatus,
                                MotivoRechazo = motivoR,
                                NumeroFactura = "",
                                Proveedor = ""
                            });
                        }
                    }
                }


                itemRefManHeader.IDOrden = dataODS.OrderID;
                itemRefManHeader.RefMan = itemRefManDetail.ToArray();
                itemsRefMan.Add(itemRefManHeader);

                Console.WriteLine(String.Format("Procesando RefMan ODS {0}", ods));

                StatusRef = new BusinessMabe().UpdateRefMan(itemsRefMan);
            }
            catch (Exception ex)
            {
                new BusinessLogger().WriteError(ex, "UpdateRefMan ODS " + ods);
            }

            if (StatusRef)
            {

                try
                {
                    //actualización de orden de servicio
                    List<srUpdateODS.DT_ActulizaODS_OutItem> itemODSList = new List<srUpdateODS.DT_ActulizaODS_OutItem>();

                    srUpdateODS.DT_ActulizaODS_OutItem itemODSHeader = new srUpdateODS.DT_ActulizaODS_OutItem();

                    List<srUpdateODS.DT_ActulizaODS_OutItemNotas> arrNotas = new List<srUpdateODS.DT_ActulizaODS_OutItemNotas>();

                    string Id_Via_de_Pago = "";
                    string Texto_para_via_pago = "";

                    var dataPayment = new BusinessPayment().GetByOrderID(dataODS.PK_OrderID);
                    if (dataPayment.OrderID != "")
                    {
                        switch (dataPayment.TypePaymentID)
                        {
                            case "Credit card":
                                Id_Via_de_Pago = "X";
                                Texto_para_via_pago = "TARJETA DE CREDITO";
                                break;
                            case "Debit card":
                                Id_Via_de_Pago = "R";
                                Texto_para_via_pago = "TARJETA DE DEBITO";
                                break;
                            case "Cash":
                                Id_Via_de_Pago = "W";
                                Texto_para_via_pago = "EFECTIVO";
                                break;
                        }
                    }

                    arrNotas.Add(new srUpdateODS.DT_ActulizaODS_OutItemNotas() { Idioma = "ES", Clase_de_texto = "", Texto = dataVisit.NoteOrder + " / " + dataVisit.NoteVisit });
                    itemODSHeader.Estatus_Item_Visita = dataODS.FK_StatusSchemeID.HasValue ? new BusinessStatusScheme().Get(dataODS.FK_StatusSchemeID.Value).StatusScheme1 : "";
                    itemODSHeader.Falla_1 = dataODS.Failure1;
                    itemODSHeader.Falla_2 = dataODS.Failure2;
                    itemODSHeader.Falla_3 = dataODS.Failure3;
                    itemODSHeader.Falla_4 = dataODS.Failure4;
                    itemODSHeader.Falla_5 = dataODS.Failure5;
                    itemODSHeader.ID_Operacion = dataODS.OrderID;
                    itemODSHeader.Notas = arrNotas.ToArray();
                    itemODSHeader.Id_Via_de_Pago = Id_Via_de_Pago;
                    itemODSHeader.Texto_para_via_pago = Texto_para_via_pago;
                    itemODSHeader.Tipo_Serv = dataODS.FK_GuarantyID.HasValue ? new BusinessGuaranty().Get(dataODS.FK_GuarantyID.Value).GuarantyID : "";
       
                    if (dataODS.OrderID.StartsWith("95"))
                    {
                        itemODSHeader.Monto_Cobrado = dataPayment.MountPayment.ToString();
                    }
                    itemODSList.Add(itemODSHeader);

                    string msg = "";
                    msg += "-------------------------Inicio Envio Datos ODS a CRM-------------------------" + Environment.NewLine;

                    msg += string.Format("Estatus_Item_Visita: {0}", dataODS.FK_StatusSchemeID.HasValue ? new BusinessStatusScheme().Get(dataODS.FK_StatusSchemeID.Value).StatusScheme1 : "") + Environment.NewLine;
                    msg += string.Format("Falla_1: {0}", dataODS.Failure1) + Environment.NewLine;
                    msg += string.Format("Falla_2: {0}", dataODS.Failure2) + Environment.NewLine;
                    msg += string.Format("Falla_3: {0}", dataODS.Failure3) + Environment.NewLine;
                    msg += string.Format("Falla_4: {0}", dataODS.Failure4) + Environment.NewLine;
                    msg += string.Format("Falla_5: {0}", dataODS.Failure5) + Environment.NewLine;
                    msg += string.Format("ID_Operacion: {0}", dataODS.OrderID) + Environment.NewLine;
                    msg += string.Format("Notas: {0}", dataVisit.NoteOrder + " / " + dataVisit.NoteVisit) + Environment.NewLine;
                    msg += string.Format("Id_Via_de_Pago: {0}", Id_Via_de_Pago) + Environment.NewLine;
                    msg += string.Format("Texto_para_via_pago: {0}", Texto_para_via_pago) + Environment.NewLine;
                    msg += string.Format("Tipo_Serv: {0}", dataODS.FK_GuarantyID.HasValue ? new BusinessGuaranty().Get(dataODS.FK_GuarantyID.Value).GuarantyID : "") + Environment.NewLine;
                    msg += string.Format("Monto_Cobrado: {0}", dataPayment.MountPayment) + Environment.NewLine;
                    msg += "-------------------------FIN Envio Datos ODS a CRM-------------------------" + Environment.NewLine;
                    new BusinessLogger().WriteEntry(msg);

                    Console.WriteLine(String.Format("Procesando ODS {0}", ods));

                    new BusinessMabe().UpdateODS(itemODSList, dataODS.OrderID);
                }
                catch (Exception ex)
                {
                    new BusinessLogger().WriteError(ex, "UpdateODS ODS " + ods );
                }
            }
            else
            {
                new BusinessLogger().WriteEntry("StatusRef no se actualizo " + ods);
            }

            Console.WriteLine(String.Format("Terminando ODS {0}", ods));
        }

        public void ProcessODS(DateTime fh, int MaxProcess, bool reintent)
        {
            Console.WriteLine("Inicio " + DateTime.Now.ToString());
            List<Task> arr = new List<Task>();
            int cont = 0;
            foreach (var item in new BusinessOrder().GetAll("Pendiente", fh))
            {
                bool extraKM = item.ExtraKilometres.HasValue ? item.ExtraKilometres.Value : false;
                
                if (extraKM)
                {
                    item.SendCRM = "PendienteKM";
                    new BusinessOrder().Update(item);
                    continue;
                }
                if (item.PreOrder == true) continue; 
                item.SendCRM = "Enviando";
                new BusinessOrder().Update(item);

                arr.Add(Task.Factory.StartNew(() => ProcessODS(item.OrderID)));

                if (cont >= MaxProcess) break;
                cont++;
            }

            Task.WaitAll(arr.ToArray());

            if (ProcessExtraKM)
            {
                arr = new List<Task>();
                foreach (var item in new BusinessOrder().GetAll("PendienteKM", fh))
                {
                    item.SendCRM = "Enviando";
                    new BusinessOrder().Update(item);

                    arr.Add(Task.Factory.StartNew(() => ProcessODS(item.OrderID)));
                }
                Task.WaitAll(arr.ToArray());
            }                       
           
            if (reintent)
            {
                if (new BusinessOrder().GetAll("Enviando", fh).Count() > 0)
                {
                    Console.WriteLine("Reenviando perdidos " + DateTime.Now.ToString());

                    Task.Delay(new TimeSpan(0, 5, 0));

                    arr = new List<Task>();
                    foreach (var item in new BusinessOrder().GetAll("Enviando", fh))
                    {
                        arr.Add(Task.Factory.StartNew(() => ProcessODS(item.OrderID)));
                    }

                    Task.WaitAll(arr.ToArray());

                    foreach (var item in new BusinessOrder().GetAll("Enviando", fh))
                    {
                        if (item.PreOrder == true) continue;
                        item.SendCRM = "Error";
                        new BusinessOrder().Update(item);
                    }
                }
            }
            else
            {
                foreach (var item in new BusinessOrder().GetAll("Enviando", fh))
                {
                    if (item.PreOrder == true) continue;
                    item.SendCRM = "Error";
                    new BusinessOrder().Update(item);
                }
            }
            Console.WriteLine("Fin " + DateTime.Now.ToString());
        }
    }
}
