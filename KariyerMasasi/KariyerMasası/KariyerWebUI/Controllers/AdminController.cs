using KariyerEntity.BaseEntity;
using KariyerEntity.Entity;
using KariyerEntity.Modal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace KariyerWebUI.Controllers
{
    public class AdminController : Controller
    {
        private SystemContext db = new SystemContext();

        [Route("yonetici"), HttpGet]
        public ActionResult Index() => View();
        [Route("PartialAddAdmin"), HttpGet]
        public ActionResult PartialAddAdmin()
        {
            return PartialView();
        }
        [Route("PartialUpdateAdmin/{id}"), HttpGet]
        public ActionResult PartialUpdateAdmin(int id)
        {
            var data = db.Admins.Where(x => x.ID == id).FirstOrDefault();
            return PartialView(data);
        }
        [Route("yonetici-sil/{id}"), HttpGet]
        public ActionResult Delete(int ID)
        {
            var data = db.Admins.Find(ID);
            data.DeletionStatus = true;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [Route("yonetici-ekle"), HttpPost]
        public ActionResult Add(Admin model)
        {
            model.DeletionStatus = false;
            model.CreatedTime = DateTime.Now;
            model.UpdatedTime = DateTime.Now;
            db.Admins.Add(model);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [Route("yonetici-guncelle"), HttpPost]
        public ActionResult Update(Admin model)
        {
            var data = db.Admins.Find(model.ID);
            data.UpdatedTime = DateTime.Now;
            data.DeletionStatus = model.DeletionStatus;
            data.EMail = model.EMail;
            data.Password = model.Password;
            data.Name = model.Name;
            data.Surname = model.Surname;
            data.Phone = model.Phone;
            db.SaveChanges();
            return View();
        }
        [Route("GetAdminData"), HttpGet]
        public JsonResult GetAdminData(string searchText)
        {
            List<Admin> data = new List<Admin>();

            if (searchText == "" || searchText == null)
            {
                data = db.Admins.Where(x => !x.DeletionStatus).ToList();
            }
            else
            {
                data = db.Admins.Where(x => !x.DeletionStatus && x.Name == searchText).ToList();
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
                             Phone = obj.Phone,
                             Password = obj.Password
                         }
            };
            return Json(query, JsonRequestBehavior.AllowGet);
        }
    }
}