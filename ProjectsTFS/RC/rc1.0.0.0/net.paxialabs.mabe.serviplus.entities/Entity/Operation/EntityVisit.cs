using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.entities.Entity.Operation
{
    public class EntityVisit
    {
        public int PK_VisitID { get; set; }
        public int FK_OrderID { get; set; }
        public Nullable<int> FK_StatusVisitID { get; set; }
        public Nullable<int> FK_StatusOrderID { get; set; }
        public Nullable<int> FK_CauseOrderID { get; set; }
        public Nullable<int> SequenceVisit { get; set; }
        public Nullable<System.DateTime> StartVisitDate { get; set; }
        public Nullable<System.DateTime> EndVisitDate { get; set; }
        public Nullable<System.DateTime> StartOrderDate { get; set; }
        public Nullable<System.DateTime> EndOrderDate { get; set; }
        public Nullable<System.DateTime> StartServiceDate { get; set; }
        public Nullable<System.DateTime> EndServiceDate { get; set; }
        public Nullable<float> LatitudeAddress { get; set; }
        public Nullable<float> LogitudeAddress { get; set; }
        public Nullable<float> LatitudeStartVisit { get; set; }
        public Nullable<float> LogitudeStartVisit { get; set; }
        public Nullable<float> LatitudeEndVisit { get; set; }
        public Nullable<float> LogitudeEndVisit { get; set; }
        public Nullable<float> LatitudeStartOrder { get; set; }
        public Nullable<float> LogitudeStartOrder { get; set; }
        public Nullable<float> LatitudeEndOrder { get; set; }
        public Nullable<float> LogitudeEndOrder { get; set; }
        public Nullable<System.TimeSpan> DurationVisit { get; set; }
        public Nullable<System.TimeSpan> DurationOrder { get; set; }
        public Nullable<System.TimeSpan> DurationExecute { get; set; }
        public Nullable<System.TimeSpan> DurationTransport { get; set; }
        public string NoteVisit { get; set; }
        public string NoteOrder { get; set; }
        public bool Status { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public Nullable<System.DateTime> ModifyDate { get; set; }

        public decimal? ExtraKilometrer { get; set; }
    }
}
