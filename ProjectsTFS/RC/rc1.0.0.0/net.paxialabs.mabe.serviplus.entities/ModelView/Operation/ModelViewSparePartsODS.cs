using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.entities.ModelView.Operation
{
    public class ModelViewSparePartsODS
    {
        public string RefManID { get; set; }
        public string SparePartDescription { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public string PosicionItem { get; set; }
        public string SparePartStatus { get; set; }
        public string StatusSchema { get; set; }
        public string StatusDescription { get; set; }
    }
}
