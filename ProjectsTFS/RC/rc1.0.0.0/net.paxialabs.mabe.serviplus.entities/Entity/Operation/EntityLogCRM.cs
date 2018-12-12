using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.entities.Entity.Operation
{
    public class EntityLogCRM
    {
        public int LogID { get; set; }
        public int OrderID { get; set; }
        public string UpdateBase { get; set; }
        public string UpdateRefMan { get; set; }
        public string UpdateODS { get; set; }
        public string ModuleReserveSP { get; set; }
        public string ADRReserveSP { get; set; }
        public bool Status { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ModifyDate { get; set; }
    }
}
