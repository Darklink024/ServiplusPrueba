//using mercadopago;
using Microsoft.Exchange.WebServices.Data;
using mx;
using net.paxialabs.mabe.serviplus.domain.Facade.Interface;
using net.paxialabs.mabe.serviplus.domain.Facade.Operation;
using net.paxialabs.mabe.serviplus.entities.Entity.Operation;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.console
{
    class Program
    {
        static void Main(string[] args)
        {
            var arrODS = FacadeOrder.GetAll().Where(p => p.OrderExecuteDate.Equals(new DateTime(2017, 11, 8)));
            int j = 1;
            Console.WriteLine("Total: " + arrODS.Count().ToString());

            foreach (var i in arrODS)
            {
                try
                {
                    Console.WriteLine(j.ToString() + " de " + arrODS.Count().ToString());

                    var dataODS = FacadeOrder.Get(i.PK_OrderID);
                    var dataClient = FacadeClient.GetByID(i.FK_ClientID);
                    var dataBaseInstalled = FacadeInstalledBase.GetByID(dataODS.FK_InstalledBaseID);
                    var dataProduct = FacadeProduct.GetByID(dataBaseInstalled.FK_ProductID.Value);
                    var dataHistory = FacadeMabe.HistoryODSByClient(dataClient.ClientID, dataBaseInstalled.InstalledBaseID);
                    
                    // insertar datos order history
                    foreach (var item in dataHistory)
                    {
                       
                        var dataDBHistory = FacadeHistory.GetByOrderID(dataODS.PK_OrderID);

                        if (dataDBHistory.Where(p => p.OrderID == item.ID_Oper).Count() > 0)
                        {
                            // update
                            var entity = dataDBHistory.Where(p => p.OrderID == item.ID_Oper).FirstOrDefault();
                            entity.CloseDate = ParseDate(item.Fecha_Cierre_Orden);
                            entity.Failure1 = string.IsNullOrEmpty(item.Desc_ID_Falla1) ? "" : item.Desc_ID_Falla1;
                            entity.Failure2 = string.IsNullOrEmpty(item.Desc_ID_Falla2) ? "" : item.Desc_ID_Falla2;
                            entity.Failure3 = string.IsNullOrEmpty(item.Desc_ID_Falla3) ? "" : item.Desc_ID_Falla3;
                            entity.FailureID1 = string.IsNullOrEmpty(item.ID_Falla1) ? "" : item.ID_Falla1;
                            entity.FailureID2 = string.IsNullOrEmpty(item.ID_Falla2) ? "" : item.ID_Falla2;
                            entity.FailureID3 = string.IsNullOrEmpty(item.ID_Falla3) ? "" : item.ID_Falla3;
                            entity.FK_ClientID = dataODS.FK_ClientID;
                            entity.FK_InstalledBaseID = dataODS.FK_InstalledBaseID;
                            entity.FK_OrderID = dataODS.PK_OrderID;
                            entity.Guaranty = string.IsNullOrEmpty(item.Tipo_Serv) ? "" : item.Tipo_Serv;
                            entity.ItemStatus = string.IsNullOrEmpty(item.Estatus_Visita) ? "" : item.Estatus_Visita;
                            entity.ModifyDate = DateTime.UtcNow;
                            entity.OrderID = string.IsNullOrEmpty(item.ID_Oper) ? "" : item.ID_Oper;
                            entity.OrderStatus = string.IsNullOrEmpty(item.Estatus_Oper) ? "" : item.Estatus_Oper;
                            entity.ShopDate = new DateTime(1980, 1, 1);
                            entity.Status = true;
                            FacadeHistory.Update(entity);
                        }
                        else
                        {
                            // insert 
                            var entity = new EntityHistory();
                            entity.CloseDate = ParseDate(item.Fecha_Cierre_Orden);
                            entity.CreateDate = DateTime.UtcNow;
                            entity.Failure1 = string.IsNullOrEmpty(item.Desc_ID_Falla1) ? "" : item.Desc_ID_Falla1;
                            entity.Failure2 = string.IsNullOrEmpty(item.Desc_ID_Falla2) ? "" : item.Desc_ID_Falla2;
                            entity.Failure3 = string.IsNullOrEmpty(item.Desc_ID_Falla3) ? "" : item.Desc_ID_Falla3;
                            entity.FailureID1 = string.IsNullOrEmpty(item.ID_Falla1) ? "" : item.ID_Falla1;
                            entity.FailureID2 = string.IsNullOrEmpty(item.ID_Falla2) ? "" : item.ID_Falla2;
                            entity.FailureID3 = string.IsNullOrEmpty(item.ID_Falla3) ? "" : item.ID_Falla3;
                            entity.FK_ClientID = dataODS.FK_ClientID;
                            entity.FK_InstalledBaseID = dataODS.FK_InstalledBaseID;
                            entity.FK_OrderID = dataODS.PK_OrderID;
                            entity.Guaranty = string.IsNullOrEmpty(item.Tipo_Serv) ? "" : item.Tipo_Serv;
                            entity.ItemStatus = string.IsNullOrEmpty(item.Estatus_Visita) ? "" : item.Estatus_Visita;
                            entity.ModifyDate = DateTime.UtcNow;
                            entity.OrderID = string.IsNullOrEmpty(item.ID_Oper) ? "" : item.ID_Oper;
                            entity.OrderStatus = string.IsNullOrEmpty(item.Estatus_Oper) ? "" : item.Estatus_Oper;
                            entity.PK_HistoryID = 0;
                            entity.ShopDate = new DateTime(1980, 1, 1);
                            entity.Status = true;
                            FacadeHistory.Insert(entity);
                        }                        
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine(ex.StackTrace);
                }

                j++;
            }

            //Inventory();
            //RestImage();

            //SendEmial();

            //foreach (TimeZoneInfo z in TimeZoneInfo.GetSystemTimeZones())
            //    Console.WriteLine(z.Id);

            //Console.WriteLine(FacadeGoogle.GetLocalDateTime(19.3850, -99.1650, DateTime.UtcNow));

            //string CLIENT_ID = "6538892993478012";
            //string CLIENT_SECRET = "GGGzSZuzfdBDpua7g7wyZo9qiTrnTvcS";
            //MP mp = new MP(CLIENT_ID, CLIENT_SECRET);

            //Hashtable data = mp.getPaymentInfo("9406314591T0170101000525SFODS");

            // Sets the filters you want
            //Dictionary<String, String> filters = new Dictionary<String, String>();
            //filters.Add("site_id", "MLM"); // Argentina: MLA; Brasil: MLB

            // Search payment data according to filters
            //Hashtable searchResult = mp.searchPayment(filters);

            // Show payment information
            //foreach (Hashtable payment in searchResult.SelectToken("response.results"))
            //{
            //    Console.WriteLine(String.Format("{0}", payment["collection"]["id"]));
            //}
            Console.WriteLine("Finish");
            Console.ReadKey();
        }

        private static void SendEmial()
        {
            ExchangeService service = new ExchangeService(ExchangeVersion.Exchange2010);
            //service.AutodiscoverUrl("youremailaddress@yourdomain.com");

            service.Url = new Uri("https://correomabe.com/ews/Exchange.asmx");
            

            //service.UseDefaultCredentials = true;
            service.Credentials = new WebCredentials(@"mabenet\SACMEXCSE", "C0t1.s3rv");


            EmailMessage message = new EmailMessage(service);
            message.Subject = "Correo de prueba desde OWA";
            message.Body = "Correo de prueba desde OWA";
            message.ToRecipients.Add("miguel.ordonez@paxialabs.com");
            message.ToRecipients.Add("benito.ruiz@mabe.com.mx");
            message.Save();

            message.Send();
        }

        static void RestImage()
        {
            string[] arrImage = { ".jpg", ".png" };
            string path = @"C:\Users\Miguel Angel\Pictures";

            string[] fileEntries = Directory.GetFiles(path);

            List<ModelViewOrderEvidenceUpload> data = new List<ModelViewOrderEvidenceUpload>();

            foreach (string fileName in fileEntries)
            {
                FileInfo file = new FileInfo(fileName);
                if (arrImage.Contains(file.Extension))
                {
                    string base64String = "";

                    using (Image image = Image.FromFile(fileName))
                    {
                        using (MemoryStream m = new MemoryStream())
                        {
                            image.Save(m, image.RawFormat);
                            byte[] imageBytes = m.ToArray();

                            // Convert byte[] to Base64 String
                            base64String = Convert.ToBase64String(imageBytes);
                        }
                    }

                    Console.WriteLine(String.Format("File: " + fileName));
                    data.Add(new ModelViewOrderEvidenceUpload() {
                        OrderID = 4,
                        MonitorOrdersID = 1,
                        EvidenceID = 0,
                        TypeEvidence = "Tipo 1",
                        FileName = file.Name,
                        Content = base64String
                    });

                    break;
                    
                }
            }
            
            var json = JsonConvert.SerializeObject(data);

            using (WebClient client = new WebClient())
            {

                byte[] response =
                client.UploadValues("http://localhost:1101/Order/SetEvidence", new NameValueCollection()
                {
                           { "data", json }
                });

                string result = System.Text.Encoding.UTF8.GetString(response);
            }


            Console.ReadKey();
        }

        static void Inventory()
        {
            try
            {
                srInventario.DT_AvailabilityStock param = new srInventario.DT_AvailabilityStock();

                param.Destino = "MEX";
                var arrItem = new List<srInventario.DT_AvailabilityStockItem>();

                arrItem.Add(new srInventario.DT_AvailabilityStockItem()
                {
                    MATERIAL = "WS01L05422",
                    PLANT = "T002",
                    STORAGELOCATION = "4700"
                });

                param.item = arrItem.ToArray();

                EndpointAddress ed = new EndpointAddress("https://collaboration.mabempresa.com:8062/XISOAPAdapter/MessageServlet?senderParty=&senderService=BS_MABE_HOMEDEPOT_FPR&receiverParty=&receiverService=&interface=SI_AvailabilityStock_Out&interfaceNamespace=http://mabe.com/MW/HD/CYM/ATP");



                srInventario.SI_AvailabilityStock_OutClient proxy = new srInventario.SI_AvailabilityStock_OutClient("HTTPS_Port", ed);

                //NetworkCredential netCred = new NetworkCredential("WS_DIGITAL", "d1g1t4l");



                proxy.ClientCredentials.UserName.UserName = "WS_DIGITAL";
                proxy.ClientCredentials.UserName.Password = "d1g1t4l";

                var result = proxy.SI_AvailabilityStock_Out(param);

                Console.WriteLine("Result: " + result.ToString());

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            //DT_AvailabilityStock param = new DT_AvailabilityStock();

            //param.Destino = "MEX";
            //var arrItem = new List<DT_AvailabilityStockItem>();

            //arrItem.Add(new DT_AvailabilityStockItem()
            //{
            //    MATERIAL = "WS01L05422",
            //    PLANT = "T002",
            //    STORAGELOCATION = "4700"
            //});

            //param.item = arrItem.ToArray();

            //try
            //{

            //    SI_AvailabilityStock_OutService client = new SI_AvailabilityStock_OutService();
            //    //ICredentials credentials = new NetworkCredential("WS_DIGITAL", "d1g1t4l");
            //    ICredentials credentials = new NetworkCredential("ecommerce", "mabe.12345");
            //    client.Credentials = credentials;
            //    //client.Url = "https://collaboration.mabempresa.com:8062/XISOAPAdapter/MessageServlet?senderParty=&senderService=BS_MABE_HOMEDEPOT_FPR&receiverParty=&receiverService=&interface=SI_AvailabilityStock_Out&interfaceNamespace=http://mabe.com/MW/HD/CYM/ATP";
            //    client.Url = "https://collaboration.mabempresa.com:8061/XISOAPAdapter/MessageServlet?senderParty=&senderService=BS_MABE_HOMEDEPOT_QAS&receiverParty=&receiverService=&interface=SI_AvailabilityStock_Out&interfaceNamespace=http://mabe.com/MW/HD/CYM/ATP";

            //    var result = client.SI_AvailabilityStock_Out(param);

            //    Console.WriteLine("Result: " + result.ToString());
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex.Message);
            //}
        }

        static DateTime ParseDate(string dt)
        {

            dt = dt.Replace('.', '/').Trim();
            string[] formatStrings;

            DateTime dateValue;

            if(!dt.Contains("/"))
            {
                if (dt.Count() == 7) dt = "0" + dt;
                if (dt.Count() == 8)
                {
                    dt = dt.Substring(0, 2) + "/" + dt.Substring(2, 2) + "/" + dt.Substring(4);
                }
            }

            formatStrings = new string[] { "MM/dd/yyyy hh:mm:ss tt", "yyyy-MM-dd hh:mm:ss" };
            if (DateTime.TryParseExact(dt, formatStrings, CultureInfo.InvariantCulture, DateTimeStyles.None, out dateValue))
                return dateValue;

            formatStrings = new string[] { "dd/MM/yyyy", "yyyy-MM-dd" };
            if (DateTime.TryParseExact(dt, formatStrings, CultureInfo.InvariantCulture, DateTimeStyles.None, out dateValue))
                return dateValue;
            
            return new DateTime(1900, 1, 1);
        }
    }


    public class ModelViewOrderEvidenceUpload
    {
        public int EvidenceID { get; set; }
        public int MonitorOrdersID { get; set; }
        public int OrderID { get; set; }
        public string TypeEvidence { get; set; }
        public string FileName { get; set; }
        public string Content { get; set; }
    }
}
