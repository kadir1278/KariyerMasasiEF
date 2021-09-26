using KariyerEntity.Entity;
using KariyerEntity.Modal;
using KariyerWebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KariyerWebUI.Controllers
{
    public class GeneralController : Controller
    {
        // GET: General
        private SystemContext db = new SystemContext();
        public ActionResult Index() => View();
        #region Partial 
        public ActionResult PartialAddGeneral()
        {
            return PartialView();
        }
        public ActionResult PartialUpdateGeneral(GeneralViewModel model, int? LangID, int? SpecialID, int? BusinessID)
        {
            if (LangID != null)
            {
                var l = db.Languages.Where(x => x.ID == LangID).FirstOrDefault();
                model.LangName = l.Name;
            }
            if (SpecialID != null)
            {
                var s = db.UserSpecialTypes.Where(x => x.ID == SpecialID).FirstOrDefault();
                model.UserSpecialName = s.Name;
            }
            if (BusinessID != null)
            {
                var b = db.BusinessAreas.Where(x => x.ID == BusinessID).FirstOrDefault();
                model.BusinessAreaName = b.Name;
            }
            return PartialView(model);
        }
        #endregion
        #region Methods
        [HttpPost]
        public ActionResult Add(GeneralViewModel model, UserSpecialType userSpecialType, Language language, BusinessArea businessArea)
        {
            if (userSpecialType != null)
            {
                userSpecialType.Name = model.UserSpecialName;
                userSpecialType.CreatedTime = DateTime.Now;
                userSpecialType.DeletionStatus = false;
                userSpecialType.UpdatedTime = DateTime.Now;
                db.UserSpecialTypes.Add(userSpecialType);
                db.SaveChanges();
            }
            if (language != null)
            {
                language.Name = model.LangName;
                language.CreatedTime = DateTime.Now;
                language.DeletionStatus = false;
                language.UpdatedTime = DateTime.Now;
                db.Languages.Add(language);
                db.SaveChanges();
            }
            if (businessArea != null)
            {
                businessArea.Name = model.BusinessAreaName;
                businessArea.CreatedTime = DateTime.Now;
                businessArea.DeletionStatus = false;
                businessArea.UpdatedTime = DateTime.Now;
                db.BusinessAreas.Add(businessArea);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult Update(GeneralViewModel model, UserSpecialType userSpecialType, Language language, BusinessArea businessArea)
        {
            if (userSpecialType != null)
            {
                userSpecialType.Name = model.UserSpecialName;
                userSpecialType.CreatedTime = DateTime.Now;
                userSpecialType.DeletionStatus = false;
                userSpecialType.UpdatedTime = DateTime.Now;
                db.UserSpecialTypes.Add(userSpecialType);
                db.SaveChanges();
            }
            if (language != null)
            {
                language.Name = model.LangName;
                language.CreatedTime = DateTime.Now;
                language.DeletionStatus = false;
                language.UpdatedTime = DateTime.Now;
                db.Languages.Add(language);
                db.SaveChanges();
            }
            if (businessArea != null)
            {
                businessArea.Name = model.BusinessAreaName;
                businessArea.CreatedTime = DateTime.Now;
                businessArea.DeletionStatus = false;
                businessArea.UpdatedTime = DateTime.Now;
                db.BusinessAreas.Add(businessArea);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        #endregion
    }
}