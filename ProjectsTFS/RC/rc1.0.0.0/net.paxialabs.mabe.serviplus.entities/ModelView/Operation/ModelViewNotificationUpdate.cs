using net.paxialabs.mabe.serviplus.entities.Entity.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.entities.ModelView.Operation
{
    public class ModelViewNotificationUpdate : EntitySecurity
    {
        public int MessageID { get; set; }
        public bool MessageRead { get; set; }
        public bool Status { get; set; }
    }
}
