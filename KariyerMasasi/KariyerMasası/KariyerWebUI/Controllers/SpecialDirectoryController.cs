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
    [Authorize(Roles = "ADMİN")]
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
        public JsonResult AddSpecial(SpecialDirectory model)
        {
            var data = db.SpecialDirectories.Where(x => !x.DeletionStatus && x.UserID == model.UserID&&x.UserSpecialTypeID==model.UserSpecialTypeID).ToList();

            if (data.Count() == 0)
            {
                model.CreatedTime = DateTime.Now;
                model.UpdatedTime = DateTime.Now;
                model.DeletionStatus = false;
                db.SpecialDirectories.Add(model);
                db.SaveChanges();
            }
            else
            {
                var error = db.SpecialDirectories.Include(x => x.UserSpecialType).Include(x => x.User).Where(x => !x.DeletionStatus && x.UserID == model.UserID && x.UserSpecialTypeID == model.UserSpecialTypeID).FirstOrDefault();
                throw new Exception(error.UserSpecialType.Name.ToUpper() + " Özel Durumuna " + error.User.Name.ToUpper() + " Kullanıcısı Zaten Sahip");
            }
            return Json(new { res = true });
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