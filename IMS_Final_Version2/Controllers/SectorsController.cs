using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using IMS_Final_Version2.Models.dbModel;

namespace IMS_Final.Controllers
{
    public class SectorsController : Controller
    {
        private db_IMSEntities db = new db_IMSEntities();

        // GET: Sectors
        public ActionResult Index()
        {
            return View(db.tblSectors.ToList());
        }

        // GET: Sectors/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblSector tblSector = db.tblSectors.Find(id);
            if (tblSector == null)
            {
                return HttpNotFound();
            }
            return View(tblSector);
        }

        // GET: Sectors/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Sectors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SectorId,SectorName,SectorDescription,CreatingDate")] tblSector tblSector)
        {
            if (ModelState.IsValid)
            {
                db.tblSectors.Add(tblSector);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tblSector);
        }

        // GET: Sectors/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblSector tblSector = db.tblSectors.Find(id);
            if (tblSector == null)
            {
                return HttpNotFound();
            }
            return View(tblSector);
        }

        // POST: Sectors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SectorId,SectorName,SectorDescription")] tblSector tblSector)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblSector).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tblSector);
        }

        // GET: Sectors/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblSector tblSector = db.tblSectors.Find(id);
            if (tblSector == null)
            {
                return HttpNotFound();
            }
            return View(tblSector);
        }

        // POST: Sectors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblSector tblSector = db.tblSectors.Find(id);
            db.tblSectors.Remove(tblSector);
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
