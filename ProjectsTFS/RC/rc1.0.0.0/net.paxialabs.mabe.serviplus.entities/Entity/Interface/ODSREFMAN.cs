using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.entities.Entity.Interface
{
    public class ODSREFMAN
    {
        public string ID_ORDEN { get; set; }
        public List<REFMAN> REFMANO { get; set; }
    }

    public class REFMAN
    {
        public string PosicionItem { get; set; }
        public string ID_RefMan { get; set; }
        public string DescripcionRefMan { get; set; }
        public string  EstatusEsq { get; set; }
        public string EstatusRefMan { get; set; }
        public float Cantidad { get; set; }
        public float Precio { get; set; }

    }
}
