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
    public class UsersController : Controller
    {
        private db_IMSEntities db = new db_IMSEntities();

        // GET: Users
        public ActionResult Index()
        {
            return View(db.tblUsers.ToList());
        }

        // GET: Users/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblUser tblUser = db.tblUsers.Find(id);
            if (tblUser == null)
            {
                return HttpNotFound();
            }
            return View(tblUser);
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserId,UserName,FirstName,LastName,Password,JobPosition,SystemPosition,Sex,PhotoPath,Email,ContactNo,SignaturePath")] tblUser tblUser)
        {
            if (ModelState.IsValid)
            {
                db.tblUsers.Add(tblUser);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tblUser);
        }

        // GET: Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblUser tblUser = db.tblUsers.Find(id);
            if (tblUser == null)
            {
                return HttpNotFound();
            }
            return View(tblUser);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserId,UserName,FirstName,LastName,Password,JobPosition,SystemPosition,Sex,PhotoPath,Email,ContactNo,SignaturePath")] tblUser tblUser)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblUser).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tblUser);
        }

        // GET: Users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblUser tblUser = db.tblUsers.Find(id);
            if (tblUser == null)
            {
                return HttpNotFound();
            }
            return View(tblUser);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblUser tblUser = db.tblUsers.Find(id);
            db.tblUsers.Remove(tblUser);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Users/Create
        public ActionResult SignUp()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateAntiForgeryToken]
        public ActionResult SignUp([Bind(Include = "UserId,UserName,FirstName,LastName,Password,JobPosition,SystemPosition,Sex,PhotoPath,Email,ContactNo,SignaturePath")] tblUser tblUser)
        {



            if (ModelState.IsValid)
            {
                tblUser.SystemPosition = 2;//all user will register as a normal user

                HttpPostedFileBase image = Request.Files[0];
                HttpPostedFileBase signature = Request.Files[1];

                string userPic = System.IO.Path.GetFileName(image.FileName);

                if (userPic != "")
                {
                    string ImagePathInServer = System.IO.Path.Combine(
                                           Server.MapPath("~/Images/UserImage"),
                                           tblUser.UserId + tblUser.UserName + userPic);

                    image.SaveAs(ImagePathInServer);
                    tblUser.PhotoPath = "~/Images/UserImage" + "/" +
                        tblUser.UserId + tblUser.UserName + userPic;
                    //this is done to identify all pic uniquely,cause different 
                    //user may upload same named pic
                }
                string signaturePic = System.IO.Path.GetFileName(signature.FileName);
                if (signaturePic != "")
                {

                    string signaturePathInServer = System.IO.Path.Combine
                        (Server.MapPath("~/Images/UserSignature"),
                        tblUser.UserId + tblUser.UserName + signaturePic);

                    signature.SaveAs(signaturePathInServer);
                    tblUser.SignaturePath = "~/Images/UserSignature" + "/"
                        + tblUser.UserId + tblUser.UserName + signaturePic;
                    //this is done to identify all pic uniquely,cause different 
                    //user may upload same named pic
                }
                db.tblUsers.Add(tblUser);
                db.SaveChanges();
            }

            return RedirectToAction("Login");
        }

        public ActionResult Login()
        {
            Response.Cookies["username"].Expires = DateTime.Now;
            return View();
        }

        // POST: Login/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(tblUser u)
        {
            if (ModelState.IsValid)
            {
                using (db_IMSEntities dc = new db_IMSEntities())
                {
                    try
                    {
                        var v = dc.tblUsers.Where(a => a.UserName.Equals(u.UserName) && a.Password.Equals(u.Password)).FirstOrDefault();

                        if (v != null)
                        {
                           
                            int systemPosition = (int)v.SystemPosition;
                            Response.Cookies["systemPosition"].Value = systemPosition+"";
                            Response.Cookies["systemPosition"].Expires = DateTime.MinValue;



                            Response.Cookies["userId"].Value = v.UserId + "";
                            Response.Cookies["userId"].Expires = DateTime.MinValue;


                            string currentUserId = "" + v.UserId;
                            string filePath = "~/File/currentUserId.txt";
                            WriteUserId(filePath, currentUserId);


                            return RedirectToAction("defineRole", "System");
                        }
                    }
                    catch (Exception e)
                    {
                        return RedirectToAction("Login");
                    }
                }


            }
            return RedirectToAction("Login");


        }

        public ActionResult Logout()
        {
            Response.Cookies["username"].Expires = DateTime.Now;
            return RedirectToAction("Login");
        }


        private void WriteUserId(string filePath, string fileContent)
        {
            if (System.IO.File.Exists(filePath))
                System.IO.File.Delete(filePath);

            filePath = Server.MapPath(filePath);
            System.IO.File.WriteAllText(filePath, fileContent);
        }

        public string getUserName(int? id)
        {
            tblUser tblUser;
            if (id != null)
            {
                tblUser = db.tblUsers.Find(id);
                return tblUser.FirstName + " " + tblUser.FirstName;
            }

            else
                return "";



        }

        public int getSystemPosition(int? id)
        {

            tblUser tblUser = db.tblUsers.Find(id);
            if (tblUser != null)
            {
                return (int)tblUser.SystemPosition;
            }
            return -1;
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
