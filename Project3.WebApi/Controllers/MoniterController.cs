using Project3.Common;
using Project3.Data.Database;
using Project3.Service.ServiceMonitor;
using Project3.WebApi.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Project3.WebApi.Controllers
{
    public class MoniterController : ApiController
    {
        private IMonitorService service = new MonitorService();

        [Route("api/finish")]
        [HttpGet]
        public int Finish(string key = null, string idSchedule = null)
        {
            if(key == null || idSchedule == null)
            {
                return 0;
            }
            if (CheckKey(key))
            {
                ProjectDbContext dbContext = new ProjectDbContext();
                int idSche = dbContext.Schedules.Where(x => x.Code == idSchedule).First().ID;
                int idCar = dbContext.Schedules.Where(x => x.Code == idSchedule).First().IDCar;
                int yes = dbContext.Historys.Where(x => x.scheduleID == idSche).Where(x => x.state == 1).Count();
                int total = dbContext.Historys.Where(x => x.scheduleID == idSche).Count();

                int score = yes * 100 / total;

                dbContext.Schedules.SingleOrDefault(x => x.ID == idSche).Status = score;
                dbContext.SaveChanges();


                string numberCar = dbContext.Cars.SingleOrDefault(x => x.ID == idCar).NumberCar;
                CurrentState currentState = dbContext.CurrentStates.SingleOrDefault(x => x.carID == numberCar);
                dbContext.CurrentStates.Remove(currentState);

                return 1;
            }
            return 0;
        }

        [Route("api/updatemonitor")]
        [HttpGet]
        public int UpdateMonitor(string key = null, string idSchedule = null, string location = null, string coordinate = null, float speed = -1, int status = -1)
        {
            if (key == null || idSchedule == null || location == null || coordinate == null || speed < 0 || (status != 0 && status != 1))
                return 0;
            if (CheckKey(key))
            {
                try
                {
                    ProjectDbContext dbContext = new ProjectDbContext();
                    
                    int idSche = dbContext.Schedules.Where(x => x.Code == idSchedule).First().ID;
                    int idCar = dbContext.Schedules.Where(x => x.Code == idSchedule).First().IDCar;
                    int idStudent = dbContext.Schedules.Where(x => x.Code == idSchedule).First().IDStudent;
                    
                    string numberCar = dbContext.Cars.SingleOrDefault(x => x.ID == idCar).NumberCar;
                    string studentName = dbContext.Students.SingleOrDefault(x => x.ID == idStudent).Name;

                    // update online
                    if (dbContext.CurrentStates.SingleOrDefault(x => x.carID == numberCar) == null)
                    {
                        CurrentState currentState = new CurrentState();
                        currentState.carID = numberCar;
                        currentState.state = status;
                        currentState.studentID = idStudent;
                        currentState.studentName = studentName;
                        currentState.location = location;
                        currentState.coordinate = coordinate;
                        currentState.currentTime = DateTime.Now;
                        dbContext.CurrentStates.Add(currentState);
                        dbContext.SaveChanges();
                    }
                    else
                    {
                        CurrentState currentState = dbContext.CurrentStates.SingleOrDefault(x => x.carID == numberCar);
                        currentState.state = status;
                        currentState.studentID = idStudent;
                        currentState.studentName = studentName;
                        currentState.location = location;
                        currentState.coordinate = coordinate;
                        currentState.currentTime = DateTime.Now;
                        dbContext.SaveChanges();
                    }

                    // update history
                    History history = new History();
                    history.carID = numberCar;
                    history.carName = numberCar;
                    history.studentID = idStudent;
                    history.studentName = studentName;
                    history.scheduleID = idSche;
                    history.exactTime = DateTime.Now;
                    history.location = location;
                    history.coordinate = coordinate;
                    history.state = status;
                    history.speed = speed;
                    history.day = DateTime.Now.Day;
                    history.month = DateTime.Now.Month;
                    history.year = DateTime.Now.Year;

                    dbContext.Historys.Add(history);
                    dbContext.SaveChanges();

                    return 1;
                }
                catch
                {
                    return 0;
                }
            }
            return 0;
        }

        // idCar là biển số xe
        // idStudent là user name, mã số học viên

        [Route("api/getmodel")]
        [HttpGet]
        public ModelLearn GetModel(string key = null, string card = null, string idSche = null)
        {
            if (key == null || card == null || idSche == null)
                return null;

            if (CheckKey(key))
            {
                try
                {
                    ProjectDbContext dbContext = new ProjectDbContext();
                    ModelLearn model = new ModelLearn();
                    model.IDStudent = dbContext.RFIDCards.Where(x => x.CodeCard == card).First().IDStudent;
                    model.StudentName = dbContext.Students.SingleOrDefault(x => x.ID == model.IDStudent).Name;
                    model.IDCar = dbContext.Schedules.Where(x => x.Code == idSche).First().IDCar;
                    model.BKS = dbContext.Cars.SingleOrDefault(x => x.ID == model.IDCar).NumberCar;
                    model.Description = "Đẹp trai, học giỏi!!!";
                    model.Mail = dbContext.Students.SingleOrDefault(x => x.ID == model.IDStudent).Email;
                    model.SDT = dbContext.Students.SingleOrDefault(x => x.ID == model.IDStudent).Phone;
                    return model;
                }
                catch
                {
                    return null;
                }
            }
            return null;
        }

        public bool CheckKey(string key)
        {
            ProjectDbContext dbContext = new ProjectDbContext();
            if (CryptoSHA.GenerateSHA512String(key) == dbContext.KeyMonitors.SingleOrDefault(x => x.ID == 1).Key)
                return true;
            return false;
        }

        /*
            key:
                UZ3J8D8F5MM84LZW6WZZWP8T6
        */

        [Route("api/getstudent")]
        [HttpGet]
        public IEnumerable<Student> GetAllStudent()
        {
            ProjectDbContext dbContext = new ProjectDbContext();
            return dbContext.Students.ToList();
        }

    }
}