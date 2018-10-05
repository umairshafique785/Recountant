using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ReCountant.Controllers
{
    public class RevenueAndReceiveablesController : Controller
    {
        public ActionResult Index()
        {
            return PartialView();
        }
        // GET: RevenueAndReceiveables
        public ActionResult Revenues()
        {
            return View();
        }
        public ActionResult Receivables()
        {
            return View();
        }
    }
}