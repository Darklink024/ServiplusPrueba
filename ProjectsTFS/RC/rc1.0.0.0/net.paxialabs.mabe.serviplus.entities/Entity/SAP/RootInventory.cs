using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace net.paxialabs.mabe.serviplus.entities.Entity.SAP
{
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute("MT_InventarioTecnico", IsNullable = false, Namespace = "")]
    public class RootInventory
    { 

        [System.Xml.Serialization.XmlElementAttribute("Inventario")]
        public SAPInventario Inventario { get; set; }

    }

    public class SAPInventario
    {
        [System.Xml.Serialization.XmlElementAttribute("OrgVentas")]
        public string OrgVentas { get; set; }

        [System.Xml.Serialization.XmlElementAttribute("Items")]
        public SAPInventarioItems Items { get; set; }
    }

    public class SAPInventarioItems
    {
        [System.Xml.Serialization.XmlElementAttribute("Item")]
        public SAPInventarioItem[] Item { get; set; }
    }

    public class SAPInventarioItem
    {
        public string IdRef { get; set; }

        public string Centro { get; set; }

        public string Almacen { get; set; }

        public string TotDisp { get; set; }
    }
}
