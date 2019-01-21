using Project3.Common;
using Project3.Data.OtherModel;
using Project3.Service.ServiceLogics;
using System.Web.Mvc;

namespace Project3.WebApi.Areas.StudentHD.Controllers
{
    public class LearnController : BaseController
    {
        private IScheduleService service2 = new ScheduleService();
        private IRegisterClassService service = new RegisterClassService();

        // GET: StudentHD/Learn
        public ActionResult Index()
        {
            var session = (UserLogin)Session[CommonConstants.USER_SESSION];
            int idStd = service.FindStudent(session.UserName);

            var model = new DetailScheduleStudent();
            model.listSchedule = service.GetResultLearn(idStd);
            model.listCar = service2.GetListAllCar();
            model.listStudent = service2.GetListAllStudent();
            model.listTeacher = service2.GetListAllTeacher();
            return View("Index", model);
        }
    }
}