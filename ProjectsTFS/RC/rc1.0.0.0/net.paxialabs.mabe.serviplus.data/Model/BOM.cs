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
    
    public partial class BOM
    {
        public int BOMID { get; set; }
        public string MaterialPT { get; set; }
        public string Centro { get; set; }
        public string IDRefaccion { get; set; }
        public string Cantidad { get; set; }
        public string Contenedor { get; set; }
        public Nullable<bool> Procesado { get; set; }
        public Nullable<System.DateTime> Creacion { get; set; }
        public Nullable<System.DateTime> Modificacion { get; set; }
    }
}
