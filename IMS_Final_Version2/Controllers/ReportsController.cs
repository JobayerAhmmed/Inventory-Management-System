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
    public class ReportsController : Controller
    {
        private db_IMSEntities db = new db_IMSEntities();

        // GET: Reports
        public ActionResult Index()
        {
            var tblReports = db.tblReports.Include(t => t.tblSector);
            return View(tblReports.ToList());
        }

        // GET: Reports/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblReport tblReport = db.tblReports.Find(id);
            if (tblReport == null)
            {
                return HttpNotFound();
            }
            return View(tblReport);
        }

        // GET: Reports/Create
        public ActionResult Create()
        {
            ViewBag.SectorId = new SelectList(db.tblSectors, "SectorId", "SectorName");
            return View();
        }

        // POST: Reports/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ReportId,SectorId,ReportTitle,ReportDate,ReportFilePath")] tblReport tblReport)
        {
            if (ModelState.IsValid)
            {
                db.tblReports.Add(tblReport);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SectorId = new SelectList(db.tblSectors, "SectorId", "SectorName", tblReport.SectorId);
            return View(tblReport);
        }

        // GET: Reports/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblReport tblReport = db.tblReports.Find(id);
            if (tblReport == null)
            {
                return HttpNotFound();
            }
            ViewBag.SectorId = new SelectList(db.tblSectors, "SectorId", "SectorName", tblReport.SectorId);
            return View(tblReport);
        }

        // POST: Reports/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ReportId,SectorId,ReportTitle,ReportDate,ReportFilePath")] tblReport tblReport)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblReport).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SectorId = new SelectList(db.tblSectors, "SectorId", "SectorName", tblReport.SectorId);
            return View(tblReport);
        }

        // GET: Reports/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblReport tblReport = db.tblReports.Find(id);
            if (tblReport == null)
            {
                return HttpNotFound();
            }
            return View(tblReport);
        }

        // POST: Reports/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblReport tblReport = db.tblReports.Find(id);
            db.tblReports.Remove(tblReport);
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
