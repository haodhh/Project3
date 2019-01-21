using Project3.Data.Database;
using System.Collections.Generic;
using System.Linq;

namespace Project3.Service.ServiceLogics
{
    public class ScheduleService : IScheduleService
    {
        private ProjectDbContext dbContext = new ProjectDbContext();

        public IEnumerable<Car> GetListAllCar()
        {
            return dbContext.Cars.OrderBy(x => x.NumberCar).ToList();
        }

        public IEnumerable<Student> GetListAllStudent()
        {
            return dbContext.Students.OrderBy(x => x.Name).ToList();
        }

        public IEnumerable<Teacher> GetListAllTeacher()
        {
            return dbContext.Teachers.OrderBy(x => x.Name).ToList();
        }

        public IEnumerable<Schedule> GetListAllSchedule()
        {
            return dbContext.Schedules.OrderBy(x => x.Code).OrderBy(x => x.Status).ToList();
            //return dbContext.Schedules.OrderBy(x => x.Code).OrderBy(x => x.Hour).OrderBy(x => x.Day).OrderBy(x => x.Month).OrderBy(x => x.Year).ToList();
        }

        public IEnumerable<Schedule> GetListManySchedule(string code, int hour = 0, int day = 0, int month = 0, int year = 0, int idCar = 0, int idStudent = 0, int idTeacher = 0, int status = -1)
        {
            IEnumerable<Schedule> list = dbContext.Schedules.ToList();
            if (code != null)
                list = list.Where(x => x.Code == code);
            if (hour != 0)
                list = list.Where(x => x.Hour == hour);
            if (day != 0)
                list = list.Where(x => x.Day == day);
            if (month != 0)
                list = list.Where(x => x.Month == month);
            if (year != 0)
                list = list.Where(x => x.Year == year);
            if (idCar != 0)
                list = list.Where(x => x.IDCar == idCar);
            if (idStudent != 0)
                list = list.Where(x => x.IDStudent == idStudent);
            if (idTeacher != 0)
                list = list.Where(x => x.IDTeacher == idTeacher);
            if (status == -1)
                return list.OrderBy(x => x.Code).OrderBy(x => x.Status);
            //return list.OrderBy(x => x.Code).OrderBy(x => x.Hour).OrderBy(x => x.Day).OrderBy(x => x.Month).OrderBy(x => x.Year);
            return list.Where(x => x.Status == -1).OrderBy(x => x.Code).OrderBy(x => x.Status);
            //return list.Where(x => x.Status == -1).OrderBy(x => x.Code).OrderBy(x => x.Hour).OrderBy(x => x.Day).OrderBy(x => x.Month).OrderBy(x => x.Year);
        }

        public Schedule GetSchedule(int id)
        {
            return dbContext.Schedules.SingleOrDefault(x => x.ID == id);
        }

        public int AddSchedule(string code, int hour, int day, int month, int year, int idCar, int idStudent, int idTeacher)
        {
            try
            {
                if (GetListManySchedule(code, 0, 0, 0, 0, 0, 0, 0, -1).Count() > 0)
                    return 1;
                if (GetListManySchedule(null, hour, day, month, year, 0, 0, 0, -1).Count() >= 5)
                    return 1;
                var schedule = new Schedule();
                schedule.Code = code;
                schedule.Hour = hour;
                schedule.Day = day;
                schedule.Month = month;
                schedule.Year = year;
                schedule.IDCar = idCar;
                schedule.IDStudent = idStudent;
                schedule.IDTeacher = idTeacher;
                schedule.Status = -1;
                dbContext.Schedules.Add(schedule);
                dbContext.SaveChanges();
                return 0;
            }
            catch
            {
                return 2;
            }
        }

        public int EditSchedule(int id, string code, int hour, int day, int month, int year, int idCar, int idStudent, int idTeacher)
        {
            try
            {
                if (GetListManySchedule(code, 0, 0, 0, 0, 0, 0, 0, -1).Count() > 0)
                    return 3;
                if (GetListManySchedule(null, hour, day, month, year, 0, 0, 0, -1).Count() >= 5)
                    return 3;
                var schedule = GetSchedule(id);
                if (schedule == null)
                    return 1;
                if (schedule.Status != -1)
                    return 1;
                schedule.Code = code;
                schedule.Hour = hour;
                schedule.Day = day;
                schedule.Month = month;
                schedule.Year = year;
                schedule.IDCar = idCar;
                schedule.IDStudent = idStudent;
                schedule.IDTeacher = idTeacher;
                dbContext.SaveChanges();
                return 0;
            }
            catch
            {
                return 2;
            }
        }

        public int DelSchedule(int id)
        {
            try
            {
                var schedule = GetSchedule(id);
                if (schedule == null)
                    return 1;
                if (schedule.Status != -1)
                    return 1;
                dbContext.Schedules.Remove(schedule);
                dbContext.SaveChanges();
                return 0;
            }
            catch
            {
                return 2;
            }
        }
    }
}