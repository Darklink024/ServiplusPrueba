using net.paxialabs.mabe.serviplus.entities.Entity.Operation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.entities.ModelView.Operation
{
     public class ModelViewPolicyRefMan :EntityPolicy
    {
        public DetailClient Client { get; set; }

        public DetailODS Detail { get; set; }
        public List<SparePart> SpareParts { get; set; }
        public EntityPayment PaymentODS { get; set; }
        public EntityInvoice Invoice { get; set; }
        public ModelViewBilling BillingDetail { get; set; }

    }
}
