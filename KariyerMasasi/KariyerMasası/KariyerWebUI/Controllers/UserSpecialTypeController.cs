using KariyerEntity.Entity;
using KariyerEntity.Modal;
using KariyerWebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace KariyerWebUI.Controllers
{
    [Authorize(Roles = "ADMIN")]
    public class UserSpecialTypeController : Controller
    {
        private SystemContext db = new SystemContext();
        [Route("ozel-durum-tip"), HttpGet]
        public ActionResult Index() => View();
        [Route("ozel-durum-tip-getir/{searchText?}"), HttpGet]
        public JsonResult GetData(string searchText)
        {
            List<UserSpecialType> data = new List<UserSpecialType>();

            if (searchText == "" || searchText == null)
            {
                data = db.UserSpecialTypes.Where(x => !x.DeletionStatus).ToList();
            }
            else
            {
                data = db.UserSpecialTypes.Where(x => !x.DeletionStatus && x.Name == searchText).ToList();
            }
            var query = new
            {
                Result = from obj in data
                         select new
                         {
                             ID = obj.ID,
                             Name = obj.Name,
                         }
            };
            return Json(query, JsonRequestBehavior.AllowGet);
        }
        [Route("ozel-durum-tip-ekle"), HttpGet]
        public ActionResult Add() => PartialView();
        [Route("ozel-durum-tip-duzenle/{id}")]
        public ActionResult Update(int id)
        {
            var data = db.UserSpecialTypes.Where(x => !x.DeletionStatus && x.ID == id).FirstOrDefault();
            return View(data);
        }
        [Route("ozel-durum-tip-sil/{id}"), HttpGet]
        public ActionResult Delete(int id)
        {
            try
            {
                var data = db.UserSpecialTypes.Where(x => x.ID == id).FirstOrDefault();
                data.DeletionStatus = true;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View(ex);
            }
        }
        [Route("ozel-durum-tip-ekle"), HttpPost]
        public JsonResult AddLang(UserSpecialType lang)
        {
            var data = db.UserSpecialTypes.Where(x => !x.DeletionStatus && x.Name == lang.Name).ToList();
            if (data.Count() == 0)
            {
                lang.CreatedTime = DateTime.Now;
                lang.UpdatedTime = DateTime.Now;
                lang.DeletionStatus = false;
                lang.Name = lang.Name.ToUpper();
                db.UserSpecialTypes.Add(lang);
                db.SaveChanges();
            }
            else
            {
                throw new Exception(lang.Name.ToUpper() + " Sistemde bulunmaktadır.");
            }
            return Json(new { res = true });
        }
        [Route("ozel-durum-tip-duzenle/{id}"), HttpPost]
        public ActionResult UpdateLang(UserSpecialType lang, int id)
        {
            try
            {
                var data = db.UserSpecialTypes.Where(x => !x.DeletionStatus && x.ID == id).FirstOrDefault();
                data.Name = lang.Name.ToUpper();
                data.UpdatedTime = DateTime.Now;
                data.CreatedTime = lang.CreatedTime;
                data.DeletionStatus = lang.DeletionStatus;
                db.SaveChanges();
                return View(data);
            }
            catch (Exception ex)
            {
                return View(ex);
            }
        }
    }
}