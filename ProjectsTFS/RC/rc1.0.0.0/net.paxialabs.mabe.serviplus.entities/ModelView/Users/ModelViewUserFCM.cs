using net.paxialabs.mabe.serviplus.entities.Entity.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.entities.ModelView.Users
{
    public class ModelViewUserFCM : EntitySecurity
    {
        public DateTime DateSynchronization { get; set; }
        public string TokenPush { get; set; }
        public string Phone { get; set; }
        public string IMEI { get; set; }
    }
}
