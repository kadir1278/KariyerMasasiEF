﻿using KariyerEntity.Entity;
using KariyerEntity.Modal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KariyerWebUI.Controllers
{
    public class LanguageController : Controller
    {
        private SystemContext db = new SystemContext();
        [Route("dil"), HttpGet]
        public ActionResult Index() => View();
        [Route("dil/{searchText?}"), HttpGet]
        public JsonResult GetData(string searchText)
        {
            List<Language> data = new List<Language>();

            if (searchText == "" || searchText == null)
            {
                data = db.Languages.Where(x => !x.DeletionStatus).ToList();
            }
            else
            {
                data = db.Languages.Where(x => !x.DeletionStatus && x.Name == searchText).ToList();
            }
            var query = new
            {
                Result = from obj in data
                         select new
                         {
                             ID = obj.ID,
                             Name = obj.Name,
                         }
            };
            return Json(query, JsonRequestBehavior.AllowGet);
        }
        [Route("dil-ekle"), HttpGet]
        public ActionResult Add() => PartialView();
        [Route("dil-duzenle/{id}")]
        public ActionResult Update(int id)
        {
            var data = db.Languages.Where(x => !x.DeletionStatus && x.ID == id).FirstOrDefault();
            return View(data);
        }
        [Route("dil-sil/{id}"), HttpGet]
        public ActionResult Delete(int id)
        {
            try
            {
                var data = db.Languages.Where(x => !x.DeletionStatus && x.ID == id).FirstOrDefault();
                data.DeletionStatus = true;
                db.SaveChanges();
                return View();
            }
            catch (Exception ex)
            {
                return View(ex);
            }
        }
        [Route("dil-ekle"), HttpPost]
        public ActionResult AddLang(Language lang)
        {
            try
            {
                lang.CreatedTime = DateTime.Now;
                lang.UpdatedTime = DateTime.Now;
                lang.DeletionStatus = false;
                lang.Name = lang.Name.ToUpper();
                db.Languages.Add(lang);
                db.SaveChanges();
                return View(lang);
            }
            catch (Exception ex)
            {
                return View(ex);
            }
        }
        [Route("dil-duzenle/{id}"), HttpPost]
        public ActionResult UpdateLang(Language lang, int id)
        {
            try
            {
                var data = db.Languages.Where(x => !x.DeletionStatus && x.ID == id).FirstOrDefault();
                data.Name = lang.Name.ToUpper();
                data.UpdatedTime = DateTime.Now;
                data.CreatedTime = lang.CreatedTime;
                data.DeletionStatus = lang.DeletionStatus;
                db.SaveChanges();
                return View(data);
            }
            catch (Exception ex)
            {
                return View(ex);
            }
        }
    }
}