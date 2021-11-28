using KariyerEntity.Entity;
using KariyerEntity.Modal;
using KariyerWebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace KariyerWebUI.Controllers
{
    [Authorize(Roles = "ADMIN")]
    public class DepartmentController : Controller
    {
        private SystemContext db = new SystemContext();
        [Route("departman"), HttpGet]
        public ActionResult Index() => View();
        [Route("departman-getir/{searchText?}"), HttpGet]
        public JsonResult GetData(string searchText)
        {
            List<Department> data = new List<Department>();

            if (searchText == "" || searchText == null)
            {
                data = db.Departments.Where(x => !x.DeletionStatus).ToList();
            }
            else
            {
                data = db.Departments.Where(x => !x.DeletionStatus && x.Name == searchText).ToList();
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
        [Route("departman-ekle"), HttpGet]
        public ActionResult Add() => PartialView();
        [Route("departman-duzenle/{id}")]
        public ActionResult Update(int id)
        {
            var data = db.Departments.Where(x => !x.DeletionStatus && x.ID == id).FirstOrDefault();
            return View(data);
        }
        [Route("departman-sil/{id}"), HttpGet]
        public ActionResult Delete(int id)
        {
            try
            {
                var data = db.Departments.Where(x => x.ID == id).FirstOrDefault();
                data.DeletionStatus = true;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View(ex);
            }
        }
        [Route("departman-ekle"), HttpPost]
        public JsonResult AddLang(Department departman)
        {
            var data = db.Departments.Where(x => !x.DeletionStatus && x.Name == departman.Name).ToList();
            if (data.Count() == 0)
            {
                departman.CreatedTime = DateTime.Now;
                departman.UpdatedTime = DateTime.Now;
                departman.DeletionStatus = false;
                departman.Name = departman.Name.ToUpper();
                db.Departments.Add(departman);
                db.SaveChanges();
            }
            else
            {
                throw new Exception(departman.Name.ToUpper() + " Sistemde bulunmaktadır.");
            }
            return Json(new { res = true });
        }
        [Route("departman-duzenle/{id}"), HttpPost]
        public ActionResult UpdateLang(Department departman, int id)
        {
            try
            {
                var data = db.Departments.Where(x => !x.DeletionStatus && x.ID == id).FirstOrDefault();
                data.Name = departman.Name.ToUpper();
                data.UpdatedTime = DateTime.Now;
                data.CreatedTime = departman.CreatedTime;
                data.DeletionStatus = departman.DeletionStatus;
                db.SaveChanges();
                return View(data);
            }
            catch (Exception ex)
            {
                return View(ex);
            }
        }
    }
}