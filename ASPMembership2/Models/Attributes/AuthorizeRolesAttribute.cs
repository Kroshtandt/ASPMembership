using ASPMembership2.DataAccess.Managers;
using System.Runtime.CompilerServices;
using System.Web.Mvc;
using System.Web.Routing;

public class AuthorizeRolesAttribute : AuthorizeAttribute
{
    public AuthorizeRolesAttribute(string callerNode, [CallerMemberName] string propertyName = null) : base()
    {
        var roles = MethodsManager.GetRolesByMethodIdentity($"{callerNode}.{propertyName}");
        if (roles.Length == 0)
            roles = new string[] { "notallowedforall" };
        Roles = string.Join(",", roles);

        var test = new CallerMemberNameAttribute();
    }

    protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
    {
        if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
        {
            filterContext.Result = new HttpUnauthorizedResult();
        }
        else
        {
            filterContext.Result = new RedirectToRouteResult(new
                RouteValueDictionary(new { controller = "AccessDenied" }));
        }
    }
}