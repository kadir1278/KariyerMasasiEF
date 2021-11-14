using KariyerEntity.Modal;
using KariyerWebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace KariyerWebUI.Controllers
{
    public class UserProfileController : Controller
    {
        private SystemContext db = new SystemContext();
        [Route("profil")]
        public ActionResult Index(UserProfileViewModel model)
        {
            var loginUser = Session["loginUser"] as LoginViewModel;
            int userID = loginUser.ID;
            model.UserInformations= db.Users.Include(x=>x.UserRoles).Where(x => !x.DeletionStatus && x.ID == userID).ToList();
            model.BusinessInformations = db.UserBusinessInformations.Where(x => !x.DeletionStatus && x.UserID == userID).ToList();
            model.Certificates = db.UserCertificates.Where(x => !x.DeletionStatus && x.UserID == userID).ToList();
            model.ComputerInformations = db.UserComputerInformations.Where(x => !x.DeletionStatus && x.UserID == userID).ToList();
            model.Educations = db.UserEducations.Where(x => !x.DeletionStatus && x.UserID == userID).ToList();
            model.Languages = db.UserLanguages.Where(x => !x.DeletionStatus&&x.UserID==userID).ToList();
            model.References = db.UserReferences.Where(x => !x.DeletionStatus && x.UserID == userID).ToList();
            model.Seminars = db.UserSeminars.Where(x => !x.DeletionStatus && x.UserID == userID).ToList();
            model.SpecialDirectories = db.SpecialDirectories.Where(x => !x.DeletionStatus && x.UserID == userID).ToList();

            return View(model);
        }
    }
}