using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.entities.ModelView.Operation
{
    public class ModelViewEvidence
        {

            //public int MonitorID { get; set; }
            //public int StatusOrderID { get; set; }
            //public int StatusVisitID { get; set; }
            //public int ModuleID { get; set; }
            //public string StatusODS { get; set; }

            public string OrderID { get; set; }
            public string EmployeeName { get; set; }
            public string Date { get; set; }
            public List<EvidenceOrder> EvidenceBadUse { get; set; }
            public List<EvidenceOrder> EvidenceFinish { get; set; }
            public List<EvidenceOrder> EvidenceNoSeriesNumber { get; set; }
            public List<EvidenceOrder> EvidencePurchaseNote { get; set; }
            public List<EvidenceOrder> EvidenceStartODS { get; set; }
            public List<EvidenceOrder> EvidenceSummary { get; set; }

        }

    }

