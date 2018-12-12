using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.entities.Entity.Operation
{
    public class EntityMonitorOrder
    {
        public int VisitID { get; set; }
        public int OrderID { get; set; }
        public int? StatusVisitID { get; set; }
        public int? StatusOrderID { get; set; }
        public int? CauseOrderID { get; set; }
        public int? SequenceVisit { get; set; }        
        public DateTime? StartVisitDate { get; set; }
        public DateTime? EndVisitDate { get; set; }
        public DateTime? StartOrderDate { get; set; }
        public DateTime? EndOrderDate { get; set; }
        public DateTime? StartServiceDate { get; set; }
        public DateTime? EndServiceDate { get; set; }
        public Single? LatitudeAddress { get; set; }
        public Single? LogitudeAddress { get; set; }
        public Single? LatitudeStartVisit { get; set; }
        public Single? LogitudeStartVisit { get; set; }
        public Single? LatitudeEndVisit { get; set; }
        public Single? LogitudeEndVisit { get; set; }
        public Single? LatitudeStartOrder { get; set; }
        public Single? LogitudeStartOrder { get; set; }
        public Single? LatitudeEndOrder { get; set; }
        public Single? LogitudeEndOrder { get; set; }
        public Nullable<System.TimeSpan> DurationVisit { get; set; }
        public Nullable<System.TimeSpan> DurationOrder { get; set; }
        public Nullable<System.TimeSpan> DurationExecute { get; set; }
        public Nullable<System.TimeSpan> DurationTransport { get; set; }
        public String NoteVisit { get; set; }
        public String NoteOrder { get; set; }
        public bool Status { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? ModifyDate { get; set; }
        public Nullable<decimal> ExtraKilometrer { get; set; }
    }
}
