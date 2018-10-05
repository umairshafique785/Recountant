using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReCountant.Models
{
    public class TransactionRecordingLinesModel
    {
        public DateTime Voucher_Date { get; set; }
        public string VoucherNumber { get; set; }
        public string ChartofAccount { get; set; }
        public string Transaction_Document_Number { get; set; }
        public string Purchase_Order { get; set; }
        public string Product { get; set; }
        public string Region { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public string Uom { get; set; }
        public int Rate { get; set; }
        public int Amount_TC { get; set; }
        public float Exchange_Rate { get; set; }
        public string Currency { get; set; }
        public int Amount_LC { get; set; }
        public int Customer_Id { get; set; }
        public int Employee_Id { get; set; }
        public int Owner_Id { get; set; }
        public int Supplier_Id { get; set; }
        public int Beneficiary { get; set; }
        public int Company_Id { get; set; }
        public string transparty { get; set; }
        public int REA { get; set; }
        public int REP { get; set; }
        public int Project { get; set; }
        public int Function_Id { get; set; }
        public int Space_Type { get; set; }
        public int ResponsibilityCenter_Id { get; set; }
        public string Voucher_Type { get; set; }
        public float Credit { get; set; }
        public float Debit { get; set; }
        public string Recording_Index { get; set; }
        public string COA { get; set; }
        public string Reference_Number { get; set; }
        public string status { get; set; }
    }
    public class PaymentRecordingModel
    {
        public string Recording_Index { get; set; }
        public DateTime Voucher_Date { get; set; }
        //public string VoucherNumber { get; set; }
        public string Voucher_Type { get; set; }
        public string ChartofAccount { get; set; }
        public float Exchange_Rate { get; set; }
        public int Amount_TC { get; set; }
        public int Amount_LC { get; set; }
        public float Total_Credit { get; set; }
      
    }

    public class TransactionSummaryModel
    {
        public DateTime Voucher_Date { get; set; }
        public string VoucherNumber { get; set; }
        public string ChartofAccount { get; set; }
        public int Supplier_Id { get; set; }
        public int Company_Id { get; set; }
        public int ResponsibilityCenter_Id { get; set; }
        public string Voucher_Type { get; set; }
        public float Credit { get; set; }
        public float Debit { get; set; }
        public string Reference_Number { get; set; }
    }
    public class Debit_Credit
    {
        public double Debit { get; set; }
        public double Credit { get; set; }
    }
}