using Project3.Data.Database;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Project3.WebApi.Controllers
{
    [RoutePrefix("monitoring")]
    public class MonitoringController : ApiController
    {
        [HttpGet]
        [Route("getCurrentState")]
        public async Task<HttpResponseMessage> GetCurrentState()
        {
            HttpResponseMessage res = new HttpResponseMessage();
            try
            {
                ProjectDbContext db = new ProjectDbContext();
                List<CurrentState> data = db.CurrentStates.ToList();
                res = Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                res = Request.CreateResponse(ex.Message);
            }
            return await Task.FromResult(res);
        }

        /// <summary>
        /// Kiem tra xe dung hoc
        /// </summary>
        /// <returns></returns>
        [HttpDelete]
        [Route("finishStudy")]
        public async Task<HttpResponseMessage> FinishStudy()
        {
            HttpResponseMessage res = new HttpResponseMessage();
            try
            {
            }
            catch (Exception ex)
            {
                res = Request.CreateResponse(ex.Message);
            }
            return await Task.FromResult(res);
        }

        /// <summary>
        /// Cập nhật trạng thái mới vào db
        /// </summary>
        /// <param name="newState"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("updateState")]
        public async Task<HttpResponseMessage> UpdateState([FromBody] CurrentState newState)
        {
            HttpResponseMessage res = new HttpResponseMessage();
            try
            {
                ProjectDbContext db = new ProjectDbContext();

                // update vào bảng currentState
                var currentState = (from cs in db.CurrentStates
                                    where cs.carID == newState.carID
                                    select cs
                                    );

                if (currentState.ToList().Count > 0)
                {
                    var cs = currentState.First();
                    cs.coordinate = newState.coordinate;
                    cs.currentTime = DateTime.Now;
                    cs.location = newState.location;
                    cs.speed = newState.speed;
                    cs.state = newState.state;

                    db.CurrentStates.Add(cs);
                }
                else
                {
                    CurrentState cs = new CurrentState
                    {
                        coordinate = newState.coordinate,
                        currentTime = DateTime.Now,
                        location = newState.location,
                        speed = newState.speed,
                        state = newState.state,
                        carID = newState.carID,
                        studentID = newState.studentID
                    };
                    db.CurrentStates.Add(cs);
                }

                db.SaveChanges();

                res = Request.CreateResponse(HttpStatusCode.OK, 1);
            }
            catch (Exception ex)
            {
                res = Request.CreateResponse(ex.Message);
            }
            return await Task.FromResult(res);
        }
    }
}