using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.entities.Entity.Operation
{
    public class EntityGuaranty
    {
        public int PK_GuarantyID { get; set; }
        public int FK_GuarantyTypeID { get; set; }
        public string GuarantyID { get; set; }
        public string Guaranty1 { get; set; }
        public bool Status { get; set; }
        public System.DateTime CreateDate { get; set; }
        public System.DateTime ModifyDate { get; set; }

    }
}
