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
    public class PlotController : Controller
    {
        private ReCountantEntities db = new ReCountantEntities();

        // GET: Plot
        public ActionResult Index()
        {
            return View(db.D_Plot.ToList());
        }

        // GET: Plot/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            D_Plot d_Plot = db.D_Plot.Find(id);
            if (d_Plot == null)
            {
                return HttpNotFound();
            }
            return View(d_Plot);
        }

        // GET: Plot/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Plot/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //  [ValidateAntiForgeryToken]
        //  public ActionResult Create([Bind(Include = "Id,Date,Block_Id,UOM,Area,Length,Width,Front,Date_Of_Sale,Plot_No,Userid,Plot_Availibilty")] D_Plot d_Plot)
        public ActionResult Create(D_Plot d_Plot)
       {
            string status = "error";
            if (ModelState.IsValid)
            {
                d_Plot.Date = DateTime.Now;
                db.D_Plot.Add(d_Plot);
                if (db.SaveChanges() > 0)
                {
                    status = "sucess";
                }
               
            }

            return Content(status);
        }

        // GET: Plot/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            D_Plot d_Plot = db.D_Plot.Find(id);
            if (d_Plot == null)
            {
                return HttpNotFound();
            }
            return View(d_Plot);
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
        public ActionResult LoadPhaseDDL(int id)
        {
            string status = "error";

            try
            {
                List<D_Phase> lst = db.D_Phase.Where(x => x.Project_Id == id).ToList();
                status = Newtonsoft.Json.JsonConvert.SerializeObject(lst);
            }
            catch (Exception ex)
            {

            }

            return Content(status);
        }
        public ActionResult LoadBlockDDL(int id)
        {
            string status = "error";

            try
            {
                List<D_Block> lst = db.D_Block.Where(x => x.Phase_Id == id).ToList();
                status = Newtonsoft.Json.JsonConvert.SerializeObject(lst);
            }
            catch (Exception ex)
            {

            }

            return Content(status);
        }
        // POST: Plot/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Date,Block_Id,UOM,Area,Length,Width,Front,Date_Of_Sale,Plot_No,Userid,Plot_Availibilty")] D_Plot d_Plot)
        {
            if (ModelState.IsValid)
            {
                db.Entry(d_Plot).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(d_Plot);
        }

        // GET: Plot/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            D_Plot d_Plot = db.D_Plot.Find(id);
            if (d_Plot == null)
            {
                return HttpNotFound();
            }
            return View(d_Plot);
        }

        // POST: Plot/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            D_Plot d_Plot = db.D_Plot.Find(id);
            db.D_Plot.Remove(d_Plot);
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

        [HttpPost]
        public JsonResult PostProject(D_Projects project)
        {
            if (ModelState.IsValid)
            {
                db.D_Projects.Add(project);
                db.SaveChanges();
            }
            return Json(true);
        }
    }
}
