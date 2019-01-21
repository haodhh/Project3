using Project3.Service.ServiceBases;
using Project3.Web.Areas.Admin.Controllers;
using System.Web.Mvc;

namespace Project3.WebApi.Areas.Admin.Controllers
{
    public class CarController : BaseController
    {
        private ICarService carService = new CarService();

        public ActionResult Index()
        {
            return View(carService.GetListAllCar());
        }

        public ActionResult Create(string number = null, int type = -1, int status = -1)
        {
            if (number == null || number == "" || type < 0 || status < 0)
                ModelState.AddModelError("error", "ERROR!");
            else if (carService.AddCar(number, type, status) == 0)
                ModelState.AddModelError("error", "Add OK!");
            else ModelState.AddModelError("error", "ERROR!");
            return View("Index", carService.GetListAllCar());
        }

        public ActionResult Edit(int id = -1, string number = null, int type = -1, int status = -1)
        {
            if (id < 0 || number == null || number == "" || type < 0 || status < 0)
                ModelState.AddModelError("error", "ERROR!");
            else if (carService.EditCar(id, number, type, status) == 0)
                ModelState.AddModelError("error", "Edit OK!");
            else ModelState.AddModelError("error", "ERROR!");
            return View("Index", carService.GetListAllCar());
        }

        public ActionResult Delete(int id = -1)
        {
            if (id < 0)
                ModelState.AddModelError("error", "ERROR!");
            else if (carService.DeleteCar(id) == 0)
                ModelState.AddModelError("error", "Delele OK!");
            else ModelState.AddModelError("error", "ERROR!");
            return View("Index", carService.GetListAllCar());
        }
    }
}