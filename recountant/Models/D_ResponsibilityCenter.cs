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
    
    public partial class D_ResponsibilityCenter
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public D_ResponsibilityCenter()
        {
            this.F_Financial_Transactions = new HashSet<F_Financial_Transactions>();
        }
    
        public int Id { get; set; }
        public string Responsibilty_Center { get; set; }
        public string Department_Head { get; set; }
        public string Approving_Authority { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<F_Financial_Transactions> F_Financial_Transactions { get; set; }
    }
}
