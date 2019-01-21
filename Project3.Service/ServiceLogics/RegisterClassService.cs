using Project3.Data.Database;
using System.Collections.Generic;
using System.Linq;

namespace Project3.Service.ServiceLogics
{
    public class RegisterClassService : IRegisterClassService
    {
        private ProjectDbContext dbContext = new ProjectDbContext();

        public bool CheckRegister(int idStd, int idSche)
        {
            var sche = dbContext.Schedules.SingleOrDefault(x => x.ID == idSche);
            if (sche.IDCar == 0)
                return false;
            if (dbContext.Schedules.Where(x => x.Hour == sche.Hour).Where(x => x.Day == sche.Day).Where(x => x.Month == sche.Month).Where(x => x.Year == sche.Year).Where(x => x.IDStudent == idStd).Count() > 0)
                return false;
            else return true;
        }

        public int FindStudent(string user)
        {
            return dbContext.Students.Where(x => x.User == user).First().ID;
        }

        public IEnumerable<Schedule> GetListSchedule(int idStd)
        {
            return dbContext.Schedules.Where(x => x.IDStudent == idStd).Where(x => x.Status == -1).ToList();
        }

        public bool RegisterClass(int idStd, int idSche)
        {
            if (CheckRegister(idStd, idSche))
            {
                try
                {
                    var sche = dbContext.Schedules.SingleOrDefault(x => x.ID == idSche);
                    if (sche.Status != -1)
                        return false;
                    sche.IDStudent = idStd;
                    dbContext.SaveChanges();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            return false;
        }

        public bool CancelRegisterClass(int idStd, int idSche)
        {
            try
            {
                var sche = dbContext.Schedules.SingleOrDefault(x => x.ID == idSche);
                if (sche.IDStudent != idStd)
                    return false;
                sche.IDStudent = 0;
                dbContext.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public IEnumerable<Schedule> GetResultLearn(int idStd)
        {
            return dbContext.Schedules.Where(x => x.IDStudent == idStd).Where(x => x.Status >= 0).ToList();
        }
    }
}