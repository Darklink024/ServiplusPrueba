using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.domain.Tools
{
    internal class ToolConsumingWS
    {
        public string CallWS(string URL, string Action, string User, string Password, string Body)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL);
            byte[] bytes;
            bytes = Encoding.ASCII.GetBytes(Body);

            request.Headers.Add("SOAPAction", Action);
            request.Headers.Add("Authorization", "Basic " + System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(User + ":" + Password)));

            request.ContentType = "text/xml; encoding='utf-8'";
            request.ContentLength = bytes.Length;
            request.Method = "POST";

            Stream requestStream = request.GetRequestStream();
            requestStream.Write(bytes, 0, bytes.Length);
            requestStream.Close();
            HttpWebResponse response;
            response = (HttpWebResponse)request.GetResponse();

            if (response.StatusCode == HttpStatusCode.OK)
            {
                Stream responseStream = response.GetResponseStream();
                string responseStr = new StreamReader(responseStream).ReadToEnd();
                return responseStr;
            }
            return null;
        }
    }
}
