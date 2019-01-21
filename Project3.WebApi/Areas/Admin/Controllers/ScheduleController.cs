using Project3.Service.ServiceLogics;
using Project3.Web.Areas.Admin.Controllers;
using Project3.WebApi.Areas.Admin.Models;
using System.Web.Mvc;

namespace Project3.WebApi.Areas.Admin.Controllers
{
    public class ScheduleController : BaseController
    {
        private IScheduleService scheduleService = new ScheduleService();

        public ActionResult Status(int month = 1, int year = 2019)
        {
            if (month < 1 || year > 2019 || year < 2016)
                ModelState.AddModelError("error", "ERROR!");
            var ssModel = new StatusScheduleModel();
            ssModel.schedule = scheduleService.GetListManySchedule(null, 0, 0, month, year, 0, 0, 0);
            ssModel.month = month;
            ssModel.year = year;
            return View("Status", ssModel);
        }

        public ActionResult Index()
        {
            var scheduleModel = new ScheduleModel();
            scheduleModel.listSchedule = scheduleService.GetListAllSchedule();
            scheduleModel.listCar = scheduleService.GetListAllCar();
            scheduleModel.listStudent = scheduleService.GetListAllStudent();
            scheduleModel.listTeacher = scheduleService.GetListAllTeacher();
            return View(scheduleModel);
        }

        public ActionResult Find(string code = null, int hour = 0, int day = 0, int month = 0, int year = 0, int idCar = 0, int idStudent = 0, int idTeacher = 0)
        {
            var scheduleModel = new ScheduleModel();
            if (hour < 0 || hour > 4 || day < 0 || day > 30 || month < 0 || month > 12 || year < 0 || year > 2019 || idCar < 0 || idStudent < 0 || idTeacher < 0)
            {
                ModelState.AddModelError("error", "ERROR!");
                scheduleModel.listSchedule = null;
            }
            else scheduleModel.listSchedule = scheduleService.GetListManySchedule(code, hour, day, month, year, idCar, idStudent, idTeacher);
            scheduleModel.listCar = scheduleService.GetListAllCar();
            scheduleModel.listStudent = scheduleService.GetListAllStudent();
            scheduleModel.listTeacher = scheduleService.GetListAllTeacher();
            return View("Index", scheduleModel);
        }

        public ActionResult Create(string code = null, int hour = -1, int day = -1, int month = -1, int year = -1, int idCar = -1, int idStudent = -1, int idTeacher = -1)
        {
            if (code == null || code == "" || hour < 1 || hour > 4 || day < 1 || day > 30 || month < 1 || month > 12 || year < 2016 || year > 2019 || idCar < 0 || idStudent < 0 || idTeacher < 0)
                ModelState.AddModelError("error", "ERROR!");
            else
            {
                var result = scheduleService.AddSchedule(code, hour, day, month, year, idCar, idStudent, idTeacher);
                if (result == 0)
                    ModelState.AddModelError("error", "Add OK!");
                else if (result == 1)
                    ModelState.AddModelError("error", "Duplicate!");
                else ModelState.AddModelError("error", "ERROR!");
            }

            return Find(null, hour, day, month, year, 0, 0, 0);
        }

        public ActionResult Edit(int id = -1, string code = null, int hour = -1, int day = -1, int month = -1, int year = -1, int idCar = -1, int idStudent = -1, int idTeacher = -1)
        {
            if (id < 0 || code == null || code == "" || hour < 1 || hour > 4 || day < 1 || day > 30 || month < 1 || month > 12 || year < 2016 || year > 2019 || idCar < 0 || idStudent < 0 || idTeacher < 0)
                ModelState.AddModelError("error", "ERROR!");
            else
            {
                var result = scheduleService.EditSchedule(id, code, hour, day, month, year, idCar, idStudent, idTeacher);
                if (result == 0)
                    ModelState.AddModelError("error", "Edit OK!");
                else if (result == 1)
                    ModelState.AddModelError("error", "Not Found!");
                else if (result == 3)
                    ModelState.AddModelError("error", "Duplicate!");
                else ModelState.AddModelError("error", "ERROR!");
            }

            return Find(null, hour, day, month, year, 0, 0, 0);
        }

        public ActionResult Delete(int id = -1)
        {
            if (id < 0)
                ModelState.AddModelError("error", "ERROR!");
            else
            {
                var result = scheduleService.DelSchedule(id);
                if (result == 0)
                    ModelState.AddModelError("error", "Delete OK!");
                else if (result == 1)
                    ModelState.AddModelError("error", "Not Found!");
                else ModelState.AddModelError("error", "ERROR!");
            }
            var scheduleModel = new ScheduleModel();
            scheduleModel.listSchedule = scheduleService.GetListAllSchedule();
            scheduleModel.listCar = scheduleService.GetListAllCar();
            scheduleModel.listStudent = scheduleService.GetListAllStudent();
            scheduleModel.listTeacher = scheduleService.GetListAllTeacher();
            return View("Index", scheduleModel);
        }
    }
}