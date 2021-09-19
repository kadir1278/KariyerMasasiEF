using KariyerEntity.BaseEntity;
using KariyerEntity.Entity;
using KariyerEntity.Modal;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace KariyerWebUI.Controllers
{
    public class UserController : Controller
    {
        private SystemContext db = new SystemContext();
        public ActionResult Index() => View();
        #region Partial
        public ActionResult PartialAddUser()
        {
            return PartialView();
        }
        public ActionResult PartialUpdateUser(int id)
        {
            var data = db.Users.Where(x => x.ID == id).FirstOrDefault();
            return PartialView(data);
        }
        #endregion
        #region Methot
        public ActionResult Delete(int ID)
        {
            var data = db.Users.Find(ID);
            data.DeletionStatus = true;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Add(User model, string Photo)
        {


            string fileUrl = Path.Combine(Server.MapPath("File/User/" + Guid.NewGuid().ToString() + ".png"));
            using (System.IO.MemoryStream stream = new System.IO.MemoryStream(Convert.FromBase64String(Photo.Substring(Photo.IndexOf("base64,") + 7, Photo.Length - (Photo.IndexOf("base64,") + 7)))))
            using (FileStream fileStream = new FileStream(fileUrl, FileMode.Create, FileAccess.ReadWrite))
            {
                stream.WriteTo(fileStream);
            }
            model.Photo = fileUrl;
            model.DeletionStatus = false;
            model.CreatedTime = DateTime.Now;
            model.UpdatedTime = DateTime.Now;
            db.Users.Add(model);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult Update(User model, string Photo)
        {
            var data = db.Users.Find(model.ID);
            if (Photo.Contains("base64,"))
            {
                string fileUrl = Path.Combine(Server.MapPath("~/File/User/" + Guid.NewGuid().ToString() + ".png"));
                using (System.IO.MemoryStream stream = new System.IO.MemoryStream(Convert.FromBase64String(Photo.Substring(Photo.IndexOf("base64,") + 7, Photo.Length - (Photo.IndexOf("base64,") + 7)))))
                using (FileStream fileStream = new FileStream(fileUrl, FileMode.Create, FileAccess.ReadWrite))
                {
                    stream.WriteTo(fileStream);
                }
                model.Photo = fileUrl;
            }

            data.UpdatedTime = DateTime.Now;
            data.DeletionStatus = model.DeletionStatus;

            data.EMail = model.EMail;
            data.Password = model.Password;
            data.Name = model.Name;
            data.Surname = model.Surname;
            data.Phone = model.Phone;
            data.Country = model.Country;
            data.City = model.City;
            data.Town = model.Town;
            data.Address = model.Address;
            data.Type = model.Type;
            data.GeneralIsActive = model.GeneralIsActive;
            data.Description = model.Description;
            data.Title = model.Title;
            data.BusinessAreaID = model.BusinessAreaID;
            data.MilitaryStatus = model.MilitaryStatus;
            data.Gender = model.Gender;
            data.MarriageStatus = model.MarriageStatus;
            data.ProgramState = model.ProgramState;
            data.Hobby = model.Hobby;
            db.SaveChanges();
            return View();
        }
        #endregion
        #region Json
        public JsonResult GetUserData(string searchText)
        {
            List<User> data = new List<User>();

            if (searchText == "" || searchText == null)
            {
                data = db.Users.Where(x => !x.DeletionStatus).ToList();
            }
            else
            {
                data = db.Users.Where(x => !x.DeletionStatus && x.Name == searchText).ToList();
            }
            var query = new
            {
                Result = from obj in data
                         select new
                         {
                             ID = obj.ID,
                             Name = obj.Name,
                             Surname = obj.Surname,
                             EMail=obj.EMail,
                             Phone=obj.Phone,
                             Photo=obj.Photo,
                         }
            };
            return Json(query, JsonRequestBehavior.AllowGet);
        }

        #endregion
    }
}