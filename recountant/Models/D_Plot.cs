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
    
    public partial class D_Plot
    {
        public int Id { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public Nullable<int> Block_Id { get; set; }
        public string UOM { get; set; }
        public Nullable<double> Area { get; set; }
        public Nullable<double> Length { get; set; }
        public Nullable<double> Width { get; set; }
        public Nullable<double> Front { get; set; }
        public Nullable<System.DateTime> Date_Of_Sale { get; set; }
        public string Plot_No { get; set; }
        public Nullable<int> Userid { get; set; }
        public Nullable<bool> Plot_Availibilty { get; set; }
    }
}