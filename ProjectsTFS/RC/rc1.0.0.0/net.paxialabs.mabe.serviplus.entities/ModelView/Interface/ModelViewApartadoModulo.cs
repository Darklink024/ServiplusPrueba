using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.entities.ModelView.Interface
{
    public class ModelViewApartadoModulo
    {
        public string OrderID { get; set; }
        public string MaterialID { get; set; }
        public string Cantidad { get; set; }
        public string Centro { get; set; }
        public string AlmacenReceptor { get; set; }
        public string AlmacenOrigen { get; set; }
    }
}
