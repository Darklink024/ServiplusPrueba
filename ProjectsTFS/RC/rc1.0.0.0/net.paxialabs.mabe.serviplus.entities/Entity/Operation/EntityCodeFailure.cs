using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.entities.Entity.Operation
{
    public class EntityCodeFailure
    {
        public int PK_CodeFailureID { get; set; }
        public string CodeFailure1 { get; set; }
        public string Failure { get; set; }
        public bool Status { get; set; }
        public System.DateTime CreateDate { get; set; }
        public System.DateTime ModifyDate { get; set; }
    }
}
