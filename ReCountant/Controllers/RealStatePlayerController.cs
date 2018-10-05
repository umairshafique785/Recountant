using ReCountant.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace ReCountant.Controllers
{
    public class RealStatePlayerController : Controller
    {
        // GET: RealStatePlayer
        private ReCountantEntities db = new ReCountantEntities();
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult SearchRealStatePlayerName()
        {
            //return (from p in db.F_Financial_Transactions
            //        where p.Voucher_Type.Contains(Supplier_voucher_type)
            //        select new Financial_Transactions { Voucher_Type = p.Voucher_Type }).ToList();
            List<RealStatePlayer> allsearch = db.REPs.Select(x => new RealStatePlayer
            {
                Id = x.Id,
                Name = x.Name
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