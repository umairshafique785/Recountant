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
    
    public partial class F_Land_Purchase_RealEstate
    {
        public int Id { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public Nullable<System.DateTime> Time_Of_Entry { get; set; }
        public Nullable<int> Land_Id { get; set; }
        public string UOM { get; set; }
        public Nullable<double> Area { get; set; }
        public Nullable<System.DateTime> Date_Of_Transfer { get; set; }
        public Nullable<System.DateTime> Date_Of_Payment { get; set; }
        public string Payment_Procedure { get; set; }
        public string Details_Of_PaymentProcedure { get; set; }
        public string Land_Description { get; set; }
        public Nullable<int> REP { get; set; }
        public Nullable<int> Project_Id { get; set; }
        public Nullable<int> Userid { get; set; }
        public string Address { get; set; }
    }
}
