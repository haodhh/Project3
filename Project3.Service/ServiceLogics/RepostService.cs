using Project3.Data.Database;
using System.Collections.Generic;
using System.Linq;

namespace Project3.Service.ServiceLogics
{
    public class RepostService : IRepostService
    {
        private ProjectDbContext dbContext = new ProjectDbContext();

        public IEnumerable<Schedule> listRepost(int hour = 0, int day = 0, int month = 0, int year = 0, int idCar = 0, int idStd = 0, int idTea = 0)
        {
            var list = dbContext.Schedules.Where(x => x.Status != 1);

            if (hour != 0)
                list = list.Where(x => x.Hour == hour);

            if (day != 0)
                list = list.Where(x => x.Day == day);

            if (month != 0)
                list = list.Where(x => x.Day == month);

            if (year != 0)
                list = list.Where(x => x.Day == year);

            if (idCar != 0)
                list = list.Where(x => x.Day == idCar);

            if (idStd != 0)
                list = list.Where(x => x.Day == idStd);

            if (idTea != 0)
                list = list.Where(x => x.Day == idTea);

            return list.OrderBy(x => x.Code).OrderBy(x => x.Year);
        }
    }
}