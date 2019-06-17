using BrunoSilvaLibrary.Models.Extended;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BrunoSilvaLibrary.Controllers
{
    public class MediaController : Controller
    {
        [HttpGet]
        public ActionResult Media(string command, string MID, string titleS, string genreS, string directorS, string language, string publishYear, string budget)
        {
            int iMID;
            if (Session["UserID"] == null) { return RedirectToAction("../User/Login"); }
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
            else if (command == "RESERVE")
            {
                Int32.TryParse(MID, out iMID);
                if (mediaDB.MediaAvailable(iMID, DateTime.Now))
                {
                    mediaDB.MediaReserve(iMID, Int32.Parse(Session["UserID"].ToString()), DateTime.Now.AddDays(7.0));
                    TempData["Message"] = "Media Borrowed your reserve will start "+ DateTime.Now.AddDays(7.0) + " until "+ DateTime.Now.AddDays(14.0);
                }
                else
                {
                    mediaDB.MediaReserve(iMID, Int32.Parse(Session["UserID"].ToString()), DateTime.Now);
                    TempData["Message"] = "You reserved this media until " + DateTime.Now.AddDays(7.0);

                }

            }
            else if (command == "BORROW")
            {
                Int32.TryParse(MID, out iMID);
                if (mediaDB.MediaAvailable(iMID, DateTime.Now))
                {
                    TempData["Message"] = "Media Borrowed.";
                }
                else
                {
                    mediaDB.MediaBorrow(iMID, Int32.Parse(Session["UserID"].ToString()), DateTime.Now, DateTime.Now.AddDays(7.0), 0);
                    TempData["Message"] = "You borrowed this media until "+ DateTime.Now.AddDays(7.0).Date ;

                }
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
        [HttpGet]
        public ActionResult BorrowMedia(string command, string MID, string titleS, string genreS, string directorS, string language, string publishYear, string budget)
        {

            return View();
        }

        }
    }