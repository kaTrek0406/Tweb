using System.Linq;
using System.Web.Mvc;

namespace JW.WebApi.Security
{
    public class RoleAuthorizeAttribute : ActionFilterAttribute
    {
        private readonly string[] _roles;

        public RoleAuthorizeAttribute(params string[] roles)
        {
            _roles = roles;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var httpContext = filterContext.HttpContext;

            if (!httpContext.User.Identity.IsAuthenticated)
            {
                DenyAccess(filterContext);
                return;
            }

            if (!_roles.Any(role => httpContext.User.IsInRole(role)))
            {
                DenyAccess(filterContext);
                return;
            }

            base.OnActionExecuting(filterContext);
        }

        private void DenyAccess(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                filterContext.Result = new RedirectToRouteResult(
                    new System.Web.Routing.RouteValueDictionary
                    {
                        { "controller", "Error" },
                        { "action", "Unauthorized" }
                    });
            }
            else
            {
                filterContext.Result = new HttpUnauthorizedResult();
            }
        }
    }
}