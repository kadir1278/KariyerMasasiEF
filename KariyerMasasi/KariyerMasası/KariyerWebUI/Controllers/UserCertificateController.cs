using KariyerEntity.Entity;
using KariyerEntity.Modal;
using KariyerWebUI.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KariyerWebUI.Controllers
{
    public class UserCertificateController : Controller
    {
        private SystemContext db = new SystemContext();
        [Route("sertifika"), HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        [Route("sertifika-ekle"), HttpGet]
        public ActionResult Add()
        {
            ViewBag.LanguageID = db.Languages.Where(x => !x.DeletionStatus).ToList();
            return PartialView();
        }
        [Route("sertifika-ekle"), HttpPost]
        public JsonResult Add(UserCertificate model, string File)
        {
            var loginUser = Session["loginUser"] as LoginViewModel;

            try
            {
                if (File != null && File.Contains("base64,"))
                {
                    string fileName = Guid.NewGuid().ToString();
                    string fileUrl = Path.Combine(Server.MapPath("File/User/Certificates/" + fileName + ".png"));
                    string filePath = "/File/User/Certificates/" + fileName + ".png";
                    using (MemoryStream stream = new MemoryStream
                        (Convert.FromBase64String(File.Substring(File.IndexOf("base64,") + 7, File.Length - (File.IndexOf("base64,") + 7)))))
                    using (FileStream fileStream = new FileStream(fileUrl, FileMode.Create, FileAccess.ReadWrite))
                    {
                        stream.WriteTo(fileStream);
                    }
                    model.File = filePath;
                }
                model.UserID = loginUser.ID;
                model.DeletionStatus = false;
                model.CreatedTime = DateTime.Now;
                model.UpdatedTime = DateTime.Now;
                model.Status = false;
                db.UserCertificates.Add(model);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Bir sorun oluştu." + ex.Message);
            }
            return Json(new { res = true });
        }
        [Route("sertifika-sil/{id}"), HttpGet]
        public ActionResult Delete(int id)
        {
            var data = db.UserCertificates.Find(id);
            data.DeletionStatus = true;
            db.SaveChanges();
            return Redirect("/profil");
        }
        [Route("sertifika-guncelle/{id}"), HttpGet]
        public ActionResult Update(int id)
        {
            ViewBag.LanguageID = db.Languages.Where(x => !x.DeletionStatus).ToList();
            var data = db.UserCertificates.Find(id);
            return PartialView(data);
        }
        [Route("sertifika-guncelle/{id}"), HttpPost]
        public ActionResult UpdateCertificate(UserCertificate model, string File)
        {
            if (ModelState.IsValid)
            {
                var data = db.UserCertificates.Where(x => !x.DeletionStatus && x.ID == model.ID).FirstOrDefault();
                if (File != null && File.Contains("base64,"))
                {
                    string fileName = Guid.NewGuid().ToString();
                    string fileUrl = Path.Combine(Server.MapPath("File/User/Certificates/" + fileName + ".png"));
                    string filePath = "/File/User/Certificates/" + fileName + ".png";
                    using (MemoryStream stream = new MemoryStream
                        (Convert.FromBase64String(File.Substring(File.IndexOf("base64,") + 7, File.Length - (File.IndexOf("base64,") + 7)))))
                    using (FileStream fileStream = new FileStream(fileUrl, FileMode.Create, FileAccess.ReadWrite))
                    {
                        stream.WriteTo(fileStream);
                    }
                    data.File = filePath;
                }
                data.Name = model.Name;
                data.FinishDate = model.FinishDate;
                data.InstitutionFromName = model.InstitutionFromName;
                data.Status = model.Status;
                data.UpdatedTime = DateTime.Now;
                data.LanguageID = model.LanguageID;
                db.SaveChanges();
            }
            return Redirect("/profil");
        }
    }
}