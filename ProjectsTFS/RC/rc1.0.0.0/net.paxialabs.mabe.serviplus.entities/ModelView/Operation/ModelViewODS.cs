using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.entities.ModelView.Operation
{
    public class ModelViewODS
    {
        public int MonitorID { get; set; }
        public int StatusOrderID { get; set; }
        public int StatusVisitID { get; set; }
        public int ModuleID { get; set; }
        public string StatusODS { get; set; }

        public string CauseOrder { get; set; }

        public string Module { get; set; }
        public string OrderID{ get; set; }
        public string EmployeeName { get; set; }
        public string ServiceType { get; set; }
        public string StatusVisit { get; set; }
        public string StartVisitODS { get; set; }
        public string StartTryODS { get; set; }
        public string StartRunODS { get; set; }

        public string EndVisitODS { get; set; }
        public string EndTryODS { get; set; }
        public string EndRunODS { get; set; }
        
        public string LatitudeStartVisit { get; set; }
        public string LogitudeStartVisit { get; set; }
        public string LatitudeEndVisit { get; set; }
        public string LogitudeEndVisit { get; set; }
        public string LatitudeStartOrder { get; set; }
        public string LogitudeStartOrder { get; set; }
        public string LatitudeEndOrder { get; set; }
        public string LogitudeEndOrder { get; set; }
        public string DurationVisit { get; set; }
        public string DurationOrder { get; set; }
        public string DurationExecute { get; set; }
        public string Notes { get; set; }

        public List<EvidenceOrder> Evidence { get; set; }
        public List<AddressV> Address { get; set; }

        public string URL { get; set; }

        public string SendCRM { get; set; }

        public DateTime OrderExecuteDate { get; set; }

        public string Invoice { get; set; }

          }

    public class EvidenceOrder
    {

       public string TypeEvidence { get; set; }
        public string URLEvidence { get; set; }
    }

    public class AddressV
    {
        public int  StatusOrderID { get; set; }
        public Single? LatitudeAddress { get; set; }
        public Single? LogitudeAddress { get; set; }
        public Single?  LatitudeStartVisit { get; set; }
        public Single?  LogitudeStartVisit { get; set; }
        public Single?  LatitudeEndVisit { get; set; }
        public Single? LogitudeEndVisit { get; set; }
        public string Status { get; set; }
        public string OrderID { get; set; }
        public string StarTime { get; set; }
        public string EndTime { get; set; }
        public int Secuence { get; set; }

        public string Rango { get; set; }

    }


}
