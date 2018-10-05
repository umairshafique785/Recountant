using Microsoft.AspNet.Identity;
using ReCountant.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ReCountant.Controllers
{
    public class PurchaseAndPayablesController : Controller
    {
        private ReCountantEntities db = new ReCountantEntities();
        // GET: PurchaseAndPayables
        public ActionResult Index()
        {
            long userid = long.Parse(User.Identity.GetUserId());
            var res = from x in db.Mapping_Company_Employee
                      join a in db.D_Company on x.Company_Id equals a.Id
                      where x.Employee_Id == userid
                      select new { a.Id , a.Name };

            ViewBag.companies = new SelectList(res,"Id","Name");
            return View();
        }
       
        [HttpPost]
        public JsonResult PostTransactionRecord(List<TransactionRecordingLinesModel> transaction, List<TransactionSummaryModel> TransSum,string reference_number)
           {

            //int Min = 0;
            //int Max = 20;
            //Random randNum = new Random();
            //int[] test2 = Enumerable
            //    .Repeat(0, 5)
            //    .Select(i => randNum.Next(Min, Max))
            //    .ToArray();
            var Get_Reference_num = db.F_Financial_Transactions.Select(x => x.Reference_Number).ToList();
           
            
            long userid = long.Parse(User.Identity.GetUserId());
            List<F_Financial_Transactions> res = new List<F_Financial_Transactions>();
            List<F_Financial_Transactions> res2 = new List<F_Financial_Transactions>();
           

            foreach (var line in    transaction)
            {
                var CoaId = Check_Accountid(line.ChartofAccount);
                var data = CheckTransType(line.Amount_LC, line.Voucher_Type, CoaId);



                F_Financial_Transactions res1 = new F_Financial_Transactions()
                {
                    Account_Id = CoaId,
                    Amount_LC = (line.Amount_TC)*(line.Exchange_Rate),
                    Amount_TC = line.Amount_TC,
                    Company_Id =  line.Company_Id,
                    Currency = line.Currency,
                    DateTime_Of_Entry = DateTime.UtcNow,
                    Space_Type=line.Space_Type,
                    Description = line.Description,
                    Document_Date = line.Voucher_Date,
                    Exchange_Rate = line.Exchange_Rate,
                    Product = line.Product,
                    Project = line.Project,
                    Quantity = line.Quantity,
                    REA = line.REA,
                    REP = line.REP,
                    ResponsibilityCenter_Id = line.ResponsibilityCenter_Id,
                    Supplier_Id = line.Supplier_Id,
                    Transaction_Party = line.Supplier_Id,
                    Userid = userid,
                    Voucher_Number = line.VoucherNumber,
                    Reference_Number = line.Reference_Number,
                    //Reference_Number=  test2.ToString(),
                    //Reference_Number = line.VoucherNumber,
                    Voucher_Type = line.Voucher_Type,
                    Transaction_Document_Number = line.Transaction_Document_Number,
                    Credit = data.Credit,
                    Debit = data.Debit,
                    Customer_Id = line.Customer_Id,
                    Employee_Id = line.Employee_Id,
                    Function_Id = line.Function_Id,
                    Owner_Id = line.Owner_Id,
                    Region = line.Region,
                    Purchase_Order = line.Purchase_Order,
                    Recording_Index = line.Recording_Index,
                    COA = line.COA,
                    Net =(data.Debit)-(data.Credit),
                    Rate = line.Rate,
                    Uom = line.Uom,
                    Status = line.status
                };
                res.Add(res1);
            }
            db.F_Financial_Transactions.AddRange(res);
            db.SaveChanges();

            foreach (var line in transaction)
            {
                if (line.COA=="supplier")

                {
                    var CoaId = Check_Accountid(line.ChartofAccount);
                    var data = CheckTransType(line.Amount_LC, line.Voucher_Type, CoaId);
                    Balance_TransactionParty balances = new Balance_TransactionParty
                    {

                        Invoice_No = line.Reference_Number,
                        Account_Id = CoaId,
                        Date = DateTime.UtcNow,
                        Time_Of_Entry=DateTime.UtcNow,
                        Transaction_Party = line.Supplier_Id,
                        Company_Id = line.Company_Id,
                        Currency = line.Currency,
                        Amount_TC = line.Amount_TC,
                        Exchange_Rate = line.Exchange_Rate,
                        Amount_LC = line.Amount_LC,
                        Description = line.Description,
                        Debit = data.Debit,
                        Credit = data.Credit,
                        Net = data.Debit - data.Credit,
                    };
                    db.Balance_TransactionParty.Add(balances);
                    db.SaveChanges();
                    
                   
                }
            }
                

            var sumres = TransSum.Select(ft => new Summary_Table()
            {
                COA = Check_Accountid(ft.ChartofAccount),
                Credit = ft.Credit,
                Debit = ft.Debit,
                Supplier_Id = ft.Supplier_Id,
                Voucher_Number = ft.Reference_Number,
                Document_Date = DateTime.UtcNow,
                ResponsibilityCenter_Id = ft.ResponsibilityCenter_Id,
                Voucher_Type = ft.Voucher_Type,
                Company_Id =  ft.Company_Id
            }).ToList();
            db.Summary_Table.AddRange(sumres);
            db.SaveChanges();

            // one row make for payable colium count

            //foreach (var line in payablerow)
            //{
            //    var CoaId = Check_Accountid(line.ChartofAccount);
            //    var data = CheckTransType(line.Amount_LC, line.Voucher_Type, CoaId);
                
            //    F_Financial_Transactions rocordingrow = new F_Financial_Transactions()
            //    {
            //        Account_Id = CoaId,
            //        Amount_LC = line.Amount_LC,
            //        Amount_TC = line.Amount_TC,
            //        Company_Id = line.Company_Id,
            //        Currency = line.Currency,
            //        DateTime_Of_Entry = DateTime.UtcNow,
            //        Space_Type = line.Space_Type,
            //        Description = line.Description,
            //        Document_Date = line.Voucher_Date,
            //        Exchange_Rate = line.Exchange_Rate,
            //        Product = line.Product,
            //        Project = line.Project,
            //        Quantity = line.Quantity,
            //        REA = line.REA,
            //        REP = line.REP,
            //        ResponsibilityCenter_Id = line.ResponsibilityCenter_Id,
            //        Supplier_Id = line.Supplier_Id,
            //        Transaction_Party = line.Supplier_Id,
            //        Userid = userid,
            //        Voucher_Number = line.VoucherNumber,
            //        Reference_Number = line.VoucherNumber,
            //        Voucher_Type = line.Voucher_Type,
            //        Transaction_Document_Number = line.Transaction_Document_Number,
            //        Credit = line.Credit,
            //        Debit = data.Debit,
            //        Customer_Id = line.Customer_Id,
            //        Employee_Id = line.Employee_Id,
            //        Function_Id = line.Function_Id,
            //        Owner_Id = line.Owner_Id,
            //        Region = line.Region,
            //        Purchase_Order = line.Purchase_Order,
            //        Recording_Index = line.Recording_Index,
            //        Net = (data.Debit) - (data.Credit),
            //        Rate = line.Rate,
            //        Uom = line.Uom
            //    };
            //    res2.Add(rocordingrow);
            //}
            //db.F_Financial_Transactions.AddRange(res2);
            //db.SaveChanges();
            return Json(true);
        }



        // post payments
        public JsonResult PostTransactionpayments( string vouch_N, float? TotalSumOfCredits, int? lc, int? EX_R, float? Amt_tc, string CashBank , string curroption, string pay_radio_val, int? full_pay_dis, string pay_ty_full)
         {
            List<F_Financial_Transactions> res = new List<F_Financial_Transactions>();
            List<F_Financial_Transactions> res2 = new List<F_Financial_Transactions>();
            List<F_Financial_Transactions> res3 = new List<F_Financial_Transactions>();

            long userid = long.Parse(User.Identity.GetUserId());

            if (pay_radio_val == "Partial_Payment")
            {
                var str = from a in db.F_Financial_Transactions where a.Reference_Number == vouch_N && a.COA == "supplier" && a.Credit > 0 select a;

                var count = db.F_Financial_Transactions.Where(a=> a.Reference_Number == vouch_N && a.COA == "supplier" && a.Credit > 0).Count();

                if (count == 1)
                {
                    foreach (var line in str)
                    {
                        //var CoaId = Check_Accountid(line.ChartofAccount);
                        //var data = CheckTransType(line.Amount_LC, line.Voucher_Type, CoaId);
                        F_Financial_Transactions ls = new F_Financial_Transactions()
                        {
                            //Account_Id = CoaId,
                            Amount_LC = Convert.ToDouble(lc),
                            Amount_TC = Amt_tc,
                            Company_Id = line.Company_Id,
                            Currency = line.Currency,
                            DateTime_Of_Entry = DateTime.UtcNow,
                            Space_Type = line.Space_Type,
                            Description = line.Description,
                            Document_Date = line.Document_Date,
                            Exchange_Rate = EX_R,
                            Product = line.Product,
                            Project = line.Project,
                            Quantity = line.Quantity,
                            REA = line.REA,
                            REP = line.REP,
                            ResponsibilityCenter_Id = line.ResponsibilityCenter_Id,
                            Supplier_Id = line.Supplier_Id,
                            Transaction_Party = line.Supplier_Id,
                            Userid = userid,
                            Voucher_Number = line.Voucher_Number,
                            Reference_Number = line.Reference_Number,
                            Voucher_Type = line.Voucher_Type,
                            Transaction_Document_Number = line.Transaction_Document_Number,
                            Credit = 0,
                            Debit = Convert.ToInt32(((line.Credit / ((line.Credit))) * Convert.ToDouble(lc))),
                            Customer_Id = line.Customer_Id,
                            Employee_Id = line.Employee_Id,
                            Function_Id = line.Function_Id,
                            Owner_Id = line.Owner_Id,
                            Region = line.Region,
                            Purchase_Order = line.Purchase_Order,
                            Recording_Index = line.Recording_Index,
                            COA = line.COA,
                            Net = Convert.ToInt32(((line.Credit / /*((-1)*TotalSumOfCredits)*/((line.Credit))) * Convert.ToDouble(lc))) - (0),
                            Rate = line.Rate,
                            Uom = line.Uom,
                            Status = "pending"

                        };
                        res.Add(ls);
                    }

                    foreach (var line in str)
                    {
                        //var CoaId = Check_Accountid(line.ChartofAccount);
                        //var data = CheckTransType(line.Amount_LC, line.Voucher_Type, CoaId);
                        F_Financial_Transactions ls2 = new F_Financial_Transactions()
                        {
                            //Account_Id = CoaId,
                            Amount_LC = Convert.ToDouble(lc),
                            Amount_TC = Amt_tc,
                            Company_Id = line.Company_Id,
                            Currency = curroption,
                            DateTime_Of_Entry = DateTime.UtcNow,
                            Space_Type = line.Space_Type,
                            Description = line.Description,
                            Document_Date = line.Document_Date,
                            Exchange_Rate = EX_R,
                            Product = line.Product,
                            Project = line.Project,
                            Quantity = line.Quantity,
                            REA = line.REA,
                            REP = line.REP,
                            ResponsibilityCenter_Id = line.ResponsibilityCenter_Id,
                            Supplier_Id = line.Supplier_Id,
                            Transaction_Party = line.Supplier_Id,
                            Userid = userid,
                            Voucher_Number = line.Voucher_Number,
                            Reference_Number = line.Reference_Number,
                            Voucher_Type = line.Voucher_Type,
                            Transaction_Document_Number = line.Transaction_Document_Number,
                            Credit = Convert.ToInt32(((line.Credit / /*((-1)-TotalSumOfCredits)*/((line.Credit))) * Convert.ToDouble(lc))),
                            Debit = 0,
                            Customer_Id = line.Customer_Id,
                            Employee_Id = line.Employee_Id,
                            Function_Id = line.Function_Id,
                            Owner_Id = line.Owner_Id,
                            Region = line.Region,
                            Purchase_Order = line.Purchase_Order,
                            Recording_Index = line.Recording_Index,
                            COA = CashBank,
                            Net = (0) - Convert.ToInt32(((line.Credit / /*((-1)*TotalSumOfCredits)*/((line.Credit) )) * Convert.ToDouble(lc))),
                            Rate = line.Rate,
                            Uom = line.Uom
                        };
                        res2.Add(ls2);
                    }

                    db.F_Financial_Transactions.AddRange(res);
                    db.SaveChanges();
                    db.F_Financial_Transactions.AddRange(res2);
                    db.SaveChanges();

                }
                else
                {
                    foreach (var line in str)
                    {
                        //var CoaId = Check_Accountid(line.ChartofAccount);
                        //var data = CheckTransType(line.Amount_LC, line.Voucher_Type, CoaId);
                        F_Financial_Transactions ls = new F_Financial_Transactions()
                        {
                            //Account_Id = CoaId,
                            Amount_LC = Convert.ToDouble(lc),
                            Amount_TC = Amt_tc,
                            Company_Id = line.Company_Id,
                            Currency = line.Currency,
                            DateTime_Of_Entry = DateTime.UtcNow,
                            Space_Type = line.Space_Type,
                            Description = line.Description,
                            Document_Date = line.Document_Date,
                            Exchange_Rate = EX_R,
                            Product = line.Product,
                            Project = line.Project,
                            Quantity = line.Quantity,
                            REA = line.REA,
                            REP = line.REP,
                            ResponsibilityCenter_Id = line.ResponsibilityCenter_Id,
                            Supplier_Id = line.Supplier_Id,
                            Transaction_Party = line.Supplier_Id,
                            Userid = userid,
                            Voucher_Number = line.Voucher_Number,
                            Reference_Number = line.Reference_Number,
                            Voucher_Type = line.Voucher_Type,
                            Transaction_Document_Number = line.Transaction_Document_Number,
                            Credit = 0,
                            Debit = Convert.ToInt32(((line.Credit / ((line.Credit) + (line.Credit))) * Convert.ToDouble(lc))),
                            Customer_Id = line.Customer_Id,
                            Employee_Id = line.Employee_Id,
                            Function_Id = line.Function_Id,
                            Owner_Id = line.Owner_Id,
                            Region = line.Region,
                            Purchase_Order = line.Purchase_Order,
                            Recording_Index = line.Recording_Index,
                            COA = line.COA,
                            Net = Convert.ToInt32(((line.Credit / /*((-1)*TotalSumOfCredits)*/((line.Credit) + (line.Credit))) * Convert.ToDouble(lc))) - (0),
                            Rate = line.Rate,
                            Uom = line.Uom,
                            Status = "pending"

                        };
                        res.Add(ls);
                    }

                    foreach (var line in str)
                    {
                        //var CoaId = Check_Accountid(line.ChartofAccount);
                        //var data = CheckTransType(line.Amount_LC, line.Voucher_Type, CoaId);
                        F_Financial_Transactions ls2 = new F_Financial_Transactions()
                        {
                            //Account_Id = CoaId,
                            Amount_LC = Convert.ToDouble(lc),
                            Amount_TC = Amt_tc,
                            Company_Id = line.Company_Id,
                            Currency = curroption,
                            DateTime_Of_Entry = DateTime.UtcNow,
                            Space_Type = line.Space_Type,
                            Description = line.Description,
                            Document_Date = line.Document_Date,
                            Exchange_Rate = EX_R,
                            Product = line.Product,
                            Project = line.Project,
                            Quantity = line.Quantity,
                            REA = line.REA,
                            REP = line.REP,
                            ResponsibilityCenter_Id = line.ResponsibilityCenter_Id,
                            Supplier_Id = line.Supplier_Id,
                            Transaction_Party = line.Supplier_Id,
                            Userid = userid,
                            Voucher_Number = line.Voucher_Number,
                            Reference_Number = line.Reference_Number,
                            Voucher_Type = line.Voucher_Type,
                            Transaction_Document_Number = line.Transaction_Document_Number,
                            Credit = Convert.ToInt32(((line.Credit / /*((-1)-TotalSumOfCredits)*/((line.Credit) + (line.Credit))) * Convert.ToDouble(lc))),
                            Debit = 0,
                            Customer_Id = line.Customer_Id,
                            Employee_Id = line.Employee_Id,
                            Function_Id = line.Function_Id,
                            Owner_Id = line.Owner_Id,
                            Region = line.Region,
                            Purchase_Order = line.Purchase_Order,
                            Recording_Index = line.Recording_Index,
                            COA = CashBank,
                            Net = (0) - Convert.ToInt32(((line.Credit / /*((-1)*TotalSumOfCredits)*/((line.Credit) + (line.Credit))) * Convert.ToDouble(lc))),
                            Rate = line.Rate,
                            Uom = line.Uom
                        };
                        res2.Add(ls2);
                    }

                    db.F_Financial_Transactions.AddRange(res);
                    db.SaveChanges();
                    db.F_Financial_Transactions.AddRange(res2);
                    db.SaveChanges();

                }

               



                return Json(true);
            }
            else if (pay_radio_val == "Full_Payment")
            {
                var str_Full_payment = from a in db.F_Financial_Transactions where a.Reference_Number == vouch_N  && a.Credit > 0 select a;
                var conversion_of_total_pay = (-1) * TotalSumOfCredits;
                var subtraction_of_payments = conversion_of_total_pay - full_pay_dis;

                foreach (var line in str_Full_payment)
                {
                    //var CoaId = Check_Accountid(line.ChartofAccount);
                    //var data = CheckTransType(line.Amount_LC, line.Voucher_Type, CoaId);
                    F_Financial_Transactions ls = new F_Financial_Transactions()
                    {
                        //Account_Id = CoaId,
                        Amount_LC = line.Amount_LC,
                        Amount_TC = line.Amount_TC,
                        Company_Id = line.Company_Id,
                        Currency = line.Currency,
                        DateTime_Of_Entry = DateTime.UtcNow,
                        Space_Type = line.Space_Type,
                        Description = line.Description,
                        Document_Date = line.Document_Date,
                        Exchange_Rate = line.Exchange_Rate,
                        Product = line.Product,
                        Project = line.Project,
                        Quantity = line.Quantity,
                        REA = line.REA,
                        REP = line.REP,
                        ResponsibilityCenter_Id = line.ResponsibilityCenter_Id,
                        Supplier_Id = line.Supplier_Id,
                        Transaction_Party = line.Supplier_Id,
                        Userid = userid,
                        Voucher_Number = line.Voucher_Number,
                        Reference_Number = line.Reference_Number,
                        Voucher_Type = line.Voucher_Type,
                        Transaction_Document_Number = line.Transaction_Document_Number,
                        Credit = 0,
                        Debit = ((line.Credit / TotalSumOfCredits) * TotalSumOfCredits),
                        Customer_Id = line.Customer_Id,
                        Employee_Id = line.Employee_Id,
                        Function_Id = line.Function_Id,
                        Owner_Id = line.Owner_Id,
                        Region = line.Region,
                        Purchase_Order = line.Purchase_Order,
                        Recording_Index = line.Recording_Index,
                        COA = line.COA,
                        Net = ((line.Credit / TotalSumOfCredits) * TotalSumOfCredits) - (0),
                        Rate = line.Rate,
                        Uom = line.Uom,
                        Status = "Parked"
                        
                    };
                    res.Add(ls);
                }

                foreach (var line in str_Full_payment)
                {
                    //var CoaId = Check_Accountid(line.ChartofAccount);
                    //var data = CheckTransType(line.Amount_LC, line.Voucher_Type, CoaId);
                    if (subtraction_of_payments > 0)
                    {
                        F_Financial_Transactions ls2 = new F_Financial_Transactions()
                        {
                            //Account_Id = CoaId,
                            Amount_LC = line.Amount_LC,
                            Amount_TC = line.Amount_TC,
                            Company_Id = line.Company_Id,
                            Currency = line.Currency,
                            DateTime_Of_Entry = DateTime.UtcNow,
                            Space_Type = line.Space_Type,
                            Description = line.Description,
                            Document_Date = line.Document_Date,
                            Exchange_Rate = line.Exchange_Rate,
                            Product = line.Product,
                            Project = line.Project,
                            Quantity = line.Quantity,
                            REA = line.REA,
                            REP = line.REP,
                            ResponsibilityCenter_Id = line.ResponsibilityCenter_Id,
                            Supplier_Id = line.Supplier_Id,
                            Transaction_Party = line.Supplier_Id,
                            Userid = userid,
                            Voucher_Number = line.Voucher_Number,
                            Reference_Number = line.Reference_Number,
                            Voucher_Type = line.Voucher_Type,
                            Transaction_Document_Number = line.Transaction_Document_Number,
                            Credit = ((line.Credit / TotalSumOfCredits) * subtraction_of_payments),
                            Debit = 0,
                            Customer_Id = line.Customer_Id,
                            Employee_Id = line.Employee_Id,
                            Function_Id = line.Function_Id,
                            Owner_Id = line.Owner_Id,
                            Region = line.Region,
                            Purchase_Order = line.Purchase_Order,
                            Recording_Index = line.Recording_Index,
                            COA = pay_ty_full,
                            Net = (0) - ((line.Credit / TotalSumOfCredits) * subtraction_of_payments),
                            Rate = line.Rate,
                            Uom = line.Uom
                            //Status="Parked"
                        };
                        res2.Add(ls2);
                    }
                    else
                    {
                        F_Financial_Transactions ls2 = new F_Financial_Transactions()
                        {
                            //Account_Id = CoaId,
                            Amount_LC = line.Amount_LC,
                            Amount_TC = line.Amount_TC,
                            Company_Id = line.Company_Id,
                            Currency = line.Currency,
                            DateTime_Of_Entry = DateTime.UtcNow,
                            Space_Type = line.Space_Type,
                            Description = line.Description,
                            Document_Date = line.Document_Date,
                            Exchange_Rate = line.Exchange_Rate,
                            Product = line.Product,
                            Project = line.Project,
                            Quantity = line.Quantity,
                            REA = line.REA,
                            REP = line.REP,
                            ResponsibilityCenter_Id = line.ResponsibilityCenter_Id,
                            Supplier_Id = line.Supplier_Id,
                            Transaction_Party = line.Supplier_Id,
                            Userid = userid,
                            Voucher_Number = line.Voucher_Number,
                            Reference_Number = line.Reference_Number,
                            Voucher_Type = line.Voucher_Type,
                            Transaction_Document_Number = line.Transaction_Document_Number,
                            Credit = ((line.Credit / TotalSumOfCredits) * TotalSumOfCredits),
                            Debit = 0,
                            Customer_Id = line.Customer_Id,
                            Employee_Id = line.Employee_Id,
                            Function_Id = line.Function_Id,
                            Owner_Id = line.Owner_Id,
                            Region = line.Region,
                            Purchase_Order = line.Purchase_Order,
                            Recording_Index = line.Recording_Index,
                            COA = pay_ty_full,
                            Net = (0) - ((line.Credit / TotalSumOfCredits) * TotalSumOfCredits),
                            Rate = line.Rate,
                            Uom = line.Uom
                            //Status = "Parked"
                        };
                        res2.Add(ls2);

                    }
                    if (full_pay_dis > 0)
                    {
                        F_Financial_Transactions ls3 = new F_Financial_Transactions()
                        {
                            //Account_Id = CoaId,
                            Amount_LC = line.Amount_LC,
                            Amount_TC = line.Amount_TC,
                            Company_Id = line.Company_Id,
                            Currency = line.Currency,
                            DateTime_Of_Entry = DateTime.UtcNow,
                            Space_Type = line.Space_Type,
                            Description = line.Description,
                            Document_Date = line.Document_Date,
                            Exchange_Rate = line.Exchange_Rate,
                            Product = line.Product,
                            Project = line.Project,
                            Quantity = line.Quantity,
                            REA = line.REA,
                            REP = line.REP,
                            ResponsibilityCenter_Id = line.ResponsibilityCenter_Id,
                            Supplier_Id = line.Supplier_Id,
                            Transaction_Party = line.Supplier_Id,
                            Userid = userid,
                            Voucher_Number = line.Voucher_Number,
                            Reference_Number = line.Reference_Number,
                            Voucher_Type = line.Voucher_Type,
                            Transaction_Document_Number = line.Transaction_Document_Number,
                            Credit = ((line.Credit / TotalSumOfCredits) * full_pay_dis),
                            Debit = 0,
                            Customer_Id = line.Customer_Id,
                            Employee_Id = line.Employee_Id,
                            Function_Id = line.Function_Id,
                            Owner_Id = line.Owner_Id,
                            Region = line.Region,
                            Purchase_Order = line.Purchase_Order,
                            Recording_Index = line.Recording_Index,
                            COA = "Discount",
                            Net = (0) - ((line.Credit / TotalSumOfCredits) * full_pay_dis),
                            Rate = line.Rate,
                            Uom = line.Uom,
                            //Status = "Parked"
                        };
                        res3.Add(ls3);

                    }
                }

                db.F_Financial_Transactions.AddRange(res);
                db.SaveChanges();
                db.F_Financial_Transactions.AddRange(res2);
                db.SaveChanges();
                db.F_Financial_Transactions.AddRange(res3);
                db.SaveChanges();





                return Json(true);
               
            }
            else if(pay_radio_val == "Running_Balance")
            {
                return Json(true);
            }
            else 
            {
                return Json(false);
            }


        }

        //public JsonResult PostTransactionpaymentsfull(string vouch_N, float? TotalSumOfCredits, string pay_ty_full, int? full_pay_dis_amount)
        //{


        //    var str = from a in db.F_Financial_Transactions where a.Reference_Number == vouch_N && a.Credit > 0 select a;

        //    List<F_Financial_Transactions> res = new List<F_Financial_Transactions>();
        //    List<F_Financial_Transactions> res2 = new List<F_Financial_Transactions>();

        //    long userid = long.Parse(User.Identity.GetUserId());


        //    foreach (var line in str)
        //    {
        //        //var CoaId = Check_Accountid(line.ChartofAccount);
        //        //var data = CheckTransType(line.Amount_LC, line.Voucher_Type, CoaId);
        //        F_Financial_Transactions ls = new F_Financial_Transactions()
        //        {
        //            //Account_Id = CoaId,
        //            Amount_LC = line.Amount_LC,
        //            Amount_TC = line.Amount_TC,
        //            Company_Id = line.Company_Id,
        //            Currency = line.Currency,
        //            DateTime_Of_Entry = DateTime.UtcNow,
        //            Space_Type = line.Space_Type,
        //            Description = line.Description,
        //            Document_Date = line.Document_Date,
        //            Exchange_Rate = line.Exchange_Rate,
        //            Product = line.Product,
        //            Project = line.Project,
        //            Quantity = line.Quantity,
        //            REA = line.REA,
        //            REP = line.REP,
        //            ResponsibilityCenter_Id = line.ResponsibilityCenter_Id,
        //            Supplier_Id = line.Supplier_Id,
        //            Transaction_Party = line.Supplier_Id,
        //            Userid = userid,
        //            Voucher_Number = line.Voucher_Number,
        //            Reference_Number = line.Voucher_Number,
        //            Voucher_Type = line.Voucher_Type,
        //            Transaction_Document_Number = line.Transaction_Document_Number,
        //            Credit = 0,
        //            Debit = ((line.Credit / TotalSumOfCredits) * TotalSumOfCredits),
        //            Customer_Id = line.Customer_Id,
        //            Employee_Id = line.Employee_Id,
        //            Function_Id = line.Function_Id,
        //            Owner_Id = line.Owner_Id,
        //            Region = line.Region,
        //            Purchase_Order = line.Purchase_Order,
        //            Recording_Index = line.Recording_Index,
        //            COA = line.COA,
        //            Net = ((line.Credit / TotalSumOfCredits) * TotalSumOfCredits) - (0),
        //            Rate = line.Rate,
        //            Uom = line.Uom
        //        };
        //        res.Add(ls);
        //    }

        //    foreach (var line in str)
        //    {
        //        //var CoaId = Check_Accountid(line.ChartofAccount);
        //        //var data = CheckTransType(line.Amount_LC, line.Voucher_Type, CoaId);
        //        F_Financial_Transactions ls2 = new F_Financial_Transactions()
        //        {
        //            //Account_Id = CoaId,
        //            Amount_LC = line.Amount_LC,
        //            Amount_TC = line.Amount_TC,
        //            Company_Id = line.Company_Id,
        //            Currency = line.Currency,
        //            DateTime_Of_Entry = DateTime.UtcNow,
        //            Space_Type = line.Space_Type,
        //            Description = line.Description,
        //            Document_Date = line.Document_Date,
        //            Exchange_Rate = line.Exchange_Rate,
        //            Product = line.Product,
        //            Project = line.Project,
        //            Quantity = line.Quantity,
        //            REA = line.REA,
        //            REP = line.REP,
        //            ResponsibilityCenter_Id = line.ResponsibilityCenter_Id,
        //            Supplier_Id = line.Supplier_Id,
        //            Transaction_Party = line.Supplier_Id,
        //            Userid = userid,
        //            Voucher_Number = line.Voucher_Number,
        //            Reference_Number = line.Voucher_Number,
        //            Voucher_Type = line.Voucher_Type,
        //            Transaction_Document_Number = line.Transaction_Document_Number,
        //            Credit = ((line.Credit / TotalSumOfCredits) * TotalSumOfCredits),
        //            Debit = 0,
        //            Customer_Id = line.Customer_Id,
        //            Employee_Id = line.Employee_Id,
        //            Function_Id = line.Function_Id,
        //            Owner_Id = line.Owner_Id,
        //            Region = line.Region,
        //            Purchase_Order = line.Purchase_Order,
        //            Recording_Index = line.Recording_Index,
        //            COA = pay_ty_full,
        //            Net = (0) - ((line.Credit / TotalSumOfCredits) * TotalSumOfCredits),
        //            Rate = line.Rate,
        //            Uom = line.Uom
        //        };
        //        res2.Add(ls2);
        //    }

        //    db.F_Financial_Transactions.AddRange(res);
        //    db.SaveChanges();
        //    db.F_Financial_Transactions.AddRange(res2);
        //    db.SaveChanges();



        //    return Json(true);
        //}


        public int? Check_Accountid(string Account)
        {
            var res = db.SP_GET_AccountHeadId(Account).FirstOrDefault();
            return res;
        }

        public ActionResult SearchInvoiceBody(int Rowid)
        {
            ViewBag.Row = Rowid;
            return PartialView();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult SearchInvoice(DateTime? StartDate, DateTime? EndDate, long? SearchSupplier, long? SearchResponsbilityCenter, string SearchTransactionType)
        {
            int? Acc_id = Check_Accountid("Account Payables");

            if (SearchResponsbilityCenter == null && SearchSupplier == null && StartDate != null && string.IsNullOrEmpty(SearchTransactionType))
            {
                DateTime st = Convert.ToDateTime(StartDate);
                DateTime et = (EndDate == null) ? DateTime.UtcNow.Date : Convert.ToDateTime(EndDate);

                var fft = (from x in db.Summary_Table
                           join a in db.D_Supplier on x.Supplier_Id equals a.Id
                           join b in db.D_Company on x.Company_Id equals b.Id
                           join c in db.D_ResponsibilityCenter on x.ResponsibilityCenter_Id equals c.Id
                           where x.Document_Date >= st.Date && x.Document_Date <= et.Date && x.COA == Acc_id
                           select new { Voucher_Number = x.Voucher_Number, Document_Date = x.Document_Date, Supplier_Id = a.Name, ResponsibilityCenter_Id = c.Responsibilty_Center, Voucher_Type = x.Voucher_Type, Company_Id = b.Name, Credit = x.Credit }).ToList();
                return Json(fft);
            }
            else if (SearchResponsbilityCenter == null && SearchSupplier != null && StartDate == null && string.IsNullOrEmpty(SearchTransactionType))
            {
                var fft = (from x in db.Summary_Table
                           join a in db.D_Supplier on x.Supplier_Id equals a.Id
                           join b in db.D_Company on x.Company_Id equals b.Id
                           join c in db.D_ResponsibilityCenter on x.ResponsibilityCenter_Id equals c.Id
                           where x.Supplier_Id == SearchSupplier && x.COA == Acc_id
                           select new { Voucher_Number = x.Voucher_Number, Document_Date = x.Document_Date, Supplier_Id = a.Name, ResponsibilityCenter_Id = c.Responsibilty_Center, Voucher_Type = x.Voucher_Type, Company_Id = b.Name, Credit = x.Credit }).ToList();

                return Json(fft);
            }
            else if (SearchResponsbilityCenter != null && SearchSupplier == null && StartDate == null && string.IsNullOrEmpty(SearchTransactionType))
            {
                var fft = (from x in db.Summary_Table
                           join a in db.D_Supplier on x.Supplier_Id equals a.Id
                           join b in db.D_Company on x.Company_Id equals b.Id
                           join c in db.D_ResponsibilityCenter on x.ResponsibilityCenter_Id equals c.Id
                           where x.ResponsibilityCenter_Id == SearchResponsbilityCenter && x.COA == Acc_id
                           select new { Voucher_Number = x.Voucher_Number, Document_Date = x.Document_Date, Supplier_Id = a.Name, ResponsibilityCenter_Id = c.Responsibilty_Center, Voucher_Type = x.Voucher_Type, Company_Id = b.Name, Credit = x.Credit }).ToList();
                return Json(fft);
            }
            else if (SearchResponsbilityCenter == null && SearchSupplier == null && StartDate == null && !(string.IsNullOrEmpty(SearchTransactionType)))
            {
                var fft = (from x in db.Summary_Table
                           join a in db.D_Supplier on x.Supplier_Id equals a.Id
                           join b in db.D_Company on x.Company_Id equals b.Id
                           join c in db.D_ResponsibilityCenter on x.ResponsibilityCenter_Id equals c.Id
                           where x.Voucher_Type == SearchTransactionType && x.COA == Acc_id
                           select new { Voucher_Number = x.Voucher_Number, Document_Date = x.Document_Date, Supplier_Id = a.Name, ResponsibilityCenter_Id = c.Responsibilty_Center, Voucher_Type = x.Voucher_Type, Company_Id = b.Name, Credit = x.Credit }).ToList();
                return Json(fft);
            }
            else if (SearchResponsbilityCenter == null && SearchSupplier == null && StartDate != null && !(string.IsNullOrEmpty(SearchTransactionType)))
            {
                DateTime st = Convert.ToDateTime(StartDate);
                DateTime et = (EndDate == null) ? DateTime.UtcNow.Date : Convert.ToDateTime(EndDate);
                var fft = (from x in db.Summary_Table
                           join a in db.D_Supplier on x.Supplier_Id equals a.Id
                           join b in db.D_Company on x.Company_Id equals b.Id
                           join c in db.D_ResponsibilityCenter on x.ResponsibilityCenter_Id equals c.Id
                           where x.Document_Date >= st.Date && x.Document_Date <= et.Date && x.Voucher_Type == SearchTransactionType && x.COA == Acc_id
                           select new { Voucher_Number = x.Voucher_Number, Document_Date = x.Document_Date, Supplier_Id = a.Name, ResponsibilityCenter_Id = c.Responsibilty_Center, Voucher_Type = x.Voucher_Type, Company_Id = b.Name, Credit = x.Credit }).ToList();
                return Json(fft);
            }
            else if (SearchResponsbilityCenter != null && SearchSupplier != null && StartDate == null && string.IsNullOrEmpty(SearchTransactionType))
            {
                var fft = (from x in db.Summary_Table
                           join a in db.D_Supplier on x.Supplier_Id equals a.Id
                           join b in db.D_Company on x.Company_Id equals b.Id
                           join c in db.D_ResponsibilityCenter on x.ResponsibilityCenter_Id equals c.Id
                           where x.Supplier_Id == SearchSupplier && x.ResponsibilityCenter_Id == SearchResponsbilityCenter && x.COA == Acc_id
                           select new { Voucher_Number = x.Voucher_Number, Document_Date = x.Document_Date, Supplier_Id = a.Name, ResponsibilityCenter_Id = c.Responsibilty_Center, Voucher_Type = x.Voucher_Type, Company_Id = b.Name, Credit = x.Credit }).ToList();
                return Json(fft);
            }
            else if (SearchResponsbilityCenter != null && SearchSupplier != null && StartDate != null && string.IsNullOrEmpty(SearchTransactionType))
            {
                DateTime st = Convert.ToDateTime(StartDate);
                DateTime et = (EndDate == null) ? DateTime.UtcNow.Date : Convert.ToDateTime(EndDate);
                var fft = (from x in db.Summary_Table
                           join a in db.D_Supplier on x.Supplier_Id equals a.Id
                           join b in db.D_Company on x.Company_Id equals b.Id
                           join c in db.D_ResponsibilityCenter on x.ResponsibilityCenter_Id equals c.Id
                           where x.Document_Date >= st.Date && x.Document_Date <= et.Date && x.Supplier_Id == SearchSupplier && x.ResponsibilityCenter_Id == SearchResponsbilityCenter && x.COA == Acc_id
                           select new { Voucher_Number = x.Voucher_Number, Document_Date = x.Document_Date, Supplier_Id = a.Name, ResponsibilityCenter_Id = c.Responsibilty_Center, Voucher_Type = x.Voucher_Type, Company_Id = b.Name, Credit = x.Credit }).ToList();
                return Json(fft);
            }
            else if (SearchResponsbilityCenter != null && SearchSupplier != null && StartDate != null && !(string.IsNullOrEmpty(SearchTransactionType)))
            {
                DateTime st = Convert.ToDateTime(StartDate);
                DateTime et = (EndDate == null) ? DateTime.UtcNow.Date : Convert.ToDateTime(EndDate);
                var fft = (from x in db.Summary_Table
                           join a in db.D_Supplier on x.Supplier_Id equals a.Id
                           join b in db.D_Company on x.Company_Id equals b.Id
                           join c in db.D_ResponsibilityCenter on x.ResponsibilityCenter_Id equals c.Id
                           where x.Document_Date >= st.Date && x.Document_Date <= et.Date && x.Supplier_Id == SearchSupplier && x.Voucher_Type == SearchTransactionType && x.ResponsibilityCenter_Id == SearchResponsbilityCenter && x.COA == Acc_id
                           select new { Voucher_Number = x.Voucher_Number, Document_Date = x.Document_Date, Supplier_Id = a.Name, ResponsibilityCenter_Id = c.Responsibilty_Center, Voucher_Type = x.Voucher_Type, Company_Id = b.Name, Credit = x.Credit }).ToList();
                return Json(fft);
            }
            else
            {
                DateTime st = Convert.ToDateTime(StartDate);
                DateTime et = (EndDate == null) ? DateTime.UtcNow.Date : Convert.ToDateTime(EndDate);
                var fft = (from x in db.Summary_Table
                           join a in db.D_Supplier on x.Supplier_Id equals a.Id
                           join b in db.D_Company on x.Company_Id equals b.Id
                           join c in db.D_ResponsibilityCenter on x.ResponsibilityCenter_Id equals c.Id
                           where x.Document_Date >= st.Date && x.Document_Date <= et.Date && x.Supplier_Id == SearchSupplier && x.Voucher_Type == SearchTransactionType && x.ResponsibilityCenter_Id == SearchResponsbilityCenter && x.COA == Acc_id
                           select new { Voucher_Number = x.Voucher_Number, Document_Date = x.Document_Date, Supplier_Id = a.Name, ResponsibilityCenter_Id = c.Responsibilty_Center, Voucher_Type = x.Voucher_Type, Company_Id = b.Name, Credit = x.Credit }).ToList();
                return Json(fft);
            }
        }


        [Authorize]
        public JsonResult GetTransaction(string Voucher_Number)
        {
            var res1 = (from x in db.F_Financial_Transactions
                        join a in db.D_Supplier on x.Supplier_Id equals a.Id
                        join b in db.D_Company on x.Company_Id equals b.Id
                        join c in db.D_ResponsibilityCenter on x.ResponsibilityCenter_Id equals c.Id
                        where x.Voucher_Number == Voucher_Number && x.Credit>0
                        select new { Company_Id = b.Name, Supplier_Id = a.Name, ResponsibilityCenter_Id = c.Responsibilty_Center }).ToList();

            var res2 = (from x in db.Summary_Table
                        join a in db.D_Company on x.Company_Id equals a.Id
                        join b in db.D_ResponsibilityCenter on x.ResponsibilityCenter_Id equals b.Id
                        join c in db.D_Supplier on x.Supplier_Id equals c.Id
                        where x.Voucher_Number == Voucher_Number
                        select new { COA = x.COA,
                            Debit = x.Debit,
                            Credit=x.Credit,
                            Supplier_Id = c.Name,
                             supp_id=c.Id,
                            ResponsibilityCenter_Id = b.Responsibilty_Center,
                            Company_Id = a.Name,
                            Voucher_Number=x.Voucher_Number,
                            Voucher_Type= x.Voucher_Type
                          }).ToList();

            var res = new { /*Trans = res1,*/ Sumary = res2  };
            return Json(res , JsonRequestBehavior.AllowGet);
        }

        private Debit_Credit CheckTransType(double Amount, string TransType, int? COA)
        {
            Debit_Credit debit_Credit = new Debit_Credit();
            string transtype = (from x in db.D_VoucherType
                                join a in db.Transaction_Rules on x.Id equals a.Transaction_Type_Id
                                where x.Voucher_Type == TransType && a.COA == COA
                                select a.Credit_Debit).SingleOrDefault();
            if (transtype == "credit")
            {
                debit_Credit.Credit = Amount;
                debit_Credit.Debit = 0;
            }
            else
            {
                debit_Credit.Credit = 0;
                debit_Credit.Debit = Amount;
            }
            return debit_Credit;
        }
    }


}