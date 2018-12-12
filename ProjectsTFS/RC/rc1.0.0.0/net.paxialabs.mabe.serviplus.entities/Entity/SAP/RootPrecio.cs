using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.entities.Entity.SAP
{
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute("Precios", Namespace = "", IsNullable = false)]
    public class RootPrecio
    {
        [System.Xml.Serialization.XmlElementAttribute("Precio")]
        public SAPPrecio[] Precio { get; set; }
    }

    public class SAPPrecio
    {
        public string Material { get; set; }
        public string TipoCondicion { get; set; }
        public string OrganizacionVentas { get; set; }
        public string CanalDistribucion { get; set; }
        public string ListaPrecios { get; set; }
        public string GrupoMaterial1 { get; set; }
        public string GrupoMaterial4 { get; set; }
        public string PrecioMaterial { get; set; }
        public string Moneda { get; set; }
        public string FechaValidez { get; set; }
        public string FechaFinValidez { get; set; }
    }
}
