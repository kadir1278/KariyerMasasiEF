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
    public class UserRoleController : Controller
    {
        private SystemContext db = new SystemContext();
        [Route("kullanici-yetki"), HttpGet]
        public ActionResult Index() => View();
        [Route("kullanici-yetki-getir/{searchText?}"), HttpGet]
        public JsonResult GetData(string searchText)
        {
            List<UserRole> data = new List<UserRole>();

            if (searchText == "" || searchText == null)
            {
                data = db.UserRoles.Include(x => x.User).Include(x => x.Role).Where(x => !x.DeletionStatus).ToList();
            }
            else
            {
                data = db.UserRoles.Include(x => x.User).Include(x => x.Role).Where(x => !x.DeletionStatus && x.User.Name == searchText).ToList();
            }
            var query = new
            {
                Result = from obj in data
                         select new
                         {
                             ID = obj.ID,
                             User = obj.User.Name,
                             Role = obj.Role.Name
                         }
            };
            return Json(query, JsonRequestBehavior.AllowGet);
        }
        [Route("kullanici-yetki-ekle"), HttpGet]
        public ActionResult Add()
        {
            UserRoleViewModel model = new UserRoleViewModel();
            model.Roles = db.Roles.Where(x => !x.DeletionStatus).ToList();
            model.Users = db.Users.Where(x => !x.DeletionStatus).ToList();
            return PartialView(model);
        }
        [Route("kullanici-yetki-sil/{id}"), HttpGet]
        public ActionResult Delete(int id)
        {
            try
            {
                var data = db.UserRoles.Where(x => x.ID == id).FirstOrDefault();
                data.DeletionStatus = true;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View(ex);
            }
        }
        [Route("kullanici-yetki-ekle"), HttpPost]
        public ActionResult AddUserRole(UserRole user)
        {
            user.CreatedTime = DateTime.Now;
            user.UpdatedTime = DateTime.Now;
            user.DeletionStatus = false;
            db.UserRoles.Add(user);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}