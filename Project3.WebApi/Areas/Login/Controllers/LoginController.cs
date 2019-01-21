using Project3.Common;
using Project3.Service.ServiceLogics;
using Project3.Web.Areas.Login.Controllers;
using System.Web.Mvc;

namespace Project3.WebApi.Areas.Login.Controllers
{
    public class LoginController : BaseController
    {
        private ILoginService loginService = new LoginService();

        // GET: Login/Login
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login(string user = null, string pass = null)
        {
            if (user == null || user == "" || pass == null || pass == "")
                ModelState.AddModelError("error", "Tài khoản hoặc Mật khẩu không đúng!");
            else if (loginService.CheckLogin(user, pass))
            {
                var userSession = new UserLogin();
                var acc = loginService.GetAccount(user);
                userSession.UserName = acc.User;
                userSession.Level = acc.Level;

                if (userSession.Level == 0)
                {
                    Session.Add(CommonConstants.USER_SESSION, userSession);
                    return RedirectToAction("Index", "Home", new { area = "Admin" });
                }
                else if (userSession.Level == 1)
                {
                    Session.Add(CommonConstants.USER_SESSION, userSession);
                    return RedirectToAction("Index", "Home", new { area = "Monitor" });
                }
                else if (userSession.Level == 2)
                {
                    Session.Add(CommonConstants.USER_SESSION, userSession);
                    return RedirectToAction("Index", "Home", new { area = "StudentHD" });
                }
            }
            else ModelState.AddModelError("error", "Tài khoản hoặc Mật khẩu không đúng!");
            return View("Index");
        }
    }
}