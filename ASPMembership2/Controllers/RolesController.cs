using ASPMembership2.DataAccess;
using ASPMembership2.DataAccess.Managers;
using System.Web.Mvc;

namespace ASPMembership2.Controllers
{
    public class RolesController : Controller
    {
        private const string className = "RolesController";

        [HttpGet]
        [AuthorizeRoles(className)]
        public ActionResult RolesList()
        {
            var roles = RolesManager.GetAllRoles();
            return View(roles);
        }

        [HttpGet]
        [AuthorizeRoles(className)]
        public ActionResult EditRole(string id)
        {
            ViewBag.RoleId = id;
            return View();
        }

        [HttpPost]
        [AuthorizeRoles(className)]
        public ActionResult EditRole([Bind(Include = "Id, Name")] AspNetRoles roleItem)
        {
            return View();
        }

        [HttpGet]
        [AuthorizeRoles(className)]
        public ActionResult Delete(string id)
        {
            if (id != null)
            {
                RolesManager.DeleteRoleById(id);
            }
            return RedirectToAction("RolesList");
        }
    }
}