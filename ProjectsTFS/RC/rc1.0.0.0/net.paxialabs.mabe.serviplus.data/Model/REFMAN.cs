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
    
    public partial class REFMAN
    {
        public int RefManID { get; set; }
        public string ID_ORDEN { get; set; }
        public string PosicionItem { get; set; }
        public string ID_RefMan { get; set; }
        public string DescripcionRefMan { get; set; }
        public string EstatusRefMan { get; set; }
        public string EstatusEsq { get; set; }
        public string Cantidad { get; set; }
        public string Precio { get; set; }
        public string Contenedor { get; set; }
        public Nullable<bool> Procesado { get; set; }
        public Nullable<System.DateTime> Creacion { get; set; }
        public Nullable<System.DateTime> Modificacion { get; set; }
    }
}
