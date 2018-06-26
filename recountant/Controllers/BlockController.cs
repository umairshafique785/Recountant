using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ReCountant.Models;

namespace ReContant.Controllers
{
    public class BlockController : Controller
    {
        private ReCountantEntities db = new ReCountantEntities();

        // GET: Block
        public ActionResult Index()
        {
            return View(db.D_Block.ToList());
        }

        // GET: Block/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            D_Block d_Block = db.D_Block.Find(id);
            if (d_Block == null)
            {
                return HttpNotFound();
            }
            return View(d_Block);
        }

        // GET: Block/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Block/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
       // [ValidateAntiForgeryToken]
        public ActionResult Create( D_Block d_Block)
        {
            string status = "error";
            if (ModelState.IsValid)
            {
                db.D_Block.Add(d_Block);
               if(db.SaveChanges() > 0)
                {
                    status = "success";
                }
                
            }

            return Content(status);
        }

        // GET: Block/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            D_Block d_Block = db.D_Block.Find(id);
            if (d_Block == null)
            {
                return HttpNotFound();
            }
            return View(d_Block);
        }

        // POST: Block/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Phase_Id,Block,Block_Code")] D_Block d_Block)
        {
            if (ModelState.IsValid)
            {
                db.Entry(d_Block).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(d_Block);
        }

        // GET: Block/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            D_Block d_Block = db.D_Block.Find(id);
            if (d_Block == null)
            {
                return HttpNotFound();
            }
            return View(d_Block);
        }

        // POST: Block/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            D_Block d_Block = db.D_Block.Find(id);
            db.D_Block.Remove(d_Block);
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
