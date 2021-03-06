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
    
    public partial class Prices
    {
        public int PK_PriceID { get; set; }
        public Nullable<int> FK_BuildOfMaterialsID { get; set; }
        public Nullable<int> FK_ProductID { get; set; }
        public Nullable<int> FK_WorkforceID { get; set; }
        public string TypeCondition { get; set; }
        public string SalesOrganization { get; set; }
        public string DistributionChannel { get; set; }
        public string ListPrice { get; set; }
        public string GroupMaterial1 { get; set; }
        public string GroupMaterial4 { get; set; }
        public Nullable<double> Price { get; set; }
        public string Coin { get; set; }
        public Nullable<System.DateTime> DateValidity { get; set; }
        public Nullable<System.DateTime> DateValidityEnd { get; set; }
        public string Policy { get; set; }
        public string Guaranty { get; set; }
        public bool Status { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public Nullable<System.DateTime> ModifyDate { get; set; }
    
        public virtual BuildOfMaterials BuildOfMaterials { get; set; }
        public virtual Workforce Workforce { get; set; }
    }
}
