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
    
    public partial class Client
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Client()
        {
            this.InstalledBase = new HashSet<InstalledBase>();
            this.Policy = new HashSet<Policy>();
            this.RefSell = new HashSet<RefSell>();
        }
    
        public int PK_ClientID { get; set; }
        public string ClientID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string StreetAddress { get; set; }
        public string NumberExternalAddress { get; set; }
        public string NumberInternalAddress { get; set; }
        public string BoroughAddress { get; set; }
        public string MunicipalityAddress { get; set; }
        public string PostalCodeAddress { get; set; }
        public string AdditionalInfoAddress1 { get; set; }
        public string AdditionalInfoAddress2 { get; set; }
        public string AdditionalInfoAddress3 { get; set; }
        public string AdditionalInfoAddress4 { get; set; }
        public string AdditionalInfoAddress5 { get; set; }
        public string PhoneNumber1 { get; set; }
        public string PhoneNumber2 { get; set; }
        public string PhoneNumber3 { get; set; }
        public string PhoneExtension1 { get; set; }
        public string Email { get; set; }
        public bool Status { get; set; }
        public System.DateTime CreateDate { get; set; }
        public System.DateTime ModifyDate { get; set; }
        public string RFC { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<InstalledBase> InstalledBase { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Policy> Policy { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RefSell> RefSell { get; set; }
    }
}