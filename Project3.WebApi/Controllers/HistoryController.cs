using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using Project3.Data.Database;

namespace Project3.WebApi.Controllers
{
    [RoutePrefix("history")]
    public class HistoryController : ApiController
    {
        private const int INTERVAL_WRITE_HISTORY = 600;
        /// <summary>
        /// Yêu cầu lấy lịch sử
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("getDataHistoryByCarID")]
        public async Task<HttpResponseMessage> GetDataHistoryByCarID([FromBody] History history)
        {
            HttpResponseMessage res = new HttpResponseMessage();
            try
            {
                ProjectDbContext db = new ProjectDbContext();
                List<History> data = (from ht
                                      in db.Historys
                                      where ht.day == history.day
                                      where ht.month == history.month
                                      where ht.year == history.year
                                      where ht.carID == history.carID
                                      select ht
                                      ).ToList();
                res = Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                res = Request.CreateResponse(ex.Message);
            }
            return await Task.FromResult(res);
        }

        [HttpPost]
        [Route("getDataHistoryByStudentID")]
        public async Task<HttpResponseMessage> GetDataHistoryByStudentID([FromBody] History history)
        {
            HttpResponseMessage res = new HttpResponseMessage();
            try
            {
                ProjectDbContext db = new ProjectDbContext();
                List<History> data = (from ht
                                      in db.Historys
                                      where ht.day == history.day
                                      where ht.month == history.month
                                      where ht.year == history.year
                                      where ht.studentID == history.studentID
                                      select ht
                                      ).ToList();
                res = Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                res = Request.CreateResponse(ex.Message);
            }
            return await Task.FromResult(res);
        }

        /// <summary>
        /// Yêu cầu ghi lịch sử
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("writeDataHistory")]
        public async Task<HttpResponseMessage> WriteDataHistory(string carID)
        {
            HttpResponseMessage res = new HttpResponseMessage();
            try
            {
                var cancellationTokenSource = new CancellationTokenSource();
                var task = Repeat.Interval(
                        TimeSpan.FromSeconds(INTERVAL_WRITE_HISTORY),
                        () => UpdateHistory(carID), cancellationTokenSource.Token);
                res = Request.CreateResponse(HttpStatusCode.OK, 1);
            }
            catch (Exception ex)
            {
                res = Request.CreateResponse(ex.Message);
            }
            return await Task.FromResult(res);
        }

        /// <summary>
        /// Hàm cập nhật lịch sử
        /// </summary>
        /// <param name="carID"></param>
        public void UpdateHistory(string carID)
        {
            ProjectDbContext db = new ProjectDbContext();
            CurrentState currentState = (from cs in db.CurrentStates
                                         where cs.carID == carID
                                         select cs
                                        ).First();
            if (currentState != null)
            {
                History history = new History
                {
                    carID = currentState.carID,
                    coordinate = currentState.coordinate,
                    location = currentState.location,
                    speed = currentState.speed,
                    state = currentState.state,
                    studentID = currentState.studentID,
                    studentName = currentState.studentName,
                    exactTime = currentState.currentTime,
                    day = currentState.currentTime.Day,
                    month = currentState.currentTime.Month,
                    year = currentState.currentTime.Year,
                    historyID = Guid.NewGuid()
                };
                db.Historys.Add(history);
                try
                {
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    return;
                }
            }
            return;
        }
    }

    /// <summary>
    /// Vòng lặp cập nhật lịch sử
    /// </summary>
    internal static class Repeat
    {
        public static Task Interval(TimeSpan pollInterval, Action action, CancellationToken token)
        {
            return Task.Factory.StartNew(
                () =>
                {
                    for (; ; )
                    {
                        if (token.WaitCancellationRequested(pollInterval))
                            break;

                        action();
                    }
                }, token, TaskCreationOptions.LongRunning, TaskScheduler.Default);
        }
    }

    static class CancellationTokenExtensions
    {
        public static bool WaitCancellationRequested(
            this CancellationToken token,
            TimeSpan timeout)
        {
            return token.WaitHandle.WaitOne(timeout);
        }
    }
}
