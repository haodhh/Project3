using Project3.Common;
using Project3.Data.OtherModel;
using Project3.Service.ServiceLogics;
using System.Web.Mvc;

namespace Project3.WebApi.Areas.StudentHD.Controllers
{
    public class ClassController : BaseController
    {
        private IScheduleService service = new ScheduleService();
        private IRegisterClassService service2 = new RegisterClassService();

        public ActionResult Index(int month = 1, int year = 2019)
        {
            var ssModel = new ClassModel();
            ssModel.schedule = service.GetListManySchedule(null, 0, 0, month, year, 0, 0, 0, 0);
            ssModel.month = month;
            ssModel.year = year;
            return View("Index", ssModel);
        }

        public ActionResult Detail(int hour = 0, int day = 0, int month = 0, int year = 0)
        {
            if (hour < 0 || hour > 4 || day < 0 || day > 30 || month < 0 || month > 12 || year < 0 || year > 2019)
            {
                var ssModel = new ClassModel();
                ssModel.schedule = service.GetListManySchedule(null, 0, 0, month, year, 0, 0, 0, 0);
                ssModel.month = month;
                ssModel.year = year;
                return View("Index", ssModel);
            }
            var model = new DetailScheduleStudent();
            model.listSchedule = service.GetListManySchedule(null, hour, day, month, year, 0, 0, 0, 0);
            model.listCar = service.GetListAllCar();
            model.listStudent = service.GetListAllStudent();
            model.listTeacher = service.GetListAllTeacher();
            model.hour = hour;
            model.day = day;
            model.month = month;
            model.year = year;
            return View("Detail", model);
        }

        public ActionResult Register(int id = -1, int hour = 0, int day = 0, int month = 0, int year = 0)
        {
            var session = (UserLogin)Session[CommonConstants.USER_SESSION];
            int idStd = service2.FindStudent(session.UserName);
            if (id < 0 || hour < 1 || hour > 4 || day < 1 || day > 30 || year < 2016 || year > 2019)
                ModelState.AddModelError("error", "ERROR!");
            else if (service2.RegisterClass(idStd, id))
                ModelState.AddModelError("error", "Add OK!");
            else ModelState.AddModelError("error", "ERROR!");
            return Detail(hour, day, month, year);
        }
    }
}