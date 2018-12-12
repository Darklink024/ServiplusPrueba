using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.entities.Entity.Interface
{
    public class Empleado
    {
        public string Sociedad { get; set; }
        public string ID_Empleado { get; set; }
        public string TipoEmpleado { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Interlocutor { get; set; }
        public string NIVEL1 { get; set; }
        public string NIVEL2 { get; set; }
        public string NIVEL3 { get; set; }
        public string NIVEL4 { get; set; }
        public string Correo { get; set; }
        public string CentroProp { get; set; }
        public string AlmacenProp { get; set; }
        public string DiferenciadorTaller { get; set; }

    }
}
