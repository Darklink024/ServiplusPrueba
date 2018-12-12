using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.entities.ModelView.Operation
{
    public class ModelViewWorkforcePrices : ModelViewPrices
    {
        public int PK_WorkforceID { get; set; }

        public string WorkforceID { get; set; }
        public string Description { get; set; }
    }
}
