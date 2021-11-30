using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KariyerEntity.Modal;
namespace KariyerWebUI.Controllers
{
    public class CompanyOtherCompaniesController : Controller
    {
        SystemContext db = new SystemContext();
        [Route("diger-firmalar"),HttpGet]
        public ActionResult Index()
        {
            var list = db.Companies.Where(x => !x.DeletionStatus).ToList();
            return View(list);
        }
    }
}