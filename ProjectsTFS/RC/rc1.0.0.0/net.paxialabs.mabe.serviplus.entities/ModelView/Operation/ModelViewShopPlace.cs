using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.entities.ModelView.Operation
{
    public class ModelViewShopPlace
    {
        public int ShopPlaceID { get; set; }
        public int StateID { get; set; }
        public string ShopPlaceDescription { get; set; }
        public string ClientID { get; set; }
        public bool ShopPlaceStatus { get; set; }
    }
}
 