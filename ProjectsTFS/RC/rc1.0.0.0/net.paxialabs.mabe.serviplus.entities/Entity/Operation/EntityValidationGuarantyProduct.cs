using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.entities.Entity.Operation
{
    public class EntityValidationGuarantyProduct
    {
        public int PK_ValidationGuarantyProductID { get; set; }
        public Nullable<int> FK_ProducID { get; set; }
        public string Country { get; set; }
        public string Model { get; set; }
        public string ClientID { get; set; }
        public string Months { get; set; }
        public Nullable<System.DateTime> ValidFrom { get; set; }
        public Nullable<System.DateTime> ValidTo { get; set; }
        public bool Status { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public Nullable<System.DateTime> ModifyDate { get; set; }
    }
}
