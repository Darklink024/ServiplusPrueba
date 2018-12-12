using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.entities.ModelView.Operation
{
    public class ModelViewPrices
    {
        public int PriceID { get; set; }
        public Nullable<int> BuildOfMaterialsID { get; set; }
        public Nullable<int> ProductID { get; set; }
        public string TypeCondition { get; set; }
        public string SalesOrganization { get; set; }
        public string DistributionChannel { get; set; }
        public string ListPrice { get; set; }
        public string GroupMaterial1 { get; set; }
        public string GroupMaterial4 { get; set; }
        public Nullable<double> Price { get; set; }
        public string Coin { get; set; }
        public string Policy { get; set; }
        public string Guaranty { get; set; }
        
    }

}