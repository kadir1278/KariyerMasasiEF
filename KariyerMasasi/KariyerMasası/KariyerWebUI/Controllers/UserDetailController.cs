//using KariyerEntity.Entity;
//using KariyerEntity.Modal;
//using KariyerWebUI.Models;
//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Web;
//using System.Web.Mvc;
//using System.Data.Entity;

//namespace KariyerWebUI.Controllers
//{
//    public class UserDetailController : Controller
//    {
//        private SystemContext db = new SystemContext();

//        private UserDetailViewModel model = new UserDetailViewModel();
//        #region Partial
//        public ActionResult PartialBusinessAdd() => PartialView();
//        public ActionResult PartialEducationAdd() => PartialView();
//        public ActionResult PartialBusinessEdit(int ID)
//        {
//            var data = db.UserBusinessInformations.Where(x => !x.DeletionStatus && x.ID == ID).FirstOrDefault();
//            return PartialView(data);
//        }
//        public ActionResult PartialEducationEdit(int ID)
//        {
//            var data = db.UserEducations.Where(x => !x.DeletionStatus && x.ID == ID).FirstOrDefault();
//            return PartialView(data);
//        }

//        #endregion
//        #region MainMenu
//        public ActionResult Index(int? UserID)
//        {
//            UserID = 2;
//            model.UserInformations = db.Users.Where(x => !x.DeletionStatus && x.ID == UserID).Include(x => x.BusinessArea).ToList();
//            if (model.UserInformations.Count > 0)
//            {
//                model.Certificates = db.UserCertificates.Include(x => x.Language).Where(x => !x.DeletionStatus && x.UserID == UserID).ToList();
//                model.ComputerInformations = db.UserComputerInformations.Where(x => !x.DeletionStatus && x.UserID == UserID).ToList();
//                model.BusinessInformations = db.UserBusinessInformations.Where(x => !x.DeletionStatus && x.UserID == UserID).ToList();
//                model.References = db.UserReferences.Where(x => !x.DeletionStatus && x.UserID == UserID).ToList();
//                model.Seminars = db.UserSeminars.Where(x => !x.DeletionStatus && x.UserID == UserID).ToList();
//                model.Educations = db.UserEducations.Where(x => !x.DeletionStatus && x.UserID == UserID).ToList();
//                model.SpecialDirectories = db.SpecialDirectories.Where(x => !x.DeletionStatus && x.UserID == UserID).ToList();

//                return View(model);
//            }
//            else
//            {
//                ViewBag.UserError = "Hatalı istek böyle bir kullanıcı yok";
//                return View();
//            }
//        }
//        #endregion
//        #region Add
//        [HttpPost]
//        public ActionResult AddWork(UserBusinessInformation userBusiness)
//        {
//            if (userBusiness != null)
//            {
//                userBusiness.UserID = 2;
//                userBusiness.DeletionStatus = false;
//                userBusiness.CreatedTime = DateTime.Now;
//                userBusiness.UpdatedTime = DateTime.Now;
//                db.UserBusinessInformations.Add(userBusiness);
//                db.SaveChanges();
//            }
//            return RedirectToAction("Index");
//        }
//        #endregion
//        #region Delete
//        [HttpPost]
//        public ActionResult DeleteWork(int id)
//        {
//            var data = db.UserBusinessInformations.Where(x => !x.DeletionStatus && x.ID == id).FirstOrDefault();
//            data.DeletionStatus = true;
//            db.SaveChanges();
//            return RedirectToAction("Index");
//        }
//        [HttpPost]
//        public ActionResult DeleteEducation(int id)
//        {
//            var data = db.UserEducations.Where(x => !x.DeletionStatus && x.ID == id).FirstOrDefault();
//            data.DeletionStatus = true;
//            db.SaveChanges();
//            return RedirectToAction("Index");
//        }
//        [HttpPost]
//        public ActionResult DeleteReference(int id)
//        {
//            var data = db.UserReferences.Where(x => !x.DeletionStatus && x.ID == id).FirstOrDefault();
//            data.DeletionStatus = true;
//            db.SaveChanges();
//            return RedirectToAction("Index");
//        }
//        [HttpPost]
//        public ActionResult DeleteCertificate(int id)
//        {
//            var data = db.UserCertificates.Where(x => !x.DeletionStatus && x.ID == id).FirstOrDefault();
//            data.DeletionStatus = true;
//            db.SaveChanges();
//            return RedirectToAction("Index");
//        }
//        [HttpPost]
//        public ActionResult DeleteSeminar(int id)
//        {
//            var data = db.UserSeminars.Where(x => !x.DeletionStatus && x.ID == id).FirstOrDefault();
//            data.DeletionStatus = true;
//            db.SaveChanges();
//            return RedirectToAction("Index");
//        }
//        #endregion
//        #region Update
//        [HttpPost]
//        public ActionResult EditWork(UserBusinessInformation userBusiness)
//        {
//            var data = db.UserBusinessInformations.Where(x => !x.DeletionStatus && x.ID == userBusiness.ID).FirstOrDefault();
//            data.CompanyName = userBusiness.CompanyName;
//            data.DeletionStatus = userBusiness.DeletionStatus;
//            data.FinishDate = userBusiness.FinishDate;
//            data.StartingDate = userBusiness.StartingDate;
//            data.Description = userBusiness.Description;
//            data.Duty = userBusiness.Duty;
//            data.UpdatedTime = DateTime.Now;
//            db.SaveChanges();
//            return RedirectToAction("Index");
//        }
//        #endregion
//    }


//}