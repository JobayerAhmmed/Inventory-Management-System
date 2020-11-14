using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using IMS_Final_Version2.Models.dbModel;

namespace IMS_Final_Version2.Controllers
{
    public class VoucharsController : Controller
    {
        private db_IMSEntities db = new db_IMSEntities();

        // GET: Vouchars
        public ActionResult Index()
        {
            return View(db.tblVouchars.ToList());
        }

        // GET: Vouchars/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblVouchar tblVouchar = db.tblVouchars.Find(id);
            if (tblVouchar == null)
            {
                return HttpNotFound();
            }
            return View(tblVouchar);
        }

        // GET: Vouchars/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Vouchars/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "VoucharId,VoucharFilePath")] tblVouchar tblVouchar)
        {
            if (ModelState.IsValid)
            {
                db.tblVouchars.Add(tblVouchar);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tblVouchar);
        }

        // GET: Vouchars/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblVouchar tblVouchar = db.tblVouchars.Find(id);
            if (tblVouchar == null)
            {
                return HttpNotFound();
            }
            return View(tblVouchar);
        }

        // POST: Vouchars/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "VoucharId,VoucharFilePath")] tblVouchar tblVouchar)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblVouchar).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tblVouchar);
        }

        // GET: Vouchars/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblVouchar tblVouchar = db.tblVouchars.Find(id);
            if (tblVouchar == null)
            {
                return HttpNotFound();
            }
            return View(tblVouchar);
        }

        // POST: Vouchars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblVouchar tblVouchar = db.tblVouchars.Find(id);
            db.tblVouchars.Remove(tblVouchar);
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
