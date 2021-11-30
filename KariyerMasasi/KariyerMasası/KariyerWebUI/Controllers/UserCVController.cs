using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KariyerWebUI.Controllers
{
    public class UserCVController : Controller
    {
        [Route("kullanici-ozgecmis"), HttpGet]
        public ActionResult Index()
        {
            return View();
        }
    }
}