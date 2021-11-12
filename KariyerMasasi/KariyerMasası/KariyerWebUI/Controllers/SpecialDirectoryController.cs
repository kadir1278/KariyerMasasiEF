using KariyerEntity.Entity;
using KariyerEntity.Modal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using KariyerWebUI.Models;

namespace KariyerWebUI.Controllers
{
    public class SpecialDirectoryController : Controller
    {
        private SystemContext db = new SystemContext();
        [Route("ozel-durum"),HttpGet]
        public ActionResult Index() => View();
        [Route("ozel-durum-getir"), HttpGet]
        public JsonResult GetSpecialDirectoryData()
        {
            List<SpecialDirectory> data = new List<SpecialDirectory>();
            data = db.SpecialDirectories.Where(x => !x.DeletionStatus).Include(x=>User).Include(x=>x.UserSpecialType).ToList();
            var query = new
            {
                Result = from obj in data
                         select new
                         {
                             ID = obj.ID,
                             User = obj.User.Name,
                             SpecialType= obj.UserSpecialType.Name,
                         }
            };
            return Json(query, JsonRequestBehavior.AllowGet);
        }
        [Route("ozel-durum-ekle"), HttpGet]
        public ActionResult Add(SpecialDirectoryViewModel model)
        {
            model.Users = db.Users.Where(x => !x.DeletionStatus).ToList();
            model.UserSpecialTypes = db.UserSpecialTypes.Where(x => !x.DeletionStatus).ToList();
            return PartialView(model);
        }
        [Route("ozel-durum-ekle"),HttpPost]
        public ActionResult AddSpecial(SpecialDirectory model)
        {
            model.CreatedTime = DateTime.Now;
            model.UpdatedTime = DateTime.Now;
            model.DeletionStatus = false;
            db.SpecialDirectories.Add(model);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [Route("ozel-durum-sil/{id}"),HttpGet]
        public ActionResult Delete(int id)
        {
            var data = db.SpecialDirectories.Find(id);
            data.DeletionStatus = true;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}