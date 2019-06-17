using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BrunoSilvaLibrary.Models;
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
        
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("./Index");
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