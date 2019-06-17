using BrunoSilvaLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BrunoSilvaLibrary.Controllers
{
    public class UserController : Controller
    {
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult Register(UserModel objUser)
        {
            if (ModelState.IsValid)
            {
                UserDataImp tLib = new UserDataImp();
                tLib.CreateUser(objUser.UserName, objUser.Password, objUser.UserEmail);
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(UserModel objUser)
        {
            if (ModelState.IsValid)
            {
                UserDataImp tLib = new UserDataImp();
                UserModel dbUser = tLib.ValidPassword(objUser.UserName, objUser.Password);
                //tLib.UpdateUser("test","123456","teste@tt",1);
                //tLib.DeleteUser("test");
                if (dbUser != null)
                {
                    Session["UserID"] = dbUser.UID;
                    Session["UserName"] = objUser.UserName;
                    Session["Password"] = "";
                    Session["Email"] = dbUser.UserEmail;
                    Session["UserLevel"] = dbUser.UserLevel;
                    return RedirectToAction("./UserDashBoard");
                }
            }
            return View(objUser);
        }
        public ActionResult UserDashBoard()
        {
            if (Session["UserID"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

    }
}