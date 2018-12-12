using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.entities.Entity.Operation
{
    public class EntityCauseVisit
    {
        public int PK_CauseVisitID { get; set; }
        public int FK_StatusVisitID { get; set; }
        public string CauseVisit1 { get; set; }
        public string Moment { get; set; }
        public bool Status { get; set; }
        public System.DateTime CreateDate { get; set; }
        public System.DateTime ModifyDate { get; set; }

    }
}
