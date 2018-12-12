

using net.paxialabs.mabe.serviplus.entities.Entity.Security;

namespace net.paxialabs.mabe.serviplus.entities.ModelView.Operation
{
    public class ModelViewInvoicing : EntitySecurity
    {
        public int InvoicingID { get; set; }
        public string OrderID { get; set; }
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
        public string Location { get; set; }
        public string Reference { get; set; }
        public string PersonType { get; set; }
        public string Folio { get; set; }
        public int EstimatedType { get; set; }
    }
}
