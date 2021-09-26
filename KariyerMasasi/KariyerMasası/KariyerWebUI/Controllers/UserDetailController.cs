using KariyerEntity.Entity;
using KariyerEntity.Modal;
using KariyerWebUI.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace KariyerWebUI.Controllers
{
    public class UserDetailController : Controller
    {
        private SystemContext db;

        public ActionResult Index => View();
        public JsonResult GetUserData(int userID)
        {
           List<User> data = new List<User>();

            data = db.Users.Where(x => !x.DeletionStatus && x.ID == userID).ToList();

            var query = new
            {
                Result = (from usr in data
                          join usrcertificate in db.UserCertificates.Where(x => !x.DeletionStatus && x.UserID == userID) on usr.ID equals usrcertificate.UserID
                          join usrcomputer in db.UserComputerInformations.Where(x => !x.DeletionStatus && x.UserID == userID) on usr.ID equals usrcomputer.UserID
                          join usrbusiness in db.UserBusinessInformations.Where(x => !x.DeletionStatus && x.UserID == userID) on usr.ID equals usrbusiness.UserID
                          join usrref in db.UserReferences.Where(x => !x.DeletionStatus && x.UserID == userID) on usr.ID equals usrref.UserID
                          join usrseminar in db.UserReferences.Where(x => !x.DeletionStatus && x.UserID == userID) on usr.ID equals usrseminar.UserID

                          select new
                         {
                             ID = usr.ID,
                             Name = usr.Name,
                             Surname = usr.Surname,
                             EMail = usr.EMail,
                             Phone = usr.Phone,
                             CertificateName=usrcertificate.CertificateName,
                         })
            };

            return Json(query, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public ActionResult Add(UserDetailViewModel model, UserCertificate userCertificate, string CertificateFile)
        {
            if (model.Certificates != null)
            {
                foreach (var item in model.Certificates)
                {
                    if (CertificateFile.Contains("base64,") && CertificateFile != null)
                    {
                        string fileName = Guid.NewGuid().ToString();
                        string fileUrl = Path.Combine(Server.MapPath("File/User/Certificate" + fileName + ".png"));
                        string filePath = "/File/User/" + fileName + ".png";
                        using (System.IO.MemoryStream stream = new System.IO.MemoryStream(Convert.FromBase64String(CertificateFile.Substring(CertificateFile.IndexOf("base64,") + 7, CertificateFile.Length - (CertificateFile.IndexOf("base64,") + 7)))))
                        using (FileStream fileStream = new FileStream(fileUrl, FileMode.Create, FileAccess.ReadWrite))
                        {
                            stream.WriteTo(fileStream);
                        }
                        userCertificate.CertificateFile = filePath;
                    }
                    userCertificate.UserID = model.UserID;
                    userCertificate.CertificateName = item.CertificateName;
                    userCertificate.InstitutionFromName = item.InstitutionFromName;
                    userCertificate.Status = item.Status;
                    userCertificate.LanguageID = item.LanguageID;
                    userCertificate.CertificateFinishDate = item.CertificateFinishDate;
                    db.UserCertificates.Add(userCertificate);
                    db.SaveChanges();
                }
            }
            return RedirectToAction("Index");
        }
    }
}