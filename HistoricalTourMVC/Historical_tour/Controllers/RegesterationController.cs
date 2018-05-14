using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Historical_tour.Models;
using HistoricTour.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;

namespace Historical_tour.Controllers
{
    public class RegesterationController : Controller
    {
        public static User myUser;

        private HistoricTourDataContext DB = new HistoricTourDataContext();

        
        [HttpGet]
        public ActionResult Create()
        {
            return View();

        }

        [HttpPost]
        public ActionResult Create(User currentuser)
        {
            try
            {
                currentuser.ID = DB.Users.Max(e => e.ID) + 1;
            }
            catch
            {
                currentuser.ID = 1;
            }

            try
            {
                DB.Users.InsertOnSubmit(currentuser);
                DB.SubmitChanges();
            }
            catch (Exception e)
            {
                //////////////////////////////////////////////
            }

            myUser = currentuser;
            return View("myprofile", currentuser);
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();

        }

        [HttpPost]
        public ActionResult Login(string username ,string password)
        {
            User user= DB.Users.Where(u => u.UserName == username && u.Password == password).SingleOrDefault();

            if (user == null)
            {
                return null;
            }
            else
            {
                //////////////////////////////////////
                myUser = user;
                return View("myprofile", user);
            }
        }

        public ActionResult myprofile()
        {
            return View(myUser);
        }

        public ActionResult Logout()
        {
            myUser = null;
            FormsAuthentication.SignOut();
            return RedirectToAction("Index","Home");
        }


        [HttpGet]
        public ActionResult contactUs()
        {
            return View("contactUs");
        }

        [HttpPost]
        public ActionResult contactUs(Contact contact)
        {
            try
            {
                contact.ID = DB.Contacts.Max(c=>c.ID)+ 1;
            }
            catch
            {
                contact.ID = 1;
            }
            contact.Date = DateTime.Now;
            DB.Contacts.InsertOnSubmit(contact);
            DB.SubmitChanges();
            return View("contactUs");
        }



    }

}
