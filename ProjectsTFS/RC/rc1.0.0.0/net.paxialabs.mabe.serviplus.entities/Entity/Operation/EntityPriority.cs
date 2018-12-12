using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.entities.Entity.Operation
{
    public class EntityPriority
    {
        public int PK_PriorityID { get; set; }
        public int FK_ScheduleID { get; set; }
        public int Priority1 { get; set; }
        public string Description { get; set; }
        public bool Status { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public Nullable<System.DateTime> ModifyDate { get; set; }
        public virtual EntitySchedule Schedule { get; set; }
    }
}
