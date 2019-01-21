using Project3.Data.Database;
using System.Collections.Generic;

namespace Project3.Data.OtherModel
{
    public class DetailScheduleStudent
    {
        public IEnumerable<Schedule> listSchedule;
        public IEnumerable<Car> listCar;
        public IEnumerable<Student> listStudent;
        public IEnumerable<Teacher> listTeacher;
        public int hour;
        public int day;
        public int month;
        public int year;
    }
}