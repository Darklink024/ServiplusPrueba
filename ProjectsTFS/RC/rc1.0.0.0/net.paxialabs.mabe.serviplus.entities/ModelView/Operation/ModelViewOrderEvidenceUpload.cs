using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.entities.ModelView.Operation
{
    public class ModelViewOrderEvidenceUpload
    {
        public int EvidenceID { get; set; }
        public int MonitorOrdersID { get; set; }
        public string OrderID { get; set; }
        public string TypeEvidence { get; set; }
        public string FileName { get; set; }
        public string Content { get; set; }
    }
}
