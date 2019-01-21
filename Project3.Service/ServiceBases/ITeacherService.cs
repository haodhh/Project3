using Project3.Data.Database;
using System.Collections.Generic;

namespace Project3.Service.ServiceBases
{
    public interface ITeacherService
    {
        int AddTeacher(string name, int age, int gender, string phone, string email, string address);

        int EditTeacher(int id, string name, int age, int gender, string phone, string email, string address);

        int DelTeacher(int id);

        Teacher GetTeacher(int id);

        IEnumerable<Teacher> GetListAllTeachers();

        IEnumerable<Teacher> GetListManyTeachers(string name);
    }
}