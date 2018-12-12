using net.paxialabs.mabe.serviplus.entities.Entity.Security;
using System;

namespace net.paxialabs.mabe.serviplus.entities.ModelView.Operation
{
    public class ModelViewPayment : EntitySecurity
    {
        public int PK_PaymentID { get; set; }
        public string OrderID { get; set; }
        public string TypePaymentID { get; set; }
        public string AuthorizationPayment { get; set; }
        public string DatePayment { get; set; }
        public double MountPayment { get; set; }
        public string Folio { get; set; }
        public int EstimatedType { get; set; }
    }
}

