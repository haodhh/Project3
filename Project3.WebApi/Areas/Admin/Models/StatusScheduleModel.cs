using Project3.Data.Database;
using System.Collections.Generic;

namespace Project3.WebApi.Areas.Admin.Models
{
    public class StatusScheduleModel
    {
        public IEnumerable<Schedule> schedule;
        public int month;
        public int year;
    }
}