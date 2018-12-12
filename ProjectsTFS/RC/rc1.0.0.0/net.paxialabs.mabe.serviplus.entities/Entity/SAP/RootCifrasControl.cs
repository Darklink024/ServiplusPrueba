using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.entities.Entity.SAP
{
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute("CifrasControl", IsNullable = false, Namespace = "")]
    public class RootCifrasControl
    {
        [System.Xml.Serialization.XmlElementAttribute("CF")]
        public SAPCF CF { get; set; }
    }

    public class SAPCF
    {
        public string NumeroArchivos { get; set; }
        public string ODS { get; set; }
        public string Clientes { get; set; }
        public string Ref_Man { get; set; }
        public string Fallas { get; set; }
    }
}
