using Project3.Data.Database;
using System.Collections.Generic;

namespace Project3.Service.ServiceLogics
{
    public interface IRepostService
    {
        IEnumerable<Schedule> listRepost(int hour, int day, int month, int year, int idCar, int idStd, int idTea);
    }
}