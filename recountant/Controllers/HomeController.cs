using ReCountant.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ReCountant.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private ReCountantEntities db = new ReCountantEntities();
        User userinfo = new User();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult HeaderInfo()
        {
            return PartialView();
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult SECO()
        {
            ViewBag.Customer = new SelectList(db.D_Customer.ToList(), "Id", "Name");
            ViewBag.Employee = new SelectList(db.D_Employee.ToList(), "Id", "Name");
            ViewBag.Owner = new SelectList(db.D_Owner.ToList(), "Id", "Name");
            ViewBag.Supplier = new SelectList(db.D_Supplier.ToList(), "Id", "Name");
            return PartialView();
        }
        public JsonResult GetCustomerInfo(string id)

        {
            var Customer = (from x in db.D_Customer
                           join y in db.Users on x.Userid equals y.Id
                           where y.CNIC_Number == id
                           select y).SingleOrDefault();
            if (Customer != null)
            {
                return Json(Customer);
            }
            else
            {
                return Json(false);
            }
        }

        public JsonResult GetUserInfo(string cnic_user, int? user_name_id , string selectes_category)

        {
            if (selectes_category == "Individual")
            {
                 var userinformation = (from a in db.Users
                                       where   a.Individual_AOP_Company=="individual" && a.CNIC_Number == cnic_user
                                       select a).SingleOrDefault();
                if (userinformation != null)
                {
                    return Json(userinformation);
                }
                else
                {
                    return Json(false);
                }
                
            }
            else if (selectes_category == "AOP")
            {
                var userinformation = (from a in db.Users
                                       where a.Id == user_name_id
                                       select a).SingleOrDefault();
                if (userinformation != null)
                {
                    return Json(userinformation);
                }
                else
                {
                    return Json(false);
                }
                

            }
            else if (selectes_category == "Company")
            {
                var userinformation = (from a in db.Users
                                       where   a.Id == user_name_id
                                       select a).SingleOrDefault();
                if (userinformation != null)
                {
                    return Json(userinformation);
                }
                else
                {
                    return Json(false);
                }
               
            }
            else             {
               
                return Json(false);
            }
           



            //var cust = db.Users.Where(x => id.Contains(x.CNIC_Number)).ToList();
           
        }


        public JsonResult individual_aop_comp_name_search()
        {
            //return (from p in db.F_Financial_Transactions
            //        where p.Voucher_Type.Contains(Supplier_voucher_type)
            //        select new Financial_Transactions { Voucher_Type = p.Voucher_Type }).ToList();
            List<UserInfo> allsearch = db.Users.Select(x => new UserInfo
            {
                Id = x.Id,
                Name = x.Name,
                CNIC_Number=x.CNIC_Number
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
        // supplier get
        public JsonResult Getsupplierinformation(string id , int? ind_aop_com)

        {
            if ((id != "") && (ind_aop_com == null))
            {
                var userinformation = (from a in db.Users
                                       where a.CNIC_Number == id
                                       select a).ToList().FirstOrDefault();
                return Json(userinformation);
            }
            else if ((id == "") && (ind_aop_com != null))
            {
                var userinformation = (from a in db.Users
                                       where a.Id == ind_aop_com
                                       select a).ToList().FirstOrDefault();
                return Json(userinformation);
            }
            else
            {
                return Json(false);
            }

            //var cust = db.Users.Where(x => id.Contains(x.CNIC_Number)).ToList();

          
        }
    }
}