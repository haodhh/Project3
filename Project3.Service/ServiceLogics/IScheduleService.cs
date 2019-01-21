using Project3.Data.Database;
using System.Collections.Generic;

namespace Project3.Service.ServiceLogics
{
    public interface IScheduleService
    {
        IEnumerable<Schedule> GetListAllSchedule();

        IEnumerable<Car> GetListAllCar();

        IEnumerable<Student> GetListAllStudent();

        IEnumerable<Teacher> GetListAllTeacher();

        IEnumerable<Schedule> GetListManySchedule(string code, int hour = 0, int day = 0, int month = 0, int year = 0, int idCar = 0, int idStudent = 0, int idTeacher = 0, int status = -1);

        Schedule GetSchedule(int id);

        int AddSchedule(string code, int hour, int day, int month, int year, int idCar, int idStudent, int idTeacher);

        int EditSchedule(int id, string code, int hour, int day, int month, int year, int idCar, int idStudent, int idTeacher);

        int DelSchedule(int id);
    }
}