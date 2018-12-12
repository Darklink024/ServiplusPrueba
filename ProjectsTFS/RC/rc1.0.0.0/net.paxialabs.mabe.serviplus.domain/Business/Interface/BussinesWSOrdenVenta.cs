using net.paxialabs.mabe.serviplus.domain.Tools;
using net.paxialabs.mabe.serviplus.entities.Entity.SAP;
using net.paxialabs.mabe.serviplus.security;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace net.paxialabs.mabe.serviplus.domain.Business.Interface
{
    internal class BussinesWSOrdenVenta
    {
        private readonly string ACTION = "http://mabe.com/MW/PEC/COM/OrdenVenta_vTEX/SI_OrdenVenta_Out";
        private readonly string URL;
        private readonly string USER;
        private readonly string PASSWORD;
        private readonly string PATH_XML;

        public BussinesWSOrdenVenta()
        {
            URL = GlobalConfiguration.endPointOrdenVentaOut;
            USER = GlobalConfiguration.endPointUser;
            PASSWORD = GlobalConfiguration.endPointPwd;
            PATH_XML = System.IO.Path.Combine(GlobalConfiguration.LocateContent, "xml");
        }

        public EntitySAPOrdenVentaResult Send(EntitySAPOrdenVentaRequest data)
        {
            string Body = GetRequestXML(data);

            string msg = "------- BussinesWSOrdenVenta Send Request Inicio -----" + Environment.NewLine;
            msg += Body;
            msg += "------- BussinesWSOrdenVenta Send Request Fin -----" + Environment.NewLine;

            new Security.BusinessLogger().WriteEntry(msg);

            var result = new ToolConsumingWS().CallWS(URL, ACTION, USER, PASSWORD, Body);

            msg = "------- BussinesWSOrdenVenta Send Response Inicio -----" + Environment.NewLine;
            msg += result;
            msg += "------- BussinesWSOrdenVenta Send Response Fin -----" + Environment.NewLine;

            new Security.BusinessLogger().WriteEntry(msg);

            return GetResult(result);
        }

        private string GetRequestXML(EntitySAPOrdenVentaRequest data)
        {
            string WSOrdenVentaRequest = System.IO.File.ReadAllText(System.IO.Path.Combine(PATH_XML, "WSOrdenVentaRequest.xml"));
            string WSOrdenVentaItemRequest = System.IO.File.ReadAllText(System.IO.Path.Combine(PATH_XML, "WSOrdenVentaItemRequest.xml"));
            string WSOrdenVentaItemRequestComplete = "";

            Type type = typeof(EntitySAPOrdenVentaRequestItem);

            foreach (var item in data.Items)
            {
                string Temp = WSOrdenVentaItemRequest;
                foreach (System.Reflection.PropertyInfo propertyInfo in type.GetProperties())
                {                   
                    Temp = Temp.Replace((propertyInfo.Name.ToUpper() + "_VALUE"), propertyInfo.GetValue(item).ToString());

                    if(propertyInfo.Name == "ClaseCondicion")
                    {
                        
                        string ClaseCondicion = "";

                        if (item.ClaseCondicion.Count > 0)
                        {
                            foreach (var itemCC in item.ClaseCondicion)
                            {
                                ClaseCondicion += "<ClaseCondicion>" +
                                    "<Clase>" + itemCC.Clase + "</Clase>" +
                                    "<Valor>" + itemCC.Valor + "</Valor>" +
                                    "</ClaseCondicion>";
                            }
                        }
                        else
                        {
                            ClaseCondicion += "<ClaseCondicion>" +
                                   "<Clase></Clase>" +
                                   "<Valor></Valor>" +
                                   "</ClaseCondicion>";
                        }
                        Temp = Temp.Replace("ITEMS_CC_VALUE", ClaseCondicion);
                    }
                }
                WSOrdenVentaItemRequestComplete += Temp + Environment.NewLine;
            }            

            WSOrdenVentaRequest = WSOrdenVentaRequest.Replace("DESTINO_VALUE", data.Destino);
            WSOrdenVentaRequest = WSOrdenVentaRequest.Replace("SATELITE_VALUE", data.Satelite);
            WSOrdenVentaRequest = WSOrdenVentaRequest.Replace("ITEMS_VALUE", WSOrdenVentaItemRequestComplete);

            return WSOrdenVentaRequest;
        }

        private EntitySAPOrdenVentaResult GetResult(string content)
        {
            EntitySAPOrdenVentaResult result;

            content = content.Replace("<SOAP:Envelope xmlns:SOAP='http://schemas.xmlsoap.org/soap/envelope/'>", "");
            content = content.Replace("<SOAP:Header/>", "");
            content = content.Replace("<SOAP:Body xmlns:ord=\'http://mabe.com/MW/PEC/COM/OrdenVenta_vTEX\'>", "");
            content = content.Replace("</SOAP:Body>", "");
            content = content.Replace("</SOAP:Envelope>", "");
            content = content.Replace("xmlns:ns0='http://mabe.com/MW/ECC/COM/OrdenVenta'", "");
            content = content.Replace("ns0:MT_NumeroOrdenVenta", "MT_NumeroOrdenVenta");
            content = content.Replace("</Item>", "</Item><Returns>");
            content = content.Replace("</MT_NumeroOrdenVenta>", "</Returns></MT_NumeroOrdenVenta>");
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(EntitySAPOrdenVentaResult), new XmlRootAttribute("MT_NumeroOrdenVenta"));

                using (var stringReader = new StringReader(content))
                {
                    using (var reader = XmlReader.Create(stringReader))
                    {
                        result = (EntitySAPOrdenVentaResult)serializer.Deserialize(reader);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }
    }
}
