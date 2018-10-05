using ReCountant.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReCountant.Models
{
    public class FinanceTran
    {
        public List<F_Financial_Transactions> FinanTrans { get; set; }
    }
    public class Financial_Transactions
    {
        public DateTime Document_Date { get; set; }
        public string Voucher_Number { get; set; }
        public DateTime? DateTime_Of_Entry { get; set; }
        public string Voucher_Type { get; set; }
        public string Reference_Number { get; set; }
        public Nullable<int> Account_Id { get; set; }
        public Nullable<int> Supplier_Id { get; set; }
        public Nullable<int> Customer_Id { get; set; }
        public Nullable<int> Employee_Id { get; set; }
        public Nullable<int> Owner_Id { get; set; }
        public Nullable<int> Company_Id { get; set; }
        public Nullable<int> transparty { get; set; }
        public Nullable<int> Plot { get; set; }
        public Nullable<int> Land { get; set; }
        public Nullable<double> Quantity { get; set; }
        public Nullable<double> Amount_TC { get; set; }
        public Nullable<double> Exchange_Rate { get; set; }
        public double Amount_LC { get; set; }
        public string Description { get; set; }
        public Nullable<double> Debit { get; set; }
        public Nullable<double> Credit { get; set; }
        public Nullable<double> Net { get; set; }
        public Nullable<int> ResponsibilityCenter_Id { get; set; }
        public Nullable<int> Function_Id { get; set; }
        public Nullable<int> Project { get; set; }
        public Nullable<int> REA { get; set; }
        public Nullable<int> REP { get; set; }
        public int? Space_Type { get; set; }
        public string Knocking_Off { get; set; }
        public Nullable<int> Userid { get; set; }
        public string Currency { get; set; }
    }
    public class Installmentdata
    {
        public int Nth_Installment { get; set; }
        public float Amount_TC { get; set; }
        public float Amount_LC { get; set; }
        public string Currency { get; set; }
        public float Exchange_Rate { get; set; }
        public string Due_Date { get; set; }
        public int Plot_Id { get; set; }

    }
    public class BookingInfo
    {
        public List<Installmentdata> inst{ get; set; }
        public long Custid { get; set; }
        public UserGeneralInfo customer { get; set; }
        public Plotinfo Plot { get; set; }

    }
    public class UserGeneralInfo
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string CNIC_Number { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }
        public string NTN { get; set; }
        public string STRN { get; set; }
        public string Residential_Status { get; set; }
        public string Industry { get; set; }
        public string Supply_Chain_Role { get; set; }
        public string Filer_NonFiler { get; set; }
        public string Individual_AOP_Company { get; set; }
        public string Business { get; set; }
        public string EPZ { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }
    public class Plotinfo
    {
        public int Plot_Id { get; set; }
        public float Value { get; set; }
        public float Exchange_Rate { get; set; }
        
    }
    public class Plot_Customer_Installments_Details
    {
        public PlotDetails Plot { get; set; }
        public D_Customer Customer{ get; set; }
        public List<Installment_Plan> Installments { get; set; }
        public double? Balance { get; set; }
    }
    public class PlotDetails
    {
        public int Id { get; set; }
        public string Block_Code { get; set; }
        public string UOM { get; set; }
        public Nullable<double> Area { get; set; }
        public Nullable<double> Length { get; set; }
        public Nullable<double> Width { get; set; }
        public Nullable<double> Front { get; set; }
        public Nullable<System.DateTime> Date_Of_Sale { get; set; }
        public string Plot_No { get; set; }
        public string Phase_Code { get; set; }
        public double Plot_Value { get; set; }
    }
    public class UserInfo
    {
        public long Id { get; set; }

        public string Name { get; set; }
        public string CNIC_Number { get; set; }
        public string Address { get; set; }
        public string Contact_Number { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public string NTN { get; set; }
        public string STRN { get; set; }
        public string Residential_Status { get; set; }
        public string Industry { get; set; }
        public string Supply_Chain_Role { get; set; }
        public string Filer_NonFiler { get; set; }
        public string Individual_AOP_Company { get; set; }
        public string Business { get; set; }
        public string EPZ { get; set; }
    }
    public partial class supplier
    {
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
    }
    public class Record_Financial_Transactions
    {
        public Nullable<int> Plot { get; set; }
        public Nullable<int> Customer_Id { get; set; }
        public Nullable<int> Account_Id { get; set; }
    }
    public class ReceiveInstallent_Details
    {
        public Nullable<int> Customer_Id { get; set; }
        public Nullable<int> Plotid { get; set; }
        public string PlotCode { get; set; }
        public string Phase { get; set; }
        public string Block { get; set; }
        public Nullable<double> Amount_TC { get; set; }
        public Nullable<double> Exchange_Rate { get; set; }
        public double Amount_LC { get; set; }
        public string Currency { get; set; }
        public float Balance { get; set; }
        public int? Instid { get; set; }
    }
    public class Project
    {
        
        public int Id { get; set; }
        public string Name { get; set; }
       
    }
    public class ResonsibilityCenter
    {

        public long Id { get; set; }
        public string Responsibility_Center { get; set; }
        public string Department_Head { get; set; }
        public string Approving_Authority { get; set; }
    }
    public class RealStateActivity
    {
      
        public long Id { get; set; }
        public string RealEstate_Activites { get; set; }
        public int RealEstate_Activites_L2 { get; set; }
        
    }
    public class RealStatePlayer
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string CNIC { get; set; }
        public string Address { get; set; }
        public string Contact { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public string NTN { get; set; }
        public string STRN { get; set; }
        public string Residential_Status { get; set; }
        public int Industry { get; set; }
        public int Supply_Chain_Role { get; set; }
        public bool Filer_NonFiler { get; set; }
        public string Individual_AOP_Company { get; set; }
        public string Bank_Account { get; set; }
        public string Bussiness { get; set; }
        public string EPZ { get; set; }
        public long Userid { get; set; }

    }
    public class company
    {
        public int Id_company { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Contact_Information { get; set; }
        public string NTN { get; set; }
        public string STRN { get; set; }
        public string Residential_Status { get; set; }
        public string Filer_NonFiler { get; set; }
        public string Individual_AOP_Company { get; set; }
        public string Supply_Chain_Role { get; set; }
        public string Business { get; set; }
        public string EPZ { get; set; }
        public string REP { get; set; }
       

    }
    public class Customer
    {
        public int Id { get; set; }
        public long USerid { get; set; }
        public string Customer_Info { get; set; }
        public string Name { get; set; }


    }
    public class Employee
    {
        public int Id { get; set; }
        public long USerid { get; set; }
        public string Employee_Info { get; set; }
        public string Name { get; set; }


    }
    public class SpaceType
    {
        public int Id { get; set; }
        
        public string Space_Type { get; set; }
      


    }
    public class Owner
    {
        public int Id { get; set; }

        public long Userid { get; set; }
        public string Owner_Info { get; set; }
        public string Name { get; set; }
        
    }
    


}