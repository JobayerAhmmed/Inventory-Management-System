using IMS_Final_Version2.Models.dbModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IMS_Final_Version2.Controllers
{
    public class SystemController : Controller
    {

        private db_IMSEntities db = new db_IMSEntities();




        // GET: System
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult defineRole()
        {
            if (cookieSystemPositionReading().Value == 1+"")
            {
                return RedirectToAction("GotoStoreInChargeInChargeAsStoreInCharge", "System");
            }

            else if (cookieSystemPositionReading().Value == 2 + "")
            {
                return RedirectToAction("GotoUser", "System");
            }

            else if (cookieSystemPositionReading().Value == 3 + "")
            {
                return RedirectToAction("GotoRecommender", "System");
            }

            else if (cookieSystemPositionReading().Value == 4 + "")
            {
                return RedirectToAction("GotoDirector", "System");
            }

            return RedirectToAction("Login", "Users");

        }


        public ActionResult GotoStoreInChargeInChargeAsStoreInCharge()
        {

            if (cookieSystemPositionReading().Value == 1 + "")
            {
                return View();
            }

            else
                return RedirectToAction("Login", "Users");
        }

        public ActionResult GotoUser()
        {
            if (cookieSystemPositionReading().Value == 2 + "" )
            {
                return View();
            }

            else
                return RedirectToAction("Login", "Users");
        }

        public ActionResult GoToRecommender()
        {
            if (cookieSystemPositionReading().Value == 3 + "")
            {
                return View();
            }

            else
                return RedirectToAction("Login", "Users");
        }

        public ActionResult GoToDirector()
        {
            if (cookieSystemPositionReading().Value == 4 + "")
            {
                return View();
            }

            else
                return RedirectToAction("Login", "Users");
        }

        public ActionResult GoToNotificationSystem()
        {

            if (cookieSystemPositionReading().Value == 1 + "" || cookieSystemPositionReading().Value == 2 + ""
                || cookieSystemPositionReading().Value == 3 + "" || cookieSystemPositionReading().Value == 4 + "")
            {
                return View();
            }

            else
                return RedirectToAction("Login", "Users");
        }

        public ActionResult SetRecommenderOrDirector()
        {
            if (cookieSystemPositionReading().Value == 1 + "")
            {
                return View();
            }

            else
                return RedirectToAction("Login", "Users");

        }

        public ActionResult SetRecommender()

        {

            if (cookieSystemPositionReading().Value == 1 + "")
            {
                 return View(db.tblUsers.ToList());
       
            }

            else
                return RedirectToAction("Login", "Users");
        }
           

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SetRecommender(string selectedRecommender)
        {
            if (cookieSystemPositionReading().Value == 1 + "")
            {
                var selectedId = Request["check"];

                //          return "Selected id is"+selectedId;

                if (!String.IsNullOrEmpty(selectedId))
                {
                    String[] selectedIdArray = selectedId.Split(',');

                    for (int i = 0; i < selectedIdArray.Length; i++)
                    {
                        if (String.IsNullOrEmpty(selectedIdArray[i]))
                            continue;
                        else
                        {
                            tblUser user = db.tblUsers.Find(Int32.Parse(selectedIdArray[i]));
                            user.SystemPosition = 3;

                        }
                    }

                    db.SaveChanges();

                }



                return RedirectToAction("Index", "Users");

            }


            else
                return RedirectToAction("Login", "Users");
           

        }


        public ActionResult SetDirector(String message)
        {
            if (cookieSystemPositionReading().Value == 1 + "")
            {
                if (message != null)
                    ViewBag.CheckedStatus = message;
                return View(db.tblUsers.ToList());
            }

            else
                return RedirectToAction("Login", "Users");

            

            
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SetDirector()

        {

            if (cookieSystemPositionReading().Value == 1 + "")
            {
                var oldDirectorNewRole = Request["oldDirectorCheck"];
                var selectedId = Request["check"];

                //          return "Selected id is"+selectedId;

                if (!String.IsNullOrEmpty(selectedId) && !String.IsNullOrEmpty(oldDirectorNewRole))
                {




                    String[] oldDirectorNewRoleArray = oldDirectorNewRole.Split(',');
                    String[] selectedIdArray = selectedId.Split(',');


                    if (oldDirectorNewRoleArray.Length > 1)
                    {
                        return RedirectToAction("SetDirector", "System",
                            new { message = "Only one role can be selected for old director" });
                    }

                    else if (selectedIdArray.Length > 1)
                    {
                        return RedirectToAction("SetDirector", "System",
                            new { message = "Only one person can be selected as New Director" });
                    }


                    else if (!String.IsNullOrEmpty(selectedIdArray[0]) && !String.IsNullOrEmpty(oldDirectorNewRoleArray[0]))
                    {

                        tblUser v = db.tblUsers.Where(a => a.SystemPosition == 4).FirstOrDefault();


                        v.SystemPosition = Int32.Parse(oldDirectorNewRoleArray[0]);

                        tblUser user = db.tblUsers.Find(Int32.Parse(selectedIdArray[0]));


                        user.SystemPosition = 4;

                    }


                    db.SaveChanges();

                }

                else
                {
                    String SMS = "";
                    if (String.IsNullOrEmpty(selectedId) && String.IsNullOrEmpty(oldDirectorNewRole))
                        SMS = "Both Old director and new Director information is empty";
                    else if (String.IsNullOrEmpty(oldDirectorNewRole))
                        SMS = "Old Director new role is not selected";
                    else if (String.IsNullOrEmpty(selectedId))
                        SMS = "New Director is not selected";

                    return RedirectToAction("SetDirector", "System", new { message = SMS });
                }



                return RedirectToAction("Index", "Users");
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

        private int GetUserId()
        {
            string filePath = "~/File/currentUserId.txt";
            
            UsersController userC = new UsersController();
            filePath = Server.MapPath(filePath);
            string s = System.IO.File.ReadAllText(filePath);
            return Int32.Parse(s);

        }
    }
}