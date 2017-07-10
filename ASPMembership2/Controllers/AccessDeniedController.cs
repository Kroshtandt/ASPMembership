using System.Web.Mvc;

namespace ASPMembership2.Controllers
{
    public class AccessDeniedController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}