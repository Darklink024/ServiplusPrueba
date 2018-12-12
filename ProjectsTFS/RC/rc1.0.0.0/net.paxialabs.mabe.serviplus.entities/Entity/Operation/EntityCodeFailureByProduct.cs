using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.entities.Entity.Operation
{
   public class EntityCodeFailureByProduct
    {
        public int FK_CodeFailureID { get; set; }
        public int FK_ProductID { get; set; }
        public Nullable<int> Complexity { get; set; }
        public bool Status { get; set; }
        public System.DateTime CreateDate { get; set; }
        public System.DateTime ModifyDate { get; set; }
    }
}
