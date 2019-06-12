using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BrunoSilvaLibrary.Models;
using BrunoSilvaLibrary.UserDataSetTableAdapters;
using System.Web.Helpers;
using BrunoSilvaLibrary.Models.Extended;

namespace BrunoSilvaLibrary.Controllers
{
    [Route("Home/SearchGenre")]
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }
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
                    return RedirectToAction("UserDashBoard");
                }
            }
            return View(objUser);
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login");
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
        [HttpGet]
        public ActionResult Media(string command, string MID, string titleS, string genreS, string directorS, string language, string publishYear, string budget)
        {
            int iMID;
            if (Session["UserID"] == null) { return RedirectToAction("Login"); }
                MediaDataImp mediaDB = new MediaDataImp();
            if (command == "UPDATE" && MID != null)
            {
                Int32.TryParse(MID, out iMID);
                mediaDB.UpdateMedia(iMID, titleS, genreS, directorS, language, publishYear, budget);
            }
            else if (command == "DELETE" && MID != null)
            {
                Int32.TryParse(MID, out iMID);
                mediaDB.DeleteMedia(iMID);
            }
            else if (command == "SEARCH")
            {
                if (titleS == "") { titleS = null; }
                if (genreS == "") { genreS = null; }
                if (directorS == "") { directorS = null; }
                ViewBag.mediaList = this.MediasFromDB(titleS, genreS, directorS);
                return View();
            }
            else if (command == "CREATE")
            {
                mediaDB.CreateMedia(titleS, genreS, directorS, language, publishYear, budget);
            }
            ViewBag.mediaList = this.MediasFromDB(null, null, null);
            return View();
        }
        private List<Media> MediasFromDB(string titleS, string genreS, string directorS)
        {
            MediaDataImp mediaDB = new MediaDataImp();
            List<Media> mediaResult = mediaDB.GetMedias(titleS, genreS, directorS);
            return mediaResult;
        }

        //------ USER LIST ------
        public ActionResult UserList(string command, string UID)
        {
            if (command == "UPDATE")
            {
            }
            else if (command == "DELETE")
            {
                UserDataImp tLib = new UserDataImp();
                tLib.DeleteUser(UID);

            }
            ViewBag.userList = this.UsersFromDB();
            return View();
        }
        private List<UserModel> UsersFromDB()
        {
            UserDataImp userDB = new UserDataImp();
            List<UserModel> userResult = userDB.GetUserList();
            return userResult;
        }



    }
}