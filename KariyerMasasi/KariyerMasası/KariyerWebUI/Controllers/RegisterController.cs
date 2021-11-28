using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KariyerWebUI.Controllers
{
    public class RegisterController : Controller
    {
        [Route("kullanici-kayit"), HttpGet]
        public ActionResult UserRegister() => View();



        [Route("kullanici-tip"),HttpGet]
        public PartialViewResult PartialUserType() => PartialView();

        [Route("sirket-kayit"), HttpGet]
        public ActionResult CompanyRegister() => View();

        

    }
}