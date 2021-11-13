using KariyerEntity.Entity;
using KariyerEntity.Modal;
using KariyerWebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace KariyerWebUI.Controllers
{
    public class BusinessAreaController : Controller
    {
        private SystemContext db = new SystemContext();
        [Route("calisma-alani"), HttpGet]
        public ActionResult Index() => View();
        [Route("calisma-alani-getir/{searchText?}"), HttpGet]
        public JsonResult GetData(string searchText)
        {
            List<BusinessArea> data = new List<BusinessArea>();

            if (searchText == "" || searchText == null)
            {
                data = db.BusinessAreas.Where(x => !x.DeletionStatus).ToList();
            }
            else
            {
                data = db.BusinessAreas.Where(x => !x.DeletionStatus && x.Name == searchText).ToList();
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
        [Route("calisma-alani-ekle"), HttpGet]
        public ActionResult Add() => PartialView();
        [Route("calisma-alani-duzenle/{id}")]
        public ActionResult Update(int id)
        {
            var data = db.BusinessAreas.Where(x => !x.DeletionStatus && x.ID == id).FirstOrDefault();
            return View(data);
        }
        [Route("calisma-alani-sil/{id}"), HttpGet]
        public ActionResult Delete(int id)
        {
            try
            {
                var data = db.BusinessAreas.Where(x => x.ID == id).FirstOrDefault();
                data.DeletionStatus = true;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View(ex);
            }
        }
        [Route("calisma-alani-ekle"), HttpPost]
        public ActionResult AddBusinessArea(BusinessArea bsnarea)
        {
            try
            {
                var data = db.BusinessAreas.Where(x => !x.DeletionStatus && x.Name == bsnarea.Name).ToList();
                if (data.Count() == 0)
                {
                    bsnarea.CreatedTime = DateTime.Now;
                    bsnarea.UpdatedTime = DateTime.Now;
                    bsnarea.DeletionStatus = false;
                    bsnarea.Name = bsnarea.Name.ToUpper();
                    db.BusinessAreas.Add(bsnarea);
                    db.SaveChanges();
                }
                return RedirectToAction("Index");

            }
            catch (Exception ex)
            {
                
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }
        [Route("calisma-alani-duzenle/{id}"), HttpPost]
        public ActionResult UpdateBusinessArea(BusinessArea bsnarea, int id)
        {
            try
            {
                var data = db.BusinessAreas.Where(x => !x.DeletionStatus && x.ID == id).FirstOrDefault();
                data.Name = bsnarea.Name.ToUpper();
                data.UpdatedTime = DateTime.Now;
                data.CreatedTime = bsnarea.CreatedTime;
                data.DeletionStatus = bsnarea.DeletionStatus;
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