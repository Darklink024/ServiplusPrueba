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
    
    public partial class LogCRM
    {
        public int PK_LogID { get; set; }
        public int FK_OrderID { get; set; }
        public string UpdateBase { get; set; }
        public string UpdateRefMan { get; set; }
        public string UpdateODS { get; set; }
        public string ModuleReserveSP { get; set; }
        public string ADRReserveSP { get; set; }
        public bool Status { get; set; }
        public System.DateTime CreateDate { get; set; }
        public System.DateTime ModifyDate { get; set; }
    
        public virtual Orders Orders { get; set; }
    }
}
