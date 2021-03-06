//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ReCountant.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class D_Supplier
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public D_Supplier()
        {
            this.F_Financial_Transactions = new HashSet<F_Financial_Transactions>();
            this.Balance_TransactionParty = new HashSet<Balance_TransactionParty>();
        }
    
        public long Id { get; set; }
        public long Userid { get; set; }
        public string Supplier_Info { get; set; }
        public string Name { get; set; }
        public string CNIC_Number { get; set; }
        public string Industry { get; set; }
        public string Supply_Chain_Role { get; set; }
        public string Business { get; set; }
        public string Billing_Address { get; set; }
        public string Shipping_Address { get; set; }
        public string Billing_Contact_Number { get; set; }
        public string Shipping_Contact_Number { get; set; }
        public string Billing_Email_Id { get; set; }
        public string Shipping_Email_Id { get; set; }
        public string Product_Type { get; set; }
        public string Supplier_Code { get; set; }
        public string Payment_Method { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<F_Financial_Transactions> F_Financial_Transactions { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Balance_TransactionParty> Balance_TransactionParty { get; set; }
    }
}
