using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.entities.ModelView.Operation
{
    public class ModelViewGuarantyBOM
    {
        public int ValidationGuarantySparePartID { get; set; }
        public int ProductID { get; set; }
        public int BuildOfMaterialsID { get; set; }
        public string Model { get; set; }
        public string SalesOrganization { get; set; }
        public string SparePartsID { get; set; }
        public string ClientID { get; set; }
        public string Months { get; set; }
        public string ValidFrom { get; set; }
        public string ValidTo { get; set; }
    }
}
