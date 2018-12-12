using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.entities.Entity.Interface
{
   public class ODS
    {
        public string IDOrden { get; set; }
        public string IDTecnico { get; set; }
        public string Region { get; set; }
        public string Modulo { get; set; }
        public string ModuloN3 { get; set; }
        public string Base { get; set; }
        public string IDBaseInstalada { get; set; }
        public string Modelo { get; set; }
        public string DescripcionModelo { get; set; }
        public string NumeroSerie { get; set; }
        public string IDLugarCompra { get; set; }
        public string DescripcionLugarCompra { get; set; }
        public string FechaCompra { get; set; }
        public string FechaCreacionODS { get; set; }
        public string FechaProgramacion { get; set; }
        public string HoraProgramacion { get; set; }
        public string Prioridad { get; set; }
        public string TipoServicio { get; set; }
        public string SintomaFalla { get; set; }
       public string Notas { get; set; }
       public string ODS_PADRE { get; set; }

        

    }
}
