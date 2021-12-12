using KariyerEntity.Entity;
using KariyerEntity.Modal;
using KariyerWebUI.Models;
using System.Linq;
using System.Web.Mvc;
using System.Data.Entity;
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
        public ActionResult Login(string email, string password)
        {
            var user = db.Users.Where(x => x.EMail == email && x.Password == password).FirstOrDefault();
            if (user != null)
            {
                FormsAuthentication.SetAuthCookie(user.EMail, false);
                var userrole = db.UserRoles.Include(x => x.Role).Where(x => !x.DeletionStatus && x.UserID == user.ID).FirstOrDefault();
                var cmpny = db.CompanyUsers.Include(x => x.Company).Where(x => !x.DeletionStatus && x.EMail == email).FirstOrDefault();

                LoginViewModel login = new LoginViewModel();
                login.Name = user.Name;
                login.Surname = user.Surname;
                login.Image = user.Photo;
                login.ID = user.ID;
                login.Role = userrole.Role.Name;
                login.Company = cmpny.Company.ID;
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
            Session.Clear();
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
    }
}