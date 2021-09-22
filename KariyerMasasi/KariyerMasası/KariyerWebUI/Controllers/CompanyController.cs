using KariyerEntity.BaseEntity;
using KariyerEntity.Entity;
using KariyerEntity.Modal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KariyerWebUI.Controllers
{
   public class CompanyController : Controller
    {
        private SystemContext db = new SystemContext();
        public ActionResult Index() => View();
        #region Partial
        public ActionResult PartialAddCompany()
        {
            ViewBag.BusinessAreaID = new SelectList(db.BusinessAreas.Where(x => !x.DeletionStatus), "ID", "Name");

            return PartialView();
        }
        public ActionResult PartialUpdateCompany(int id)
        {
            ViewBag.BusinessAreaID = new SelectList(db.Users.Where(x => !x.DeletionStatus), "ID", "Name");

            var data = db.Companies.Where(x => x.ID == id).FirstOrDefault();
            return PartialView(data);
        }
        #endregion
        #region Methot
        public ActionResult Delete(int ID)
        {
            var data = db.Companies.Find(ID);
            data.DeletionStatus = true;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Add(Company model)
        {       
            model.DeletionStatus = false;
            model.CreatedTime = DateTime.Now;
            model.UpdatedTime = DateTime.Now;
            db.Companies.Add(model);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult Update(Company model)
        {
            var data = db.Companies.Find(model.ID);   
            data.UpdatedTime = DateTime.Now;
            data.DeletionStatus = model.DeletionStatus;

            data.EMail = model.EMail;
            data.Name = model.Name;
            data.Phone = model.Phone;
            data.Country = model.Country;
            data.City = model.City;
            data.Town = model.Town;
            data.Address = model.Address;
            data.Fax = model.Fax;
            data.TaxNumber = model.TaxNumber;
            data.TaxFile = model.TaxFile;
            data.TaxAddress = model.TaxAddress;
            data.ProgramState = model.ProgramState;
            data.GeneralIsActiveStatus = model.GeneralIsActiveStatus;
            data.PaymentStatus = model.PaymentStatus;     
            db.SaveChanges();
            return View();
        }
        #endregion
        #region Json
        public JsonResult GetCompanyData(string searchText)
        {
            List<Company> data = new List<Company>();

            if (searchText == "" || searchText == null)
            {
                data = db.Companies.Where(x => !x.DeletionStatus).ToList();
            }
            else
            {
                data = db.Companies.Where(x => !x.DeletionStatus && x.Name == searchText).ToList();
            }
            var query = new
            {
                Result = from obj in data
                         select new
                         {
                             ID = obj.ID,
                             Name = obj.Name,
                             EMail = obj.EMail,
                             Phone = obj.Phone,         
                             Country = obj.Country,
                             Logo = obj.Logo
                         }
            };
            return Json(query, JsonRequestBehavior.AllowGet);
        }

        #endregion

    }
}