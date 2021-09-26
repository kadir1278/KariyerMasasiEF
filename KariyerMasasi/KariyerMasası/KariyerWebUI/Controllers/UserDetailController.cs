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
            var query = new
            {
                Result = (from usr in db.Users.Where(x => !x.DeletionStatus && x.ID == userID)
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
                              CertificateName = usrcertificate.CertificateName,
                          }).ToList()
            };

            return Json(query, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public ActionResult Add(UserDetailViewModel model, UserCertificate userCertificate, UserSeminar userSeminar, UserComputerInformation userComputer, UserReference userReference, UserBusinessInformation userBusiness, string CertificateFile)
        {
            if (model.Certificates.Count > 0)
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
                    userCertificate.DeletionStatus = false;
                    userCertificate.CreatedTime = DateTime.Now;
                    userCertificate.UpdatedTime = DateTime.Now;
                    db.UserCertificates.Add(userCertificate);
                    db.SaveChanges();
                }
            }
            if (model.ComputerInformations.Count > 0)
            {
                foreach (var item in model.ComputerInformations)
                {
                    userComputer.UserID = model.UserID;
                    userComputer.Name = item.Name;
                    userComputer.Description = item.Description;
                    userComputer.DeletionStatus = false;
                    userComputer.CreatedTime = DateTime.Now;
                    userComputer.UpdatedTime = DateTime.Now;
                    db.UserComputerInformations.Add(userComputer);
                    db.SaveChanges();
                }
            }
            if (model.BusinessInformations.Count>0)
            {
                foreach (var item in model.BusinessInformations)
                {
                    userBusiness.UserID = model.UserID;
                    userBusiness.CompanyName = item.CompanyName;
                    userBusiness.StartingDate = item.StartingDate;
                    userBusiness.FinishDate = item.FinishDate;
                    userBusiness.Duty = item.Duty;
                    userBusiness.Description = item.Description;
                    userBusiness.DeletionStatus = false;
                    userBusiness.CreatedTime = DateTime.Now;
                    userBusiness.UpdatedTime = DateTime.Now;
                    db.UserBusinessInformations.Add(userBusiness);
                    db.SaveChanges();
                }
            }
            if (model.References.Count>0)
            {
                foreach (var item in model.References)
                {
                    userReference.UserID = model.UserID;
                    userReference.NameSurname = item.NameSurname;
                    userReference.Phone = item.Phone;
                    userReference.EMail = item.EMail;
                    userReference.DeletionStatus = false;
                    userReference.CreatedTime = DateTime.Now;
                    userReference.UpdatedTime = DateTime.Now;
                    db.UserReferences.Add(userReference);
                    db.SaveChanges();
                }
            }
            if (model.Seminars.Count>0)
            {
                foreach (var item in model.Seminars)
                {
                    userSeminar.UserID = model.UserID;
                    userSeminar.Name = item.Name;
                    userSeminar.SeminarDate = item.SeminarDate;
                    userSeminar.Description = item.Description;
                    userSeminar.DeletionStatus = false;
                    userSeminar.CreatedTime = DateTime.Now;
                    userSeminar.UpdatedTime = DateTime.Now;
                    db.UserSeminars.Add(userSeminar);
                    db.SaveChanges();
                }
            }
            return RedirectToAction("Index");
        }
    }
}