using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.entities.ModelView.Interface
{
    public class ModelViewApartadoADR
    {
        public string OrderID { get; set; }
        public string MaterialID { get; set; }
        public string Cantidad { get; set; }
        public string CentroSunimistrador { get; set; }
        public string CentroSolicitador{ get; set; }
        public string AlmacenSolicitador { get; set; }
        public string Sociedad { get; set; }
    }
}
