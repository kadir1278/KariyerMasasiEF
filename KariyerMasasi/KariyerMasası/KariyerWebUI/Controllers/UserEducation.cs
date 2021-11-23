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
    public class UserEducationController : Controller
    {
        private SystemContext db = new SystemContext();
        [Route("egitim"), HttpGet]
        public ActionResult Index() => View();
        [Route("egitim-ekle"), HttpGet]
        public ActionResult Add() => PartialView();
        [Route("egitim-ekle"), HttpPost]
        public JsonResult Add(UserEducation model)
        {
            var loginUser = Session["loginUser"] as LoginViewModel;

            try
            {
                model.UserID = loginUser.ID;
                model.DeletionStatus = false;
                model.CreatedTime = DateTime.Now;
                model.UpdatedTime = DateTime.Now;
                db.UserEducations.Add(model);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Bir sorun oluştu." + ex.Message);
            }
            return Json(new { res = true });
        }
        [Route("egitim-sil/{id}"), HttpGet]
        public ActionResult Delete(int id)
        {
            var data = db.UserEducations.Find(id);
            data.DeletionStatus = true;
            db.SaveChanges();
            return Redirect("/profil");
        }
        [Route("egitim-guncelle/{id}"), HttpGet]
        public ActionResult Update(int id)
        {
            var data = db.UserEducations.Find(id);
            return PartialView(data);
        }
        [Route("egitim-guncelle/{id}"), HttpPost]
        public ActionResult UpdateEducation(UserEducation model)
        {
            if (ModelState.IsValid)
            {
                var data = db.UserEducations.Where(x => !x.DeletionStatus && x.ID == model.ID).FirstOrDefault();
                data.SchoolName = model.SchoolName;
                data.GraduationStatus = model.GraduationStatus;
                data.GraduationYear = model.GraduationYear;
                data.GraduationGrade = model.GraduationGrade;
                data.Department = model.Department;
                data.Description = model.Description;
                data.UpdatedTime = DateTime.Now;
                db.SaveChanges();
            }
            return Redirect("/profil");
        }

    }
}