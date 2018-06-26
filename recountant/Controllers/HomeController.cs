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


       

    }
}