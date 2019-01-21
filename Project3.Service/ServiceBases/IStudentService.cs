using Project3.Data.Database;
using System.Collections.Generic;

namespace Project3.Service.ServiceBases
{
    public interface IStudentService
    {
        int AddStudent(Student student);

        int AddStudent(string name, int age, int gender, string phone, string email, string address, string user);

        int EditStudent(int id, Student student);

        int EditStudent(int id, string name, int age, int gender, string phone, string email, string address);

        int DeleteStudent(int id);

        Student GetStudent(int id);

        IEnumerable<Student> GetListAllStudent();

        IEnumerable<Student> GetListManyStudent(int id);
    }
}