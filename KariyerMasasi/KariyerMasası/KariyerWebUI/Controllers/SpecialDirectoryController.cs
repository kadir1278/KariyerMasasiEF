using KariyerEntity.Entity;
using KariyerEntity.Modal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using KariyerWebUI.Models;

namespace KariyerWebUI.Controllers
{
    [Authorize(Roles = "ADMIN")]
    public class SpecialDirectoryController : Controller
    {
        private SystemContext db = new SystemContext();
        [Route("kullanici-ozel-durum"), HttpGet]
        public ActionResult Index() => View();
        [Route("kullanici-ozel-durum-ekle"), HttpGet]
        public ActionResult Add()
        {
            var data = db.UserSpecialTypes.Where(x => !x.DeletionStatus).ToList();
            ViewBag.UserSpecialTypeID = data;
            return PartialView();
        }
        [Route("kullanici-ozel-durum-ekle"), HttpPost]
        public JsonResult Add(SpecialDirectory model)
        {
            var loginUser = Session["loginUser"] as LoginViewModel;

            try
            {
                model.UserID = loginUser.ID;
                model.DeletionStatus = false;
                model.CreatedTime = DateTime.Now;
                model.UpdatedTime = DateTime.Now;
                db.SpecialDirectories.Add(model);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Bir sorun oluştu." + ex.Message);
            }
            return Json(new { res = true });
        }
        [Route("kullanici-ozel-durum-sil/{id}"), HttpGet]
        public ActionResult Delete(int id)
        {
            var data = db.SpecialDirectories.Find(id);
            data.DeletionStatus = true;
            db.SaveChanges();
            return Redirect("/profil");
        }
        [Route("kullanici-ozel-durum-guncelle/{id}"), HttpGet]
        public ActionResult Update(int id)
        {
            var data = db.SpecialDirectories.Find(id);
            ViewBag.UserSpecialTypeID = db.UserSpecialTypes.Where(x => !x.DeletionStatus).ToList();
            return PartialView(data);
        }
        [Route("kullanici-ozel-durum-guncelle/{id}"), HttpPost]
        public ActionResult UpdateSpecial(SpecialDirectory model)
        {
            if (ModelState.IsValid)
            {
                var data = db.SpecialDirectories.Where(x => !x.DeletionStatus && x.ID == model.ID).FirstOrDefault();
                data.UserSpecialTypeID = model.UserSpecialTypeID;
                data.UpdatedTime = DateTime.Now;
                db.SaveChanges();
            }
            return Redirect("/profil");
        }

    }
}