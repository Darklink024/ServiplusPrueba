using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.entities.ModelView.Operation
{
    public class ModelViewGuarantyProduct
    {
        public int ValidationGuarantyProductID { get; set; }
        public int ProducID { get; set; }
        public string Country { get; set; }
        public string Model { get; set; }
        public string ClientID { get; set; }
        public string Months { get; set; }
        public string ValidFrom { get; set; }
        public string ValidTo { get; set; }
        public int ShopPlaceID { get; set; }
    }
}

