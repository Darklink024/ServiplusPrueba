//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace net.paxialabs.mabe.serviplus.data.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class Clientes
    {
        public int ClienteID { get; set; }
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
        public string ODS_PADRE { get; set; }
        public string Contenedor { get; set; }
        public Nullable<bool> Procesado { get; set; }
        public Nullable<System.DateTime> Creacion { get; set; }
        public Nullable<System.DateTime> Modificacion { get; set; }
    }
}