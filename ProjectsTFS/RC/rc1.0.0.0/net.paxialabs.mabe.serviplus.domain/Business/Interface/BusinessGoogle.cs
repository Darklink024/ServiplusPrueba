using net.paxialabs.mabe.serviplus.domain.Business.Users;
using net.paxialabs.mabe.serviplus.entities.Entity.Interface;
using net.paxialabs.mabe.serviplus.entities.ModelView.Operation;
using net.paxialabs.mabe.serviplus.security;
using net.paxialabs.mabe.serviplus.security.ManagerExceptions.Commons;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Text;
using System.Web.Script.Serialization;


namespace net.paxialabs.mabe.serviplus.domain.Business.Interface
{
    internal class BusinessGoogle
    {
        public EntityGoogleMapsDistanceMatrixResponse GetDistanceMatrix(ModelViewGoogleMapsDistanceMatrixRequest model)
        {
            var dataUsuario = new BusinessUsers().GetUserByToken(model.TokenUser);

            if (model.TokenApp != GlobalConfiguration.TokenWEB)
                if (model.TokenApp != GlobalConfiguration.TokenMobile)
                    throw new Exception("TokenInvalid");

            if (dataUsuario == null) throw new Exception("UserPasswordInvalid");


            EntityGoogleMapsDistanceMatrixRequest request = new EntityGoogleMapsDistanceMatrixRequest();
            request.origins = model.origins;
            request.destinations = model.destinations;
            request.units = "metric";
            request.departure_time = "now";
            request.key = GlobalConfiguration.GoogleMapsDistanceMatrixTOKEN;

            EntityGoogleMapsDistanceMatrixResponse objResponse = null;
            try
            {
                string Url = GlobalConfiguration.GoogleMapsDistanceMatrixURL;

                Url += "?units=" + request.units;
                Url += "&origins=" + request.origins;
                Url += "&destinations=" + request.destinations;
                Url += "&departure_time=" + request.departure_time;
                Url += "&key=" + request.key;

                HttpWebRequest webrequest = (HttpWebRequest)WebRequest.Create(Url);
                webrequest.Method = "POST";
                webrequest.ContentType = "application/json";
                webrequest.ContentLength = 0;
                Stream stream = webrequest.GetRequestStream();
                stream.Close();
                string result;
                using (WebResponse response = webrequest.GetResponse())
                {
                    using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                    {
                        result = reader.ReadToEnd();
                        objResponse = JsonConvert.DeserializeObject<EntityGoogleMapsDistanceMatrixResponse>(result); 
                    }
                }

                if (objResponse == null)
                {
                    throw new Exception("Error parser " + result + " | Url " + Url);
                }
            }
            catch(Exception ex)
            {
                throw new Generic_Exception(ex, Generic_Exception.ErrorCodes.ErrorGoogleMaps);
            }
            return objResponse;
        }

        public AndroidFCMPushNotificationStatus SendNotification(string deviceId, string title, string message)
        {
            AndroidFCMPushNotificationStatus result = new AndroidFCMPushNotificationStatus();
            string serverApiKey = GlobalConfiguration.GoogleFCMServerToken;
            string senderId = GlobalConfiguration.GoogleFCMSenderIDToken;

            try
            {
                result.Successful = true;
                result.Error = null;
                // var value = message;
                var requestUri = GlobalConfiguration.GoogleFCMUri;

                WebRequest webRequest = WebRequest.Create(requestUri);
                webRequest.Method = "POST";
                webRequest.Headers.Add(string.Format("Authorization: key={0}", serverApiKey));
                webRequest.Headers.Add(string.Format("Sender: id={0}", senderId));
                webRequest.ContentType = "application/json";

                var data = new
                {
                    to = deviceId, // Uncoment this if you want to test for single device
                    // to = "/topics/" + _topic, // this is for topic 
                    notification = new
                    {
                        title = title,
                        body = message,
                        //icon="myicon"
                    }
                };
                var serializer = new JavaScriptSerializer();
                var json = serializer.Serialize(data);

                Byte[] byteArray = Encoding.UTF8.GetBytes(json);

                webRequest.ContentLength = byteArray.Length;
                using (Stream dataStream = webRequest.GetRequestStream())
                {
                    dataStream.Write(byteArray, 0, byteArray.Length);

                    using (WebResponse webResponse = webRequest.GetResponse())
                    {
                        using (Stream dataStreamResponse = webResponse.GetResponseStream())
                        {
                            using (StreamReader tReader = new StreamReader(dataStreamResponse))
                            {
                                String sResponseFromServer = tReader.ReadToEnd();
                                result.Response = sResponseFromServer;
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                result.Successful = false;
                result.Response = null;
                result.Error = ex;
            }
            return result;
        }

        public DateTime GetLocalDateTime(double latitude, double longitude, DateTime utcDate)
        {
            string URL = "https://maps.googleapis.com" + "/maps/api/timezone/json";

            URL += "?location=" + latitude + "," + longitude;
            URL += "&timestamp=" + utcDate.ToTimestamp().ToString();
            URL += "&sensor=false";

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL);
            request.Method = "GET";
            request.ContentType = "application/json";
           
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            Stream stream = response.GetResponseStream();
            string content = "";
            using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
            {
                content = reader.ReadToEnd();
            }

            var data = new JavaScriptSerializer().Deserialize<GoogleTimeZone>(content);

            return utcDate.AddSeconds(data.rawOffset + data.dstOffset);
        }




        

    }
}
