using Project3.Data.OtherModel;
using Project3.Service.ServiceBases;
using Project3.Service.ServiceLogics;
using Project3.Web.Areas.Admin.Controllers;
using System.Web.Mvc;

namespace Project3.WebApi.Areas.Admin.Controllers
{
    public class RepostController : BaseController
    {
        private IRepostService service = new RepostService();
        private ICarService service1 = new CarService();
        private IStudentService service2 = new StudentService();
        private ITeacherService service3 = new TeacherService();


        public ActionResult Index(int hour = 0, int day = 0, int month = 0, int year = 0, int idCar = 0, int idStd = 0, int idTea = 0)
        {
            var model = new DetailScheduleStudent();

            if (hour < 0 || hour > 4 || day < 0 || day > 30 || month < 0 || month > 12 || year < 2016 || year > 2019 || idCar < 0 || idStd < 0 || idTea < 0)
                model.listSchedule = service.listRepost(0, 0, 0, 0, 0, 0, 0);
            model.listSchedule = service.listRepost(hour, day, month, year, idCar, idStd, idTea);

            model.listCar = service1.GetListAllCar();
            model.listStudent = service2.GetListAllStudent();
            model.listTeacher = service3.GetListAllTeachers();

            model.hour = hour;
            model.day = day;
            model.month = month;
            model.year = year;

            return View(model);
        }
    }
}