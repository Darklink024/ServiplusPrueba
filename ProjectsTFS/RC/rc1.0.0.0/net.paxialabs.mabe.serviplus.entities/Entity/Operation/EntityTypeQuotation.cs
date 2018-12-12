using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.entities.Entity.Operation
{
    public class EntityTypeQuotation
    {
        public int PK_TypeQuotation { get; set; }

        public string  Description {get;set;}
        public Nullable<bool> Status { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public Nullable<System.DateTime> ModifyDate { get; set; }

    }
}
