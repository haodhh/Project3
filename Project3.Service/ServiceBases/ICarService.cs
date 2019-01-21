using Project3.Data.Database;
using System.Collections.Generic;

namespace Project3.Service.ServiceBases
{
    public interface ICarService
    {
        int AddCar(string number, int type, int status);

        int EditCar(int id, string number, int type, int status);

        int DeleteCar(int id);

        Car GetCar(int id);

        IEnumerable<Car> GetListAllCar();

        IEnumerable<Car> GetListManyCar(int id);
    }
}