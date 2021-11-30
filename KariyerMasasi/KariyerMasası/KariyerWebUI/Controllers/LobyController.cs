using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KariyerWebUI.Controllers
{
    public class LobyController : Controller
    {
        [Route("lobi"),HttpGet]
        public ActionResult Index()
        {
            return View();
        }
    }
}