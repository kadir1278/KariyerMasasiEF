using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KariyerWebUI.Controllers
{
    public class UserViewsController : Controller
    {
        // GET: UserViews
        [Route("kullanici-basvuru"),HttpGet]
        public ActionResult UserApplication()
        {
            return View();
        }

        [Route("kullanici-ozgecmis"), HttpGet]
        public ActionResult Ozgecmis()
        {
            return View();
        }
        [Route("kullanici-sertifikalar"), HttpGet]
        public ActionResult SertifikaListesi()
        {
            return View();
        }
        [Route("kullanici-sertifika-olustur"),HttpGet]
        public ActionResult SertifikaOlustur()
        {
            return View();
        }
        [Route("kullanici-toplantilar"), HttpGet]
        public ActionResult KullaniciToplantilar()
        {
            return View();
        }
    }
}