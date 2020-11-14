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
    public class ItemsController : Controller
    {
        private db_IMSEntities db = new db_IMSEntities();

        public ActionResult IndexItem(int sectorId, int inventoryId)
        {
            ViewBag.SectorId = sectorId;
            ViewBag.InventoryId = inventoryId;

            List<tblItem> allItems = db.tblItems.ToList();
            List<tblItem> items = new List<tblItem>();
            foreach (var item in allItems)
            {
                if (item.SectorId == sectorId && item.InventoryId == inventoryId)
                {
                    items.Add(item);
                }
            }
            return View(items);
        }

        public ActionResult CreateItem(int sectorId, int inventoryId)
        {
            ViewBag.InventoryId = new SelectList(db.tblInventories, "InventoryId", "InventoryName");
            ViewBag.SectorId = new SelectList(db.tblSectors, "SectorId", "SectorName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateItem([Bind(Include = "ItemId,ItemDescription,IsPicked,SectorId,InventoryId")] tblItem tblItem)
        {
            if (ModelState.IsValid)
            {
                db.tblItems.Add(tblItem);
                db.SaveChanges();

                return RedirectToAction("IndexItem", new { sectorId = tblItem.SectorId, inventoryId = tblItem.InventoryId });

            }

            ViewBag.InventoryId = new SelectList(db.tblInventories, "InventoryId", "InventoryName", tblItem.InventoryId);
            ViewBag.SectorId = new SelectList(db.tblSectors, "SectorId", "SectorName", tblItem.SectorId);
            return View(tblItem);
        }

        // GET: Items
        public ActionResult Index()
        {
            var tblItems = db.tblItems.Include(t => t.tblInventory).Include(t => t.tblSector);
            return View(tblItems.ToList());
        }

        // GET: Items/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblItem tblItem = db.tblItems.Find(id);
            if (tblItem == null)
            {
                return HttpNotFound();
            }
            return View(tblItem);
        }

        // GET: Items/Create
        public ActionResult Create()
        {
            ViewBag.InventoryId = new SelectList(db.tblInventories, "InventoryId", "InventoryName");
            ViewBag.SectorId = new SelectList(db.tblSectors, "SectorId", "SectorName");
            return View();
        }

        // POST: Items/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ItemId,ItemDescription,IsPicked,SectorId,InventoryId")] tblItem tblItem)
        {
            if (ModelState.IsValid)
            {
                db.tblItems.Add(tblItem);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.InventoryId = new SelectList(db.tblInventories, "InventoryId", "InventoryName", tblItem.InventoryId);
            ViewBag.SectorId = new SelectList(db.tblSectors, "SectorId", "SectorName", tblItem.SectorId);
            return View(tblItem);
        }

        // GET: Items/Edit/5
        public ActionResult Edit(int? id, int sectorId, int inventoryId)
        {
            ViewBag.SectorId = sectorId;
            ViewBag.InventoryId = inventoryId;

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblItem tblItem = db.tblItems.Find(id);
            if (tblItem == null)
            {
                return HttpNotFound();
            }
            ViewBag.InventoryId = new SelectList(db.tblInventories, "InventoryId", "InventoryName", tblItem.InventoryId);
            ViewBag.SectorId = new SelectList(db.tblSectors, "SectorId", "SectorName", tblItem.SectorId);
            return View(tblItem);
        }

        // POST: Items/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ItemId,ItemDescription,IsPicked,SectorId,InventoryId")] tblItem tblItem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblItem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("IndexItem", new { sectorId = tblItem.SectorId, inventoryId = tblItem.InventoryId });
            }
            ViewBag.InventoryId = new SelectList(db.tblInventories, "InventoryId", "InventoryName", tblItem.InventoryId);
            ViewBag.SectorId = new SelectList(db.tblSectors, "SectorId", "SectorName", tblItem.SectorId);
            return View(tblItem);
        }

        // GET: Items/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblItem tblItem = db.tblItems.Find(id);
            if (tblItem == null)
            {
                return HttpNotFound();
            }
            return View(tblItem);
        }

        // POST: Items/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblItem tblItem = db.tblItems.Find(id);
            db.tblItems.Remove(tblItem);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult ShowAllItemsInAnInventories(int inventoryId)
        {
            if (cookieSystemPositionReading().Value == 1 + "" || cookieSystemPositionReading().Value == 2 + ""
                || cookieSystemPositionReading().Value == 3 + "" || cookieSystemPositionReading().Value == 4 + "")
            {


                ViewBag.inventoryId = inventoryId;
                return View(db.tblItems.ToList());
            }


            else
                return RedirectToAction("Login", "Users");
        }

        public ActionResult ShowAllItems()
        {

            if (cookieSystemPositionReading().Value == 1 + "" || cookieSystemPositionReading().Value == 2 + ""
                || cookieSystemPositionReading().Value == 3 + "" || cookieSystemPositionReading().Value == 4 + "")
            {


                return View(db.tblItems.ToList());
            }


            else
                return RedirectToAction("Login", "Users");

            
        }

        public ActionResult SelectInventory()
        {
            if (cookieSystemPositionReading().Value == 1 + "" || cookieSystemPositionReading().Value == 2 + ""
                || cookieSystemPositionReading().Value == 3 + "" || cookieSystemPositionReading().Value == 4 + "")
            {


                return View(db.tblItems.ToList());
            }


            else
                return RedirectToAction("Login", "Users");



           
        }

        /* public Action SaveInventoryInformtion(string name,int inventoryId)
         {
             string inventoryInformation = inventoryId + "," + inventoryId;
         }*/

        public ActionResult SelectItemFromInventory(int id)
        {


            if (cookieSystemPositionReading().Value == 1 + "" || cookieSystemPositionReading().Value == 2 + ""
                || cookieSystemPositionReading().Value == 3 + "" || cookieSystemPositionReading().Value == 4 + "")
            {


                ViewBag.inventoryId = id;
                return View(db.tblItems.ToList());
            }


            else
                return RedirectToAction("Login", "Users");

            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SelectItemFromInventory()
        {


            var selectedId = Request["check"];

            //          return "Selected id is"+selectedId;

            if (!String.IsNullOrEmpty(selectedId))
            {
                String[] selectedIdArray = selectedId.Split(',');
                string filePath = "~/File/itemIdForReqSlip.txt";

                string allId = ReadItemId(filePath);



                for (int i = 0; i < selectedIdArray.Length; i++)
                {
                    if (String.IsNullOrEmpty(selectedIdArray[i]))
                        continue;
                    else
                    {


                        allId = allId + selectedIdArray[i];
                        if (i != selectedIdArray.Length - 1)
                            allId = allId + ",";

                    }



                }

                allId = allId + "\n";

                WriteItemId(filePath, allId);
            }

            return RedirectToAction("ShowAllCategoryForRequisitionSlip", "Inventories");///////
        }





        private string ReadItemId(string filePath)
        {
            filePath = Server.MapPath(filePath);
            string s = System.IO.File.ReadAllText(filePath);
            return s;
        }

        private void WriteItemId(string filePath, string fileContent)
        {
            if (System.IO.File.Exists(filePath))
                System.IO.File.Delete(filePath);

            filePath = Server.MapPath(filePath);
            System.IO.File.WriteAllText(filePath, fileContent);
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

        public HttpCookie cookieUserIdReading()
        {
            HttpCookie acookie = null;
            if (Request.Cookies["userId"] != null)
            {
                acookie = Request.Cookies["userId"];
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
