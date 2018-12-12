using net.paxialabs.mabe.serviplus.entities.ModelView.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.entities.ModelView.Operation
{
    public class ModelViewBilling
    {
        public string ODS { get; set; }
        public string EMails { get; set; }
        public string IVA { get; set; }
        public string SubTotal { get; set; }
        public string Total { get; set; }
        public string WorkForce { get; set; }
        public int? FK_ProductID { get; set; }
        public int? FK_ShopPlaceID { get; set; }
        public string SerialNumber { get; set; }
        public string ShopDate { get; set; }
        public string ServiceType { get; set; }
        public string ServiceTypes { get; set; }
        public int PreOrden { get; set; }
        public string OrderID { get; set; }
        

        public List<BillingDetail> BillingDetails { get; set; }
        public List<Failure> Failures { get; set; }
        public policy Policies { get; set; }
        public int EstimatedTipe { get; set; }
    }
    public class BillingDetail
    {
        public string Origins { get; set; }
        public int ProductID { get; set; }
        public int Quantity { get; set; }
        public string Totals { get; set; }
        public string Price { get; set; }
        public string RefManID { get; set; }
        public string SparePartsDescription { get; set; }
        
    }

    public class Failure
    {
        public int FailureID { get; set; }

    }

    public class policy
    {
        public double Budget { get; set; }
        public string ClientID { get; set; }
        public string Coin { get; set; }
        public string Folio { get; set; }
        public string GuarantyEnd { get; set; }
        public string GuarantyStart { get; set; }
        public string Model { get; set; }
        public string PolicyDate { get; set; }
        public string PolicyPrice { get; set; }
        public string PolicyID { get; set; }
        public string PolicyDescription { get; set; }
        public int ProductID { get; set; }
        public string SalesOrg { get; set; }
        public string PriceList { get; set; }
        public int Selected { get; set; }
        public string ccGroup { get; set; }

        public string MaterialGroup4 { get; set; }

















    }
}
