using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.entities.Entity.Security
{
    public class EntityMobileSync
    {
        public int PK_MobileSyncID { get; set; }
        public int FK_UserID { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string ODS { get; set; }
        public string Type { get; set; }
        public System.DateTime SyncDate { get; set; }
        public bool Status { get; set; }
        public System.DateTime CreateDate { get; set; }
        public System.DateTime ModifyDate { get; set; }
    }
}
