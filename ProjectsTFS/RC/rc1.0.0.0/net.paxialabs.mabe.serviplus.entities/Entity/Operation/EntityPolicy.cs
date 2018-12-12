using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.entities.Entity.Operation
{
    public class EntityPolicy
    {
        public int PK_PolicyID { get; set; }
        public int FK_OrderID { get; set; }
        public int FK_ClientID { get; set; }
        public int FK_EmployeeID { get; set; }
        public int FK_PaymentID { get; set; }
        public Nullable<int> FK_Invoice_ID { get; set; }
        public int FK_QuotationID { get; set; }
        public int FK_ProductID { get; set; }
        public int FK_ShopPlace { get; set; }
        public string IDPolicy { get; set; }
        public string PolicyPrice { get; set; }
        public string Coin { get; set; }
        public string SalesOrg { get; set; }
        public string ccGroup { get; set; }
        public string PriceList { get; set; }
        public string SerialNumber { get; set; }
        public string MaterialGroup4 { get; set; }
        public System.DateTime GuarantyEnd { get; set; }
        public System.DateTime GuarantyStart { get; set; }
        public string PolicyDate { get; set; }
        public bool Status { get; set; }
        public System.DateTime CreateDate { get; set; }
        public System.DateTime ModifyDate { get; set; }







    }
}
