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
    internal class BusinessWSADRReserveSP
    {
        private readonly string ACTION = "http://mabe.com/MW/HD/CYM/ATP/SI_AvailabilityStock_Out";
        private readonly string URL;
        private readonly string USER;
        private readonly string PASSWORD;
        private readonly string PATH_XML;

        public BusinessWSADRReserveSP()
        {
            URL = GlobalConfiguration.endPointADRReserveSP;
            USER = GlobalConfiguration.endPointUser;
            PASSWORD = GlobalConfiguration.endPointPwd;
            PATH_XML = System.IO.Path.Combine(GlobalConfiguration.LocateContent, "xml");
        }

        public EntitySAPADRReserveSPResponse Send(EntitySAPADRReserveSPRequest data)
        {
            string Body = GetRequestXML(data);

            string msg = "------- BusinessWSADRReserveSP Send Request Inicio -----" + Environment.NewLine;
            msg += Body;
            msg += "------- BusinessWSADRReserveSP Send Request Fin -----" + Environment.NewLine;

            new Security.BusinessLogger().WriteEntry(msg);

            var result = new ToolConsumingWS().CallWS(URL, ACTION, USER, PASSWORD, Body);

            msg = "------- BusinessWSADRReserveSP Send Response Inicio -----" + Environment.NewLine;
            msg += result;
            msg += "------- BusinessWSADRReserveSP Send Response Fin -----" + Environment.NewLine;

            new Security.BusinessLogger().WriteEntry(msg);

            return GetResult(result);
        }

        private string GetRequestXML(EntitySAPADRReserveSPRequest data)
        {
            string WSRequest = System.IO.File.ReadAllText(System.IO.Path.Combine(PATH_XML, "WSADRReserveSPRequest.xml"));
            string WSItemRequest = System.IO.File.ReadAllText(System.IO.Path.Combine(PATH_XML, "WSADRReserveSPItemRequest.xml"));
            string WSItemRequestComplete = "";

            Type type = typeof(EntitySAPADRReserveSPRequestItem);

            foreach (var item in data.Items)
            {
                string Temp = WSItemRequest;
                foreach (System.Reflection.PropertyInfo propertyInfo in type.GetProperties())
                {
                    Temp = Temp.Replace((propertyInfo.Name.ToUpper() + "_VALUE"), propertyInfo.GetValue(item).ToString());                    
                }
                WSItemRequestComplete += Temp + Environment.NewLine;
            }

            WSRequest = WSRequest.Replace("SATELITE_VALUE", data.Satelite);
            WSRequest = WSRequest.Replace("MANDANTE_VALUE", data.Mandante);
            WSRequest = WSRequest.Replace("IDIOMA_VALUE", data.Idioma);
            WSRequest = WSRequest.Replace("ITEMS_VALUE", WSItemRequestComplete);

            return WSRequest;
        }

        private EntitySAPADRReserveSPResponse GetResult(string content)
        {
            EntitySAPADRReserveSPResponse result;

            content = content.Replace("<SOAP:Envelope xmlns:SOAP='http://schemas.xmlsoap.org/soap/envelope/'>", "");
            content = content.Replace("<SOAP:Header/>", "");
            content = content.Replace("<SOAP:Body xmlns:proc=\'http://mabe.com/MW/CONFINAL/Mobile/Process_RPL'>", "");
            content = content.Replace("</SOAP:Body>", "");
            content = content.Replace("</SOAP:Envelope>", "");
            content = content.Replace("xmlns:ns1='http://mabe.com/MW/CONFINAL/Mobile/Process_RPL'", "");
            content = content.Replace("ns1:MT_ProcessRPL_Res", "MT_ProcessRPL_Res");
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(EntitySAPADRReserveSPResponse), new XmlRootAttribute("MT_ProcessRPL_Res"));

                using (var stringReader = new StringReader(content))
                {
                    using (var reader = XmlReader.Create(stringReader))
                    {
                        result = (EntitySAPADRReserveSPResponse)serializer.Deserialize(reader);
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
