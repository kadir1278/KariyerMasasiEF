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
    public class UserReferenceController : Controller
    {
        private SystemContext db = new SystemContext();
        [Route("referans"), HttpGet]
        public ActionResult Index() => View();
        [Route("referans-ekle"), HttpGet]
        public ActionResult Add() => PartialView();
        [Route("referans-ekle"), HttpPost]
        public JsonResult Add(UserReference model)
        {
            var loginUser = Session["loginUser"] as LoginViewModel;

            try
            {
                model.UserID = loginUser.ID;
                model.DeletionStatus = false;
                model.CreatedTime = DateTime.Now;
                model.UpdatedTime = DateTime.Now;
                db.UserReferences.Add(model);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Bir sorun oluştu." + ex.Message);
            }
            return Json(new { res = true });
        }
        [Route("referans-sil/{id}"), HttpGet]
        public ActionResult Delete(int id)
        {
            var data = db.UserReferences.Find(id);
            data.DeletionStatus = true;
            db.SaveChanges();
            return Redirect("/profil");
        }
        [Route("referans-guncelle/{id}"), HttpGet]
        public ActionResult Update(int id)
        {
            var data = db.UserReferences.Find(id);
            return PartialView(data);
        }
        [Route("referans-guncelle/{id}"), HttpPost]
        public ActionResult UpdateReference(UserReference model)
        {
            if (ModelState.IsValid)
            {
                var data = db.UserReferences.Where(x => !x.DeletionStatus && x.ID == model.ID).FirstOrDefault();
                data.NameSurname = model.NameSurname;
                data.Phone = model.Phone;
                data.EMail = model.EMail;
                data.Position = model.Position;
                data.CompanyName = model.CompanyName;
                data.UpdatedTime = DateTime.Now;
                db.SaveChanges();
            }
            return Redirect("/profil");
        }
    }
}