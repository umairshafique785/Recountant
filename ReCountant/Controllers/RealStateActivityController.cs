using ReCountant.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ReCountant.Controllers
{
    public class RealStateActivityController : Controller
    {
        // GET: RealStateActivity
        private ReCountantEntities db = new ReCountantEntities();
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult SearchRealStateActivityName(string RealStateAct)
        {
            //return (from p in db.F_Financial_Transactions
            //        where p.Voucher_Type.Contains(Supplier_voucher_type)
            //        select new Financial_Transactions { Voucher_Type = p.Voucher_Type }).ToList();
            List<RealStateActivity> allsearch = db.RealEstate_Activity.Select(x => new RealStateActivity
            {
                Id = x.Id,
                RealEstate_Activites = x.RealEstate_Activites
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