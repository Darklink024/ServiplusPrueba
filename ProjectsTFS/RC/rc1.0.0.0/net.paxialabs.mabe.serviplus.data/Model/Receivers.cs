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
    
    public partial class Receivers
    {
        public int FK_ConfigurationID { get; set; }
        public int FK_UserID { get; set; }
        public bool MessageCreate { get; set; }
        public System.DateTime CreateDate { get; set; }
        public System.DateTime ModifyDate { get; set; }
    
        public virtual Configuration Configuration { get; set; }
        public virtual User User { get; set; }
    }
}
