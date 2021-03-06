using KariyerEntity.Entity;
using KariyerEntity.Modal;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;

namespace KariyerWebUI.Controllers
{
    [Authorize(Roles = "ADMIN")]
    public class UserController : Controller
    {
        private SystemContext db = new SystemContext();
        [Route("kullanici"), HttpGet]
        public ActionResult Index() => View();
        [Route("PartialAddUser")]
        public ActionResult PartialAddUser()
        {
            ViewBag.BusinessAreaID = db.BusinessAreas.Where(x => !x.DeletionStatus).ToList();
            return PartialView();
        }
        [Route("PartialUpdateUser/{id}"), HttpGet]
        public ActionResult PartialUpdateUser(int id)
        {
            ViewBag.BusinessAreaID = db.BusinessAreas.Where(x => !x.DeletionStatus).ToList();
            var data = db.Users.Where(x => x.ID == id).FirstOrDefault();
            return PartialView(data);
        }
        [Route("kullanici-sil/{id}"), HttpGet]
        public ActionResult Delete(int id)
        {
            try
            {
                var data = db.Users.Find(id);
                data.DeletionStatus = true;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                throw;
            }
        }
        [Route("kullanici-ekle"), HttpPost]
        public JsonResult Add(User model, string Photo)
        {
            var data = db.Users.Where(x => !x.DeletionStatus && x.Name == model.EMail).ToList();
            if (data.Count() == 0)
            {
                if (Photo != null && Photo.Contains("base64,"))
                {
                    string fileName = Guid.NewGuid().ToString();
                    string fileUrl = Path.Combine(Server.MapPath("File/User/" + fileName + ".png"));
                    string filePath = "/File/User/" + fileName + ".png";
                    using (MemoryStream stream = new MemoryStream
                        (Convert.FromBase64String(Photo.Substring(Photo.IndexOf("base64,") + 7, Photo.Length - (Photo.IndexOf("base64,") + 7)))))
                    using (FileStream fileStream = new FileStream(fileUrl, FileMode.Create, FileAccess.ReadWrite))
                    {
                        stream.WriteTo(fileStream);
                    }
                    model.Photo = filePath;
                }
                model.DeletionStatus = false;
                model.CreatedTime = DateTime.Now;
                model.UpdatedTime = DateTime.Now;
                db.Users.Add(model);
                db.SaveChanges();
            }
            else
            {
                throw new Exception(model.EMail.ToUpper() + " Sistemde bulunmaktadır.");
            }
            return Json(new { res = true });
        }
        [Route("kullanici-guncelle/{id}"), HttpPost]
        public ActionResult Update(User model, string Photo)
        {
            var data = db.Users.Find(model.ID);
            if (Photo.Contains("base64,"))
            {
                string fileName = Guid.NewGuid().ToString();
                string fileUrl = Path.Combine(Server.MapPath("/File/User/" + fileName + ".png"));
                string filePath = "/File/User/" + fileName + ".png"; using (MemoryStream stream = new MemoryStream
                    (Convert.FromBase64String(Photo.Substring(Photo.IndexOf("base64,") + 7, Photo.Length - (Photo.IndexOf("base64,") + 7)))))
                using (FileStream fileStream = new FileStream(fileUrl, FileMode.Create, FileAccess.ReadWrite))
                {
                    stream.WriteTo(fileStream);
                }
                data.Photo = filePath;
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
            data.MilitaryStatus = model.MilitaryStatus;
            data.Gender = model.Gender;
            data.MarriageStatus = model.MarriageStatus;
            data.ProgramState = model.ProgramState;
            data.Hobby = model.Hobby;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [Route("kullanici-getir/{searchText?}"), HttpGet]
        public JsonResult GetData(string searchText)
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
                             EMail = obj.EMail,
                             Phone = obj.Phone,
                         }
            };
            return Json(query, JsonRequestBehavior.AllowGet);
        }
    }
}