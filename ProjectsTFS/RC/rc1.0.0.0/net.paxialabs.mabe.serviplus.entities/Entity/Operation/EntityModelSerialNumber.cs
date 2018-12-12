using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.entities.Entity.Operation
{
    public class EntityModelSerialNumber
    {
        public int PK_ModelSerialNumberID { get; set; }
        public int FK_ProducID { get; set; }
        public string Model { get; set; }
        public string ValidationFormatID { get; set; }
        public Nullable<System.DateTime> InitialDate { get; set; }
        public Nullable<System.DateTime> EndDate { get; set; }
        public bool ValidDate { get; set; }
        public bool Status { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public Nullable<System.DateTime> ModifyDate { get; set; }

    }
}
