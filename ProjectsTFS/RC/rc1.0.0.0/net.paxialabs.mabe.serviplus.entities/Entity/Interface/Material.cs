using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.entities.Entity.Interface
{
   public class Material
    {
        public string IDMaterial { get; set; }
        public string DescripcionRefaccion { get; set; }
        public string OrganizacionVentas { get; set; }
        public string CanalDistribucion { get; set; }
        public string Centro { get; set; }
        public string GrupoMaterial1 { get; set; }
        public string GrupoMaterial4 { get; set; }
        public string EstatusMaterial { get; set; }
        public string TipoProducto { get; set; }

    }
}
