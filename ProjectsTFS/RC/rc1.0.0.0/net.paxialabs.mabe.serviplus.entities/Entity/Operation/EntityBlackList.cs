using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.entities.Entity.Operation
{
    public class EntityBlackList
    {
        public int PK_BlackListID { get; set; }
        public string BlackListName { get; set; }
        public bool Status { get; set;}
        public System.DateTime CreateDate { get; set; }
        public System.DateTime ModifyDate { get; set; }

    }
}
