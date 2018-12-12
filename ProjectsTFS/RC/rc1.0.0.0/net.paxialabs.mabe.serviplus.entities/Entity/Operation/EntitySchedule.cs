using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.entities.Entity.Operation
{
    public class EntitySchedule
    {
        public int PK_ScheduleID { get; set; }
        public string ScheduleStart { get; set; }
        public string ScheduleEnd { get; set; }
        public bool Status { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public Nullable<System.DateTime> ModifyDate { get; set; }
        public virtual ICollection<EntityPriority> Priority { get; set; }
    }
}
