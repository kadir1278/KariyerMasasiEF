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
    public class UserSeminarController : Controller
    {
        private SystemContext db = new SystemContext();
        [Route("seminer"), HttpGet]
        public ActionResult Index() => View();
        [Route("seminer-ekle"), HttpGet]
        public ActionResult Add() => PartialView();
        [Route("seminer-ekle"), HttpPost]
        public JsonResult Add(UserSeminar model)
        {
            var loginUser = Session["loginUser"] as LoginViewModel;

            try
            {
                model.UserID = loginUser.ID;
                model.DeletionStatus = false;
                model.CreatedTime = DateTime.Now;
                model.UpdatedTime = DateTime.Now;
                db.UserSeminars.Add(model);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Bir sorun oluştu." + ex.Message);
            }
            return Json(new { res = true });
        }
        [Route("seminer-sil/{id}"), HttpGet]
        public ActionResult Delete(int id)
        {
            var data = db.UserSeminars.Find(id);
            data.DeletionStatus = true;
            db.SaveChanges();
            return Redirect("/profil");
        }
        [Route("seminer-guncelle/{id}")]
        public ActionResult Update(int id)
        {
            var data = db.UserSeminars.Find(id);
            return PartialView(data);
        }

    }
}