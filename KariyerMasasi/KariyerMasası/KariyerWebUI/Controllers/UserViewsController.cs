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

        public ActionResult UserApplication()
        {
            return View();
        }

        public ActionResult Ozgecmis()
        {
            return View();
        }
        public ActionResult SertifikaListesi()
        {
            return View();
        }
        public ActionResult SertifikaOlustur()
        {
            return View();
        }
        
    }
}