using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.entities.ModelView.Operation
{
    public class ModelViewPermission
    {
        public int ModuleID { get; set; }
        public int ProfileID { get; set; }
        public bool Access { get; set; }
        public string Module { get; set; }
        public string URL { get; set; }

    }
}
