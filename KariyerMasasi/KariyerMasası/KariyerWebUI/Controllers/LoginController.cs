using KariyerEntity.Modal;
using KariyerWebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KariyerWebUI.Controllers
{
    public class LoginController : Controller
    {
        private SystemContext db = new SystemContext();
        [Route("giris-yap"),HttpGet]
        public ActionResult Index() => View();
        [Route("giris-yap"),HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            var user = db.Users.Where(x => x.EMail == model.EMail && x.Password == model.Password).Count();
            if (user==1)
            {
                return Redirect("/");
            }
            else
            {
                return View(model);
            }
        }
    }
}