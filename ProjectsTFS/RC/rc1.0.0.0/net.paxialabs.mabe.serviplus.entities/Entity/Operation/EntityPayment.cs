using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.entities.Entity.Operation
{
    public class EntityPayment
    {
        public int PK_PaymentID { get; set; }
        public int FK_OrderID { get; set; }
        public string TypePaymentID { get; set; }
        public string AuthorizationPayment { get; set; }
        public Nullable<System.DateTime> DatePayment { get; set; }
        public Nullable<double> MountPayment { get; set; }
        public bool Status { get; set; }
        public System.DateTime CreateDate { get; set; }
        public System.DateTime ModifyDate { get; set; }
        public string Folio { get; set; }
        public Nullable<int> Fk_TypeQuotation { get; set; }

    }
}
