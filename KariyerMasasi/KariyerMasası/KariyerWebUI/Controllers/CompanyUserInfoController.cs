using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KariyerEntity.Modal;
using KariyerEntity.Entity;
namespace KariyerWebUI.Controllers
{
    public class CompanyUserInfoController : Controller
    {
        SystemContext db = new SystemContext();
        [Route("sirket-kullanicilari"), HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        [Route("sirket-kullanicisi-ekle"),HttpGet]
        public ActionResult PartialAddCompanyUser()
        {
            ViewBag.CompanyID = db.Companies.Where(x => !x.DeletionStatus).ToList();
            return PartialView();
        }
   
        public ActionResult PartialUpdateCompanyUser(int id)
        {
            ViewBag.CompanyID = db.Companies.Where(x => !x.DeletionStatus).ToList(); ;
            var data = db.CompanyUsers.Where(x => x.ID == id).FirstOrDefault();
            return PartialView(data);
        }
    }
}