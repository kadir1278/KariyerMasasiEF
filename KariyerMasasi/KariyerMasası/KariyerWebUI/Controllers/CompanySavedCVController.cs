using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KariyerWebUI.Controllers
{
    public class CompanySavedCVController : Controller
    {
        [Route("kayitli-ozgecmisler")]
        public ActionResult Index()
        {
            return View();
        }
    }
}