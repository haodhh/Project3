using Project3.Common;
using System.Web.Mvc;
using System.Web.Routing;

namespace Project3.Web.Areas.Login.Controllers
{
    public class BaseController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var session = (UserLogin)Session[CommonConstants.USER_SESSION];

            if (session != null)
            {
                if (session.Level == 0)
                {
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Home", action = "Index", area = "Admin" }));
                }
                else if (session.Level == 1)
                {
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Home", action = "Index", area = "Monitor" }));
                }
                else if (session.Level == 2)
                {
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Home", action = "Index", area = "StudentHD" }));
                }
            }
            base.OnActionExecuting(filterContext);
        }
    }
}