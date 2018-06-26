using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ReCountant.Controllers
{
    public class SideWidgetController : Controller
    {
        public ActionResult LeftSidePanel()
        {
            return PartialView();
        }
        public ActionResult RightSidePanel()
        {
            return PartialView();
        }
    }
}