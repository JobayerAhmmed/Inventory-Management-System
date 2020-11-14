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
    public class NotificationsController : Controller
    {
        private db_IMSEntities db = new db_IMSEntities();

        // GET: Notifications
        public ActionResult Index()
        {
            var tblNotifications = db.tblNotifications.Include(t => t.tblUser).Include(t => t.tblUser1);
            return View(tblNotifications.ToList());
        }

        // GET: Notifications/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblNotification tblNotification = db.tblNotifications.Find(id);
            if (tblNotification == null)
            {
                return HttpNotFound();
            }
            return View(tblNotification);
        }

        // GET: Notifications/Create
        public ActionResult Create()
        {
            ViewBag.ReceiverId = new SelectList(db.tblUsers, "UserId", "UserName");
            ViewBag.SenderId = new SelectList(db.tblUsers, "UserId", "UserName");
            return View();
        }

        // POST: Notifications/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "NotificationId,SenderId,ReceiverId,NotificationDate,NotificationBody,IsSeen")] tblNotification tblNotification)
        {
            if (ModelState.IsValid)
            {
                db.tblNotifications.Add(tblNotification);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ReceiverId = new SelectList(db.tblUsers, "UserId", "UserName", tblNotification.ReceiverId);
            ViewBag.SenderId = new SelectList(db.tblUsers, "UserId", "UserName", tblNotification.SenderId);
            return View(tblNotification);
        }

        // GET: Notifications/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblNotification tblNotification = db.tblNotifications.Find(id);
            if (tblNotification == null)
            {
                return HttpNotFound();
            }
            ViewBag.ReceiverId = new SelectList(db.tblUsers, "UserId", "UserName", tblNotification.ReceiverId);
            ViewBag.SenderId = new SelectList(db.tblUsers, "UserId", "UserName", tblNotification.SenderId);
            return View(tblNotification);
        }

        // POST: Notifications/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "NotificationId,SenderId,ReceiverId,NotificationDate,NotificationBody,IsSeen")] tblNotification tblNotification)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblNotification).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ReceiverId = new SelectList(db.tblUsers, "UserId", "UserName", tblNotification.ReceiverId);
            ViewBag.SenderId = new SelectList(db.tblUsers, "UserId", "UserName", tblNotification.SenderId);
            return View(tblNotification);
        }

        // GET: Notifications/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblNotification tblNotification = db.tblNotifications.Find(id);
            if (tblNotification == null)
            {
                return HttpNotFound();
            }
            return View(tblNotification);
        }

        // POST: Notifications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblNotification tblNotification = db.tblNotifications.Find(id);
            db.tblNotifications.Remove(tblNotification);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult NotificationQueue()
        {
            if (cookieSystemPositionReading().Value == 1 + "" || cookieSystemPositionReading().Value == 2 + ""
                || cookieSystemPositionReading().Value == 3 + "" || cookieSystemPositionReading().Value == 4 + "")
            {
                string filePath = "~/File/currentUserId.txt";
                string currentUserId = read(filePath);
                ViewBag.userId = Int32.Parse(currentUserId);

                var tblNotifications = db.tblNotifications.Include(t => t.tblUser).Include(t => t.tblUser1);
                return View(tblNotifications.ToList());
            }

            else
                return RedirectToAction("Login", "Users");


            
        }


        public ActionResult SendNotification()
        {
            if (cookieSystemPositionReading().Value == 1 + "" || cookieSystemPositionReading().Value == 2 + ""
                || cookieSystemPositionReading().Value == 3 + "" || cookieSystemPositionReading().Value == 4 + "")
            {
                ViewBag.ReceiverId = new SelectList(db.tblUsers, "UserId", "UserName");
                ViewBag.SenderId = new SelectList(db.tblUsers, "UserId", "UserName");
                return View();
            }

            else
                return RedirectToAction("Login", "Users");


            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SendNotification([Bind(Include = "NotificationId,SenderId,ReceiverId,NotificationDate,NotificationBody,IsSeen")] tblNotification tblNotification)
        {

            if (cookieSystemPositionReading().Value == 1 + "" || cookieSystemPositionReading().Value == 2 + ""
                || cookieSystemPositionReading().Value == 3 + "" || cookieSystemPositionReading().Value == 4 + "")
            {
                if (ModelState.IsValid)
                {
                    string filePath = "~/File/currentUserId.txt";
                    string currentUserId = read(filePath);
                    tblNotification.SenderId = Int32.Parse(currentUserId);
                    tblNotification.NotificationDate = DateTime.Now;
                    tblNotification.IsSeen = 0;
                    db.tblNotifications.Add(tblNotification);
                    db.SaveChanges();

                }

                return RedirectToAction("GoToNotificationSystem", "System");
            }

            else
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

        public ActionResult tob(string m)
        {
            ViewBag.ReceiverId = new SelectList(db.tblUsers, "UserId", "UserName");
            ViewBag.SenderId = new SelectList(db.tblUsers, "UserId", "UserName");
            return View();
        }

        public ActionResult sendNotiIfRejected()
        {
            if (
                cookieSystemPositionReading().Value == 3 + "" || cookieSystemPositionReading().Value == 4 + "")
            {
                if (ModelState.IsValid)
                {
                    return View();
                    db.SaveChanges();

                }

                return RedirectToAction("GoToNotificationSystem", "System");
            }

            else
                return RedirectToAction("Login", "Users");


           
            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult sendNotiIfRejected([Bind(Include = "NotificationId,SenderId,ReceiverId,NotificationDate,NotificationBody,IsSeen")] tblNotification tblNotification)
        {

            if (
                cookieSystemPositionReading().Value == 3 + "" || cookieSystemPositionReading().Value == 4 + "")
            {
                string ReqSlipFilePath = "~/File/reqSlipId.txt";
            string currentReqSlipId = read(ReqSlipFilePath);
            //int reqSlipId = Int32.Parse(currentReqSlipId);
            int senderId = Int32.Parse(read("~/File/currentUserId.txt"));

            if (!string.IsNullOrEmpty(currentReqSlipId))
            {
                tblRequisitionSlip rqpSlip = db.tblRequisitionSlips.Find(Int32.Parse(currentReqSlipId));

                rqpSlip.NotificationId = tblNotification.NotificationId;

                tblNotification.NotificationDate = DateTime.Now;

                

                tblNotification.SenderId = senderId;
                tblNotification.ReceiverId = rqpSlip.ApplicantId;
                tblNotification.IsSeen = 0;
                db.tblNotifications.Add(tblNotification);
                db.SaveChanges();

                Write(ReqSlipFilePath, "");

                
            }

            tblUser sender = db.tblUsers.Find(senderId);

            if (sender.SystemPosition == 3)
            {
                return RedirectToAction("seeUncheckedRequisitionSlip", "RequisitionSlips");
            }

            else
            {
                return RedirectToAction("seeUncheckedRequisitionSlipDirector", "RequisitionSlips");
            }
            }

            else
                return RedirectToAction("Login", "Users");

            



            
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
