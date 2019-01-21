using Project3.Data.Database;
using System.Collections.Generic;
using System.Linq;

namespace Project3.Service.ServiceBases
{
    public class CarService : ICarService
    {
        private ProjectDbContext dbContext = new ProjectDbContext();

        public int AddCar(string number, int type, int status)
        {
            try
            {
                var car = new Car();
                car.NumberCar = number;
                car.Type = type;
                car.Status = status;
                dbContext.Cars.Add(car);
                dbContext.SaveChanges();
                

                dbContext.SaveChanges();
                return 0;
            }
            catch
            {
                return 2;
            }
        }

        public int EditCar(int id, string number, int type, int status)
        {
            try
            {
                var car = GetCar(id);
                if (car == null)
                    return 1;
                car.NumberCar = number;
                car.Type = type;
                car.Status = status;
                dbContext.SaveChanges();
                return 0;
            }
            catch
            {
                return 2;
            }
        }

        public int DeleteCar(int id)
        {
            try
            {
                var car = GetCar(id);
                if (car == null)
                    return 1;
                dbContext.Cars.Remove(car);
                

                dbContext.SaveChanges();
                return 0;
            }
            catch
            {
                return 2;
            }
        }

        public Car GetCar(int id)
        {
            return dbContext.Cars.SingleOrDefault(x => x.ID == id);
        }

        public IEnumerable<Car> GetListAllCar()
        {
            return dbContext.Cars.OrderBy(x => x.NumberCar).ToList();
        }

        public IEnumerable<Car> GetListManyCar(int id)
        {
            return dbContext.Cars.Where(x => x.ID == id).ToList();
        }
    }
}