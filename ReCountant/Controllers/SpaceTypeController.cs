using ReCountant.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ReCountant.Controllers
{
    public class SpaceTypeController : Controller
    {
        // GET: SpaceType
        private ReCountantEntities db = new ReCountantEntities();
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult SearchSpaceType()
        {

            List<SpaceType> allsearch = db.D_SpaceType.Select(x => new SpaceType
            {
                Id = x.Id,
                Space_Type = x.Space_Type
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