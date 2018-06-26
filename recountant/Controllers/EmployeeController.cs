using ReCountant.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ReCountant.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Supplier
        private ReCountantEntities db = new ReCountantEntities();

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AddEmployee()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult AddEmployee(UserInfo userinfo)
        {
            AccountController ac = new AccountController();
            long userid = ac.RegisterUser(userinfo);

            D_Employee emp = new D_Employee()
            {
                Userid = userid,
                Employee_Info = "Employee ki info"
            };
            db.D_Employee.Add(emp);
            db.SaveChanges();

            return Json(true);
        }
    }
}