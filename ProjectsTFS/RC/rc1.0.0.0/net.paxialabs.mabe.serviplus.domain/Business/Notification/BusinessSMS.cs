using net.paxialabs.mabe.serviplus.security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace net.paxialabs.mabe.serviplus.domain.Business.Notification
{
    internal class BusinessSMS
    {
        private string fromNumber;

        public BusinessSMS()
        {
            string accountSid = GlobalConfiguration.TwilioAccountSid;
            string authToken = GlobalConfiguration.TwilioAuthToken;

            fromNumber = GlobalConfiguration.TwilioFromNumber;

            TwilioClient.Init(accountSid, authToken);
        }

        public string SendSMS(string number, string bodyMessage)
        {
            //"+525536749083"

            var mediaUrl = new List<Uri>()
            {
                //new Uri( "http://www.goretail.mx/index.html" )
            };
            var to = new PhoneNumber(number);
            var message = MessageResource.Create(
              to,
              from: new PhoneNumber(fromNumber),
              body: bodyMessage,
              mediaUrl: mediaUrl);

            return message.Sid;
        }
    }
}
