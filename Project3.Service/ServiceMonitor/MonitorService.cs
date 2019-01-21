using Project3.Common;
using Project3.Data.Database;
using System;
using System.Linq;

namespace Project3.Service.ServiceMonitor
{
    public class MonitorService : IMonitorService
    {
        private ProjectDbContext dbContext = new ProjectDbContext();

        public bool checkKey(string key)
        {
            key = CryptoSHA.GenerateSHA512String(key);
            if (dbContext.KeyMonitors.Where(x => x.Key == key).Count() > 0)
                return true;
            return false;
        }

        public Student GetStudent(string codeCard)
        {
            var card = dbContext.RFIDCards.Where(x => x.CodeCard == codeCard).First();
            return dbContext.Students.SingleOrDefault(x => x.ID == card.IDStudent);
        }

        public bool checkSchedule(string code, string idCar, string idStd)
        {
            try
            {
                var schedule = dbContext.Schedules.Where(x => x.Code == code).First();
                if (schedule == null)
                    return false;
                var car = dbContext.Cars.Where(x => x.NumberCar == idCar).First();
                if (car == null)
                    return false;
                var student = dbContext.Students.Where(x => x.User == idStd).First();
                if (student == null)
                    return false;

                if (schedule.IDCar != car.ID)
                    return false;
                if (schedule.IDStudent != student.ID)
                    return false;

                return true;
            }
            catch
            {
                return false;
            }
        }

        //public bool UpdateMonitor(string code, int locationX, int locationY, int speed, int status)
        //{
        //    try
        //    {
        //        var schedule = dbContext.Schedules.Where(x => x.Code == code).First();

        //        var monitorOn = dbContext.MonitorOnlines.Where(x => x.IDCar == schedule.IDCar).First();
        //        var monitorOf = new MonitorHistory();

        //        var date = DateTime.Now;

        //        monitorOn.IDSchedule = schedule.ID;
        //        monitorOn.IDCar = schedule.IDCar;
        //        monitorOn.IDStudent = schedule.IDStudent;
        //        monitorOn.LocationX = locationX;
        //        monitorOn.LocationY = locationY;
        //        monitorOn.Speed = speed;
        //        monitorOn.Status = status;
        //        monitorOn.Second = date.Second;
        //        monitorOn.Minute = date.Minute;
        //        monitorOn.Hour = date.Hour;
        //        monitorOn.Day = date.Day;
        //        monitorOn.Month = date.Month;
        //        monitorOn.Year = date.Year;

        //        monitorOf.IDSchedule = schedule.ID;
        //        monitorOf.IDCar = schedule.IDCar;
        //        monitorOf.IDStudent = schedule.IDStudent;
        //        monitorOf.LocationX = locationX;
        //        monitorOf.LocationY = locationY;
        //        monitorOf.Speed = speed;
        //        monitorOf.Status = status;
        //        monitorOf.Second = date.Second;
        //        monitorOf.Minute = date.Minute;
        //        monitorOf.Hour = date.Hour;
        //        monitorOf.Day = date.Day;
        //        monitorOf.Month = date.Month;
        //        monitorOf.Year = date.Year;

        //        dbContext.MonitorHistorys.Add(monitorOf);
        //        dbContext.SaveChanges();

        //        return true;
        //    }
        //    catch
        //    {
        //        return false;
        //    }
        //}

        public bool checkScheduleAlone(string code)
        {
            try
            {
                var schedule = dbContext.Schedules.Where(x => x.Code == code).First();
                if (schedule == null)
                    return false;
                return true;
            }
            catch
            {
                return false;
            }
        }

        //public bool CalculatorPoint(string code)
        //{
        //    try
        //    {
        //        var schedule = dbContext.Schedules.Where(x => x.Code == code).First();

        //        var x1 = dbContext.MonitorHistorys.Where(x => x.IDSchedule == schedule.ID).Where(x => x.Status == 1).Count();
        //        var x2 = dbContext.MonitorHistorys.Where(x => x.IDSchedule == schedule.ID).Count();

        //        schedule.Status = x1 * 100 / x2;
        //        dbContext.SaveChanges();
        //        return true;
        //    }
        //    catch
        //    {
        //        return false;
        //    }
        //}
    }
}