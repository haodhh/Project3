using Project3.Web.Areas.Admin.Controllers;
using System.Web.Mvc;

namespace Project3.WebApi.Areas.Admin.Controllers
{
    public class HomeController : BaseController
    {
        // GET: Admin/Home
        public ActionResult Index(string type)
        {
            ViewBag.text = type;
            if (TempData["shortMessage"] != null)
            {
                ViewBag.text = TempData["shortMessage"].ToString();

            }
            return View();
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index", "Login", new { area = "Login" });
        }

        public ActionResult Monitor(string type)
        {
            TempData["shortMessage"] = type;
            return RedirectToAction("Index", "Home", new { area = "" });
        }
    }
}