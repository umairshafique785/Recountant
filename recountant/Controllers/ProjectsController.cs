using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ReCountant.Models;

namespace ReCountant.Controllers
{
    public class ProjectsController : Controller
    {
        private ReCountantEntities db = new ReCountantEntities();

        // GET: Projects
        public ActionResult Index()
        {
            return View(db.D_Projects.ToList());
        }

        // GET: Projects/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            D_Projects d_Projects = db.D_Projects.Find(id);
            if (d_Projects == null)
            {
                return HttpNotFound();
            }
            return View(d_Projects);
        }

        // GET: Projects/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Projects/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create(D_Projects d_Projects)
        {
            string status = "error";
            if (ModelState.IsValid)
            {
                db.D_Projects.Add(d_Projects);
               if( db.SaveChanges()>0)
                {
                    status = "success";
                }

                return Content(status);
            }

            return View(d_Projects);
        }

        // GET: Projects/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            D_Projects d_Projects = db.D_Projects.Find(id);
            if (d_Projects == null)
            {
                return HttpNotFound();
            }
            return View(d_Projects);
        }

        // POST: Projects/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] D_Projects d_Projects)
        {
            if (ModelState.IsValid)
            {
                db.Entry(d_Projects).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(d_Projects);
        }

        // GET: Projects/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            D_Projects d_Projects = db.D_Projects.Find(id);
            if (d_Projects == null)
            {
                return HttpNotFound();
            }
            return View(d_Projects);
        }

        // POST: Projects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            D_Projects d_Projects = db.D_Projects.Find(id);
            db.D_Projects.Remove(d_Projects);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        public JsonResult SearchProjectName(string RealStateAct)
        {
            //return (from p in db.F_Financial_Transactions
            //        where p.Voucher_Type.Contains(Supplier_voucher_type)
            //        select new Financial_Transactions { Voucher_Type = p.Voucher_Type }).ToList();
            List<Project> allsearch = db.D_Projects.Select(x => new Project
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
