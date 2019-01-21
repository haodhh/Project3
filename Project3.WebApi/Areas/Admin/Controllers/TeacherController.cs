using Project3.Service.ServiceBases;
using Project3.Web.Areas.Admin.Controllers;
using System.Web.Mvc;

namespace Project3.WebApi.Areas.Admin.Controllers
{
    public class TeacherController : BaseController
    {
        private ITeacherService teacherService = new TeacherService();

        public ActionResult Index()
        {
            return View(teacherService.GetListAllTeachers());
        }

        public ActionResult Create(string name = null, int age = -1, int gender = -1, string phone = null, string email = null, string address = null)
        {
            if (name == null || name == "" || age < 0 || gender < 0 || phone == null || email == null || address == null)
                ModelState.AddModelError("error", "ERROR!");
            else if (teacherService.AddTeacher(name, age, gender, phone, email, address) == 0)
                ModelState.AddModelError("error", "Add OK!");
            else ModelState.AddModelError("error", "ERROR!");
            return View("Index", teacherService.GetListAllTeachers());
        }

        public ActionResult Edit(int id = -1, string name = null, int age = -1, int gender = -1, string phone = null, string email = null, string address = null)
        {
            if (id < 0 || name == null || name == "" || age < 0 || gender < 0 || phone == null || email == null || address == null)
                ModelState.AddModelError("error", "ERROR!");
            else if (teacherService.EditTeacher(id, name, age, gender, phone, email, address) == 0)
                ModelState.AddModelError("error", "Edit OK!");
            else ModelState.AddModelError("error", "ERROR!");
            return View("Index", teacherService.GetListAllTeachers());
        }

        public ActionResult Delete(int id = -1)
        {
            if (id < 0)
                ModelState.AddModelError("error", "ERROR!");
            else if (teacherService.DelTeacher(id) == 0)
                ModelState.AddModelError("error", "Delete OK!");
            else ModelState.AddModelError("error", "ERROR!");
            return View("Index", teacherService.GetListAllTeachers());
        }
    }
}