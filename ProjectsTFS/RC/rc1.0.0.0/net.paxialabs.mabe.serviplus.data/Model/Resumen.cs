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
    
    public partial class Resumen
    {
        public int ResumenID { get; set; }
        public string Tipo { get; set; }
        public string Contenedor { get; set; }
        public Nullable<System.DateTime> Fecha { get; set; }
        public Nullable<int> Registros { get; set; }
        public Nullable<int> Actualizados { get; set; }
        public Nullable<int> Insertados { get; set; }
        public Nullable<bool> Procesado { get; set; }
        public Nullable<bool> BI_ODS_Udp { get; set; }
        public Nullable<System.DateTime> Inicio { get; set; }
        public Nullable<System.DateTime> Termino { get; set; }
        public Nullable<System.DateTime> Creacion { get; set; }
        public Nullable<System.DateTime> Modificacion { get; set; }
    }
}
