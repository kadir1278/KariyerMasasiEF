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
    public class UserBusinessInformationController : Controller
    {
        private SystemContext db = new SystemContext();
        [Route("kullanici-is"), HttpGet]
        public ActionResult Index() => View();
        [Route("kullanici-is-ekle"), HttpGet]
        public ActionResult Add() => PartialView();
        [Route("kullanici-is-ekle"), HttpPost]
        public JsonResult Add(UserBusinessInformation model)
        {
            var loginUser = Session["loginUser"] as LoginViewModel;

            try
            {
                model.UserID = loginUser.ID;
                model.DeletionStatus = false;
                model.CreatedTime = DateTime.Now;
                model.UpdatedTime = DateTime.Now;
                db.UserBusinessInformations.Add(model);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Bir sorun oluştu." + ex.Message);
            }
            return Json(new { res = true });
        }
        [Route("kullanici-is-sil/{id}"), HttpGet]
        public ActionResult Delete(int id)
        {
            var data = db.UserBusinessInformations.Find(id);
            data.DeletionStatus = true;
            db.SaveChanges();
            return Redirect("/profil");
        }
        [Route("kullanici-is-guncelle/{id}"), HttpGet]
        public ActionResult Update(int id)
        {
            var data = db.UserBusinessInformations.Find(id);
            return PartialView(data);
        }
        [Route("kullanici-is-guncelle/{id}"), HttpPost]
        public ActionResult UpdateBusinessInformation(UserBusinessInformation model)
        {
            if (ModelState.IsValid)
            {
                var data = db.UserBusinessInformations.Where(x => !x.DeletionStatus && x.ID == model.ID).FirstOrDefault();
                data.CompanyName = model.CompanyName;
                data.StartingDate = model.StartingDate;
                data.FinishDate = model.FinishDate;
                data.Duty = model.Duty;
                data.Description = model.Description;
                data.UpdatedTime = DateTime.Now;
                db.SaveChanges();
            }
            return Redirect("/profil");
        }

    }
}