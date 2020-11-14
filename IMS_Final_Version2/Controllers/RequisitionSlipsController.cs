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
    public class RequisitionSlipsController : Controller
    {
        private db_IMSEntities db = new db_IMSEntities();

        // GET: RequisitionSlips
        public ActionResult Index()
        {
            var tblRequisitionSlips = db.tblRequisitionSlips.Include(t => t.tblNotification).Include(t => t.tblUser).Include(t => t.tblUser1).Include(t => t.tblUser2);
            return View(tblRequisitionSlips.ToList());
        }

        // GET: RequisitionSlips/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblRequisitionSlip tblRequisitionSlip = db.tblRequisitionSlips.Find(id);
            if (tblRequisitionSlip == null)
            {
                return HttpNotFound();
            }
            return View(tblRequisitionSlip);
        }

        // GET: RequisitionSlips/Create
        public ActionResult Create()
        {
            ViewBag.NotificationId = new SelectList(db.tblNotifications, "NotificationId", "NotificationBody");
            ViewBag.ApplicantId = new SelectList(db.tblUsers, "UserId", "UserName");
            ViewBag.DirectorId = new SelectList(db.tblUsers, "UserId", "UserName");
            ViewBag.RecommenderId = new SelectList(db.tblUsers, "UserId", "UserName");
            return View();
        }

        // POST: RequisitionSlips/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RequisitionSlipId,RequisitionDate,IssuingPurpose,AllInventoryNameWithAmount,ApplicantId,RecommenderId,DirectorId,RecommenderResponse,DirectorResponse,ApplicationStatus,ItemsId,NotificationId")] tblRequisitionSlip tblRequisitionSlip)
        {
            if (ModelState.IsValid)
            {
                db.tblRequisitionSlips.Add(tblRequisitionSlip);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.NotificationId = new SelectList(db.tblNotifications, "NotificationId", "NotificationBody", tblRequisitionSlip.NotificationId);
            ViewBag.ApplicantId = new SelectList(db.tblUsers, "UserId", "UserName", tblRequisitionSlip.ApplicantId);
            ViewBag.DirectorId = new SelectList(db.tblUsers, "UserId", "UserName", tblRequisitionSlip.DirectorId);
            ViewBag.RecommenderId = new SelectList(db.tblUsers, "UserId", "UserName", tblRequisitionSlip.RecommenderId);
            return View(tblRequisitionSlip);
        }

        // GET: RequisitionSlips/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblRequisitionSlip tblRequisitionSlip = db.tblRequisitionSlips.Find(id);
            if (tblRequisitionSlip == null)
            {
                return HttpNotFound();
            }
            ViewBag.NotificationId = new SelectList(db.tblNotifications, "NotificationId", "NotificationBody", tblRequisitionSlip.NotificationId);
            ViewBag.ApplicantId = new SelectList(db.tblUsers, "UserId", "UserName", tblRequisitionSlip.ApplicantId);
            ViewBag.DirectorId = new SelectList(db.tblUsers, "UserId", "UserName", tblRequisitionSlip.DirectorId);
            ViewBag.RecommenderId = new SelectList(db.tblUsers, "UserId", "UserName", tblRequisitionSlip.RecommenderId);
            return View(tblRequisitionSlip);
        }

        // POST: RequisitionSlips/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RequisitionSlipId,RequisitionDate,IssuingPurpose,AllInventoryNameWithAmount,ApplicantId,RecommenderId,DirectorId,RecommenderResponse,DirectorResponse,ApplicationStatus,ItemsId,NotificationId")] tblRequisitionSlip tblRequisitionSlip)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblRequisitionSlip).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.NotificationId = new SelectList(db.tblNotifications, "NotificationId", "NotificationBody", tblRequisitionSlip.NotificationId);
            ViewBag.ApplicantId = new SelectList(db.tblUsers, "UserId", "UserName", tblRequisitionSlip.ApplicantId);
            ViewBag.DirectorId = new SelectList(db.tblUsers, "UserId", "UserName", tblRequisitionSlip.DirectorId);
            ViewBag.RecommenderId = new SelectList(db.tblUsers, "UserId", "UserName", tblRequisitionSlip.RecommenderId);
            return View(tblRequisitionSlip);
        }

        // GET: RequisitionSlips/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblRequisitionSlip tblRequisitionSlip = db.tblRequisitionSlips.Find(id);
            if (tblRequisitionSlip == null)
            {
                return HttpNotFound();
            }
            return View(tblRequisitionSlip);
        }

        // POST: RequisitionSlips/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblRequisitionSlip tblRequisitionSlip = db.tblRequisitionSlips.Find(id);
            db.tblRequisitionSlips.Remove(tblRequisitionSlip);
            db.SaveChanges();
            return RedirectToAction("Index");




        }


        public ActionResult RequisitionSlipFirstInterface()
        {
            string inventoryFilePath = "~/File/inventoryForReqSlip.txt";
            string itemFilePath = "~/File/itemIdForReqSlip.txt";

            Write(inventoryFilePath, "");
            Write(itemFilePath, "");

            return View();
        }


        public ActionResult MyRequisitionHistory()
        {

            if (cookieSystemPositionReading().Value == 1 + "" || cookieSystemPositionReading().Value == 2 + ""
                || cookieSystemPositionReading().Value == 3 + "" || cookieSystemPositionReading().Value == 4 + "") {

                    string filePath = "~/File/currentUserId.txt";
                    string currentUserId = read(filePath);

                    var tblRequisitionSlips = db.tblRequisitionSlips.Include(t => t.tblNotification).Include(t => t.tblUser).Include(t => t.tblUser1).Include(t => t.tblUser2);
                    return View(tblRequisitionSlips.ToList());
            }

            return RedirectToAction("Login", "Users");

        }

        public ActionResult UserRecommenderDirectorRequisitionHistory()
        {
            string filePath = "~/File/currentUserId.txt";
            string currentUser = read(filePath);
            int currentUserId = Int32.Parse(currentUser);

                    tblUser user = db.tblUsers.Find(currentUserId);
            int systemPosition = (int)user.SystemPosition;

            if (cookieSystemPositionReading().Value == 1 + "")
            {
                return RedirectToAction("StoreInChargeRequisitionHistoryCheck");
            }

            if (cookieSystemPositionReading().Value == 2 + "")
                return RedirectToAction("MyRequisitionHistory");
            if (cookieSystemPositionReading().Value == 3 + "")
                return RedirectToAction("RecommenderRequisitionHistoryCheck");
            if (cookieSystemPositionReading().Value == 4 + "")
            {
                return RedirectToAction("DirectorRequisitionHistoryCheck");
            }

            else
                return RedirectToAction("Login","System");
        }

        public ActionResult StoreInChargeRequisitionHistoryCheck()
        {
            if (cookieSystemPositionReading().Value == 1 + "")
            {
                return View();
            }

            else
                return RedirectToAction("Login", "Users");
        }

        public ActionResult StoreInChargeRequisitionHistoryCheckAsStoreInCharge()
        {

            if (cookieSystemPositionReading().Value == 1 + "")
            {
                var tblRequisitionSlips = db.tblRequisitionSlips.Include(t => t.tblNotification).Include(t => t.tblUser).Include(t => t.tblUser1).Include(t => t.tblUser2);
                return View(tblRequisitionSlips.ToList());
            }

            else
                return RedirectToAction("Login", "Users");
        }

        public ActionResult RecommenderRequisitionHistoryCheck()
        {
            if (cookieSystemPositionReading().Value == 3 + "")
            {
                return View();
            }

            else
                return RedirectToAction("Login", "Users");
        }

        public ActionResult RecommenderRequisitionHistoryCheckAsRecommender()
        {

            if (cookieSystemPositionReading().Value == 3 + "")
            {
                string filePath = "~/File/currentUserId.txt";
                string currentUserId = read(filePath);

                ViewBag.UserId = Int32.Parse(currentUserId);
                var tblRequisitionSlips = db.tblRequisitionSlips.Include(t => t.tblNotification).Include(t => t.tblUser).Include(t => t.tblUser1).Include(t => t.tblUser2);
                return View(tblRequisitionSlips.ToList());
            }

            else
                return RedirectToAction("Login", "Users");



           
        }

        public ActionResult DirectorRequisitionHistoryCheck()
        {
            if (cookieSystemPositionReading().Value == 4 + "")
            {
                return View();
            }

            else
                return RedirectToAction("Login", "Users");
        }

        public ActionResult DirectorRequisitionHistoryCheckAsRecommender()
        {
            if (cookieSystemPositionReading().Value == 4 + "")
            {
                string filePath = "~/File/currentUserId.txt";
                string currentUserId = read(filePath);

                ViewBag.UserId = Int32.Parse(currentUserId);
                var tblRequisitionSlips = db.tblRequisitionSlips.Include(t => t.tblNotification).Include(t => t.tblUser).Include(t => t.tblUser1).Include(t => t.tblUser2);
                return View(tblRequisitionSlips.ToList()); return View();
            }

            else
                return RedirectToAction("Login", "Users");



            
        }

        // GET: RequisitionSlips/Create
        public ActionResult CreateRequisitionSlip()
        {
            if (cookieSystemPositionReading().Value == 1 + "" || cookieSystemPositionReading().Value == 2 + ""
                || cookieSystemPositionReading().Value == 3 + "" || cookieSystemPositionReading().Value == 4 + "")
            {

                string inventoryFilePath = "~/File/inventoryForReqSlip.txt";
                string itemFilePath = "~/File/itemIdForReqSlip.txt";

                string inventoryInfo = read(inventoryFilePath);
                string itemInfo = read(itemFilePath);

                string[] inventoryInfoArray = inventoryInfo.Split('\n');


                string[] itemInfoArray = itemInfo.Split('\n');




                string[] inventoriesName = new string[inventoryInfoArray.Length];
                int[] itemAmountInACategory = new int[inventoryInfoArray.Length];

                for (int i = 0; i < inventoryInfoArray.Length; i++)
                {
                    if (string.IsNullOrEmpty(inventoryInfoArray[i]))
                        continue;
                    string[] temp = inventoryInfoArray[i].Split(',');
                    inventoriesName[i] = getItemAmount(itemInfoArray[i]) + " " + temp[1];



                }

                ViewData["inventoryName"] = inventoriesName;


                ViewBag.NotificationId = new SelectList(db.tblNotifications, "NotificationId", "NotificationBody");
                ViewBag.ApplicantId = new SelectList(db.tblUsers, "UserId", "UserName");
                ViewBag.DirectorId = new SelectList(db.tblUsers, "UserId", "UserName");
                ViewBag.RecommenderId = new SelectList(db.tblUsers, "UserId", "UserName");
                return View();
                //return "ss";
            }

            return RedirectToAction("Login", "Users");




            
        }

        // POST: RequisitionSlips/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateRequisitionSlip([Bind(Include = "RequisitionSlipId,RequisitionDate,IssuingPurpose,AllInventoryNameWithAmount,ApplicantId,RecommenderId,DirectorId,RecommenderResponse,DirectorResponse,ApplicationStatus,ItemsId,NotificationId")] tblRequisitionSlip tblRequisitionSlip)
        {

            if (cookieSystemPositionReading().Value == 1 + "" || cookieSystemPositionReading().Value == 2 + ""
                || cookieSystemPositionReading().Value == 3 + "" || cookieSystemPositionReading().Value == 4 + "")
            {
                try
                {
                    if (ModelState.IsValid)
                    {

                        string inventoryFilePath = "~/File/inventoryForReqSlip.txt";
                        string itemFilePath = "~/File/itemIdForReqSlip.txt";

                        string inventoryInfo = read(inventoryFilePath);
                        string itemInfo = read(itemFilePath);

                        string[] inventoryInfoArray = inventoryInfo.Split('\n');


                        string[] itemInfoArray = itemInfo.Split('\n');






                        string[] inventoriesName = new string[inventoryInfoArray.Length];
                        int[] itemAmountInACategory = new int[inventoryInfoArray.Length];


                        string categoryNameAndAmountToSaveInDatabase = "";

                        for (int i = 0; i < inventoryInfoArray.Length; i++)//hewlw last index contains "" or emptyvalue
                        {
                            if (string.IsNullOrEmpty(inventoryInfoArray[i]))
                                continue;
                            string[] temp = inventoryInfoArray[i].Split(',');
                            inventoriesName[i] = temp[1] + " : " + getItemAmount(itemInfoArray[i]);


                            categoryNameAndAmountToSaveInDatabase = categoryNameAndAmountToSaveInDatabase + inventoriesName[i];

                            if (i != inventoryInfoArray.Length - 2)
                                categoryNameAndAmountToSaveInDatabase = categoryNameAndAmountToSaveInDatabase + ",";

                        }





                        tblRequisitionSlip.RequisitionDate = DateTime.Now;
                        tblRequisitionSlip.AllInventoryNameWithAmount = categoryNameAndAmountToSaveInDatabase;

                        string itemsId = "";

                        for (int i = 0; i < itemInfoArray.Length; i++)
                        {
                            if (!string.IsNullOrEmpty(itemInfoArray[i]))
                            {
                                itemsId = itemsId + itemInfoArray[i];

                                if (i != itemInfoArray.Length - 2)
                                    itemsId = itemsId + ",";
                            }
                        }
                        //return itemInfoArray[0] + itemInfoArray[1] + itemInfoArray[2];
                        // return itemsId;


                        tblRequisitionSlip.ItemsId = itemsId;

                        string filePath = "~/File/currentUserId.txt";
                        string currentUserId = read(filePath);

                        tblRequisitionSlip.ApplicantId = Int32.Parse(currentUserId);

                        tblRequisitionSlip.RecommenderResponse = "unseen";// default value to ease work
                        tblRequisitionSlip.DirectorResponse = "unseen";

                        tblRequisitionSlip.ApplicationStatus = "pending";

                        db.tblRequisitionSlips.Add(tblRequisitionSlip);
                        db.SaveChanges();
                        Write(inventoryFilePath, "");
                        Write(itemFilePath, "");
                        return RedirectToAction("defineRole", "System");
                        // return "";
                    }
                }
                catch (Exception e)
                {

                    string filePath = "~/File/currentUserId.txt";
                    int currentUserId = Int32.Parse(read(filePath));
                    UsersController userC = new UsersController();
                    int systemPosition = userC.getSystemPosition(currentUserId);

                    if (systemPosition == 1)
                    {
                        return RedirectToAction("GotoStoreInCharge", "System");
                    }

                    else if (systemPosition == 2)
                    {
                        return RedirectToAction("GotoUser", "System");
                    }

                    else if (systemPosition == 3)
                    {
                        return RedirectToAction("GotoRecommender", "System");
                    }

                    else if (systemPosition == 4)
                    {
                        return RedirectToAction("GotoDirector", "System");
                    }

                    return RedirectToAction("Login", "Users");
                }

                ViewBag.NotificationId = new SelectList(db.tblNotifications, "NotificationId", "NotificationBody");
                ViewBag.ApplicantId = new SelectList(db.tblUsers, "UserId", "UserName");
                ViewBag.DirectorId = new SelectList(db.tblUsers, "UserId", "UserName");
                ViewBag.RecommenderId = new SelectList(db.tblUsers, "UserId", "UserName");
                return View(tblRequisitionSlip);
                //return "";


            }
            return RedirectToAction("Login", "Users");
            
        }

        private string read(string filePath)
        {
            filePath = Server.MapPath(filePath);
            string s = System.IO.File.ReadAllText(filePath);
            return s;

        }
        private void Write(string filePath, string fileContent)
        {
            if (System.IO.File.Exists(filePath))
                System.IO.File.Delete(filePath);

            filePath = Server.MapPath(filePath);
            System.IO.File.WriteAllText(filePath, fileContent);
        }

        public ActionResult seeUncheckedRequisitionSlip()
        {
            if (cookieSystemPositionReading().Value == 3 + "")
            {
                var tblRequisitionSlips = db.tblRequisitionSlips.Include(t => t.tblNotification).Include(t => t.tblUser).Include(t => t.tblUser1).Include(t => t.tblUser2);

                return View(tblRequisitionSlips.ToList());
            }

            else
                return RedirectToAction("Login", "Users");



            
        }

        public ActionResult seeUncheckedRequisitionSlipDirector()
        {
            if (cookieSystemPositionReading().Value == 4 + "")
            {
                var tblRequisitionSlips = db.tblRequisitionSlips.Include(t => t.tblNotification).Include(t => t.tblUser).Include(t => t.tblUser1).Include(t => t.tblUser2);

                return View(tblRequisitionSlips.ToList());
            }

            else
                return RedirectToAction("Login", "Users");



           
        }

        public ActionResult seeSpecificRequisitionSlipForm(int? id)
        {
            if (cookieSystemPositionReading().Value == 3 + "")
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                tblRequisitionSlip tblRequisitionSlip = db.tblRequisitionSlips.Find(id);
                if (tblRequisitionSlip == null)
                {
                    return HttpNotFound();
                }
                return View(tblRequisitionSlip);
            }

            else
                return RedirectToAction("Login", "Users");


            
        }


        public ActionResult seeSpecificRequisitionSlipFormDirector(int? id)
        {

            if (cookieSystemPositionReading().Value == 4 + "")
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                tblRequisitionSlip tblRequisitionSlip = db.tblRequisitionSlips.Find(id);
                if (tblRequisitionSlip == null)
                {
                    return HttpNotFound();
                }
                return View(tblRequisitionSlip);
            }

            else
                return RedirectToAction("Login", "Users");

            
        }

        public ActionResult recommend(int? reqSlipId)
        {
            if (cookieSystemPositionReading().Value == 3 + "")
            {
                if (reqSlipId != null)
                {

                    tblRequisitionSlip tblRequisitionSlip = db.tblRequisitionSlips.Find(reqSlipId);

                    tblRequisitionSlip.RecommenderResponse = "yes";
                    string filePath = "~/File/currentUserId.txt";
                    string currentUserId = read(filePath);

                    tblRequisitionSlip.RecommenderId = Int32.Parse(currentUserId);

                    db.SaveChanges();
                    return RedirectToAction("seeUncheckedRequisitionSlip");
                }
                return View();
            }

            else
                return RedirectToAction("Login", "Users");



            
        }

        public ActionResult verifyByDirector(int? reqSlipId)
        {

            if (cookieSystemPositionReading().Value == 4 + "")
            {
                if (reqSlipId != null)
                {

                    tblRequisitionSlip tblRequisitionSlip = db.tblRequisitionSlips.Find(reqSlipId);

                    tblRequisitionSlip.DirectorResponse = "yes";
                    tblRequisitionSlip.ApplicationStatus = "accepted";
                    string filePath = "~/File/currentUserId.txt";
                    string currentUserId = read(filePath);

                    tblRequisitionSlip.DirectorId = Int32.Parse(currentUserId);

                    db.SaveChanges();
                    db.SaveChanges();
                    return RedirectToAction("seeUncheckedRequisitionSlipDirector");
                }
                return View();
            }

            else
                return RedirectToAction("Login", "Users");


            
        }


        public ActionResult reject(int? reqSlipId)
        {
            if (cookieSystemPositionReading().Value == 3 + "")
            {
                if (reqSlipId != null)
                {

                    if (reqSlipId != null)
                    {

                        tblRequisitionSlip tblRequisitionSlip = db.tblRequisitionSlips.Find(reqSlipId);

                        tblRequisitionSlip.RecommenderResponse = "no";
                        tblRequisitionSlip.ApplicationStatus = "rejected";
                        string filePath = "~/File/currentUserId.txt";
                        string currentUserId = read(filePath);

                        tblRequisitionSlip.RecommenderId = Int32.Parse(currentUserId);

                        db.SaveChanges();


                        string ReqSlipFilePath = "~/File/reqSlipId.txt";
                        Write(ReqSlipFilePath, reqSlipId + "");

                        return RedirectToAction("sendNotiIfRejected", "Notifications");
                    }
                    return View();
                }
                return View();
            }

            else
                return RedirectToAction("Login", "Users");



            
        }

        public ActionResult rejectByDirector(int? reqSlipId)
        {
            if (cookieSystemPositionReading().Value == 4 + "")
            {
                if (reqSlipId != null)
                {

                    if (reqSlipId != null)
                    {

                        tblRequisitionSlip tblRequisitionSlip = db.tblRequisitionSlips.Find(reqSlipId);

                        tblRequisitionSlip.DirectorResponse = "no";
                        tblRequisitionSlip.ApplicationStatus = "rejected";
                        string filePath = "~/File/currentUserId.txt";
                        string currentUserId = read(filePath);

                        tblRequisitionSlip.DirectorId = Int32.Parse(currentUserId);


                        db.SaveChanges();
                        string ReqSlipFilePath = "~/File/reqSlipId.txt";
                        Write(ReqSlipFilePath, reqSlipId + "");

                        return RedirectToAction("sendNotiIfRejected", "Notifications");
                    }
                    return View();
                }
                return View();
            }

            else
                return RedirectToAction("Login", "Users");




           
        }

        private int getItemAmount(string itemsInACategory)
        {
            string[] allItems = itemsInACategory.Split(',');
            return allItems.Length;
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
