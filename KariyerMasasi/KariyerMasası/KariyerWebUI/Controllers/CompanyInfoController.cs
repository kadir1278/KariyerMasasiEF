using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KariyerEntity.Modal;
using KariyerEntity.Entity;
namespace KariyerWebUI.Controllers
{
    public class CompanyInfoController : Controller
    {
        SystemContext db = new SystemContext();
        [Route("sirket-bilgilerini-guncelle"), HttpGet]
        public ActionResult Index(int id = 2) // sayfa gelsin diye verdim oylesine
        {
            var model = db.Companies.Find(id);
            return View(model);
        }
    }
}