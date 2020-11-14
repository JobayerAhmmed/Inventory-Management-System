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
    public class LeftPagesController : Controller
    {
        private db_IMSEntities db = new db_IMSEntities();

        public ActionResult IndexWithSectorAndInventoryId(int sectorId, int inventoryId)
        {
            ViewBag.SectorId = sectorId;
            ViewBag.InventoryId = inventoryId;

            List<tblLeftPage> tables = db.tblLeftPages.ToList();
            List<tblLeftPage> leftPages = new List<tblLeftPage>();
            foreach (var item in tables)
            {
                if (item.SectorId == sectorId && item.InventoryId == inventoryId)
                {
                    leftPages.Add(item);
                }
            }
            return View(leftPages);
        }

        public ActionResult CreateWithSectorAndInventoryId(int sectorId, int inventoryId)
        {
            //ViewBag.SectorId = sectorId;
            //ViewBag.InventoryId = inventoryId;
            ViewBag.InventoryId = new SelectList(db.tblInventories, "InventoryId", "InventoryName");
            ViewBag.SectorId = new SelectList(db.tblSectors, "SectorId", "SectorName");
            ViewBag.VoucharId = new SelectList(db.tblVouchars, "VoucharId", "VoucharFilePath");

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateWithSectorAndInventoryId([Bind(Include = "PageId,ReceiveingInfo,ReceivingSerial,ReceivingDate,PreviousBalance,NoOfReceivingArticles,TotalBalance,SectorId,InventoryId,VoucharId")] tblLeftPage tblLeftPage)
        {
            //tblSector sector = db.tblSectors.Find(sectorId);
            // tblInventory inventory = db.tblInventories.Find(inventoryId);

            if (ModelState.IsValid)
            {
                List<tblItem> items = db.tblItems.ToList();
                List<tblLeftPage> leftPages = db.tblLeftPages.ToList();

                // count items which are not picked
                int count = 0;
                foreach (var item in items)
                {
                    if (item.SectorId == tblLeftPage.SectorId && item.InventoryId == tblLeftPage.InventoryId && item.IsPicked == 0)
                    {
                        count++;
                    }
                }
                tblLeftPage.PreviousBalance = count;

                // count revceiving serial
                int receive = 0;
                foreach (var item in leftPages)
                {
                    if (item.SectorId == tblLeftPage.SectorId && item.InventoryId == tblLeftPage.InventoryId)
                    {
                        receive++;
                    }
                }
                receive++;
                tblLeftPage.ReceivingSerial = receive;

                // set date
                tblLeftPage.ReceivingDate = DateTime.Now;

                //set total balance
                tblLeftPage.TotalBalance = count + tblLeftPage.NoOfReceivingArticles;

                db.tblLeftPages.Add(tblLeftPage);
                db.SaveChanges();

                return RedirectToAction("CreateItem", "Items", new { sectorId = tblLeftPage.SectorId, inventoryId = tblLeftPage.InventoryId });

            }

            ViewBag.InventoryId = new SelectList(db.tblInventories, "InventoryId", "InventoryName", tblLeftPage.InventoryId);
            ViewBag.SectorId = new SelectList(db.tblSectors, "SectorId", "SectorName", tblLeftPage.SectorId);
            ViewBag.VoucharId = new SelectList(db.tblVouchars, "VoucharId", "VoucharFilePath", tblLeftPage.VoucharId);
            return View(tblLeftPage);

        }

        // GET: LeftPages
        public ActionResult Index()
        {
            //var receivedInventories = db.tblLeftPages.

            /*if (inventoryId != null)
            {
                receivedInventories.tblLeftPages = db.tblInventories.Where(i => i.InventoryId == inventoryId.Value).Single().tblLeftPages();
            }*/

            var tblLeftPages = db.tblLeftPages.Include(t => t.tblInventory).Include(t => t.tblSector).Include(t => t.tblVouchar);
            return View(tblLeftPages.ToList());
        }

        /* public void CallCreateItem(int? sId, int? inId)
         {
             while ( articleNumber != 0)
             {
                 articleNumber--;
                 RedirectToAction("CreateItem", "Items", new { sectorId = sId, inventoryId = inId });
             }
         }*/

        // GET: LeftPages/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblLeftPage tblLeftPage = db.tblLeftPages.Find(id);
            if (tblLeftPage == null)
            {
                return HttpNotFound();
            }
            return View(tblLeftPage);
        }

        // GET: LeftPages/Create
        public ActionResult Create()
        {
            ViewBag.InventoryId = new SelectList(db.tblInventories, "InventoryId", "InventoryName");
            ViewBag.SectorId = new SelectList(db.tblSectors, "SectorId", "SectorName");
            ViewBag.VoucharId = new SelectList(db.tblVouchars, "VoucharId", "VoucharFilePath");
            return View();
        }

        // POST: LeftPages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PageId,ReceiveingInfo,ReceivingSerial,ReceivingDate,PreviousBalance,NoOfReceivingArticles,TotalBalance,SectorId,InventoryId,VoucharId")] tblLeftPage tblLeftPage)
        {
            if (ModelState.IsValid)
            {
                db.tblLeftPages.Add(tblLeftPage);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.InventoryId = new SelectList(db.tblInventories, "InventoryId", "InventoryName", tblLeftPage.InventoryId);
            ViewBag.SectorId = new SelectList(db.tblSectors, "SectorId", "SectorName", tblLeftPage.SectorId);
            ViewBag.VoucharId = new SelectList(db.tblVouchars, "VoucharId", "VoucharFilePath", tblLeftPage.VoucharId);
            return View(tblLeftPage);
        }

        // GET: LeftPages/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblLeftPage tblLeftPage = db.tblLeftPages.Find(id);
            if (tblLeftPage == null)
            {
                return HttpNotFound();
            }
            ViewBag.InventoryId = new SelectList(db.tblInventories, "InventoryId", "InventoryName", tblLeftPage.InventoryId);
            ViewBag.SectorId = new SelectList(db.tblSectors, "SectorId", "SectorName", tblLeftPage.SectorId);
            ViewBag.VoucharId = new SelectList(db.tblVouchars, "VoucharId", "VoucharFilePath", tblLeftPage.VoucharId);
            return View(tblLeftPage);
        }

        // POST: LeftPages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PageId,ReceiveingInfo,ReceivingSerial,ReceivingDate,PreviousBalance,NoOfReceivingArticles,TotalBalance,SectorId,InventoryId,VoucharId")] tblLeftPage tblLeftPage)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblLeftPage).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("IndexWithSectorAndInventoryId", new { sectorId = tblLeftPage.SectorId, inventoryId = tblLeftPage.InventoryId});
            }
            ViewBag.InventoryId = new SelectList(db.tblInventories, "InventoryId", "InventoryName", tblLeftPage.InventoryId);
            ViewBag.SectorId = new SelectList(db.tblSectors, "SectorId", "SectorName", tblLeftPage.SectorId);
            ViewBag.VoucharId = new SelectList(db.tblVouchars, "VoucharId", "VoucharFilePath", tblLeftPage.VoucharId);
            return View(tblLeftPage);
        }

        // GET: LeftPages/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblLeftPage tblLeftPage = db.tblLeftPages.Find(id);
            if (tblLeftPage == null)
            {
                return HttpNotFound();
            }
            return View(tblLeftPage);
        }

        // POST: LeftPages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblLeftPage tblLeftPage = db.tblLeftPages.Find(id);
            db.tblLeftPages.Remove(tblLeftPage);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        public HttpCookie cookieSystemPositionReading()
        {
            HttpCookie acookie = null;
            if (Request.Cookies["systemPosition"] != null)
            {
                acookie = Request.Cookies["systemPosition"];
            }
            return acookie;
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
