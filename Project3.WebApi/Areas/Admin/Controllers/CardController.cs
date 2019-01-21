using Project3.Service.ServiceBases;
using Project3.Web.Areas.Admin.Controllers;
using Project3.WebApi.Areas.Admin.Models;
using System.Web.Mvc;

namespace Project3.WebApi.Areas.Admin.Controllers
{
    public class CardController : BaseController
    {
        private ICardService cardService = new CardService();
        private IStudentService stdService = new StudentService();

        public ActionResult Index()
        {
            var csModel = new CardStudentModel();
            csModel.listCard = cardService.GetListAllCard();
            csModel.listStudent = stdService.GetListAllStudent();
            return View(csModel);
        }

        public ActionResult Create(string code = null, int idStd = -1, int status = -1)
        {
            if (code == null || code == "" || idStd < 0 || status < 0)
                ModelState.AddModelError("error", "ERROR!");
            else if (cardService.AddCard(code, idStd, status) == 0)
                ModelState.AddModelError("error", "Add OK!");
            else ModelState.AddModelError("error", "ERROR!");
            var csModel = new CardStudentModel();
            csModel.listCard = cardService.GetListAllCard();
            csModel.listStudent = stdService.GetListAllStudent();
            return View("Index", csModel);
        }

        public ActionResult Edit(int id = -1, string code = null, int idStd = -1, int status = -1)
        {
            if (id < 0 || code == null || code == "" || idStd < 0 || status < 0)
                ModelState.AddModelError("error", "ERROR!");
            else if (cardService.EditCard(id, code, idStd, status) == 0)
                ModelState.AddModelError("error", "Edit OK!");
            else ModelState.AddModelError("error", "ERROR!");
            var csModel = new CardStudentModel();
            csModel.listCard = cardService.GetListAllCard();
            csModel.listStudent = stdService.GetListAllStudent();
            return View("Index", csModel);
        }

        public ActionResult Delete(int id = -1)
        {
            if (id < 0)
                ModelState.AddModelError("error", "ERROR!");
            else if (cardService.DeleteCard(id) == 0)
                ModelState.AddModelError("error", "Delele OK!");
            else ModelState.AddModelError("error", "ERROR!");
            var csModel = new CardStudentModel();
            csModel.listCard = cardService.GetListAllCard();
            csModel.listStudent = stdService.GetListAllStudent();
            return View("Index", csModel);
        }
    }
}