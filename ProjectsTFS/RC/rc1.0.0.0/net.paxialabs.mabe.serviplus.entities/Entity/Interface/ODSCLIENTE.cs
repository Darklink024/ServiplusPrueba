using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.entities.Entity.Interface
{
    [Serializable()]
    public class ODSCLIENTE
    {
        public string IDOrden { get; set; }
        public string IDCliente { get; set; }
        public string Nombrecliente { get; set; }
        public string ApellidoCliente { get; set; }
        public string Calle { get; set; }
        public string Numero { get; set; }
        public string NumeroInterior { get; set; }
        public string Colonia { get; set; }
        public string Delegacion { get; set; }
        public string CodigoPosta { get; set; }
        public string Referencia1 { get; set; }
        public string Referencia2 { get; set; }
        public string Referencia3 { get; set; }
        public string Referencia4 { get; set; }
        public string Referencia5 { get; set; }
        public string TEL_CASA { get; set; }
        public string TEL_TRABAJO { get; set; }
        public string EXT_TRABAJO { get; set; }
        public string CELULAR { get; set; }
        public string email { get; set; }
        public string RFCCEDULA { get; set; }
    }
}
