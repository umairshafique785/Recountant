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
        public JsonResult PostTransactionRecord(List<TransactionRecordingLinesModel> transaction, List<TransactionSummaryModel> TransSum)
        {
            long userid = long.Parse(User.Identity.GetUserId());
            List<F_Financial_Transactions> res = new List<F_Financial_Transactions>();
            foreach (var line in transaction)
            {
                var CoaId = Check_Accountid(line.ChartofAccount);
                var data = CheckTransType(line.Amount_LC, line.Voucher_Type, CoaId);
                F_Financial_Transactions res1 = new F_Financial_Transactions()
                {
                    Account_Id = CoaId,
                    Amount_LC = line.Amount_LC,
                    Amount_TC = line.Amount_TC,
                    Company_Id = line.Company_Id,
                    Currency = line.Currency,
                    DateTime_Of_Entry = DateTime.UtcNow,
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
                    Rate = line.Rate,
                    Uom = line.Uom
                };
                res.Add(res1);
            }
            db.F_Financial_Transactions.AddRange(res);
            db.SaveChanges();

            var sumres = TransSum.Select(ft => new Summary_Table()
            {
                COA = Check_Accountid(ft.ChartofAccount),
                Credit = ft.Credit,
                Debit = ft.Debit,
                Supplier_Id = ft.Supplier_Id,
                Voucher_Number = ft.VoucherNumber,
                Document_Date = ft.Voucher_Date,
                ResponsibilityCenter_Id = ft.ResponsibilityCenter_Id,
                Voucher_Type = ft.Voucher_Type,
                Company_Id = ft.Company_Id
            }).ToList();
            db.Summary_Table.AddRange(sumres);
            db.SaveChanges();

            return Json(true);
        }

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
        public JsonResult GetTransaction(string Voucher_Number)
        {
            var res1 = (from x in db.F_Financial_Transactions
                        where x.Voucher_Number == Voucher_Number
                        select x).ToList();

            var res2 = (from x in db.Summary_Table
                        where x.Voucher_Number == Voucher_Number
                        select x).ToList();

            var res = new { Trans = res1, Sumary = res2 };
            return Json(res);
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