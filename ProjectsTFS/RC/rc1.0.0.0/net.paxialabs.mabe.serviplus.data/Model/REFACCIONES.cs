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
    
    public partial class REFACCIONES
    {
        public int RefaccionID { get; set; }
        public string ID_REF { get; set; }
        public string CENTRO { get; set; }
        public string ALMACEN { get; set; }
        public Nullable<int> TOTDISP { get; set; }
        public Nullable<bool> Procesado { get; set; }
        public Nullable<System.DateTime> Creacion { get; set; }
        public Nullable<System.DateTime> Modificacion { get; set; }
    }
}
