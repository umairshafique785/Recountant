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
        [ValidateAntiForgeryToken]
        public JsonResult AddSupplier(UserInfo userinfo)
        {
            AccountController ac = new AccountController();
            long userid = ac.RegisterUser(userinfo);

            D_Supplier sup = new D_Supplier()
            {
                Userid = userid,
                Supplier_Info = "Supplier ki info"
            };
            db.D_Supplier.Add(sup);
            db.SaveChanges();

            return Json(true);
        }
    }
}