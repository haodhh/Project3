using System.Web.Mvc;

namespace Project3.WebApi.Areas.StudentHD
{
    public class StudentHDAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "StudentHD";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "StudentHD_default",
                "StudentHD/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                namespaces: new string[] { "Project3.WebApi.Areas.StudentHD.Controllers" }
            );
        }
    }
}