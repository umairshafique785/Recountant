using ReCountant.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ReCountant.Controllers
{
    public class SupplierController : Controller
    {
        // GET: Supplier
        private ReCountantEntities db = new ReCountantEntities();

        public ActionResult Index()
        {

            return View();
        }
        public ActionResult AddSupplier()
        {
            return View();
        }
        [HttpPost]
        public JsonResult AddSupplier(D_Supplier supp, string id, int? ind_aop_company)

        {
            //var UserIdGet = db.Users.Where(p => p.CNIC_Number == id).Select(p => p.Id).ToList().SingleOrDefault();
            if ((id != "") && (ind_aop_company == null))
            {
                var UserIdGet = db.Users.Where(p => p.CNIC_Number == id).Select(p => p.Id).ToList().SingleOrDefault();
                var SupplierUserIdGet = db.D_Supplier.Where(p => p.Userid == UserIdGet).Select(p => p.Userid).ToList().SingleOrDefault();
                if (UserIdGet != SupplierUserIdGet)
                {
                    D_Supplier supplier = new D_Supplier
                    {
                        Userid = UserIdGet,
                        Name = supp.Name,
                        CNIC_Number = supp.CNIC_Number,
                        Industry = supp.Industry,
                        Supply_Chain_Role = supp.Supply_Chain_Role,
                        Business = supp.Business,
                        Billing_Address = supp.Billing_Address,
                        Shipping_Address = supp.Shipping_Address,
                        Billing_Contact_Number = supp.Billing_Contact_Number,
                        Shipping_Contact_Number = supp.Shipping_Contact_Number,
                        Billing_Email_Id = supp.Billing_Email_Id,
                        Shipping_Email_Id = supp.Shipping_Email_Id,
                        Product_Type = supp.Product_Type,
                        Payment_Method=supp.Payment_Method
                    };
                    db.D_Supplier.Add(supplier);
                    db.SaveChanges();
                    return Json(supplier, JsonRequestBehavior.AllowGet);



                }
                else
                {
                    //return Json(new { Success = false, Message = "user as supplier already exist" });
                    return Json(false);
                }
            }
            else if ((id == "") && (ind_aop_company != null))
            {
                var UserIdGet = db.Users.Where(p => p.Id == ind_aop_company).Select(p => p.Id).ToList().SingleOrDefault();
                var SupplierUserIdGet = db.D_Supplier.Where(p => p.Userid == UserIdGet).Select(p => p.Userid).ToList().SingleOrDefault();
                if (UserIdGet != SupplierUserIdGet)
                {
                    D_Supplier supplier = new D_Supplier
                    {
                        Userid = UserIdGet,
                        Name = supp.Name,
                        CNIC_Number = supp.CNIC_Number,
                        Industry = supp.Industry,
                        Supply_Chain_Role = supp.Supply_Chain_Role,
                        Business = supp.Business,
                        Billing_Address = supp.Billing_Address,
                        Shipping_Address = supp.Shipping_Address,
                        Billing_Contact_Number = supp.Billing_Contact_Number,
                        Shipping_Contact_Number = supp.Shipping_Contact_Number,
                        Billing_Email_Id = supp.Billing_Email_Id,
                        Shipping_Email_Id = supp.Shipping_Email_Id,
                        Product_Type = supp.Product_Type,
                        Payment_Method= supp.Payment_Method
                    };
                    db.D_Supplier.Add(supplier);
                    db.SaveChanges();
                    return Json(true);



                }
                else
                {
                    //return Json(new { Success = false, Message = "user as supplier already exist" });
                    return Json(false);
                }

            }
            else
            {
                return Json(false);
            }

        }

        public JsonResult GetAndPrintSupplier()
        {

            var information = db.F_Financial_Transactions.Select(x => new
            {

                Project = x.Project,
                Transaction_Party = x.Transaction_Party,
                Voucher_Type = x.Voucher_Type,
                voucher_number = x.Voucher_Number,
                document_Date = x.Document_Date.ToString(),
                dateTime_Of_Entry = x.DateTime_Of_Entry.ToString(),
                Description = x.Description,
                Space_Type = x.Space_Type,
                Plot = x.Plot,
                Land = x.Land,
                Quantity = x.Quantity,
                Amount_LC = x.Amount_LC,


            }).ToList();
            return Json(information, JsonRequestBehavior.AllowGet);
        }

        // Get Name of supplier for report search drop don

        public JsonResult SearchSpplierUniqueCode(string Supplier_U_Id)
       {
         
            List<supplier> allsearch = db.D_Supplier.Where(x => x.Supplier_Code.Contains(Supplier_U_Id)).Select(x => new supplier
            {

               Supplier_Code  = x.Supplier_Code
            }).ToList();
            if (allsearch != null)
            {
                return new JsonResult { Data = allsearch, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            else
            {
                return Json(false);
            }
            
        }
   
        public JsonResult SearchSpplierVoucher(string Supplier_VT)
        {
            //return (from p in db.F_Financial_Transactions
            //        where p.Voucher_Type.Contains(Supplier_voucher_type)
            //        select new Financial_Transactions { Voucher_Type = p.Voucher_Type }).ToList();
            List<Financial_Transactions> allsearch = db.F_Financial_Transactions.Where(x => x.Voucher_Type.Contains(Supplier_VT)).Select(x => new Financial_Transactions
            {
               
                Voucher_Type = x.Voucher_Type
            }).Distinct().ToList();
           

            if (allsearch != null)
            {
                return new JsonResult { Data = allsearch, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            else
            {
                return Json(false);
            }

        }

        public JsonResult SearchSpplierProject(string Supplier_project)
        {
            //return (from p in db.F_Financial_Transactions
            //        where p.Voucher_Type.Contains(Supplier_voucher_type)
            //        select new Financial_Transactions { Voucher_Type = p.Voucher_Type }).ToList();
            List<Project> allsearch = db.D_Projects.Where(x => x.Name.Contains(Supplier_project)).Select(x => new Project
            {

                Name = x.Name
            }).Distinct().ToList();


            if (allsearch != null)
            {
                return new JsonResult { Data = allsearch, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            else
            {
                return Json(false);
            }

        }

        public JsonResult SearchSpplierName()
        {
            //return (from p in db.F_Financial_Transactions
            //        where p.Voucher_Type.Contains(Supplier_voucher_type)
            //        select new Financial_Transactions { Voucher_Type = p.Voucher_Type }).ToList();
            List<supplier> allsearch = db.D_Supplier.Select(x => new supplier
            {
                Id=x.Id,
                Name = x.Name
            }).ToList();


            if (allsearch != null)
            {
                return new JsonResult { Data = allsearch, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            else
            {
                return Json(false);
            }

        }
        public JsonResult Search_Only_Running_Balance_suppliers()
        {
            //return (from p in db.F_Financial_Transactions
            //        where p.Voucher_Type.Contains(Supplier_voucher_type)
            //        select new Financial_Transactions { Voucher_Type = p.Voucher_Type }).ToList();
            List<supplier> allsearch = db.D_Supplier.Where(x=>x.Payment_Method== "running_balance_based").Select(x => new supplier
            {
                Id = x.Id,
                Name = x.Name
            }).ToList();


            if (allsearch != null)
            {
                return new JsonResult { Data = allsearch, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            else
            {
                return Json(false);
            }

        }
        //public IEnumerable<F_Financial_Transactions> SearchSpplierVoucher(string Supplier_voucher_type)
        //{
        //    return (from p in db.Set<F_Financial_Transactions>()where (p.Voucher_Type.Contains(Supplier_voucher_type))
        //            select new
        //    {
        //        vouch = p.Voucher_Type
        //    }).ToList().Select(x => new F_Financial_Transactions { Voucher_Type = x.vouch });
        //}
        //public JsonResult SearchSppliercnic(string Suppliercnic)
        //{

        //    List<supplier> allsearch = db.D_Supplier.Where(x => x.CNIC_Number.Contains(Suppliercnic)).Select(x => new supplier
        //    {

        //        CNIC_Number = x.CNIC_Number
        //    }).ToList();
        //    return new JsonResult { Data = allsearch, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        //}
        [HttpPost]
        public JsonResult GetSname(string s_name)
        {
            var res = db.D_Supplier.Where(x => x.CNIC_Number == s_name).Select(x => new { Id = x.Id, name = x.Name });
            if (res != null)
            {

                return Json(res);
            }
            else
            {
                return Json(false);
            }
            
        }

        public JsonResult GetReportWIthNameNAndCnic(string VT, string Supplier_Unique_Id, DateTime? fromdate, DateTime? todate)
        {
            var GetsupplierID = db.D_Supplier.Where(p => p.Supplier_Code == Supplier_Unique_Id).Select(p => p.Id).ToList().SingleOrDefault();
            //var Get_Financial_T_ID_In_FT_table =db.F_Financial_Transactions.Where(p=>p.Supplier_Id=GetsupplierID)
            if ((VT != "") && (Supplier_Unique_Id != "") && (fromdate.HasValue) && (todate.HasValue))
            {


                var information = db.F_Financial_Transactions.Where(x => x.Supplier_Id == GetsupplierID && x.Document_Date >= fromdate && x.Document_Date <= todate).Select(x => new
                {

                    Project = x.Project,
                    Transaction_Party = x.Transaction_Party,
                    Voucher_Type = x.Voucher_Type,
                    voucher_number = x.Voucher_Number,
                    document_Date = x.Document_Date.ToString(),
                    dateTime_Of_Entry = x.DateTime_Of_Entry.ToString(),
                    Description = x.Description,
                    Space_Type = x.Space_Type,
                    Plot = x.Plot,
                    Land = x.Land,
                    Quantity = x.Quantity,
                    Amount_LC = x.Amount_LC,


                }).ToList();

                return Json(information, JsonRequestBehavior.AllowGet);
            }
            else if ((Supplier_Unique_Id != "")&&(VT == "")  && (!fromdate.HasValue) && (!todate.HasValue))
            {
                var information = db.F_Financial_Transactions.Where(x => x.Supplier_Id == GetsupplierID).Select(x => new
                {

                    Project = x.Project,
                    Transaction_Party = x.Transaction_Party,
                    Voucher_Type = x.Voucher_Type,
                    voucher_number = x.Voucher_Number,
                    document_Date = x.Document_Date.ToString(),
                    dateTime_Of_Entry = x.DateTime_Of_Entry.ToString(),
                    Description = x.Description,
                    Space_Type = x.Space_Type,
                    Plot = x.Plot,
                    Land = x.Land,
                    Quantity = x.Quantity,
                    Amount_LC = x.Amount_LC,


                }).ToList();

                return Json(information, JsonRequestBehavior.AllowGet);
            }
            else if ((fromdate.HasValue) && (todate.HasValue) && (VT == "") && (Supplier_Unique_Id == ""))
            {
                var Invoice_Information = (from x in db.F_Financial_Transactions
                                           join a in db.D_Projects on x.Project equals a.Id
                                           join b in db.D_Supplier on x.Supplier_Id equals b.Id
                                           join c in db.D_SpaceType on x.Space_Type equals c.Id
                                           where x.Document_Date >= fromdate && x.Document_Date <= todate
                                           select new
                                           {
                                               Project = a.Name,
                                               Transaction_Party = b.Name,
                                               Voucher_Type = x.Voucher_Type,
                                               voucher_number = x.Voucher_Number,
                                               document_Date = x.Document_Date.ToString(),
                                               dateTime_Of_Entry = x.DateTime_Of_Entry.ToString(),
                                               Description = x.Description,
                                               Space_Type = c.Space_Type,
                                               Plot = x.Plot,
                                               Land = x.Land,
                                               Quantity = x.Quantity,
                                               Amount_LC = x.Amount_LC,
                                           }).ToList();

                //var datefilersearch = db.F_Financial_Transactions.Where(x => x.Document_Date >= fromdate && x.Document_Date <= todate).Select(x => new
                //{

                //    Project = x.Project,
                //    Transaction_Party = x.Transaction_Party,
                //    Voucher_Type = x.Voucher_Type,
                //    voucher_number = x.Voucher_Number,
                //    document_Date = x.Document_Date.ToString(),
                //    dateTime_Of_Entry = x.DateTime_Of_Entry.ToString(),
                //    Description = x.Description,
                //    Space_Type = x.Space_Type,
                //    Plot = x.Plot,
                //    Land = x.Land,
                //    Quantity = x.Quantity,
                //    Amount_LC = x.Amount_LC,


                //}).ToList();

                return Json(Invoice_Information, JsonRequestBehavior.AllowGet);

            }
            else if ((VT != "") && (Supplier_Unique_Id == "") && (!fromdate.HasValue) && (!todate.HasValue))
            {
                //var getSupplierIdFromVoucherType = db.F_Financial_Transactions.Where(p => p.Voucher_Type == VT).Select(p => p.Supplier_Id).ToList().SingleOrDefault();
                var information = db.F_Financial_Transactions.Where(x => x.Voucher_Type == VT).Select(x => new
                {

                    Project = x.Project,
                    Transaction_Party = x.Transaction_Party,
                    Voucher_Type = x.Voucher_Type,
                    voucher_number = x.Voucher_Number,
                    document_Date = x.Document_Date.ToString(),
                    dateTime_Of_Entry = x.DateTime_Of_Entry.ToString(),
                    Description = x.Description,
                    Space_Type = x.Space_Type,
                    Plot = x.Plot,
                    Land = x.Land,
                    Quantity = x.Quantity,
                    Amount_LC = x.Amount_LC,


                }).ToList();

                return Json(information, JsonRequestBehavior.AllowGet);

            }
            else if ((VT == "") && (Supplier_Unique_Id == "") && (!fromdate.HasValue) && (todate.HasValue))
            {
                var information = db.F_Financial_Transactions.Where(x => x.Document_Date <= todate).Select(x => new
                {

                    Project = x.Project,
                    Transaction_Party = x.Transaction_Party,
                    Voucher_Type = x.Voucher_Type,
                    voucher_number = x.Voucher_Number,
                    document_Date = x.Document_Date.ToString(),
                    dateTime_Of_Entry = x.DateTime_Of_Entry.ToString(),
                    Description = x.Description,
                    Space_Type = x.Space_Type,
                    Plot = x.Plot,
                    Land = x.Land,
                    Quantity = x.Quantity,
                    Amount_LC = x.Amount_LC,


                }).ToList();

                return Json(information, JsonRequestBehavior.AllowGet);

            }
            else if ((VT == "") && (Supplier_Unique_Id == "") && (fromdate.HasValue) && (!todate.HasValue))
            {
                var information = db.F_Financial_Transactions.Where(x => x.Document_Date >= fromdate).Select(x => new
                {

                    Project = x.Project,
                    Transaction_Party = x.Transaction_Party,
                    Voucher_Type = x.Voucher_Type,
                    voucher_number = x.Voucher_Number,
                    document_Date = x.Document_Date.ToString(),
                    dateTime_Of_Entry = x.DateTime_Of_Entry.ToString(),
                    Description = x.Description,
                    Space_Type = x.Space_Type,
                    Plot = x.Plot,
                    Land = x.Land,
                    Quantity = x.Quantity,
                    Amount_LC = x.Amount_LC,


                }).ToList();

                return Json(information, JsonRequestBehavior.AllowGet);

            }
            else if ((VT == "") && (Supplier_Unique_Id == "") && (!fromdate.HasValue) && (!todate.HasValue))
            {
                var Invoice_Information = (from x in db.F_Financial_Transactions
                                           join a in db.D_Projects on x.Project equals a.Id
                                           join b in db.D_Supplier on x.Supplier_Id equals b.Id
                                           join c in db.D_SpaceType on x.Space_Type equals c.Id
                                           select new
                                           {
                                               Project = a.Name,
                                               Transaction_Party = b.Name,
                                               Voucher_Type = x.Voucher_Type,
                                               voucher_number = x.Voucher_Number,
                                               document_Date = x.Document_Date.ToString(),
                                               dateTime_Of_Entry = x.DateTime_Of_Entry.ToString(),
                                               Description = x.Description,
                                               Space_Type = c.Space_Type,
                                               Plot = x.Plot,
                                               Land = x.Land,
                                               Quantity = x.Quantity,
                                               Amount_LC = x.Amount_LC,
                                           }).ToList();
                //var information = db.F_Financial_Transactions.Select(x => new
                //{

                //    Project = x.Project,
                //    Transaction_Party = x.Transaction_Party,
                //    Voucher_Type = x.Voucher_Type,
                //    voucher_number = x.Voucher_Number,
                //    document_Date = x.Document_Date.ToString(),
                //    dateTime_Of_Entry = x.DateTime_Of_Entry.ToString(),
                //    Description = x.Description,
                //    Space_Type = x.Space_Type,
                //    Plot = x.Plot,
                //    Land = x.Land,
                //    Quantity = x.Quantity,
                //    Amount_LC = x.Amount_LC,


                //}).ToList();
                return Json(Invoice_Information, JsonRequestBehavior.AllowGet);

            }

            else
            {
                return Json(false);
            }
                
                
            
        }

        public JsonResult DateFilter(DateTime fromdate,DateTime todate)
        {
            //var datefiltersearch = from e in db.F_Financial_Transactions
            //            where( e.Document_Date >= fromdate  && e.Document_Date <= todate
            //                 select e;
            var datefilersearch = db.F_Financial_Transactions.Where(x=>x.Document_Date>=fromdate && x.Document_Date<=todate ).Select(x => new
            {

                Project = x.Project,
                Transaction_Party = x.Transaction_Party,
                Voucher_Type = x.Voucher_Type,
                voucher_number = x.Voucher_Number,
                document_Date = x.Document_Date.ToString(),
                dateTime_Of_Entry = x.DateTime_Of_Entry.ToString(),
                Description = x.Description,
                Space_Type = x.Space_Type,
                Plot = x.Plot,
                Land = x.Land,
                Quantity = x.Quantity,
                Amount_LC = x.Amount_LC,


            }).ToList();
            if (datefilersearch != null)
            {
                return Json(datefilersearch);
            }
            else
            {
                return Json(false);
            }
                
           
            
        }
    }
}