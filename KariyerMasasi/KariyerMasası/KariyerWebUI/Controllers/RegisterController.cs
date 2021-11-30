using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KariyerEntity.Entity;
using KariyerEntity.Modal;
using KariyerWebUI.Models;
namespace KariyerWebUI.Controllers
{
    public class RegisterController : Controller
    {
        SystemContext db = new SystemContext();
        [Route("kullanici-kayit"), HttpGet]
        public ActionResult UserRegister() => View();

        [Route("kullanici-kayit"), HttpPost]
        public ActionResult UserRegister(User data)
        {

            return RedirectToAction("/");
        }

        [Route("kullanici-tip"),HttpGet]
        public PartialViewResult PartialUserType()
        {
            UserRegisterViewModel viewmodel = new UserRegisterViewModel();
            viewmodel.BusinessAreas = new SelectList(db.BusinessAreas.Where(x=>!x.DeletionStatus).ToList(), "ID", "Name");
            return PartialView(viewmodel);
        }

        [Route("sirket-kayit"), HttpGet]
        public ActionResult CompanyRegister() => View();

        

    }
}