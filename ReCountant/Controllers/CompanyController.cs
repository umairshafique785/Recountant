using ReCountant.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ReCountant.Controllers
{
    public class CompanyController : Controller
    {
        ReCountantEntities db = new ReCountantEntities();
        // GET: Company
        public ActionResult Index()
        {
            return View();
        }
        
        public ActionResult PostCompany( )
        {
            return View();
        }
        [HttpPost]
        public JsonResult PostComapny(D_Company company)
        {

            db.D_Company.Add(company);
            db.SaveChanges();
            return Json(true);
        }

        public JsonResult GetCompanyName()
        {
            List<company> allsearch = db.D_Company.Select(x => new company
            {
                Id_company = x.Id,
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