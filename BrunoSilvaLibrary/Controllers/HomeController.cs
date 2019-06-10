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
                tLib.CreateUser(objUser.UserName,objUser.Password, objUser.UserEmail);
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
                //tLib.CreateUser("test", "123321", "teste@teste");
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
        public ActionResult Media(string titleS, string genreS, string directorS)
        {
            MediaDataImp mediaDB = new MediaDataImp();
            List<Media> mediaResult = mediaDB.GetMedias(titleS, genreS, directorS);

            UserDataSetTableAdapters.TableMediaTableAdapter sda = new UserDataSetTableAdapters.TableMediaTableAdapter();
            UserDataSet.TableMediaDataTable ds = new UserDataSet.TableMediaDataTable();
            if(string.IsNullOrEmpty(titleS) && string.IsNullOrEmpty(genreS) && string.IsNullOrEmpty(directorS)) sda.Fill(ds);
            else if (titleS == "" && directorS == "" && !string.IsNullOrEmpty(genreS)) sda.FillByGenre(ds, genreS);
            else if (genreS == "" && directorS == "" && !string.IsNullOrEmpty(titleS)) sda.FillByTitle(ds, titleS);
            else if (genreS == "" && titleS == "" && !string.IsNullOrEmpty(directorS)) sda.FillByDirector(ds, directorS);
            else sda.FillByGenreAndTitle(ds, titleS, genreS);
            Media me = new Media();
            return View(me.ConvertToMediaList(ds));
        }
    }
}