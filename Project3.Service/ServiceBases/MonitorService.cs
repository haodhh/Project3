using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project3.Data.Database;

namespace Project3.Service.ServiceBases
{
    public class MonitorService : IMonitorService
    {
        private ProjectDbContext dbContext = new ProjectDbContext();
        public bool Add()
        {
            throw new NotImplementedException();
        }

        public bool Delete()
        {
            throw new NotImplementedException();
        }

        public bool Edit()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<MonitorOnline> GetListAllO()
        {
            return dbContext.MonitorOnlines.ToList();
        }
        
    }
}
