using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KariyerWebUI.Controllers
{
    public class UserCertificateInfoController : Controller
    {
        [Route("kullanici-sertifika"), HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [Route("kullanici-sertifika-olustur"), HttpGet]
        public ActionResult Add()
        {
            return View();
        }
    }
}