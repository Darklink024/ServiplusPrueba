using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.entities.ModelView.Operation
{
    public class ModelViewSpareParts
    {
        public int BuildOfMaterialsID { get; set; }
        public int ProductID { get; set; }
        public string Model { get; set; }
        public string SparePartsID { get; set; }
        public int Quantity { get; set; }
        public string StatusBOM { get; set; }
        public string SpartePartDescription { get; set; }
        public bool Status { get; set; }

        public int TotalRows { get; set; }
    }
}
