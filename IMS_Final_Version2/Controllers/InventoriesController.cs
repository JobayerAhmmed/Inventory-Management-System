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
    public class InventoriesController : Controller
    {
        private db_IMSEntities db = new db_IMSEntities();

        public ActionResult IndexWithSectorId(int sectorId)
        {
            ViewBag.SectorId = sectorId;

            var inventories = db.tblInventories.OrderBy(i => i.InventoryName);

            return View(inventories.ToList());
        }

        // GET: Inventories
        public ActionResult Index()
        {
            //var viewModel = new InventoryIndexModel();

            //viewModel.inventories = db.tblInventories.OrderBy(i => i.InventoryName);

            var inventories = db.tblInventories.OrderBy(i => i.InventoryName);

            return View(inventories.ToList());
        }

        // GET: Inventories/Details/5
        public ActionResult Details(int? id, int sectorId)
        {
            ViewBag.SectorId = sectorId;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblInventory tblInventory = db.tblInventories.Find(id);
            if (tblInventory == null)
            {
                return HttpNotFound();
            }
            return View(tblInventory);
        }

        // GET: Inventories/Create
        public ActionResult Create(int sectorId)
        {
            ViewBag.SectorId = sectorId;
            return View();
        }

        // POST: Inventories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "InventoryId,InventoryName,InventoryDescription,InventoryPicturePath")] tblInventory tblInventory)
        {
            if (ModelState.IsValid)
            {
                db.tblInventories.Add(tblInventory);
                db.SaveChanges();
                return RedirectToAction("IndexWithSectorId", new { sectorId = Request.QueryString["sectorId"] });
            }

            return View(tblInventory);
        }

        // GET: Inventories/Edit/5
        public ActionResult Edit(int? id, int sectorId)
        {
            ViewBag.SectorId = sectorId;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblInventory tblInventory = db.tblInventories.Find(id);
            if (tblInventory == null)
            {
                return HttpNotFound();
            }
            return View(tblInventory);
        }

        // POST: Inventories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "InventoryId,InventoryName,InventoryDescription,InventoryPicturePath")] tblInventory tblInventory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblInventory).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("IndexWithSectorId", new { sectorId = Request.QueryString["sectorId"] });
            }
            return View(tblInventory);
        }

        // GET: Inventories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblInventory tblInventory = db.tblInventories.Find(id);
            if (tblInventory == null)
            {
                return HttpNotFound();
            }
            return View(tblInventory);
        }

        // POST: Inventories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblInventory tblInventory = db.tblInventories.Find(id);
            db.tblInventories.Remove(tblInventory);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        public ActionResult ShowAllCategories()
        {
            if (cookieSystemPositionReading().Value == 1 + "" || cookieSystemPositionReading().Value == 2 + ""
                || cookieSystemPositionReading().Value == 3 + "" || cookieSystemPositionReading().Value == 4 + "")
            {


                return View(db.tblInventories.ToList());
            }

            else
                return RedirectToAction("Login", "Users");
        }


        public ActionResult ShowAllCategoryForRequisitionSlip()
        {

            if (cookieSystemPositionReading().Value == 1 + "" || cookieSystemPositionReading().Value == 2 + ""
                || cookieSystemPositionReading().Value == 3 + "" || cookieSystemPositionReading().Value == 4 + "")
            {


                return View(db.tblInventories.ToList());
            }

            else
                return RedirectToAction("Login", "Users");


           
        }

        public ActionResult SaveInventoryForRequsitionSlip(string name, int id)
        {

            if (cookieSystemPositionReading().Value == 1 + "" || cookieSystemPositionReading().Value == 2 + ""
                || cookieSystemPositionReading().Value == 3 + "" || cookieSystemPositionReading().Value == 4 + "")
            {
                string filepath = "~/File/inventoryForReqSlip.txt";
                string fileContent = WriteSavedReqSlipInfo(filepath);

                fileContent = fileContent + id + "," + name + "\n";
                WriteInventoryInformation(filepath, fileContent);

                return RedirectToAction("SelectItemFromInventory", "Items", new { id = id });
            }

            else
                return RedirectToAction("Login", "Users");


           

        }

        private void WriteInventoryInformation(string filePath, string fileContent)
        {
            if (System.IO.File.Exists(filePath))
                System.IO.File.Delete(filePath);

            filePath = Server.MapPath(filePath);
            System.IO.File.WriteAllText(filePath, fileContent);
        }

        private string WriteSavedReqSlipInfo(string filePath)
        {
            filePath = Server.MapPath(filePath);
            string s = System.IO.File.ReadAllText(filePath);
            return s;
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
