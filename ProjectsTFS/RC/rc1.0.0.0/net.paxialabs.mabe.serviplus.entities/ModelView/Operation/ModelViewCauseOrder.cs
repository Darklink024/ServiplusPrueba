using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.entities.ModelView.Operation
{
    public  class ModelViewCauseOrder
    {
        public int CauseOrderID { get; set; }
        public string CauseOrder { get; set; }
        public string Moment { get; set; }
        public int StatusOrderID { get; set; }
        public string StatusOrder { get; set; }  
        public bool Status { get; set; }

    }
}
