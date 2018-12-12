using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace net.paxialabs.mabe.serviplus.entities.Entity.Interface
{

    public class Inventario
    { 
        public string OrgVentas { get; set; }
        public List<Item> Items { get; set; }
    }

    public class Item
    {
        public string IdRef { get; set; }
        public string Centro { get; set; }
        public string Almacen { get; set; }
        public float TotDisp { get; set; }
    }

}
