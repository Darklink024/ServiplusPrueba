using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.entities.Entity.Operation
{
    public class EntityProduct
    {
        public int PK_ProductID { get; set; }
        public string Model { get; set; }
        public string ProductName { get; set; }
        public string BarCode { get; set; }
        public string SaleOrganization { get; set; }
        public string DistributionChannel { get; set; }
        public string Center { get; set; }
        public string MaterialGroup1 { get; set; }
        public string MaterialGroup4 { get; set; }
        public string ProductType { get; set; }
        public bool Status { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public Nullable<System.DateTime> ModifyDate { get; set; }
    }
}
