using Project3.Data.Database;
using Project3.WebApi.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Project3.WebApi.Areas.Admin.Controllers
{
    public class TrainingController : Controller
    {
        // GET: Admin/Training
        public ActionResult Index()
        {
            ProjectDbContext dbContext = new ProjectDbContext();
            IEnumerable<Student> list = dbContext.Students.Where(x => x.Model == null).ToList();
            ModelStudent model = new ModelStudent();
            model.list = list;
            return View("Index", model);
        }

        [HttpPost]
        public async Task<ActionResult> UploadFile(int idStd, HttpPostedFileBase[] files)
        {
            ProjectDbContext dbContext = new ProjectDbContext();
          
            try
            {
                string directoryPath = Path.Combine(Server.MapPath("~/Models/Students/"), idStd.ToString());
                if (!Directory.Exists(directoryPath))
                    Directory.CreateDirectory(directoryPath);
                var n = 0;
                foreach (HttpPostedFileBase file in files)
                {
                    if (file != null)
                    {
                        
                        string fileName = Path.GetFileName(file.FileName);
                        string path = Path.Combine(Server.MapPath("~/Models/Students/" + idStd.ToString()), n.ToString() + ".jpg");
                        file.SaveAs(path);
                        n++;
                        path = Path.Combine(Server.MapPath("~/Models/Students/" + idStd.ToString()), n.ToString() + ".jpg");
                        file.SaveAs(path);
                        n++;
                        path = Path.Combine(Server.MapPath("~/Models/Students/" + idStd.ToString()), n.ToString() + ".jpg");
                        file.SaveAs(path);
                        n++;
                    }
                }

                CallApi(idStd);

                dbContext.Students.SingleOrDefault(x => x.ID == idStd).Model = "~/Models/Students/" + idStd.ToString();
                dbContext.SaveChanges();

                ModelState.AddModelError("error", "Add OK: " + idStd.ToString());
                return Index();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("error", ex.ToString());
                return Index();
            }
        }

        public async void CallApi(int id)
        {
            Thread.Sleep(5000);
            string apiUrl = "http://localhost:52866/faceid/sudo?id=" + id.ToString();

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    var table = Newtonsoft.Json.JsonConvert.DeserializeObject<System.Data.DataTable>(data);
                }
            }
            return;
        }
    }
}