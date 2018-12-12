using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.entities.Entity.SAP
{
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute("MT_NumeroOrdenVenta", Namespace = "", IsNullable = false)]
    public class EntitySAPOrdenVentaResult
    {
        public EntitySAPOrdenVentaItemResult Item;

        [System.Xml.Serialization.XmlElementAttribute("Returns")]
        public EntitySAPOrdenVentaReturnResults Returns;
    }

    public class EntitySAPOrdenVentaItemResult
    {
        public string OrdenWeb { get; set; }
        public string PedidoSAP { get; set; }
    }
    public class EntitySAPOrdenVentaReturnResults
    {
        [System.Xml.Serialization.XmlElementAttribute("Return")]
        public EntitySAPOrdenVentaReturnResult[] Return;
    }
    public class EntitySAPOrdenVentaReturnResult
    {
        public string Type { get; set; }

        public string ID { get; set; }

        public string Number { get; set; }

        public string Message { get; set; }

        public string Log_No { get; set; }

        public string Log_Msg_No { get; set; }

        public string Message_V1 { get; set; }

        public string Message_V2 { get; set; }

        public string Message_V3 { get; set; }

        public string Message_V4 { get; set; }

        public string Parameter { get; set; }

        public string Row { get; set; }

        public string Field { get; set; }

        public string System { get; set; }
    }
}
