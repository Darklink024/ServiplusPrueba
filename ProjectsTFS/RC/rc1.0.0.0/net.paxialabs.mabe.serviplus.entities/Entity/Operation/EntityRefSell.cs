using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.entities.Entity.Operation
{
     public class EntityRefSell
    {
        public int PK_RefSellID { get; set; }
        public int FK_OrderID { get; set; }
        public int FK_ClientID { get; set; }
        public int FK_EmployeeID { get; set; }
        public int FK_PaymentID { get; set; }
        public Nullable<int> FK_Invoice_ID { get; set; }
        public int FK_QuotationID { get; set; }
        public int FK_ProductID { get; set; }
        public int FK_ShopPlace { get; set; }
        public string IDRefSell { get; set; }
        public string OrdenVenta { get; set; }
        public bool Status { get; set; }
        public System.DateTime CreateDate { get; set; }
        public System.DateTime ModifyDate { get; set; }
    }
}
