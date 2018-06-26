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
    public class PhaseController : Controller
    {
        private ReCountantEntities db = new ReCountantEntities();
        // GET: Phase
        public ActionResult Index()
        {
            return View(db.D_Phase.ToList());
        }

        // GET: Phase/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            D_Phase d_Phase = db.D_Phase.Find(id);
            if (d_Phase == null)
            {
                return HttpNotFound();
            }
            return View(d_Phase);
        }

        // GET: Phase/Create
        public ActionResult Create()
        {
            return View();
        }

        public ActionResult LoadProjectDDL()
        {
            string status = "error";

            try
            {
                List<D_Projects> lst = db.D_Projects.ToList();
                status = Newtonsoft.Json.JsonConvert.SerializeObject(lst);
            }
            catch (Exception ex)
            {

            }

            return Content(status);
        }
        public ActionResult LoadPhaseDDL()
        {
            string status = "error";

            try
            {
                List<D_Phase> lst = db.D_Phase.ToList();
                status = Newtonsoft.Json.JsonConvert.SerializeObject(lst);
            }
            catch (Exception ex)
            {
              
            }

            return Content(status);
        }




        // POST: Phase/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
      
        public ActionResult Create(D_Phase d_Phase)
        {
            string status = "error";
            if (ModelState.IsValid)
            {
                db.D_Phase.Add(d_Phase);
                if (db.SaveChanges() > 0)
                {
                    status = "success";
                }
              
            }
            return Content(status);

        }

        // GET: Phase/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            D_Phase d_Phase = db.D_Phase.Find(id);
            if (d_Phase == null)
            {
                return HttpNotFound();
            }
            return View(d_Phase);
        }

        // POST: Phase/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Project_Id,Phase,Phase_Code")] D_Phase d_Phase)
        {
            if (ModelState.IsValid)
            {
                db.Entry(d_Phase).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(d_Phase);
        }

        // GET: Phase/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            D_Phase d_Phase = db.D_Phase.Find(id);
            if (d_Phase == null)
            {
                return HttpNotFound();
            }
            return View(d_Phase);
        }

        // POST: Phase/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            D_Phase d_Phase = db.D_Phase.Find(id);
            db.D_Phase.Remove(d_Phase);
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
    }
}
