using Project3.Common;
using System.Web.Mvc;
using System.Web.Routing;

namespace Project3.WebApi.Areas.StudentHD.Controllers
{
    public class BaseController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var session = (UserLogin)Session[CommonConstants.USER_SESSION];

            if (session == null)
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Login", action = "Index", area = "Login" }));
            }
            if (session != null)
            {
                if (session.Level == 1)
                {
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Home", action = "Index", area = "Monitor" }));
                }
                else if (session.Level == 0)
                {
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Home", action = "Index", area = "Admin" }));
                }
            }
            base.OnActionExecuting(filterContext);
        }
    }
}