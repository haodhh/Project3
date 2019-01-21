using Project3.Common;
using Project3.Service.ServiceBases;
using Project3.Service.ServiceLogics;
using System.Web.Mvc;

namespace Project3.WebApi.Areas.StudentHD.Controllers
{
    public class HomeController : BaseController
    {
        private IRegisterClassService service = new RegisterClassService();
        private IStudentService service2 = new StudentService();
        private IChangePassService service3 = new ChangePassService();

        // GET: StudentHD/Home
        public ActionResult Index()
        {
            var session = (UserLogin)Session[CommonConstants.USER_SESSION];
            int idStd = service.FindStudent(session.UserName);
            return View("Index", service2.GetStudent(idStd));
        }

        public ActionResult ChangePass(string p1 = null, string p2 = null, string p3 = null)
        {
            if (p1 == null || p2 == null || p3 == null || p1 == "" || p2 == "" || p3 == "")
            {
                ModelState.AddModelError("error", "Chưa nhập đủ các trường!");
                return Index();
            }
            var session = (UserLogin)Session[CommonConstants.USER_SESSION];
            int idStd = service.FindStudent(session.UserName);
            var acc = service2.GetStudent(idStd);
            if (p2 != p3)
                ModelState.AddModelError("error", "Mật khẩu nhập lại không đúng!");
            else if (service3.ChangePass(acc.User, p1, p2))
                ModelState.AddModelError("error", "Thay đổi mật khẩu thành công!");
            else
                ModelState.AddModelError("error", "Mật khẩu cũ không đúng!");
            return View("Index", service2.GetStudent(idStd));
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index", "Login", new { area = "Login" });
        }
    }
}