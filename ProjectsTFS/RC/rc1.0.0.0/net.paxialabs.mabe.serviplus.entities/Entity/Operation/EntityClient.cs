using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.entities.Entity.Operation
{
    public class EntityClient
    {
        public int PK_ClientID { get; set; }
        public string ClientID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string StreetAddress { get; set; }
        public string NumberExternalAddress { get; set; }
        public string NumberInternalAddress { get; set; }
        public string BoroughAddress { get; set; }
        public string MunicipalityAddress { get; set; }

        public string PostalCodeAddress { get; set; }
        public string AdditionalInfoAddress1 { get; set; }
        public string AdditionalInfoAddress2 { get; set; }
        public string AdditionalInfoAddress3 { get; set; }
        public string AdditionalInfoAddress4 { get; set; }
        public string AdditionalInfoAddress5 { get; set; }
        public string PhoneNumber1 { get; set; }
        public string PhoneNumber2 { get; set; }
        public string PhoneNumber3 { get; set; }
        public string PhoneExtension1 { get; set; }
        public string Email { get; set; }
        public string RCF { get; set; }
        public bool Status { get; set; }
        public System.DateTime CreateDate { get; set; }
        public System.DateTime ModifyDate { get; set; }
       
    }
}
