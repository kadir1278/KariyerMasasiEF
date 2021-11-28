using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KariyerWebUI.Controllers
{
    public class RegisterController : Controller
    {
        // GET: Register
        public ActionResult UserRegister()
        {
            return View();
        }

        [Route("sirket-kayit"), HttpGet]
        public ActionResult CompanyRegister() => View();

    }
}