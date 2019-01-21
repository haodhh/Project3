using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication9.Controllers
{
    public class HomeController : Controller
    {
        [Route("home/index")]
        [HttpGet]
        public ActionResult Index(string type = "Monitoring")
        {
            //var monitoring = "Monitoring";
            //var history = "History";

            ViewBag.Title = "Home Page";
            ViewBag.text = type;
            return View();
        }
    }
}
