using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.entities.Entity.Interface
{
   public class BOM
    {
        public string MaterialPT { get; set; }
        public string Centro { get; set; }
        public string IDRefaccion { get; set; }
        public float Cantidad { get; set; }
    }
}
