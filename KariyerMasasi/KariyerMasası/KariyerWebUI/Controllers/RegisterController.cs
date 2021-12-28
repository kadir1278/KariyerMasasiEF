using System;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using KariyerEntity.Entity;
using KariyerEntity.Modal;
namespace KariyerWebUI.Controllers
{
    [AllowAnonymous]
    public class RegisterController : Controller
    {
        SystemContext db = new SystemContext();
        [Route("kullanici-kayit"), HttpGet]
        public ActionResult UserRegister()
        {
            ViewBag.SpecialType = db.UserSpecialTypes.Where(x => !x.DeletionStatus).ToList();
            ViewBag.BusinessAreaID = db.BusinessAreas.Where(x => !x.DeletionStatus).ToList();
            ViewBag.DepartmentID = db.Departments.Where(x => !x.DeletionStatus).ToList();

            return View();
        }

        [Route("sirket-kayit"), HttpGet]
        public ActionResult CompanyRegister()
        {
            ViewBag.BusinessAreaID = db.BusinessAreas.Where(x => !x.DeletionStatus).ToList();
            ViewBag.DepartmentID = db.Departments.Where(x => !x.DeletionStatus).ToList();
            return View();
        }
        [Route("sirket-kayit"), HttpPost]
        public ActionResult CompanyRegister(Company company, string[] Business, /*string[] SpecType,*/ string[] Depart)
        {
            var entity = db.Companies.Where(x => !x.DeletionStatus && x.EMail == company.EMail).FirstOrDefault();
            if (entity == null)
            {
                company.DeletionStatus = false;
                company.CreatedTime = DateTime.Now;
                company.UpdatedTime = DateTime.Now;
                company.ProgramState = false;
                company.GeneralIsActiveStatus = false;
                company.PaymentStatus = false;
                if (Business != null)
                {
                    foreach (var item in Business)
                    {
                        bool businessCheck = db.BusinessAreas.Any(x => x.Name == item);
                        if (!businessCheck)
                        {
                            BusinessArea b = new BusinessArea() { Name = item, UpdatedTime = DateTime.Now, CreatedTime = DateTime.Now };
                            db.BusinessAreas.Add(b);
                            db.SaveChanges();


                        }
                    }
                }
                //if (SpecType != null)
                //{
                //    foreach (var item in SpecType)
                //    {
                //        bool specCheck = db.UserSpecialTypes.Any(x => x.Name == item);
                //        if (!specCheck)
                //        {
                //            UserSpecialType spec = new UserSpecialType() { Name = item, UpdatedTime = DateTime.Now, CreatedTime = DateTime.Now };
                //            db.UserSpecialTypes.Add(spec);
                //            db.SaveChanges();
                //        }
                //    }
                //}
                if (Depart != null)
                {
                    foreach (var item in Depart)
                    {
                        bool depCheck = db.Departments.Any(x => x.Name == item);
                        if (!depCheck)
                        {
                            Department dep = new Department() { Name = item, UpdatedTime = DateTime.Now, CreatedTime = DateTime.Now };
                            db.Departments.Add(dep);
                            db.SaveChanges();
                        }
                    }
                }
                db.Companies.Add(company);
                db.SaveChanges();
                CompanyBusinessArea cba = new CompanyBusinessArea();
                if (Business != null)
                {
                    foreach (var item in Business)
                    {
                        cba.CompanyID = db.Companies.Where(x => x.EMail == company.EMail).FirstOrDefault().ID;
                        cba.BusinessAreaID = db.BusinessAreas.Where(x => x.Name == item).FirstOrDefault().ID;
                        cba.DeletionStatus = false;
                        cba.CreatedTime = DateTime.Now;
                        cba.UpdatedTime = DateTime.Now;
                        db.SaveChanges();
                    }
                }
                if (Depart!=null)
                {
                CompanyDepartment cd = new CompanyDepartment();
                foreach (var item in Depart)
                {
                    cd.CompanyID = db.Companies.Where(x => x.EMail == company.EMail).FirstOrDefault().ID;
                    cd.DepartmentID = db.Departments.Where(x => x.Name == item).FirstOrDefault().ID;
                    cd.DeletionStatus = false;
                    cd.CreatedTime = DateTime.Now;
                    cd.UpdatedTime = DateTime.Now;
                    db.SaveChanges();
                }
                }
                //CompanySpecialType cst = new CompanySpecialType();
                //foreach (var item in SpecType)
                //{
                //    cst.CompanyID = db.Companies.Where(x => x.EMail == company.EMail).FirstOrDefault().ID;
                //    cst.UserSpecialTypeID = db.UserSpecialTypes.Where(x => x.Name == item).FirstOrDefault().ID;
                //    cst.DeletionStatus = false;
                //    cst.CreatedTime = DateTime.Now;
                //    cst.UpdatedTime = DateTime.Now;
                //    db.SaveChanges();
                //}
                ViewBag.Message = company.EMail + " adresi ile kayıt başvurunuz alınmıştır.";
                ViewBag.BusinessAreaID = db.BusinessAreas.Where(x => !x.DeletionStatus).ToList();
                ViewBag.DepartmentID = db.Departments.Where(x => !x.DeletionStatus).ToList();

                return View();
            }
            else
            {
                ViewBag.Message = "Bu mail adresine kayıtlı bir şirket hesabı bulunmaktadır.";
                ViewBag.BusinessAreaID = db.BusinessAreas.Where(x => !x.DeletionStatus).ToList();
                ViewBag.DepartmentID = db.Departments.Where(x => !x.DeletionStatus).ToList();

                return View();
            }
        }
        [Route("kullanici-kayit-ekle"), HttpPost]
        public ActionResult UserAdd(User model, string Photo, string[] Business, string[] SpecType, string[] Depart)
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
                if (Business != null)
                {
                    foreach (var item in Business)
                    {
                        bool businessCheck = db.BusinessAreas.Any(x => x.Name == item);
                        if (!businessCheck)
                        {
                            BusinessArea b = new BusinessArea() { Name = item, UpdatedTime = DateTime.Now, CreatedTime = DateTime.Now };
                            db.BusinessAreas.Add(b);
                            db.SaveChanges();
                        }
                    }
                }
                if (SpecType != null)
                {
                    foreach (var item in SpecType)
                    {
                        bool specCheck = db.UserSpecialTypes.Any(x => x.Name == item);
                        if (!specCheck)
                        {
                            UserSpecialType spec = new UserSpecialType() { Name = item, UpdatedTime = DateTime.Now, CreatedTime = DateTime.Now };
                            db.UserSpecialTypes.Add(spec);
                            db.SaveChanges();
                        }
                    }
                }
                if (Depart != null)
                {
                    foreach (var item in Depart)
                    {
                        bool depCheck = db.Departments.Any(x => x.Name == item);
                        if (!depCheck)
                        {
                            Department dep = new Department() { Name = item, UpdatedTime = DateTime.Now, CreatedTime = DateTime.Now };
                            db.Departments.Add(dep);
                            db.SaveChanges();
                        }
                    }
                }
                // db.Users.Add(model);
                db.SaveChanges();
                ViewBag.Message = "Sayın " + model.Name + " " + model.Surname + " giriş yapabilirsiniz";
                return RedirectToAction("giris-yap");
            }
            else
            {
                ViewBag.Message = model.EMail.ToUpper() + " Sistemde bulunmaktadır.";
                return View();
            }
        }
    }
}