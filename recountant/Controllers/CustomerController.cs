using ReCountant.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ReCountant.Controllers
{
    public class CustomerController : Controller
    {
        // GET: Supplier
        private ReCountantEntities db = new ReCountantEntities();

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AddCustomer()
        {
            return View();
        }
        [HttpPost]

        public JsonResult AddCustomer(User userinfo)
        {

            if (ModelState.IsValid)
            {
                db.Users.Add(userinfo);
                db.SaveChanges();
            }
            //var GetUserIdForCustomer= db.Users.Select(x => x.Id).ToList().LastOrDefault();

            //AccountController ac = new AccountController();
            //long userid = ac.RegisterUser(userinfo);

            D_Customer cus = new D_Customer()
            {
                Userid = userinfo.Id,
                Customer_Info = "Customer ki info",
                Name = userinfo.Name

            };
            db.D_Customer.Add(cus);
            db.SaveChanges();

            return Json(true);
        }

        public JsonResult SearchCustomerName()
        {

            List<Customer> allsearch = db.D_Customer.Select(x => new Customer
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
    }
}