﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class ReCountantEntities : DbContext
    {
        public ReCountantEntities()
            : base("name=ReCountantEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<C__MigrationHistory> C__MigrationHistory { get; set; }
        public virtual DbSet<Balance_COA> Balance_COA { get; set; }
        public virtual DbSet<Balance_Installments> Balance_Installments { get; set; }
        public virtual DbSet<D_Block> D_Block { get; set; }
        public virtual DbSet<D_Land_RealEstate> D_Land_RealEstate { get; set; }
        public virtual DbSet<D_Phase> D_Phase { get; set; }
        public virtual DbSet<D_Plot> D_Plot { get; set; }
        public virtual DbSet<D_Plot_Booking> D_Plot_Booking { get; set; }
        public virtual DbSet<D_Plot_Value> D_Plot_Value { get; set; }
        public virtual DbSet<D_Project_Specs> D_Project_Specs { get; set; }
        public virtual DbSet<D_Projects> D_Projects { get; set; }
        public virtual DbSet<D_ReportingFunction> D_ReportingFunction { get; set; }
        public virtual DbSet<D_VoucherType> D_VoucherType { get; set; }
        public virtual DbSet<F_Land_Purchase_RealEstate> F_Land_Purchase_RealEstate { get; set; }
        public virtual DbSet<Installment_Plan> Installment_Plan { get; set; }
        public virtual DbSet<Installment_Received> Installment_Received { get; set; }
        public virtual DbSet<LandPlotMapping> LandPlotMappings { get; set; }
        public virtual DbSet<Mapping_Company_Customer> Mapping_Company_Customer { get; set; }
        public virtual DbSet<Mapping_Company_Employee> Mapping_Company_Employee { get; set; }
        public virtual DbSet<Mapping_Company_Owner> Mapping_Company_Owner { get; set; }
        public virtual DbSet<Mapping_Company_ResponsibilityCenter> Mapping_Company_ResponsibilityCenter { get; set; }
        public virtual DbSet<Mapping_Company_Supplier> Mapping_Company_Supplier { get; set; }
        public virtual DbSet<Mapping_Employee_ResponsibilityCenter> Mapping_Employee_ResponsibilityCenter { get; set; }
        public virtual DbSet<Mapping_Installment_Planned_Recevied> Mapping_Installment_Planned_Recevied { get; set; }
        public virtual DbSet<Mapping_Land_Plot> Mapping_Land_Plot { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<UserClaim> UserClaims { get; set; }
        public virtual DbSet<UserLogin> UserLogins { get; set; }
        public virtual DbSet<Transaction_Rules> Transaction_Rules { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<D_Company> D_Company { get; set; }
        public virtual DbSet<D_ResponsibilityCenter> D_ResponsibilityCenter { get; set; }
        public virtual DbSet<RealEstate_Activity> RealEstate_Activity { get; set; }
        public virtual DbSet<REP> REPs { get; set; }
        public virtual DbSet<D_Customer> D_Customer { get; set; }
        public virtual DbSet<D_SpaceType> D_SpaceType { get; set; }
        public virtual DbSet<D_Employee> D_Employee { get; set; }
        public virtual DbSet<D_Owner> D_Owner { get; set; }
        public virtual DbSet<F_Financial_Transactions> F_Financial_Transactions { get; set; }
        public virtual DbSet<Summary_Table> Summary_Table { get; set; }
        public virtual DbSet<D_Supplier> D_Supplier { get; set; }
        public virtual DbSet<Balance_TransactionParty> Balance_TransactionParty { get; set; }
    
        public virtual int SP_Add_AccountHead(Nullable<int> parent_Id, string accountName)
        {
            var parent_IdParameter = parent_Id.HasValue ?
                new ObjectParameter("Parent_Id", parent_Id) :
                new ObjectParameter("Parent_Id", typeof(int));
    
            var accountNameParameter = accountName != null ?
                new ObjectParameter("AccountName", accountName) :
                new ObjectParameter("AccountName", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SP_Add_AccountHead", parent_IdParameter, accountNameParameter);
        }
    
        public virtual ObjectResult<SP_Get_AccountHead_Result> SP_Get_AccountHead()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_Get_AccountHead_Result>("SP_Get_AccountHead");
        }
    
        public virtual ObjectResult<Nullable<int>> SP_GET_AccountHeadId(string accountName)
        {
            var accountNameParameter = accountName != null ?
                new ObjectParameter("AccountName", accountName) :
                new ObjectParameter("AccountName", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<int>>("SP_GET_AccountHeadId", accountNameParameter);
        }
    }
}
