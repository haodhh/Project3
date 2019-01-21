using Project3.Service.ServiceBases;
using Project3.Web.Areas.Admin.Controllers;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace Project3.WebApi.Areas.Admin.Controllers
{
    public class StudentController : BaseController
    {
        private IStudentService studentService = new StudentService();

        public ActionResult Index()
        {
            return View(studentService.GetListAllStudent());
        }

        public ActionResult Create(string name = null, int age = -1, int gender = -1, string phone = null, string email = null, string address = null, string user = null)
        {
            if (name == null || name == "" || age < 0 || gender < 0 || phone == null || email == null || address == null || user == null || user == "")
                ModelState.AddModelError("error", "ERROR OK!");
            else if (studentService.AddStudent(name, age, gender, phone, email, address, user) == 0)
                ModelState.AddModelError("error", "Add OK!");
            else ModelState.AddModelError("error", "ERROR!");
            return View("Index", studentService.GetListAllStudent());
        }

        public ActionResult Edit(int id = -1, string name = null, int age = -1, int gender = -1, string phone = null, string email = null, string address = null)
        {
            if (id < 0 || name == null || name == "" || age < 0 || gender < 0 || phone == null || email == null || address == null)
                ModelState.AddModelError("error", "ERROR OK!");
            else if (studentService.EditStudent(id, name, age, gender, phone, email, address) == 0)
                ModelState.AddModelError("error", "Edit OK!");
            else ModelState.AddModelError("error", "ERROR!");
            return View("Index", studentService.GetListAllStudent());
        }

        public ActionResult Delete(int id = -1)
        {
            if (id < 0)
                ModelState.AddModelError("error", "ERROR OK!");
            else if (studentService.DeleteStudent(id) == 0)
                ModelState.AddModelError("error", "Delete OK!");
            else ModelState.AddModelError("error", "ERROR!");
            return View("Index", studentService.GetListAllStudent());
        }
        
    }
}