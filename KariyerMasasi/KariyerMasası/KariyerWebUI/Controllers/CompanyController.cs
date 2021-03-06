using KariyerEntity.Entity;
using KariyerEntity.Modal;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace KariyerWebUI.Controllers
{
    [Authorize(Roles = "ADMIN")]
    public class CompanyController : Controller
    {
        private SystemContext db = new SystemContext();
        [Route("sirket-aktif"), HttpGet]
        public ActionResult IndexActive() => View();
        [Route("sirket-pasif"), HttpGet]
        public ActionResult IndexPassive() => View();
        [Route("PartialAddCompany"), HttpGet]
        public ActionResult PartialAddCompany()
        {
            ViewBag.BusinessAreaID = db.BusinessAreas.Where(x => !x.DeletionStatus).ToList();
            ViewBag.DepartmentID = db.Departments.Where(x => !x.DeletionStatus).ToList();
            return PartialView();
        }
        [Route("PartialUpdateCompany/{id}"), HttpGet]
        public ActionResult PartialUpdateCompany(int id)
        {
            ViewBag.BusinessAreaID = db.BusinessAreas.Where(x => !x.DeletionStatus).ToList();
            ViewBag.DepartmentID = db.Departments.Where(x => !x.DeletionStatus).ToList();
            ViewBag.CompanyDepartment = db.CompanyDepartments.Include(x=>x.Company).Include(x=>x.Department).Where(x => !x.DeletionStatus && x.CompanyID == id).ToList();
            var data = db.Companies.Where(x => x.ID == id).FirstOrDefault();
            return PartialView(data);
        }
        [Route("sirket-sil/{id}"), HttpGet]
        public ActionResult Delete(int ID)
        {
            try
            {
                var data = db.Companies.Find(ID);
                data.DeletionStatus = true;
                db.SaveChanges();
                return View("Index");
            }
            catch (Exception ex)
            {

                return View(ex);
            }

        }
        [Route("sirket-ekle"), HttpPost]
        public JsonResult Add(Company model, string Logo, string TaxFile)
        {
            var data = db.Companies.Where(x => !x.DeletionStatus && x.EMail == model.EMail).ToList();
            if (data.Count() == 0)
            {
                if (Logo != null && Logo.Contains("base64,"))
                {
                    string fileName = Guid.NewGuid().ToString();
                    string fileUrl = Path.Combine(Server.MapPath("File/Company/Logo/" + fileName + ".png"));
                    string filePath = "/File/Company/Logo/" + fileName + ".png";
                    using (MemoryStream stream = new MemoryStream
                        (Convert.FromBase64String(Logo.Substring(Logo.IndexOf("base64,") + 7, Logo.Length - (Logo.IndexOf("base64,") + 7)))))
                    using (FileStream fileStream = new FileStream(fileUrl, FileMode.Create, FileAccess.ReadWrite))
                    {
                        stream.WriteTo(fileStream);
                    }
                    model.Logo = filePath;
                }
                if (TaxFile != null && TaxFile.Contains("base64,"))
                {
                    string fileName = Guid.NewGuid().ToString();
                    string fileUrl = Path.Combine(Server.MapPath("File/Company/TaxFile/" + fileName + ".pdf"));
                    string filePath = "/File/Company/TaxFile/" + fileName + ".pdf";
                    using (MemoryStream stream = new MemoryStream
                        (Convert.FromBase64String(TaxFile.Substring(TaxFile.IndexOf("base64,") + 7, TaxFile.Length - (TaxFile.IndexOf("base64,") + 7)))))
                    using (FileStream fileStream = new FileStream(fileUrl, FileMode.Create, FileAccess.ReadWrite))
                    {
                        stream.WriteTo(fileStream);
                    }
                    model.TaxFile = filePath;
                }
                model.DeletionStatus = false;
                model.CreatedTime = DateTime.Now;
                model.UpdatedTime = DateTime.Now;
                model.PaymentStatus = false;
                model.GeneralIsActiveStatus = false;
                model.ProgramState = false;
                db.Companies.Add(model);
                db.SaveChanges();
            }
            else
            {
                throw new Exception(model.EMail.ToUpper() + " Sistemde bulunmaktadır.");
            }
            return Json(new { res = true });


        }
        [Route("sirket-guncelle"), HttpPost]
        public ActionResult Update(Company model, string Logo, string TaxFile)
        {
            var data = db.Companies.Find(model.ID);
            if (Logo != null && Logo.Contains("base64,"))
            {
                string fileName = Guid.NewGuid().ToString();
                string fileUrl = Path.Combine(Server.MapPath("File/Company/Logo/" + fileName + ".png"));
                string filePath = "/File/Company/Logo/" + fileName + ".png";
                using (MemoryStream stream = new MemoryStream
                    (Convert.FromBase64String(Logo.Substring(Logo.IndexOf("base64,") + 7, Logo.Length - (Logo.IndexOf("base64,") + 7)))))
                using (FileStream fileStream = new FileStream(fileUrl, FileMode.Create, FileAccess.ReadWrite))
                {
                    stream.WriteTo(fileStream);
                }
                data.Logo = filePath;
            }
            if (TaxFile != null && TaxFile.Contains("base64,"))
            {
                string fileName = Guid.NewGuid().ToString();
                string fileUrl = Path.Combine(Server.MapPath("File/Company/TaxFile/" + fileName + ".pdf"));
                string filePath = "/File/Company/TaxFile/" + fileName + ".pdf";
                using (MemoryStream stream = new MemoryStream
                    (Convert.FromBase64String(TaxFile.Substring(TaxFile.IndexOf("base64,") + 7, TaxFile.Length - (TaxFile.IndexOf("base64,") + 7)))))
                using (FileStream fileStream = new FileStream(fileUrl, FileMode.Create, FileAccess.ReadWrite))
                {
                    stream.WriteTo(fileStream);
                }
                data.TaxFile = filePath;
            }
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
            data.TaxAddress = model.TaxAddress;
            data.ProgramState = model.ProgramState;
            data.GeneralIsActiveStatus = model.GeneralIsActiveStatus;
            data.PaymentStatus = model.PaymentStatus;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [Route("sirket-aktif-getir/{searchText?}"), HttpGet]
        public JsonResult GetDataActive(string searchText)
        {
            List<Company> data = new List<Company>();

            if (searchText == "" || searchText == null)
            {
                data = db.Companies.Where(x => !x.DeletionStatus && x.GeneralIsActiveStatus).ToList();
            }
            else
            {
                data = db.Companies.Where(x => !x.DeletionStatus && x.Name == searchText && x.GeneralIsActiveStatus).ToList();
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
                         }
            };
            return Json(query, JsonRequestBehavior.AllowGet);
        }
        [Route("sirket-pasif-getir/{searchText?}"), HttpGet]
        public JsonResult GetDataPassive(string searchText)
        {
            List<Company> data = new List<Company>();

            if (searchText == "" || searchText == null)
            {
                data = db.Companies.Where(x => !x.DeletionStatus && !x.GeneralIsActiveStatus).ToList();
            }
            else
            {
                data = db.Companies.Where(x => !x.DeletionStatus && x.Name == searchText && !x.GeneralIsActiveStatus).ToList();
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
                         }
            };
            return Json(query, JsonRequestBehavior.AllowGet);
        }
        [Route("sirket-onayla/{id}"), HttpGet]
        public ActionResult SetCompanyActiveStatus(int ID)
        {
            try
            {
                var data = db.Companies.Find(ID);
                data.GeneralIsActiveStatus = true;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View(ex);
            }

        }
    }
}