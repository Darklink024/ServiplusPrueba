using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.entities.Entity.Operation
{
    public class EntityOrderEvidence
    {
        public int EvidenceID { get; set; }
        public int MonitorOrdersID { get; set; }
        public int OrderID { get; set; }
        public string TypeEvidence { get; set; }
        public string URLEvidence { get; set; }
        public bool Status { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ModifyDate { get; set; }        
    }
}
