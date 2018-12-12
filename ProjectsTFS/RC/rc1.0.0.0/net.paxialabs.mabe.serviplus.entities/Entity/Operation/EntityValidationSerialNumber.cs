using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.entities.Entity.Operation
{
    public class EntityValidationSerialNumber
    {
        public int PK_ValidationsSerialNumberID { get; set; }
        public int FK_ModelSerialNumberID { get; set; }
        public string ValidationFormatID { get; set; }
        public string ValidationName { get; set; }
        public int InitialPosition { get; set; }
        public int FinalPosition { get; set; }
        public string Allowed { get; set; }
        public string RankID { get; set; }
        public bool Status { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public Nullable<System.DateTime> ModifyDate { get; set; }
    }
}
