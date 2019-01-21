using Project3.Data.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project3.Service.ServiceBases
{
    public interface IMonitorService
    {
        bool Add();
        bool Edit();
        bool Delete();
        IEnumerable<MonitorOnline> GetListAllO();
    }
}
