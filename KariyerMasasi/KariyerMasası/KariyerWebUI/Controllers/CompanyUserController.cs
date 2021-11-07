using KariyerEntity.BaseEntity;
using KariyerEntity.Entity;
using KariyerEntity.Modal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KariyerWebUI.Controllers
{
    public class CompanyUserController : Controller
    {
        private SystemContext db = new SystemContext();
        [Route("sirket-personel"),HttpGet]
        public ActionResult Index() => View();
        #region Partial
        [Route("PartialAddCompanyUser"),HttpGet]
        public ActionResult PartialAddCompanyUser()
        {
            ViewBag.CompanyID = new SelectList(db.Companies.Where(x => !x.DeletionStatus), "ID", "Name");
            return PartialView();
        }
        [Route("PartialUpdateCompanyUser/{id}"), HttpGet]
        public ActionResult PartialUpdateCompanyUser(int id)
        {
            ViewBag.CompanyID = new SelectList(db.Companies.Where(x => !x.DeletionStatus), "ID", "Name");
            var data = db.CompanyUsers.Where(x => x.ID == id).FirstOrDefault();
            return PartialView(data);
        }
        #endregion
        #region Methot
        [Route("sirket-personel-sil"),HttpGet]
        public ActionResult Delete(int ID)
        {
            var data = db.CompanyUsers.Find(ID);
            data.DeletionStatus = true;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [Route("sirket-personel-ekle"),HttpPost]
        public ActionResult Add(CompanyUser model)
        {
            model.DeletionStatus = false;
            model.CreatedTime = DateTime.Now;
            model.UpdatedTime = DateTime.Now;
            db.CompanyUsers.Add(model);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [Route("sirket-personel-guncelle"),HttpPost]
        public ActionResult Update(CompanyUser model)
        {
            var data = db.CompanyUsers.Find(model.ID);
            data.UpdatedTime = DateTime.Now;
            data.DeletionStatus = model.DeletionStatus;

            data.EMail = model.EMail;
            data.Name = model.Name;
            data.Phone = model.Phone;
            data.Password = model.Password;
            data.Title = model.Title;
            db.SaveChanges();
            return View();
        }
        #endregion
        #region Json
        [Route("GetCompanyUserData"),HttpGet]
        public JsonResult GetCompanyUserData(string searchText)
        {
            List<CompanyUser> data = new List<CompanyUser>();

            if (searchText == "" || searchText == null)
            {
                data = db.CompanyUsers.Where(x => !x.DeletionStatus).ToList();
            }
            else
            {
                data = db.CompanyUsers.Where(x => !x.DeletionStatus && x.Name == searchText).ToList();
            }
            var query = new
            {
                Result = from obj in data
                         select new
                         {
                             ID = obj.ID,
                             Name = obj.Name,
                             Surname = obj.Surname,
                             EMail = obj.EMail,
                             Password = obj.Password,
                             Phone = obj.Phone,
                             Title = obj.Title
                         }
            };
            return Json(query, JsonRequestBehavior.AllowGet);
        }

        #endregion
      
    }
}