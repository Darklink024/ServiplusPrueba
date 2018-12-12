using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.entities.Entity.Operation
{
    public class EntityCauseOrder
    {
        public int PK_CauseOrderID { get; set; }
        public int FK_StatusOrderID { get; set; }
        public string CauseOrder1 { get; set; }
        public string Moment { get; set; }
        public bool Status { get; set; }
        public System.DateTime CreateDate { get; set; }
        public System.DateTime ModifyDate { get; set; }
    }
}
