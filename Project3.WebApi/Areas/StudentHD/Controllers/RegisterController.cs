using Project3.Common;
using Project3.Data.OtherModel;
using Project3.Service.ServiceLogics;
using System.Web.Mvc;

namespace Project3.WebApi.Areas.StudentHD.Controllers
{
    public class RegisterController : BaseController
    {
        private IScheduleService service2 = new ScheduleService();
        private IRegisterClassService service = new RegisterClassService();

        // GET: StudentHD/Register
        public ActionResult Index()
        {
            var session = (UserLogin)Session[CommonConstants.USER_SESSION];
            int idStd = service.FindStudent(session.UserName);

            var model = new DetailScheduleStudent();
            model.listSchedule = service.GetListSchedule(idStd);
            model.listCar = service2.GetListAllCar();
            model.listStudent = service2.GetListAllStudent();
            model.listTeacher = service2.GetListAllTeacher();
            return View("Index", model);
        }

        public ActionResult CancelRegister(int id = -1)
        {
            if (id < 0)
            {
                ModelState.AddModelError("error", "ERROR!");
                return Index();
            }
            var session = (UserLogin)Session[CommonConstants.USER_SESSION];
            int idStd = service.FindStudent(session.UserName);
            if (service.CancelRegisterClass(idStd, id))
                ModelState.AddModelError("error", "OK!");
            else ModelState.AddModelError("error", "ERROR!");
            return Index();
        }
    }
}