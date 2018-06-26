using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ReCountant.Controllers
{
    public class BalanceController : Controller
    {
        public ActionResult PartyBalance()
        {
            return View();
        }
    }
}