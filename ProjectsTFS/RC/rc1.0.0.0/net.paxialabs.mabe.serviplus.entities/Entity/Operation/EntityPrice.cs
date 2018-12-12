using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.entities.Entity.Operation
{
    public class EntityPrice
    {
        public int PK_PriceID { get; set; }
        public Nullable<int> FK_BuildOfMaterialsID { get; set; }
        public Nullable<int> FK_ProductID { get; set; }
        public Nullable<int> FK_WorkforceID { get; set; }
        public string TypeCondition { get; set; }
        public string SalesOrganization { get; set; }
        public string DistributionChannel { get; set; }
        public string ListPrice { get; set; }
        public string GroupMaterial1 { get; set; }
        public string GroupMaterial4 { get; set; }
        public Nullable<double> Price { get; set; }
        public string Coin { get; set; }
        public Nullable<System.DateTime> DateValidity { get; set; }
        public Nullable<System.DateTime> DateValidityEnd { get; set; }
        public string Policy { get; set; }
        public string Guaranty { get; set; }
        public bool Status { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public Nullable<System.DateTime> ModifyDate { get; set; }

        public string Refaccion { get; set; }
    }
}
