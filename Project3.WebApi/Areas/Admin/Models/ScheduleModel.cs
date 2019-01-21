using Project3.Data.Database;
using System.Collections.Generic;

namespace Project3.WebApi.Areas.Admin.Models
{
    public class ScheduleModel
    {
        public IEnumerable<Schedule> listSchedule;
        public IEnumerable<Teacher> listTeacher;
        public IEnumerable<Student> listStudent;
        public IEnumerable<Car> listCar;
    }
}