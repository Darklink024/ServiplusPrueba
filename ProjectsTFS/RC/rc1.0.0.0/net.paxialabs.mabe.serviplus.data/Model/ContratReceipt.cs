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
    
    public partial class ContratReceipt
    {
        public int PK_ContratID { get; set; }
        public int Fk_OrderID { get; set; }
        public string Folio { get; set; }
        public string Ruta { get; set; }
        public bool Status { get; set; }
        public System.DateTime CreateDate { get; set; }
        public System.DateTime ModifyDate { get; set; }
    
        public virtual Orders Orders { get; set; }
    }
}
