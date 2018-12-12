using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.entities.Entity.SAP
{
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute("FALLAS", IsNullable = false, Namespace = "")]
    public class RootFalla
    {
        [System.Xml.Serialization.XmlElementAttribute("ODSMODELO")]
        public SAPODSModelo[] ODSMODELO { get; set; }
    }

    public class SAPODSModelo
    {
        public String Modelo { get; set; }
        public String ODS_PADRE { get; set; }
        [System.Xml.Serialization.XmlElementAttribute("Fail")]
        public SAPFallaFail Fail { get; set; }
    }

    public class SAPFallaFail
    {
        [System.Xml.Serialization.XmlElementAttribute("Falla")]
        public SAPFalla[] Fallas { get; set; }
    }

    public class SAPFalla
    {
        public string IDFalla { get; set; }
        public string DescripcionFalla { get; set; }
        public string Complejidad { get; set; }
    }
}
