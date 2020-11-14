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
    public class RightPagesController : Controller
    {
        private db_IMSEntities db = new db_IMSEntities();

        // GET: RightPages
        public ActionResult Index()
        {
            var tblRightPages = db.tblRightPages.Include(t => t.tblItem).Include(t => t.tblLeftPage).Include(t => t.tblRequisitionSlip).Include(t => t.tblUser);
            return View(tblRightPages.ToList());
        }

        // GET: RightPages/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblRightPage tblRightPage = db.tblRightPages.Find(id);
            if (tblRightPage == null)
            {
                return HttpNotFound();
            }
            return View(tblRightPage);
        }

        // GET: RightPages/Create
        public ActionResult Create()
        {
            ViewBag.ItemId = new SelectList(db.tblItems, "ItemId", "ItemDescription");
            ViewBag.PageId = new SelectList(db.tblLeftPages, "PageId", "ReceiveingInfo");
            ViewBag.RequisitionSerialNumber = new SelectList(db.tblRequisitionSlips, "RequisitionSlipId", "IssuingPurpose");
            ViewBag.UserId = new SelectList(db.tblUsers, "UserId", "UserName");
            return View();
        }

        // POST: RightPages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,TakingDate,WhereLocated,PageId,ItemId,UserId,RequisitionSerialNumber")] tblRightPage tblRightPage)
        {
            if (ModelState.IsValid)
            {
                db.tblRightPages.Add(tblRightPage);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ItemId = new SelectList(db.tblItems, "ItemId", "ItemDescription", tblRightPage.ItemId);
            ViewBag.PageId = new SelectList(db.tblLeftPages, "PageId", "ReceiveingInfo", tblRightPage.PageId);
            ViewBag.RequisitionSerialNumber = new SelectList(db.tblRequisitionSlips, "RequisitionSlipId", "IssuingPurpose", tblRightPage.RequisitionSerialNumber);
            ViewBag.UserId = new SelectList(db.tblUsers, "UserId", "UserName", tblRightPage.UserId);
            return View(tblRightPage);
        }

        // GET: RightPages/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblRightPage tblRightPage = db.tblRightPages.Find(id);
            if (tblRightPage == null)
            {
                return HttpNotFound();
            }
            ViewBag.ItemId = new SelectList(db.tblItems, "ItemId", "ItemDescription", tblRightPage.ItemId);
            ViewBag.PageId = new SelectList(db.tblLeftPages, "PageId", "ReceiveingInfo", tblRightPage.PageId);
            ViewBag.RequisitionSerialNumber = new SelectList(db.tblRequisitionSlips, "RequisitionSlipId", "IssuingPurpose", tblRightPage.RequisitionSerialNumber);
            ViewBag.UserId = new SelectList(db.tblUsers, "UserId", "UserName", tblRightPage.UserId);
            return View(tblRightPage);
        }

        // POST: RightPages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,TakingDate,WhereLocated,PageId,ItemId,UserId,RequisitionSerialNumber")] tblRightPage tblRightPage)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblRightPage).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ItemId = new SelectList(db.tblItems, "ItemId", "ItemDescription", tblRightPage.ItemId);
            ViewBag.PageId = new SelectList(db.tblLeftPages, "PageId", "ReceiveingInfo", tblRightPage.PageId);
            ViewBag.RequisitionSerialNumber = new SelectList(db.tblRequisitionSlips, "RequisitionSlipId", "IssuingPurpose", tblRightPage.RequisitionSerialNumber);
            ViewBag.UserId = new SelectList(db.tblUsers, "UserId", "UserName", tblRightPage.UserId);
            return View(tblRightPage);
        }

        // GET: RightPages/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblRightPage tblRightPage = db.tblRightPages.Find(id);
            if (tblRightPage == null)
            {
                return HttpNotFound();
            }
            return View(tblRightPage);
        }

        // POST: RightPages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblRightPage tblRightPage = db.tblRightPages.Find(id);
            db.tblRightPages.Remove(tblRightPage);
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
