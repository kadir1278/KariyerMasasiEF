using System.Linq;
using System.Web.Mvc;
using KariyerEntity.Modal;
namespace KariyerWebUI.Controllers
{
    [AllowAnonymous]
    public class RegisterController : Controller
    {
        SystemContext db = new SystemContext();
        [Route("kullanici-kayit"), HttpGet]
        public ActionResult UserRegister()
        {
            ViewBag.SpecialType = db.UserSpecialTypes.Where(x => !x.DeletionStatus).ToList();
            ViewBag.BusinessAreaID = db.BusinessAreas.Where(x => !x.DeletionStatus).ToList();
            ViewBag.DepartmentID = db.Departments.Where(x => !x.DeletionStatus).ToList();

            return View();
        }

        [Route("sirket-kayit"), HttpGet]
        public ActionResult CompanyRegister()
        {
            ViewBag.BusinessAreaID = db.BusinessAreas.Where(x => !x.DeletionStatus).ToList();
            ViewBag.DepartmentID = db.Departments.Where(x => !x.DeletionStatus).ToList();
            return View();
        }

    }
}