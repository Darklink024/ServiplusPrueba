using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.entities.Entity.SAP
{
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute("LUGARCOMPRA", IsNullable = false, Namespace = "")]
    public class RootLugarCompra
    {
        [System.Xml.Serialization.XmlElementAttribute("LugComp")]
        public SAPLugComp[] LugComps { get; set; }
    }

    public class SAPLugComp
    {
        public string ID_PAIS { get; set; }
        public string ESTADO { get; set; }
        public string IDSucursal { get; set; }
        public string LugarCompra { get; set; }
        public string IDClienteDist { get; set; }
    }
}
