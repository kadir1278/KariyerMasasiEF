using KariyerEntity.Entity;
using KariyerEntity.Modal;
using KariyerWebUI.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KariyerWebUI.Controllers
{
    [Authorize(Roles = "ADMİN")]
    public class RoleController : Controller
    {
        private SystemContext db = new SystemContext();
        [Route("yetki"), HttpGet]
        public ActionResult Index() => View();
        [Route("yetki-getir/{searchText?}"), HttpGet]
        public JsonResult GetData(string searchText)
        {
            List<Role> data = new List<Role>();

            if (searchText == "" || searchText == null)
            {
                data = db.Roles.Where(x => !x.DeletionStatus).ToList();
            }
            else
            {
                data = db.Roles.Where(x => !x.DeletionStatus && x.Name == searchText).ToList();
            }
            var query = new
            {
                Result = from obj in data
                         select new
                         {
                             ID = obj.ID,
                             Name = obj.Name,
                         }
            };
            return Json(query, JsonRequestBehavior.AllowGet);
        }
        [Route("yetki-ekle"), HttpGet]
        public ActionResult Add() => PartialView();
        [Route("yetki-sil/{id}"), HttpGet]
        public ActionResult Delete(int id)
        {
            try
            {
                var data = db.Roles.Where(x => x.ID == id).FirstOrDefault();
                data.DeletionStatus = true;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View(ex);
            }
        }
        [Route("yetki-ekle"), HttpPost]
        public JsonResult AddRole(Role model)
        {
            string RoleName = StringHelper.StringReplacer(model.Name.ToUpper());
            var dataCount = db.Roles.Where(x => !x.DeletionStatus && x.Name == RoleName).Count();
            if (!(dataCount > 0))
            {
                model.DeletionStatus = false;
                model.CreatedTime = DateTime.Now;
                model.UpdatedTime = DateTime.Now;
                model.Name = RoleName;
                db.Roles.Add(model);
                db.SaveChanges();
            }
            else
            {
                throw new Exception(RoleName + " Sistemde bulunmaktadır.");
            }
            return Json(new { res = true });
        }
    }
}