using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using ReCountant.Models;

namespace ReCountant.Controllers
{
    public class FinancialTransactionsController : Controller
    {
        // GET: FinancialTransactions
        private ReCountantEntities db = new ReCountantEntities();

        public ActionResult Index()
        {
            return View();
        }
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.Voucher_Type = new SelectList(db.D_VoucherType, "Id", "Voucher_Type").ToList();
            ViewBag.Account_Id = new SelectList(db.SP_Get_AccountHead(), "Id", "Account_Head").ToList();
            ViewBag.Supplier_Id = new SelectList(db.D_Supplier, "Id", "Name").ToList();
            ViewBag.Customer_Id = new SelectList(db.D_Customer, "Id", "Name").ToList();
            ViewBag.Employee_Id = new SelectList(db.D_Employee, "Id", "Name").ToList();
            ViewBag.Owner_Id = new SelectList(db.D_Owner, "Id", "Name").ToList();
            ViewBag.Company_Id = new SelectList(db.D_Company, "Id", "Name").ToList();
            ViewBag.Plot = new SelectList(db.D_Plot, "Id", "Plot_No").ToList();
            ViewBag.Land = new SelectList(db.F_Land_Purchase_RealEstate, "Id", "Address").ToList();
            ViewBag.ResponsibilityCenter_Id = new SelectList(db.D_ResponsibilityCenter, "Id", "Responsibilty_Center").ToList();
            ViewBag.Function_Id = new SelectList(db.D_ReportingFunction, "Id", "Function").ToList();
            ViewBag.Project = new SelectList(db.D_Projects, "Id", "Name").ToList();
            ViewBag.REA = new SelectList(db.RealEstate_Activity, "Id", "RealEstate_Activites").ToList();
            ViewBag.REP = new SelectList(db.REPs, "Id", "Name").ToList();
            ViewBag.Space_Type = new SelectList(db.D_SpaceType, "Id", "Space_Type").ToList();
            return PartialView();
        }
        public ActionResult CreateFinancialTrans(int? Plotid, int CustomerId, string Currency, float AmountTC, float AmountLC, float ExchangeRate, int InstId)
        {
            List<Financial_Transactions> ft = new List<Financial_Transactions>();
            Financial_Transactions ft1 = new Financial_Transactions()
            {
                Account_Id = 1008,
                Customer_Id = CustomerId,
                Amount_TC = AmountTC,
                Amount_LC = AmountLC,
                Exchange_Rate = ExchangeRate,
                Currency = Currency,
                Debit = AmountTC,
                Plot = Plotid
            };
            Financial_Transactions ft2 = new Financial_Transactions()
            {
                Account_Id = 1050,
                Customer_Id = CustomerId,
                Amount_TC = AmountTC,
                Amount_LC = AmountLC,
                Exchange_Rate = ExchangeRate,
                Currency = Currency,
                Plot = Plotid,
                Credit = AmountTC,

            };
            ft.Add(ft1);
            ft.Add(ft2);
            ViewBag.InstId = InstId;
            ViewBag.Voucher_Type = new SelectList(db.D_VoucherType, "Id", "Voucher_Type").ToList();
            ViewBag.Account_Id = new SelectList(db.SP_Get_AccountHead(), "Id", "Account_Head").ToList();
            ViewBag.Supplier_Id = new SelectList(db.D_Supplier, "Id", "Name").ToList();
            ViewBag.Customer_Id = new SelectList(db.D_Customer, "Id", "Name").ToList();
            ViewBag.Employee_Id = new SelectList(db.D_Employee, "Id", "Name").ToList();
            ViewBag.Owner_Id = new SelectList(db.D_Owner, "Id", "Name").ToList();
            ViewBag.Company_Id = new SelectList(db.D_Company, "Id", "Name").ToList();
            ViewBag.Plot = new SelectList(db.D_Plot, "Id", "Plot_No").ToList();
            ViewBag.Land = new SelectList(db.F_Land_Purchase_RealEstate, "Id", "Address").ToList();
            ViewBag.ResponsibilityCenter_Id = new SelectList(db.D_ResponsibilityCenter, "Id", "Responsibilty_Center").ToList();
            ViewBag.Function_Id = new SelectList(db.D_ReportingFunction, "Id", "Function").ToList();
            ViewBag.Project = new SelectList(db.D_Projects, "Id", "Name").ToList();
            ViewBag.REA = new SelectList(db.RealEstate_Activity, "Id", "RealEstate_Activites").ToList();
            ViewBag.REP = new SelectList(db.REPs, "Id", "Name").ToList();
            ViewBag.Space_Type = new SelectList(db.D_SpaceType, "Id", "Space_Type").ToList();
            return View(ft);
        }
        [HttpPost]
        public JsonResult AddJournalVoucher(List<Financial_Transactions> FinancialTransc)
        {
            long userid = User.Identity.GetUserId<long>();
            List<F_Financial_Transactions> fft = FinancialTransc.Select(x => new F_Financial_Transactions
            {
                Account_Id = x.Account_Id,
                Amount_TC = x.Amount_TC,
                Amount_LC = x.Amount_LC,
                Company_Id = x.Company_Id,
                Credit = x.Credit,
                Customer_Id = x.Customer_Id,
                DateTime_Of_Entry = DateTime.UtcNow,
                Debit = x.Debit,
                Description = x.Description,
                Document_Date = DateTime.UtcNow,
                Employee_Id = x.Employee_Id,
                Exchange_Rate = x.Exchange_Rate,
                Function_Id = x.Function_Id,
                Knocking_Off = x.Knocking_Off,
                Land = x.Land,
                Owner_Id = x.Owner_Id,
                Plot = x.Plot,
                Project = x.Project,
                Quantity = x.Quantity,
                Reference_Number = x.Reference_Number,
                REA = x.REA,
                REP = x.REP,
                ResponsibilityCenter_Id = x.ResponsibilityCenter_Id,
                Space_Type = x.Space_Type,
                Supplier_Id = x.Supplier_Id,
                Transaction_Party = x.transparty,
                Userid = userid,
                Voucher_Number = x.Voucher_Number,
                Voucher_Type = x.Voucher_Type
            }).ToList();
            db.F_Financial_Transactions.AddRange(fft);
            db.SaveChanges();




            return Json(true);
        }

        public ActionResult CreateTransactionTypes()
        {
            ViewBag.COA = new SelectList(db.SP_Get_AccountHead() , "Id", "Account_Head");
            return View();
        }

        [HttpPost]
        public JsonResult SaveTransactionsTypes(string TransactionType, List<Transaction_Rules> transaction_Rules)
        {
            D_VoucherType trans_type = new D_VoucherType
            {
                Voucher_Type = TransactionType
            };
            db.D_VoucherType.Add(trans_type);
            db.SaveChanges();

            List<Transaction_Rules> tr = transaction_Rules.Select(x => new Transaction_Rules
            {
                COA = x.COA,
                Credit_Debit = x.Credit_Debit,
                Transaction_Type_Id = trans_type.Id
            }).ToList();
            db.Transaction_Rules.AddRange(tr);
            db.SaveChanges();

            return Json(true);
        }


        [HttpPost]
        public JsonResult GetInvoices(int id)
        {
            var res = db.F_Financial_Transactions.Where(x => x.Supplier_Id == id /*&& x.Credit!=0*/).Select(x => new
            {/* Id = x.Id,*/ Reference_Number = x.Reference_Number }).Distinct().ToList();
            return Json(res);
        }

        public JsonResult GetInvoicesDetails(string invo, int sup)
        {
          
            
            var parstatus = db.F_Financial_Transactions.Where(x =>x.Reference_Number==invo && x.Supplier_Id==sup  && x.Status.Contains("Parked")).Select(x=>x.Status).FirstOrDefault();

            if (parstatus == "Parked")
            {
                return Json(false);
            }
            if (invo == "Running_Balance")
            {

            }
            //if (invo == "Running_Balance")
            //{
                
            //}
            var sum = db.F_Financial_Transactions.Where(x => x.Reference_Number == invo && x.Supplier_Id == sup && x.Status == "pending").Sum(foo => foo.Net);
            //var stauts_of_accounts = db.F_Financial_Transactions
            //    .Where(x => x.Reference_Number == invo && x.Supplier_Id == sup && x.Net < 0).Select(x => x.Status);
            var Conversion_Of_Sum_Into_Positive_Integer = ((-1) * (sum));




            //var cust = db.F_Financial_Transactions.Where(x => x.Reference_Number== invo && x.Supplier_Id== sup).ToList();


            //var sumcheck = db.F_Financial_Transactions.Where(x => x.Reference_Number == invo && x.Supplier_Id == sup && x.Status == "pending" && x.Status=="Parked").Sum(foo => foo.Net);

            //var sumx = (from x in db.F_Financial_Transactions
            //            where x.Reference_Number == invo && x.Supplier_Id == sup && x.Status == "pending" 
            //            select x.Net).Sum();
            //var sumx2 = (from x in db.F_Financial_Transactions
            //            where x.Reference_Number == invo && x.Supplier_Id == sup && x.Status == "pendind"
            //            select x.Net).Sum();
            //if (sum != null)
            //{
            //    return new JsonResult { Data = sum };
            //}
            //else
            //{
            //    return Json(false);
            //}
            //var invoicesinformation = db.Summary_Table.Where(x => x.Voucher_Number == invo && x.Supplier_Id == sup && x.Credit > 0).Select(x => new
            //{

            //    //Id = x.Id,
            //    //Reference_Number = x.Reference_Number,
            //    //Transaction_Document_Number = x.Transaction_Document_Number,
            //    //Purchase_Order = x.Purchase_Order,
            //    //Product = x.Product,
            //    //Description = x.Description,
            //    //Space_Type = x.Space_Type,
            //    //Quantity = x.Quantity,
            //    //Uom = x.Uom,
            //    //Rate = x.Rate,
            //    //Amount_TC = x.Amount_TC,
            //    //Exchange_Rate = x.Exchange_Rate,
            //    //Currency = x.Currency,
            //    //Amount_LC = x.Amount_LC,
            //    //Customer_Id = x.Customer_Id,
            //    //Employee_Id = x.Employee_Id,
            //    //Owner_Id = x.Owner_Id,
            //    //REP = x.REP,
            //    //REA = x.REA,
            //    //Project = x.Project,
            //    //ResponsibilityCenter_Id = x.ResponsibilityCenter_Id,
            //    //Company_Id = x.Company_Id,
            //    Credit = sum,
            //    Voucher_Number = x.Voucher_Number


            //}).ToList();



           return new JsonResult { Data = Conversion_Of_Sum_Into_Positive_Integer };

        }


    }
}