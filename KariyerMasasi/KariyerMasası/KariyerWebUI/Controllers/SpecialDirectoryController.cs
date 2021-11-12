using KariyerEntity.Entity;
using KariyerEntity.Modal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace KariyerWebUI.Controllers
{
    public class SpecialDirectoryController : Controller
    {
        private SystemContext db = new SystemContext();
        [Route("ozel-durum"),HttpGet]
        public ActionResult Index() => View();
        [Route("GetSpecialDirectoryData"), HttpGet]
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
                             UserID = obj.User.Name,
                             UserSpecialTypeID = obj.UserSpecialType.Name,
                         }
            };
            return Json(query, JsonRequestBehavior.AllowGet);
        }
    }
}