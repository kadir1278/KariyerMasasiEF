using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KariyerEntity.Entity;
using KariyerEntity.Modal;
namespace KariyerWebUI.Controllers
{
    public class CompanyViewsController : Controller
    {
        SystemContext db = new SystemContext();
     
        public ActionResult Application()
        {
            return View();
        }
        
        public ActionResult DigerFirmalar()
        {
            var list = db.Companies.Where(x => !x.DeletionStatus).ToList();

            return View(list);
        }
        
        public ActionResult SirketBilgi(int id = 2)
        {
            var model = db.Companies.Find(id);
            return View(model);
        }
        #region Sirket Kullanicilari
        public ActionResult SirketKullanici(int id = 2)
        {
            var model = db.CompanyUsers.Where(x => !x.DeletionStatus && x.CompanyID == id).ToList();
            return View(model);
        }

        
        public ActionResult PartialSirketKullaniciEkle()
        {
            ViewBag.CompanyID = db.Companies.Where(x => !x.DeletionStatus).ToList();
            return PartialView();
        }
        public ActionResult PartialSirketKullaniciGuncelle(int id)
        {
            ViewBag.CompanyID = db.Companies.Where(x => !x.DeletionStatus).ToList(); ;
            var data = db.CompanyUsers.Where(x => x.ID == id).FirstOrDefault();
            return PartialView(data);
        }
        #endregion

        public ActionResult Meetings()
        {
            return View();
        }

    }
}