using System.Web.Mvc;

namespace Project3.WebApi.Areas.Monitor
{
    public class MonitorAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Monitor";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Monitor_default",
                "Monitor/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                namespaces: new string[] { "Project3.WebApi.Areas.Monitor.Controllers" }
            );
        }
    }
}