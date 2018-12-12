using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.entities.Entity.SAP
{
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute("REFMAN", Namespace = "", IsNullable = false)]
    public class RootRefMan
    {
        [System.Xml.Serialization.XmlElementAttribute("ODSREFMAN")]
        public ODSREFMAN[] ODSREFMAN { get; set; }

    }

    public class ODSREFMAN
    {
        public string ID_ORDEN { get; set; }

        [System.Xml.Serialization.XmlElementAttribute("REFMANO")]
        public REFMANO[] REFMANO { get; set; }

        public string ODS_PADRE { get; set; }
    }

    public class REFMANO
    {
        [System.Xml.Serialization.XmlElementAttribute("REFMAN")]
        public REFMAN[] REFMAN { get; set; }
    }

    public class REFMAN
    {
        public string PosicionItem { get; set; }
        public string ID_RefMan { get; set; }
        public string DescripcionRefMan { get; set; }
        public string EstatusRefMan { get; set; }
        public string EstatusEsq { get; set; }
        public string Cantidad { get; set; }
        public string Precio { get; set; }
    }
}
