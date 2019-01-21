using Project3.Data.Database;
using System.Collections.Generic;

namespace Project3.WebApi.Areas.Admin.Models
{
    public class CardStudentModel
    {
        public IEnumerable<RFIDCard> listCard;
        public IEnumerable<Student> listStudent;
    }
}