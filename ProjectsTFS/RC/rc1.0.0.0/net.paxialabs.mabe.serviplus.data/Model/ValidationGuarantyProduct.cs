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
    
    public partial class ValidationGuarantyProduct
    {
        public int PK_ValidationGuarantyProductID { get; set; }
        public Nullable<int> FK_ProducID { get; set; }
        public string Country { get; set; }
        public string Model { get; set; }
        public string ClientID { get; set; }
        public string Months { get; set; }
        public Nullable<System.DateTime> ValidFrom { get; set; }
        public Nullable<System.DateTime> ValidTo { get; set; }
        public bool Status { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public Nullable<System.DateTime> ModifyDate { get; set; }
    
        public virtual Product Product { get; set; }
    }
}
