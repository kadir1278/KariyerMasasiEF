using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KariyerWebUI.Controllers
{
    public class LobyController : Controller
    {
        [Route("kullanici-lobi"),HttpGet]
        public ActionResult Customer()
        {
            return View();
        }
        [Route("sirket-lobi"), HttpGet]
        public ActionResult Company()
        {
            return View();
        }
    }
}