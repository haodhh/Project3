using Project3.Data.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project3.WebApi.Areas.Admin.Models
{
    public class ModelStudent
    {
        public int id;
        public IEnumerable<Student> list;
        public HttpPostedFileBase[] files;
    }
}