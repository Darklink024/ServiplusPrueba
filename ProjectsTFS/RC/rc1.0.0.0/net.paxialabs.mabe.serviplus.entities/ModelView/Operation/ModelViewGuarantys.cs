using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.entities.ModelView.Operation
{
    public class ModelViewGuarantys
    {
        public int GuarantyID { get; set; }
        public string GuarantyCode { get; set; }
        public string Guaranty { get; set; }
        public bool Status { get; set; }
        public int GuarantyTypeID { get; set; }
        public string GurantyType { get; set; }
    }

}
