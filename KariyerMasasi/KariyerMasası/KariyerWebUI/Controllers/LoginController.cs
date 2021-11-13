using KariyerEntity.Entity;
using KariyerEntity.Modal;
using KariyerWebUI.Models;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
namespace KariyerWebUI.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {
        private SystemContext db = new SystemContext();
        [Route("giris-yap"), HttpGet]
        public ActionResult Login() => View();
        [Route("giris-yap"), HttpPost]
        public ActionResult Login(User model)
        {
            var user = db.Users.Where(x => x.EMail == model.EMail && x.Password == model.Password).FirstOrDefault();
            if (user != null)
            {
                FormsAuthentication.SetAuthCookie(user.Name, false);
                var userrole = db.Roles.Where(x => x.DeletionStatus).FirstOrDefault();

                LoginViewModel login = new LoginViewModel();
                login.Name = user.Name;
                login.Surname = user.Surname;
                login.Image = user.Photo;
                if (userrole != null)
                {
                    login.Role = userrole.Name;
                }
                Session["loginUser"] = login;
                return Redirect("/");
            }
            else
            {
                ViewBag.LoginError = "Hatalı kullanıcı adı veya şifre girdiniz";
                return View();
            }
        }
        [Route("cikis-yap"), HttpGet]
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
    }
}