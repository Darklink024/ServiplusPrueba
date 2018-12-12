using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.entities.ModelView.Operation
{
   public class ModelViewProducts
    {
        public int ProductID { get; set; }
        public string Model { get; set; }
        public string ProductName { get; set; }
        public string BarCode { get; set; }
        public string ListPrice { get; set; }
        public string GroupMaterial1 { get; set; }
        public string GroupMaterial4 { get; set; }
        public bool Status { get; set; }
        //public List<Prices> Price { get; set; }
    }
    //public class Prices
    //{
        
    //    public int BuildOfMaterialsID { get; set; }
    //    public int Quantity { get; set; }
    //    public double Price { get; set; }
    //}
}
