using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.entities.Entity.SAP
{
    public class EntitySAPADRReserveSPRequest
    {
        public string Satelite { get; set; }
        public string Mandante { get; set; }
        public string Idioma { get; set; }
        public List<EntitySAPADRReserveSPRequestItem> Items { get; set; }
    }

    public class EntitySAPADRReserveSPRequestItem
    {
        public string IDOrden { get; set; }
        public string Material { get; set; }
        public int Cantidad { get; set; }
        public string Sociedad { get; set; }
        public string CentroSuministrador { get; set; }
        public string CentroSolicitador { get; set; }
        public string AlmacenSolicitador { get; set; }
    }


    public class EntitySAPADRReserveSPResponse
    {
        [System.Xml.Serialization.XmlElementAttribute("Return")]
        public EntitySAPADRReserveSPItemResponse[] Return;
    }

    public class EntitySAPADRReserveSPItemResponse
    {
        public string TipoMensaje { get; set; }
        public string Mensaje { get; set; }
        public string Descripcion { get; set; }
    }
}
