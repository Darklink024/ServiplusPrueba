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
    
    public partial class Product
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Product()
        {
            this.BuildOfMaterials = new HashSet<BuildOfMaterials>();
            this.CodeFailureByProduct = new HashSet<CodeFailureByProduct>();
            this.InstalledBase = new HashSet<InstalledBase>();
            this.ModelSerialNumber = new HashSet<ModelSerialNumber>();
            this.ValidationGuarantyProduct = new HashSet<ValidationGuarantyProduct>();
            this.ValidationGuarantyBOM = new HashSet<ValidationGuarantyBOM>();
            this.RefSell = new HashSet<RefSell>();
        }
    
        public int PK_ProductID { get; set; }
        public string Model { get; set; }
        public string ProductName { get; set; }
        public string BarCode { get; set; }
        public bool Status { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public Nullable<System.DateTime> ModifyDate { get; set; }
        public string SaleOrganization { get; set; }
        public string DistributionChannel { get; set; }
        public string Center { get; set; }
        public string MaterialGroup1 { get; set; }
        public string MaterialGroup4 { get; set; }
        public string ProductType { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BuildOfMaterials> BuildOfMaterials { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CodeFailureByProduct> CodeFailureByProduct { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<InstalledBase> InstalledBase { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ModelSerialNumber> ModelSerialNumber { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ValidationGuarantyProduct> ValidationGuarantyProduct { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ValidationGuarantyBOM> ValidationGuarantyBOM { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RefSell> RefSell { get; set; }
    }
}
