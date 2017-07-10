using ASPMembership2.DataAccess;
using ASPMembership2.DataAccess.Managers;
using ASPMembership2.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace ASPMembership2.API
{
    public class AppMethodsAPIController : ApiController
    {
        private const string className = "AppMethodsAPIController";

        [HttpGet]
        [Route("appMethodsApi/getApplicationMethodsList")]
        [AuthorizeRoles(className)]
        public List<Methods> GetApplicationMethodsList()
        {
            var list = MethodsManager.GetApplicationMethodsList();
            return list;
        }


        [HttpPost]
        [Route("appMethodsApi/attachMethodToRole")]
        [AuthorizeRoles(className)]
        public void AttachMethodToRole(RoleMethodViewModel data)
        {
            if (data != null)
            {
                MethodsManager.AttachMethodToRole(data.RoleId, data.MethodId);
            }
        }

        [HttpPost]
        [Route("appMethodsApi/detachMethodFromRole")]
        [AuthorizeRoles(className)]
        public void DetachMethodFromRole(RoleMethodViewModel data)
        {
            if (data != null)
            {
                MethodsManager.DetachMethodFromRole(data.RoleId, data.MethodId);
            }
        }
    }
}
