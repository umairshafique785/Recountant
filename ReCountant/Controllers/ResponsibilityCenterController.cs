using ReCountant.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ReCountant.Controllers
{
    public class ResponsibilityCenterController : Controller
    {
        // GET: ResponsibilityCenter
        private ReCountantEntities db = new ReCountantEntities();
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult SearchResponsibilityCenterName()
        {
            //return (from p in db.F_Financial_Transactions
            //        where p.Voucher_Type.Contains(Supplier_voucher_type)
            //        select new Financial_Transactions { Voucher_Type = p.Voucher_Type }).ToList();
            List<ResonsibilityCenter> allsearch = db.D_ResponsibilityCenter.Select(x => new ResonsibilityCenter
            {
                Id = x.Id,
                Responsibility_Center = x.Responsibilty_Center
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