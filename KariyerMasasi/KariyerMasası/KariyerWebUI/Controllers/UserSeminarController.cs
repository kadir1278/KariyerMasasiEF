using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KariyerWebUI.Controllers
{
    public class UserSeminarController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        [Route("semienr-ekle"),HttpGet]
        public ActionResult Add() => PartialView();
    }
}