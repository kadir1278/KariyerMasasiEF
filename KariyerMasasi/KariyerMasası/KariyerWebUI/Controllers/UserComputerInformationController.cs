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
    public class UserComputerInformationController : Controller
    {
        private SystemContext db = new SystemContext();
        [Route("bilgisayar-bilgisi"), HttpGet]
        public ActionResult Index() => View();
        [Route("bilgisayar-bilgisi-ekle"), HttpGet]
        public ActionResult Add() => PartialView();
        [Route("bilgisayar-bilgisi-ekle"), HttpPost]
        public JsonResult Add(UserComputerInformation model)
        {
            var loginUser = Session["loginUser"] as LoginViewModel;

            try
            {
                model.UserID = loginUser.ID;
                model.DeletionStatus = false;
                model.CreatedTime = DateTime.Now;
                model.UpdatedTime = DateTime.Now;
                db.UserComputerInformations.Add(model);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Bir sorun oluştu." + ex.Message);
            }
            return Json(new { res = true });
        }
        [Route("bilgisayar-bilgisi-sil/{id}"), HttpGet]
        public ActionResult Delete(int id)
        {
            var data = db.UserComputerInformations.Find(id);
            data.DeletionStatus = true;
            db.SaveChanges();
            return Redirect("/profil");
        }
        [Route("bilgisayar-bilgisi-guncelle/{id}"), HttpGet]
        public ActionResult Update(int id)
        {
            var data = db.UserComputerInformations.Find(id);
            return PartialView(data);
        }
        [Route("bilgisayar-bilgisi-guncelle/{id}"), HttpPost]
        public ActionResult UpdateComputerInformation(UserComputerInformation model)
        {
            if (ModelState.IsValid)
            {
                var data = db.UserComputerInformations.Where(x => !x.DeletionStatus && x.ID == model.ID).FirstOrDefault();
                data.Name = model.Name;
                data.Description = model.Description;
                data.UpdatedTime = DateTime.Now;
                db.SaveChanges();
            }
            return Redirect("/profil");
        }
    }
}