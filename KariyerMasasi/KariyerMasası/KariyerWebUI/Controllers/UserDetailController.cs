using KariyerEntity.Modal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KariyerWebUI.Controllers
{
    public class UserDetailController : Controller
    {
        private SystemContext db = new SystemContext();
        public ActionResult Index()
        {
            return View();
        }
    }
}