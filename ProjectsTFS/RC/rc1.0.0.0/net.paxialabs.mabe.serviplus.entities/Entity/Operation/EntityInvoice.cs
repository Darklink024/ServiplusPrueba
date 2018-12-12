using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.entities.Entity.Operation
{
    public class EntityInvoice
    {
        public int PK_InvoiceID { get; set; }
        public int FK_OrderID { get; set; }
        public string BusinessName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string RFC { get; set; }
        public string Email { get; set; }
        public string CountryAddress { get; set; }
        public string StateAddress { get; set; }
        public string CityAddress { get; set; }
        public string MunicipalityAddress { get; set; }
        public string StreetAddress { get; set; }
        public string NumIntAddress { get; set; }
        public string NumExtAddress { get; set; }
        public string CPAddress { get; set; }
        public bool Status { get; set; }
        public string Location { get; set; }
        public string Reference { get; set; }
        public string PersonType { get; set; }
        public System.DateTime CreateDate { get; set; }
        public System.DateTime ModifyDate { get; set; }
        public string Folio { get; set; }
        public int TypeQuotation { get; set; }

    }
}
