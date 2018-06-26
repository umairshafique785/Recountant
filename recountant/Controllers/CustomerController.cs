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
        [ValidateAntiForgeryToken]
        public JsonResult AddCustomer(UserInfo userinfo)
        {
            AccountController ac = new AccountController();
            long userid = ac.RegisterUser(userinfo);

            D_Customer cus = new D_Customer()
            {
                Userid = userid,
                Customer_Info = "Customer ki info"
            };
            db.D_Customer.Add(cus);
            db.SaveChanges();

            return Json(true);
        }
    }
}