using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.entities.ModelView.Operation
{
    public class ModelViewPriority
    {
        public int PriorityID { get; set; }
        public int ScheduleID { get; set; }
        public int Priority { get; set; }
        public string Description { get; set; }
        public bool Status { get; set; }
        public string ScheduleStart { get; set; }
        public string ScheduleEnd { get; set; }
    }
}
