using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.entities.Entity.SAP
{
    public class EntitySAPResumen
    {
        // ResumenID, Tipo, Contenedor, Fecha, Registros, Actualizados, Insertados, Procesado, BI_ODS_Udp, Inicio, Termino, Creacion, Modificacion
        public int ResumenID { get; set; }
        public string Tipo { get; set; }
        public string Contenedor { get; set; }
        public DateTime? Fecha { get; set; }
        public int? Registros { get; set; }
        public int? Actualizados { get; set; }
        public int? Insertados { get; set; }
        public bool? Procesado { get; set; }
        public bool? BI_ODS_Udp { get; set; }
        public DateTime? Inicio { get; set; }
        public DateTime? Termino { get; set; }
        public DateTime? Creacion { get; set; }
        public DateTime? Modificacion { get; set; }
    }
}
