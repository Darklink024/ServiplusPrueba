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
    
    public partial class InterfaceLog
    {
        public int InterfaceLogID { get; set; }
        public Nullable<int> Linea { get; set; }
        public string Mensaje { get; set; }
        public Nullable<int> Numero { get; set; }
        public string Procedimiento { get; set; }
        public Nullable<int> Serveridad { get; set; }
        public Nullable<int> Estado { get; set; }
        public string Proceso { get; set; }
        public string Contenerdor { get; set; }
        public Nullable<System.DateTime> Fecha { get; set; }
    }
}
