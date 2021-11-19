using KariyerEntity.Modal;
using KariyerWebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Net;
using KariyerEntity.Entity;

namespace KariyerWebUI.Controllers
{
    public class UserProfileController : Controller
    {
        private SystemContext db = new SystemContext();
        [Route("profil")]
        public ActionResult Index(UserProfileViewModel model)
        {
            var loginUser = Session["loginUser"] as LoginViewModel;
            if (loginUser == null)
            {
                Redirect("/");
            }
            else
            {
                int userID = loginUser.ID;
                InModel(model, userID);
                return View(model);
            }
            return View();
        }

        private void InModel(UserProfileViewModel model, int userID)
        {
            model.UserInformations = db.Users.Include(x => x.BusinessArea).Where(x => !x.DeletionStatus && x.ID == userID).ToList(); //+
            model.BusinessInformations = db.UserBusinessInformations.Where(x => !x.DeletionStatus && x.UserID == userID).ToList(); //+
            model.Certificates = db.UserCertificates.Include(x => x.Language).Where(x => !x.DeletionStatus && x.UserID == userID).ToList(); //+
            model.ComputerInformations = db.UserComputerInformations.Where(x => !x.DeletionStatus && x.UserID == userID).ToList(); //+
            model.Educations = db.UserEducations.Where(x => !x.DeletionStatus && x.UserID == userID).ToList();// +
            model.Languages = db.UserLanguages.Where(x => !x.DeletionStatus && x.UserID == userID).ToList();// +
            model.References = db.UserReferences.Where(x => !x.DeletionStatus && x.UserID == userID).ToList(); //+
            model.Seminars = db.UserSeminars.Where(x => !x.DeletionStatus && x.UserID == userID).ToList(); //+
            model.SpecialDirectories = db.SpecialDirectories.Include(x=>x.UserSpecialType).Where(x => !x.DeletionStatus && x.UserID == userID).ToList();
        }

    }
}