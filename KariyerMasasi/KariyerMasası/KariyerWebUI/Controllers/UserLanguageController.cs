using KariyerEntity.Entity;
using KariyerEntity.Modal;
using KariyerWebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KariyerWebUI.Controllers
{
    public class UserLanguageController : Controller
    {
        private SystemContext db = new SystemContext();
        [Route("kullanici-dil"), HttpGet]
        public ActionResult Index() => View();
        [Route("kullanici-dil-ekle"), HttpGet]
        public ActionResult Add()
        {
            ViewBag.LanguageID = db.Languages.Where(x => !x.DeletionStatus).ToList();
            return PartialView();
        }
        [Route("kullanici-dil-ekle"), HttpPost]
        public JsonResult Add(UserLanguage model)
        {
            var loginUser = Session["loginUser"] as LoginViewModel;

            try
            {
                model.UserID = loginUser.ID;
                model.DeletionStatus = false;
                model.CreatedTime = DateTime.Now;
                model.UpdatedTime = DateTime.Now;
                db.UserLanguages.Add(model);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Bir sorun oluştu." + ex.Message);
            }
            return Json(new { res = true });
        }
        [Route("kullanici-dil-sil/{id}"), HttpGet]
        public ActionResult Delete(int id)
        {
            var data = db.UserLanguages.Find(id);
            data.DeletionStatus = true;
            db.SaveChanges();
            return Redirect("/profil");
        }
        [Route("kullanici-dil-guncelle/{id}"), HttpGet]
        public ActionResult Update(int id)
        {
            ViewBag.LanguageID = db.Languages.Where(x => !x.DeletionStatus).ToList();
            var data = db.UserLanguages.Find(id);
            return PartialView(data);
        }
        [Route("kullanici-dil-guncelle/{id}"), HttpPost]
        public ActionResult UpdateLanguage(UserLanguage model)
        {
            if (ModelState.IsValid)
            {
                var data = db.UserLanguages.Where(x => !x.DeletionStatus && x.ID == model.ID).FirstOrDefault();
                data.SpeakingLevel = model.SpeakingLevel;
                data.WritingLevel = model.WritingLevel;
                data.ListeningLevel = model.ListeningLevel;
                data.Language = model.Language;
                data.Description = model.Description;
                data.UpdatedTime = DateTime.Now;
                db.SaveChanges();
            }
            return Redirect("/profil");
        }

    }
}