using ReCountant.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ReCountant.Controllers
{
    public class OwnerController : Controller
    {
        // GET: Supplier
        private ReCountantEntities db = new ReCountantEntities();

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AddOwner()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult AddOwner(UserInfo userinfo)
        {
            AccountController ac = new AccountController();
            long userid = ac.RegisterUser(userinfo);

            D_Owner own = new D_Owner()
            {
                Userid = userid,
                Owner_Info= "Owner ki info"
            };
            db.D_Owner.Add(own);
            db.SaveChanges();

            return Json(true);
        }
    }
}