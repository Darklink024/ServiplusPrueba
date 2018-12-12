using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.entities.Entity.SAP
{
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute("BOMS", Namespace = "", IsNullable = false)]
    public class RootBOM
    {
        [System.Xml.Serialization.XmlElementAttribute("BOM")]
        public SAPBOM[] BOM { get; set; }
    }

    public class SAPBOM
    {
        public string MaterialPT { get; set; }
        public string Centro { get; set; }
        public string IDRefaccion { get; set; }
        public string Cantidad { get; set; }

    }
}
