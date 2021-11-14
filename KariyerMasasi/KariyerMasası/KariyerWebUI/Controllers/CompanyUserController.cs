using KariyerEntity.Entity;
using KariyerEntity.Modal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Data.Entity;

namespace KariyerWebUI.Controllers
{
    [Authorize(Roles = "ADMIN")]
    public class CompanyUserController : Controller
    {
        private SystemContext db = new SystemContext();
        [Route("sirket-personel"), HttpGet]
        public ActionResult Index() => View();
        [Route("PartialAddCompanyUser"), HttpGet]
        public ActionResult PartialAddCompanyUser()
        {
            ViewBag.CompanyID = db.Companies.Where(x => !x.DeletionStatus).ToList();
            return PartialView();
        }
        [Route("PartialUpdateCompanyUser/{id}"), HttpGet]
        public ActionResult PartialUpdateCompanyUser(int id)
        {
            ViewBag.CompanyID = db.Companies.Where(x => !x.DeletionStatus).ToList(); ;
            var data = db.CompanyUsers.Where(x => x.ID == id).FirstOrDefault();
            return PartialView(data);
        }
        [Route("sirket-personel-sil/{id}"), HttpGet]
        public ActionResult Delete(int ID)
        {
            var data = db.CompanyUsers.Find(ID);
            data.DeletionStatus = true;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [Route("sirket-personel-ekle"), HttpPost]
        public JsonResult Add(CompanyUser model)
        {
            var data = db.CompanyUsers.Where(x => !x.DeletionStatus && x.EMail == model.EMail).ToList();
            if (data.Count() == 0)
            {
                model.DeletionStatus = false;
                model.CreatedTime = DateTime.Now;
                model.UpdatedTime = DateTime.Now;
                db.CompanyUsers.Add(model);
                db.SaveChanges();
            }
            else
            {
                throw new Exception(model.EMail.ToUpper() + " Sistemde bulunmaktadır.");
            }
            return Json(new { res = true });
          
        }
        [Route("sirket-personel-guncelle"), HttpPost]
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
            return RedirectToAction("Index");
        }
        [Route("sirket-personel-getir/{searchText?}"), HttpGet]
        public JsonResult GetData(string searchText)
        {
            List<CompanyUser> data = new List<CompanyUser>();

            if (searchText == "" || searchText == null)
            {
                data = db.CompanyUsers.Where(x => !x.DeletionStatus).Include(x=>x.Company).ToList();
            }
            else
            {
                data = db.CompanyUsers.Where(x => !x.DeletionStatus && x.Name == searchText).Include(x => x.Company).ToList();
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
                             Title = obj.Title,
                             Company=obj.Company.Name
                         }
            };
            return Json(query, JsonRequestBehavior.AllowGet);
        }
    }
}