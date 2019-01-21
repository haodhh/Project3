using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Hosting;
using System.Web.Http;

namespace Project3.WebApi.Controllers
{
    [RoutePrefix("faceid")]
    public class FaceIDController : ApiController
    {
        private string subcriptionKey = "126e0d185ff843a78dc0a5a39b56d7fd";
        private string endPoint = "https://westcentralus.api.cognitive.microsoft.com/face/v1.0";
        private string groupId = "groupid22";
        private string personId = "";
        private string[] imageArray = new String[15];

        private int isFinishCreatePerson = 0;
        private int isFinishAddImage = 0;

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("sudo")]
        public int TestAPIAsync(int id = 0)
        {
            Thread.Sleep(5000);
            //string path = Path.Combine(HostingEnvironment.MapPath("~/Models/Students/"+ id.ToString()));
            imageArray[0] = "http://haodhh.somee.com/Models/Students/" + id.ToString() + "/0.jpg";
            imageArray[1] = "http://haodhh.somee.com/Models/Students/" + id.ToString() + "/1.jpg";
            imageArray[2] = "http://haodhh.somee.com/Models/Students/" + id.ToString() + "/2.jpg";
            imageArray[3] = "http://haodhh.somee.com/Models/Students/" + id.ToString() + "/3.jpg";
            imageArray[4] = "http://haodhh.somee.com/Models/Students/" + id.ToString() + "/4.jpg";
            imageArray[5] = "http://haodhh.somee.com/Models/Students/" + id.ToString() + "/5.jpg";
            imageArray[6] = "http://haodhh.somee.com/Models/Students/" + id.ToString() + "/6.jpg";
            imageArray[7] = "http://haodhh.somee.com/Models/Students/" + id.ToString() + "/7.jpg";
            imageArray[8] = "http://haodhh.somee.com/Models/Students/" + id.ToString() + "/8.jpg";
            imageArray[9] = "http://haodhh.somee.com/Models/Students/" + id.ToString() + "/9.jpg";
            imageArray[10] = "http://haodhh.somee.com/Models/Students/" + id.ToString() + "/10.jpg";
            imageArray[11] = "http://haodhh.somee.com/Models/Students/" + id.ToString() + "/11.jpg";
            imageArray[12] = "http://haodhh.somee.com/Models/Students/" + id.ToString() + "/12.jpg";
            imageArray[13] = "http://haodhh.somee.com/Models/Students/" + id.ToString() + "/13.jpg";
            imageArray[14] = "http://haodhh.somee.com/Models/Students/" + id.ToString() + "/14.jpg";
            //CreateGroup();
            CreatePerson(id);
            
            return 1;
        }

        /// <summary>
        /// Khởi tạo group
        /// </summary>
        private async void CreateGroup()
        {
            var client = new HttpClient();
            var queryString = HttpUtility.ParseQueryString(string.Empty);

            // Request headers
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", subcriptionKey);

            var uri = endPoint + "/persongroups/" + groupId + "?" + queryString;

            HttpResponseMessage response;

            // Request body
            byte[] byteData = Encoding.UTF8.GetBytes("{'name':'" + groupId + "','userData':'user'}");

            using (var content = new ByteArrayContent(byteData))
            {
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                response = await client.PutAsync(uri, content);
            }
        }

        /// <summary>
        /// Khởi tạo người
        /// </summary>
        private async void CreatePerson(int idstd)
        {
            var client = new HttpClient();
            var queryString = HttpUtility.ParseQueryString(string.Empty);

            // Request headers
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", subcriptionKey);

            var uri = endPoint + "/persongroups/" + groupId + "/persons?" + queryString;

            HttpResponseMessage response;

            // Request body
            byte[] byteData = Encoding.UTF8.GetBytes("{'name':'" + idstd.ToString() + "','userData':'Handsome boy'}");

            using (var content = new ByteArrayContent(byteData))
            {
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                response = await client.PostAsync(uri, content);

                string result = await response.Content.ReadAsStringAsync();

                string[] words = result.Split('"');

                personId = words[3];
            }
            Thread thread = new Thread(new ThreadStart(LoopAddFace));
            thread.Start();
        }

        private void LoopAddFace()
        {
            for (int i = 0; i < imageArray.Length; i++)
            {
                AddFace(imageArray[i]);
            }
            Training();
        }

        /// <summary>
        /// Thêm mặt cho người
        /// </summary>
        private async void AddFace(string image)
        {
            var client = new HttpClient();
            var queryString = HttpUtility.ParseQueryString(string.Empty);

            // Request headers
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", subcriptionKey);

            // Request parameters
            var uri = endPoint + "/persongroups/" + groupId + "/persons/" + personId + "/persistedFaces?" + queryString;

            HttpResponseMessage response;

            // Request body
            byte[] byteData = Encoding.UTF8.GetBytes("{'url': '" + image + "'}");

            using (var content = new ByteArrayContent(byteData))
            {
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                response = await client.PostAsync(uri, content);
            }
        }

        private async void Training()
        {
            var client = new HttpClient();
            var queryString = HttpUtility.ParseQueryString(string.Empty);

            // Request headers
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", subcriptionKey);

            var uri = endPoint + "/persongroups/" + groupId + "/train?" + queryString;

            HttpResponseMessage response;

            // Request body
            byte[] byteData = Encoding.UTF8.GetBytes("");

            using (var content = new ByteArrayContent(byteData))
            {
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                Thread.Sleep(5000);
                response = await client.PostAsync(uri, content);
            }
        }
    }
}