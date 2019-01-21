using Project3.Data.Database;
using System.Collections.Generic;

namespace Project3.Service.ServiceLogics
{
    public interface IRegisterClassService
    {
        bool CheckRegister(int idStd, int idSche);

        bool RegisterClass(int idStd, int idSche);

        int FindStudent(string user);

        IEnumerable<Schedule> GetListSchedule(int idStd);

        IEnumerable<Schedule> GetResultLearn(int idStd);

        bool CancelRegisterClass(int idStd, int idSche);
    }
}