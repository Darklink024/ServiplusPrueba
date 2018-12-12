using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.entities.Entity.Operation
{
    public class EntityStatusScheme
    {
        public int PK_StatusSchemeID { get; set; }
        public string StatusScheme1 { get; set; }
        public string StatusHeadboard { get; set; }
        public string Description { get; set; }
        public bool Status { get; set; }
        public System.DateTime CreateDate { get; set; }
        public System.DateTime ModifyDate { get; set; }
    }
}
