using Project3.Common;
using Project3.Data.Database;
using System.Collections.Generic;
using System.Linq;

namespace Project3.Service.ServiceBases
{
    public class StudentService : IStudentService
    {
        private ProjectDbContext dbContext = new ProjectDbContext();

        public int AddStudent(Student student)
        {
            try
            {
                var acc = new Account();
                acc.User = student.User;
                acc.Pass = CryptoSHA.GenerateSHA512String(student.User);
                acc.Level = 2;
                acc.Status = 1;
                dbContext.Accounts.Add(acc);
                dbContext.Students.Add(student);
                dbContext.SaveChanges();
                return 0;
            }
            catch
            {
                return 2;
            }
        }

        public int AddStudent(string name, int age, int gender, string phone, string email, string address, string user)
        {
            try
            {
                var student = new Student();
                student.Name = name;
                student.Age = age;
                student.Gender = gender;
                student.Phone = phone;
                student.Email = email;
                student.Address = address;
                student.Status = 0;
                student.User = user;
                ////
                return AddStudent(student);
            }
            catch
            {
                return 2;
            }
        }

        public int EditStudent(int id, Student student)
        {
            try
            {
                var std = GetStudent(id);
                if (std == null)
                    return 1;
                std.Name = student.Name;
                std.Age = student.Age;
                std.Gender = student.Gender;
                std.Phone = student.Phone;
                std.Email = student.Email;
                std.Address = student.Address;
                dbContext.SaveChanges();
                return 0;
            }
            catch
            {
                return 2;
            }
        }

        public int EditStudent(int id, string name, int age, int gender, string phone, string email, string address)
        {
            try
            {
                var student = new Student();
                student.Name = name;
                student.Age = age;
                student.Gender = gender;
                student.Phone = phone;
                student.Email = email;
                student.Address = address;
                ////
                return EditStudent(id, student);
            }
            catch
            {
                return 2;
            }
        }

        public int DeleteStudent(int id)
        {
            try
            {
                var std = GetStudent(id);
                if (std == null)
                    return 1;
                if (std.Status == 1)
                {
                    dbContext.RFIDCards.Where(x => x.IDStudent == id).First().IDStudent = 0;
                }
                var acc = dbContext.Accounts.SingleOrDefault(x => x.User == std.User);
                if (acc != null)
                    dbContext.Accounts.Remove(acc);
                dbContext.Students.Remove(std);

                dbContext.SaveChanges();
                return 0;
            }
            catch
            {
                return 2;
            }
        }

        public Student GetStudent(int id)
        {
            return dbContext.Students.SingleOrDefault(x => x.ID == id);
        }

        public IEnumerable<Student> GetListAllStudent()
        {
            return dbContext.Students.OrderBy(x => x.ID).ToList();
        }

        public IEnumerable<Student> GetListManyStudent(int id)
        {
            return dbContext.Students.Where(x => x.ID == id).ToList();
        }
    }
}