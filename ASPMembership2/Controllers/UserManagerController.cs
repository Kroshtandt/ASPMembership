using Microsoft.AspNet.Identity.Owin;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASPMembership2.Controllers
{
    public class UserManagerController : Controller
    {
        private const string className = "UserManagerController";

        private ApplicationUserManager _userManager;
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
 
        [AuthorizeRoles(className)]
        public ActionResult UsersList()
        {
            var list = UserManager.Users.ToList();
            return View(list);
        }

        [AuthorizeRoles(className)]
        public ActionResult EditUser(string id)
        {
            ViewBag.UserId = id;
            return View();
        }
    }
}
