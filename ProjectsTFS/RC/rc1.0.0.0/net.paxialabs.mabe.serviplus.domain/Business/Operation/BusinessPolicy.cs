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
using net.paxialabs.mabe.serviplus.entities.ModelView.Security;

namespace net.paxialabs.mabe.serviplus.domain.Business.Operation
{
    
    internal class BusinessPolicy
    {

        public List<EntityPolicy> GetAll()
        {
            return new RepositoryPolicy().GetAll();
        }

   
        public EntityPolicy Insert(EntityPolicy Poliza)
        {
            var objRepository = new RepositoryPolicy();
            EntityPolicy data = new EntityPolicy()
            {
                PK_PolicyID = 0,
                FK_OrderID = Poliza.FK_OrderID,
                FK_ClientID = Poliza.FK_ClientID,
                FK_EmployeeID = Poliza.FK_EmployeeID,
                FK_QuotationID = Poliza.FK_QuotationID,
                FK_ProductID = Poliza.FK_ProductID,
                FK_ShopPlace=Poliza.FK_ShopPlace,
                IDPolicy = Poliza.IDPolicy,
                PolicyPrice = Poliza.PolicyPrice,
                Coin = Poliza.Coin,
                SalesOrg = Poliza.SalesOrg,
                ccGroup = Poliza.ccGroup,
                SerialNumber=Poliza.SerialNumber,
                PriceList=Poliza.PriceList,
                MaterialGroup4=Poliza.MaterialGroup4,
                GuarantyStart = Poliza.GuarantyStart,
                GuarantyEnd = Poliza.GuarantyEnd,
                PolicyDate = Poliza.PolicyDate,
                Status = true,
                CreateDate = DateTime.UtcNow.ToLocalTime(),
                ModifyDate = DateTime.UtcNow.ToLocalTime()

            };
             data = objRepository.Insert(data);
            return data;
        }
        public EntityPolicy GetByOrder(int OrderID)
        {
            return new RepositoryPolicy().GetByOrder(OrderID);
        }
        public EntityPolicy GetByFolio(int OrderID, string Folio)
        {
            var data = new RepositoryPolicy().GetPolicyFolio(OrderID, Folio);

            if (data != null)
                return new EntityPolicy()
                {
                    PK_PolicyID = data.PK_PolicyID,
                    FK_OrderID = data.FK_OrderID,
                    FK_ClientID = data.FK_ClientID,
                    FK_EmployeeID = data.FK_EmployeeID,
                    FK_PaymentID = data.FK_PaymentID,
                    FK_Invoice_ID = data.FK_Invoice_ID,
                    FK_QuotationID = data.FK_QuotationID,
                    FK_ProductID = data.FK_ProductID,
                    FK_ShopPlace = data.FK_ShopPlace,
                    IDPolicy = data.IDPolicy,
                    PolicyPrice = data.PolicyPrice,
                    Coin = data.Coin,
                    SalesOrg = data.SalesOrg,
                    ccGroup = data.ccGroup,
                    PriceList = data.PriceList,
                    SerialNumber = data.SerialNumber,
                    MaterialGroup4 = data.MaterialGroup4,
                    GuarantyEnd=data.GuarantyEnd,
                    GuarantyStart=data.GuarantyStart,
                    PolicyDate=data.PolicyDate,
                    Status=data.Status,
                    CreateDate=data.CreateDate,
                    ModifyDate=data.ModifyDate 
    };
            else
                return new EntityPolicy();
        }
        public EntityPolicy Update(EntityPolicy data)
        {
          return new RepositoryPolicy().Update(data);
        }

        public List<EntityPolicy> GetByEmpoyeeDate(int UserID, DateTime date)
        {
            return new RepositoryPolicy().GetByEmpoyeeDate(UserID, date);
        }

        public string SetPolicyQuotation(ModelViewUserG objCred, ModelViewBilling obj)
        {
            var NegocioOrders = new BusinessOrder();

            var objAlerta = new BusinessNotification();
            var NegocioUsuario = new BusinessUsers();
            var NegocioCliente = new BusinessClient();
            var NegocioProducto = new BusinessProduct();
            var NegocioCotizacion = new BusinessQuotation();
            var NegocioEmpleado = new BusinessEmployee();
            var NegocioMonitor = new BusinessVisit();
            var NegocioPayment = new BusinessPayment();
            var NegocioInvoice = new BusinessInvoice();

            var dataUsuario = NegocioUsuario.GetUserByToken(objCred.TokenUser);
            if (objCred.TokenApp != GlobalConfiguration.TokenWEB)
                if (objCred.TokenApp != GlobalConfiguration.TokenMobile)
                    throw new Exception("TokenInvalid");
            if (dataUsuario == null) throw new Exception("UserPasswordInvalid");

            var empleado = NegocioEmpleado.GetByUserID(dataUsuario.UserID);
            var orden = NegocioOrders.GetByOrderID(obj.ODS);
            var cliente = NegocioCliente.GetByID(orden.FK_ClientID);
            var pro = NegocioProducto.GetByID(obj.FK_ProductID.Value);
            string Folio = obj.Policies.Folio;
            var cotizacion = NegocioCotizacion.GetByOrderFolio(orden.PK_OrderID, Folio);
            var Payment = NegocioPayment.GetPolicyPayment(orden.PK_OrderID, Folio);
            var Invoice = NegocioInvoice.GetPolicyInvoice(orden.PK_OrderID, Folio);
            var LsPolicy = GetByFolio(orden.PK_OrderID,Folio);
            SpreadsheetInfo.SetLicense("EQU2-3K5L-UZDC-SDYN");
            string Origin = GlobalConfiguration.MabeAttachmentsLocal + "FormatoPolizas.xlsx";
            List<string> result = obj.EMails.Split(new char[] { ';' }).ToList();
            ExcelFile ef = ExcelFile.Load(Origin);
            ExcelWorksheet ws = ef.Worksheets[0];

            ws.Cells["L6"].Value = Folio;
            //ws.Cells["L7"].Value = obj.ODS;
            ws.Cells["L9"].Value = cliente.ClientID;
            ws.Cells["L11"].Value = cliente.FirstName.ToUpper() + " " + cliente.LastName.ToUpper();
            ws.Cells["L13"].Value = pro.Model + " / " + pro.ProductName.ToUpper();
            ws.Cells["U13"].Value = DateTime.Today.ToString("dd-MM-yyyy");
            ws.Cells["L79"].Value = Folio;
            //ws.Cells["L80"].Value = obj.ODS;

            int cantidad = 30;
            int cantidad1 = 31;

            ws.Cells["L" + cantidad1.ToString()].Value = obj.Policies.PolicyDescription;
            ws.Cells["L" + cantidad.ToString()].Value = "Venta de Poliza";
            ws.Cells["J" + cantidad.ToString()].Value = 1;
            ws.Cells["V" + cantidad.ToString()].Value = Convert.ToDouble(obj.SubTotal);

            double subtotal = Convert.ToDouble(obj.SubTotal.Substring(0));
            double iva = Convert.ToDouble(obj.IVA.Substring(0));
            double total = Convert.ToDouble(obj.Total.Substring(0));
            double subtotalref = subtotal;
            string totalletras = NegocioOrders.enletras(total.ToString());
            ws.Cells["M17"].Value = subtotal;
            ws.Cells["M18"].Value = iva;
            ws.Cells["U16"].Value = total;
            ws.Cells["M19"].Value = totalletras;
            string file = "CotizaciónPoliza_" + Folio + ".pdf";
            string quotion = "CotizacionesPoliza_" + DateTime.Today.ToString("yyyyMMdd");
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
                
                if (cotizacion == null)
                {
                    cotizacion = NegocioCotizacion.Insert(orden.PK_OrderID, subtotal.ToString(), iva.ToString(), total.ToString(), Folio, URL, obj.EstimatedTipe, empleado[0].PK_EmployeeID);

                }
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
                string sb = File.ReadAllText(GlobalConfiguration.LocateBodyMail + "NotificationPoliza.txt");
                string lugCom = obj.ShopDate != null ? obj.ShopDate : "";
                sb = sb.Replace("#%Nombre%#", cliente.FirstName + " " + cliente.LastName);
                sb = sb.Replace("#%OrderID%#", obj.ODS);
                sb = sb.Replace("#%Folio%#", Folio);
                sb = sb.Replace("#%Modelo%#", pro.Model);
                sb = sb.Replace("#%Descripcion%#", pro.ProductName.ToUpper());
                sb = sb.Replace("#%Fecha%#", obj.Policies.PolicyDate);
                sb = sb.Replace("#%Vigencia1%#", obj.Policies.PolicyDescription);
                sb = sb.Replace("#%Precio%#", obj.Total);
                objAlerta.SendMailExchange(GlobalConfiguration.exchangeUserCotiza, GlobalConfiguration.exchangePwdCotiza, result, "Cotización ServiPlus", sb.ToString(), Destiny);
            }
            else {

                if (LsPolicy.PK_PolicyID == 0)
                {
                    if (cotizacion == null)
                    {
                        cotizacion = NegocioCotizacion.Insert(orden.PK_OrderID, subtotal.ToString(), iva.ToString(), total.ToString(), Folio, URL, obj.EstimatedTipe, empleado[0].PK_EmployeeID);

                    }
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
                    var NewPolicy = new EntityPolicy();
                    var NuevaPoliza = new EntityPolicy();

                    NewPolicy.FK_OrderID = orden.PK_OrderID;
                    NewPolicy.FK_ClientID = cliente.PK_ClientID;
                    NewPolicy.FK_EmployeeID = empleado[0].PK_EmployeeID;
                    NewPolicy.FK_PaymentID = Payment.PK_PaymentID;
                    NewPolicy.FK_Invoice_ID = Invoice.InvoicingID;
                    NewPolicy.FK_QuotationID = cotizacion.PK_QuotationID;
                    NewPolicy.FK_ProductID = pro.PK_ProductID;
                    NewPolicy.FK_ShopPlace = obj.FK_ShopPlaceID.Value;
                    NewPolicy.IDPolicy = obj.Policies.Folio;
                    NewPolicy.SerialNumber = obj.SerialNumber;
                    NewPolicy.PriceList = obj.Policies.PriceList;
                    NewPolicy.PolicyPrice = obj.Policies.PolicyPrice;
                    NewPolicy.Coin = obj.Policies.Coin;
                    NewPolicy.SalesOrg = obj.Policies.SalesOrg;
                    NewPolicy.ccGroup = obj.Policies.ccGroup;
                    NewPolicy.MaterialGroup4 = obj.Policies.MaterialGroup4;
                    NewPolicy.GuarantyStart = Convert.ToDateTime(obj.Policies.GuarantyStart);
                    NewPolicy.GuarantyEnd = Convert.ToDateTime(obj.Policies.GuarantyEnd);
                    NewPolicy.PolicyDate = obj.Policies.PolicyDate;

                    NuevaPoliza = Insert(NewPolicy);
                }
                else
                {
                    LsPolicy.FK_OrderID = orden.PK_OrderID;
                    LsPolicy.FK_ClientID = cliente.PK_ClientID;
                    LsPolicy.FK_EmployeeID = empleado[0].PK_EmployeeID;
                    LsPolicy.FK_PaymentID = Payment.PK_PaymentID;
                    LsPolicy.FK_Invoice_ID = Invoice.InvoicingID;
                    LsPolicy.FK_QuotationID = LsPolicy.FK_QuotationID;
                    LsPolicy.FK_ProductID = obj.Policies.ProductID;
                    LsPolicy.FK_ShopPlace = obj.FK_ShopPlaceID.Value;
                    LsPolicy.IDPolicy = obj.Policies.Folio;
                    LsPolicy.PolicyPrice = obj.Policies.PolicyPrice;
                    LsPolicy.Coin = obj.Policies.Coin;
                    LsPolicy.SalesOrg = obj.Policies.SalesOrg;
                    LsPolicy.PriceList = obj.Policies.PriceList;
                    LsPolicy.SerialNumber = obj.SerialNumber;
                    LsPolicy.ccGroup = obj.Policies.ccGroup;
                    LsPolicy.MaterialGroup4 = obj.Policies.MaterialGroup4;
                    LsPolicy.GuarantyEnd = Convert.ToDateTime(obj.Policies.GuarantyEnd);
                    LsPolicy.GuarantyStart = Convert.ToDateTime(obj.Policies.GuarantyStart);
                    LsPolicy.PolicyDate = obj.Policies.PolicyDate;

                    Update(LsPolicy);
                    if (cotizacion == null)
                    {
                        cotizacion = NegocioCotizacion.Insert(orden.PK_OrderID, subtotal.ToString(), iva.ToString(), total.ToString(), Folio, URL, obj.EstimatedTipe, empleado[0].PK_EmployeeID);

                    }
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
                var Refacciones = new BusinessMabe().Orden_Venta(obj.ODS, obj.ServiceTypes, Folio);
            } 
                          


            return obj.ODS;
            
            
        }

        public string CreateContract(ModelViewContrat obj)
        {
            var NegocioOrden = new BusinessOrder();
            var NegocioCliente = new BusinessClient();
            var NegocioContrato = new BusinessContrat();
            var objAlerta = new BusinessNotification();
            var Cliente = NegocioCliente.GetByClientID(obj.NumbClient);
            SpreadsheetInfo.SetLicense("EQU2-3K5L-UZDC-SDYN");
            string Origin = GlobalConfiguration.MabeAttachmentsLocal + "FormatoContrato.xlsx";
            List<string> result = obj.EMails.Split(new char[] { ';' }).ToList();
            
            ExcelFile ef = ExcelFile.Load(Origin);
            ExcelWorksheet ws = ef.Worksheets[0];

            ws.Cells["N15"].Value = obj.Folio;
            //ws.Cells["L7"].Value = obj.ODS;
            ws.Cells["M20"].Value = obj.Period;
            ws.Cells["M21"].Value = obj.StartDate;
            ws.Cells["T21"].Value = obj.EndDate;
            ws.Cells["M30"].Value = obj.NumbClient;
            ws.Cells["M31"].Value = Cliente.FirstName + " " + Cliente.LastName.ToUpper();
            ws.Cells["M32"].Value = Cliente.StreetAddress;
            ws.Cells["M33"].Value = Cliente.BoroughAddress;
            ws.Cells["M34"].Value = Cliente.MunicipalityAddress;
            ws.Cells["M35"].Value = Cliente.PostalCodeAddress;
            ws.Cells["M36"].Value = obj.Town;
            ws.Cells["M37"].Value = Cliente.PhoneNumber1;
            ws.Cells["M38"].Value = obj.Totals;
            ws.Cells["M48"].Value = obj.Model;
            ws.Cells["M49"].Value = obj.Description;
            ws.Cells["M50"].Value = obj.Serie;
            ws.Cells["R145"].Value = obj.Town;
            ws.Cells["V145"].Value = DateTime.Today.ToString("yyyyMMdd");
                                 

            string file = "ContratoPoliza" + obj.Folio + ".pdf";
            string quotion = "Polizas_" + DateTime.Today.ToString("yyyyMMdd");
            string Destiny = GlobalConfiguration.MabeAttachmentsLocal + quotion + "/" + file;

            string Cotizaciones = new DirectoryInfo(GlobalConfiguration.MabeAttachmentsLocal).ToString() + quotion;
            if (!(Directory.Exists(Cotizaciones)))
            {
                Directory.CreateDirectory(Cotizaciones);
            }
            ef.Save(Destiny);
            string URL = GlobalConfiguration.urlRequest + "Content/Attachments/" + quotion + "/" + file;
            var orden = NegocioOrden.GetByOrderID(obj.FK_OrderID);
            var Contrato = NegocioContrato.GetByOrderFolio(orden.PK_OrderID, obj.Folio);
            if (obj.EMails != "")
            {

                if (Contrato == null)
                {
                    Contrato = NegocioContrato.Insert(orden.PK_OrderID, obj.Folio, URL);

                }
                else
                {
                    Contrato.Fk_OrderID = orden.PK_OrderID;
                    Contrato.Folio = obj.Folio;
                    Contrato.Ruta = URL;
                    NegocioContrato.Update(Contrato);

                }
                string sb = File.ReadAllText(GlobalConfiguration.LocateBodyMail + "MailContratoPoliza.txt");
                
                sb = sb.Replace("#%Nombre%#", Cliente.FirstName + " " + Cliente.LastName);
                sb = sb.Replace("#%Folio%#", obj.Folio);
                sb = sb.Replace("#%Modelo%#", obj.Model);
                sb = sb.Replace("#%Descripcion%#", obj.Description.ToUpper());
                sb = sb.Replace("#%Serie%#", obj.Serie);
                sb = sb.Replace("#%Compra%#", DateTime.Today.ToString("dd/MM/yyyy"));
                
                objAlerta.SendMailExchange(GlobalConfiguration.exchangeUserCotiza, GlobalConfiguration.exchangePwdCotiza, result, "Contrato Póliza", sb.ToString(), Destiny);
            }
            else
            {
                string Tipo = "Poliza";
                var EnvioSMS = SendNotification(obj.PhoneNumber,Tipo,URL);

                return EnvioSMS;
            }

            return URL;

        }


        public string CreateReceipt (ModelViewUserG objCred,ModelViewReceiptRef obj)
        {
            var objRepository = new RepositoryOrder();
            var NegocioOrden = new BusinessOrder();
            var objAlerta = new BusinessNotification();
            var NegocioUsuario = new BusinessUsers();
            var NegocioCliente = new BusinessClient();
            var NegocioContrato = new BusinessContrat();
            var NegocioEmpleado = new BusinessEmployee();
            var NegocioBOM = new BusinessBuildOfMaterial();

            var dataUsuario = NegocioUsuario.GetUserByToken(objCred.TokenUser);
            if (objCred.TokenApp != GlobalConfiguration.TokenWEB)
                if (objCred.TokenApp != GlobalConfiguration.TokenMobile)
                    throw new Exception("TokenInvalid");
            if (dataUsuario == null) throw new Exception("UserPasswordInvalid");


            var empleado = NegocioEmpleado.GetByUserID(dataUsuario.UserID);
            var orden = NegocioOrden.GetByOrderID(obj.FK_OrderID);
            var cliente = NegocioCliente.GetByID(orden.FK_ClientID);
              
            
            SpreadsheetInfo.SetLicense("EQU2-3K5L-UZDC-SDYN");
            string Origin = GlobalConfiguration.MabeAttachmentsLocal + "FormatoReciboRef.xlsx";
            List<string> result = obj.EMails.Split(new char[] { ';' }).ToList();
            ExcelFile ef = ExcelFile.Load(Origin);
            ExcelWorksheet ws = ef.Worksheets[0];
            

            ws.Cells["W9"].Value = obj.Folio;
            ws.Cells["W12"].Value = DateTime.Today.ToString("dd-MM-yyyy");
            ws.Cells["L18"].Value = cliente.FirstName.ToUpper();
            ws.Cells["P18"].Value = cliente.LastName.ToUpper();
            ws.Cells["L21"].Value = cliente.PhoneNumber1;
            ws.Cells["T21"].Value = cliente.PhoneNumber2;
            ws.Cells["M23"].Value = cliente.Email;

            
            int cantidad = 29;
            

            foreach (var jko in obj.SpareParts)
            {
              
              
                    if (cantidad == 60)
                    {
                        cantidad = 85;
                        
                    }
                    ////var bom = NegocioBOM.GetByID(jko.ProductID);
                    //if (bom != null)
                    //{
                    //    ws.Cells["L" + cantidad1.ToString()].Value = bom.SparePartsID;
                    //    ws.Cells["L" + cantidad.ToString()].Value = bom.SparePartDescription;
                    //}
                    else
                    {
                        ws.Cells["K" + cantidad.ToString()].Value = jko.RefManID;
                        ws.Cells["M" + cantidad.ToString()].Value = jko.SparePartsDescription;
                    }
                    ws.Cells["R" + cantidad.ToString()].Value = jko.Quantity;
                    ws.Cells["U" + cantidad.ToString()].Value = Convert.ToDouble(jko.Price);
                    ws.Cells["W" + cantidad.ToString()].Value = Convert.ToDouble(jko.Totals);
                    cantidad = cantidad + 1;
                    
              
            }

            double subtotal = Convert.ToDouble(obj.SubTotal);
            double iva = Convert.ToDouble(obj.IVA);
            double total = Convert.ToDouble(obj.Total);
            double subtotalref = subtotal;
            string totalletras = NegocioOrden.enletras(total.ToString());

            ws.Cells["W41"].Value = subtotalref;
            
            ws.Cells["W42"].Value = iva;
            ws.Cells["W43"].Value = total;
          
            string file = "ReciboRef" + obj.Folio + ".pdf";
            string quotion = "RecibosRefaccion_" + DateTime.Today.ToString("yyyyMMdd");
            string Destiny = GlobalConfiguration.MabeAttachmentsLocal + quotion + "/" + file;
            if (obj.SpareParts.Count < 11)
            { ws.NamedRanges.SetPrintArea(ws.Cells.GetSubrange("J1", "X54")); }
            string Cotizaciones = new DirectoryInfo(GlobalConfiguration.MabeAttachmentsLocal).ToString() + quotion;
            if (!(Directory.Exists(Cotizaciones)))
            {
                Directory.CreateDirectory(Cotizaciones);
            }
            ef.Save(Destiny);
            string URL = GlobalConfiguration.urlRequest + "Content/Attachments/" + quotion + "/" + file;
            var Recibo = NegocioContrato.GetByOrderFolio(orden.PK_OrderID, obj.Folio);



            if (obj.EMails != "")
            {
                if (Recibo == null)
                {
                    Recibo = NegocioContrato.Insert(orden.PK_OrderID, obj.Folio, URL);

                }
                else
                {
                    Recibo.Fk_OrderID = orden.PK_OrderID;
                    Recibo.Folio = obj.Folio;
                    Recibo.Ruta = URL;
                    NegocioContrato.Update(Recibo);

                }

                string sb = File.ReadAllText(GlobalConfiguration.LocateBodyMail + "MaiReciboRef.txt");
                sb = sb.Replace("#%Nombre%#", cliente.FirstName + " " + cliente.LastName);
                sb = sb.Replace("#%Folio%#", obj.Folio);
              
                objAlerta.SendMailExchange(GlobalConfiguration.exchangeUserCotiza, GlobalConfiguration.exchangePwdCotiza, result, "Recibo Refacciones", sb.ToString(), Destiny);
                return URL;
            }
            else
            {
                string Tipo = "Recibo";
                var EnvioSMS = SendNotification(obj.PhoneNumber, Tipo,URL);

                return EnvioSMS;             

            }
            
        }
        public string SetRefQuotation(ModelViewUserG objCred, ModelViewBilling obj)
        {
            var objRepository = new RepositoryOrder();
            var NegocioOrden = new BusinessOrder();
            var objAlerta = new BusinessNotification();
            var NegocioUsuario = new BusinessUsers();
            var NegocioCliente = new BusinessClient();
            var NegocioBase = new BusinessInstalledBase();
            var NegocioProducto = new BusinessProduct();
            var NegocioFallas = new BusinessCodeFailure();
            var NegocioBOM = new BusinessBuildOfMaterial();
            var NegocioCotizacion = new BusinessQuotation();
            var NegocioCotizacionDetalle = new BusinessRefsellDetail();
            var NegocioCFP = new BusinessCodeFailureByProduct();
            var NegocioCF = new BusinessCodeFailure();
            var NegocioLugarCompra = new BusinessShopPlace();
            var NegocioEmpleado = new BusinessEmployee();
            var NegocioMonitor = new BusinessVisit();
            var NegocioHistorico = new BusinessHistory();
            var NegocioGarantia = new BusinessGuaranty();
            var NegocioPayment = new BusinessPayment();
            var NegocioInvoice = new BusinessInvoice();
            var NegocioRefsell = new BusinessRefsell();
            var dataUsuario = NegocioUsuario.GetUserByToken(objCred.TokenUser);
            if (objCred.TokenApp != GlobalConfiguration.TokenWEB)
                if (objCred.TokenApp != GlobalConfiguration.TokenMobile)
                    throw new Exception("TokenInvalid");
            if (dataUsuario == null) throw new Exception("UserPasswordInvalid");

            var empleado = NegocioEmpleado.GetByUserID(dataUsuario.UserID);
            var orden = NegocioOrden.GetByOrderID(obj.ODS);

            
            var cliente = NegocioCliente.GetByID(orden.FK_ClientID);
          
            var lugarcompra = NegocioLugarCompra.GetByShopPlaceID(obj.FK_ShopPlaceID.Value);
            var pro = NegocioProducto.GetByID(obj.FK_ProductID.Value);
                       
            SpreadsheetInfo.SetLicense("EQU2-3K5L-UZDC-SDYN");
            string Origin = GlobalConfiguration.MabeAttachmentsLocal + "FormatoVentaRefacciones.xlsx";
            List<string> result = obj.EMails.Split(new char[] { ';' }).ToList();
            ExcelFile ef = ExcelFile.Load(Origin);
            ExcelWorksheet ws = ef.Worksheets[0];
            string Folio = "";

            ws.Cells["L7"].Value = obj.ODS;
            ws.Cells["L9"].Value = cliente.ClientID;
            ws.Cells["L11"].Value = cliente.FirstName.ToUpper() + " " + cliente.LastName.ToUpper();
            ws.Cells["L13"].Value = pro.Model + " / " + pro.ProductName.ToUpper();
            ws.Cells["U13"].Value = DateTime.Today.ToString("dd-MM-yyyy");

            ws.Cells["L80"].Value = obj.ODS;
            int cantidad = 30;
            int cantidad1 = 31;

            foreach (var jko in obj.BillingDetails)
            {
                if (jko.SparePartsDescription.EndsWith("-R"))
                {
                    
                    Folio = jko.SparePartsDescription;
                    ws.Cells["L6"].Value = Folio;
                    ws.Cells["L79"].Value = Folio;
                    
                }
                else
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
                        ws.Cells["L" + cantidad1.ToString()].Value = jko.RefManID;
                        ws.Cells["L" + cantidad.ToString()].Value = jko.SparePartsDescription;
                    }
                    ws.Cells["J" + cantidad.ToString()].Value = jko.Quantity;
                    ws.Cells["T" + cantidad.ToString()].Value = Convert.ToDouble(jko.Price);
                    ws.Cells["V" + cantidad.ToString()].Value = Convert.ToDouble(jko.Totals);
                    cantidad = cantidad + 3;
                    cantidad1 = cantidad1 + 3;
                }
            }
                                                
            double subtotal = Convert.ToDouble(obj.SubTotal.Substring(1));
            double iva = Convert.ToDouble(obj.IVA.Substring(1));
            double total = Convert.ToDouble(obj.Total.Substring(1));
            double subtotalref = subtotal;
            string totalletras = NegocioOrden.enletras(total.ToString());
            
            ws.Cells["M16"].Value = subtotalref;
            ws.Cells["M17"].Value = subtotal;
            ws.Cells["M18"].Value = iva;
            ws.Cells["U17"].Value = total;
            ws.Cells["M19"].Value = totalletras;
            string file = "Cotización_" + Folio + ".pdf";
            string quotion = "CotizacionesRefaccion_" + DateTime.Today.ToString("yyyyMMdd");
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
            var cotizacion = NegocioCotizacion.GetByOrderFolio(orden.PK_OrderID, Folio);
            var Payment = NegocioPayment.GetPolicyPayment(orden.PK_OrderID, Folio);
            var Invoice = NegocioInvoice.GetPolicyInvoice(orden.PK_OrderID, Folio);
              
            
            var LsVenta = NegocioRefsell.GetByFolio(orden.PK_OrderID,Folio);

            if (obj.EMails != "")
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
            }
            else {
                
                if (LsVenta.PK_RefSellID == 0)
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
                    var NewRefSell = new EntityRefSell();
                    var NuevaVenta = new EntityRefSell();

                    NewRefSell.FK_OrderID = orden.PK_OrderID;
                    NewRefSell.FK_ClientID = cliente.PK_ClientID;
                    NewRefSell.FK_EmployeeID = empleado[0].PK_EmployeeID;
                    NewRefSell.FK_PaymentID = Payment.PK_PaymentID;
                    NewRefSell.FK_Invoice_ID = Invoice.InvoicingID;
                    NewRefSell.FK_ProductID = obj.FK_ProductID.Value;
                    NewRefSell.FK_ShopPlace = obj.FK_ShopPlaceID.Value;
                    NewRefSell.FK_QuotationID = cotizacion.PK_QuotationID;
                    NewRefSell.IDRefSell = Folio;
                    NuevaVenta = NegocioRefsell.Insert(NewRefSell);
                }
                else
                {
                    LsVenta.FK_OrderID = orden.PK_OrderID;
                    LsVenta.FK_ClientID = cliente.PK_ClientID;
                    LsVenta.FK_EmployeeID = empleado[0].PK_EmployeeID;
                    LsVenta.FK_PaymentID = Payment.PK_PaymentID;
                    LsVenta.FK_Invoice_ID = Invoice.InvoicingID;
                    LsVenta.FK_QuotationID = LsVenta.FK_QuotationID;
                    LsVenta.FK_ProductID = obj.FK_ProductID.Value;
                    LsVenta.FK_ShopPlace = obj.FK_ShopPlaceID.Value;
                    
                    NegocioRefsell.Update(LsVenta);

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
                string[] lstADR = { "T002", "T012", "T024" };

                foreach (var jko in obj.BillingDetails)
                {
                    string ADRAnt = "";
                    int ADRCount = 0;
                    bool AddFlete = false;

                    if (jko.SparePartsDescription != Folio)
                    {
                        switch (jko.SparePartsDescription)
                        {
                            case "Fletes":
                                { 
                               var DetailCotation2 = NegocioCotizacionDetalle.GetList(cotizacion.PK_QuotationID);
                                    
                                    foreach( var items in DetailCotation2)
                                    {
                                        if (lstADR.Contains(items.Origen))
                                        {
                                            if(ADRAnt != items.Origen)
                                            {
                                                ADRAnt = items.Origen;
                                                ADRCount++;
                                                if (ADRCount < 3)
                                                    AddFlete = true;
                                                else
                                                    AddFlete = false;
                                            }
                                            if (AddFlete)
                                            {
                                                items.Flete = true;
                                                items.CostoFlete = jko.Price;
                                                NegocioCotizacionDetalle.Update(items);
                                                AddFlete = false;
                                            }
                                        }
                                    }
                                }
                             
                                break;
                            default:
                                var newDetailquotion = NegocioCotizacionDetalle.GetDetail(cotizacion.PK_QuotationID, jko.ProductID);

                                if (newDetailquotion == null)
                                { NegocioCotizacionDetalle.Insert(cotizacion.PK_QuotationID, jko.ProductID, jko.Quantity.ToString(), jko.RefManID, jko.Origins,jko.Price); }
                                break;

                        }
                       
                    }
                }


                var Refacciones = new BusinessMabe().Orden_Venta(obj.ODS, obj.ServiceTypes, Folio);

            }
            return obj.ODS;
        }


        public string SendNotification(string PhoneNumber, string Tipo, string URL)
        {
            List<string> Numresult = PhoneNumber.Split(new char[] { ';' }).ToList();
            string message = "";
            foreach ( var jko in Numresult) { 
                switch (Tipo)
                {

                    case "Recibo":
                        message = "Su Recibo de Venta de Refacciones se genero con éxito." + URL;
                        message = Tools.TinyURL.ToTinyURLS(message);
                        break;
                    default:
                        message = "Su poliza de servicio extendido se genero con éxito." + URL;
                        message = Tools.TinyURL.ToTinyURLS(message);
                        break;


                }
            var NumClave = "+52" + jko;

            new BusinessSMS().SendSMS(NumClave, message);
        }
            string status = "Enviado";

            return status;
        }

    }
    

}

