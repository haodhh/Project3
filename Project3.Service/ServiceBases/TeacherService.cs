using Project3.Data.Database;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Project3.Service.ServiceBases
{
    public class TeacherService : ITeacherService
    {
        private ProjectDbContext dbContext = new ProjectDbContext();

        public int AddTeacher(string name, int age, int gender, string phone, string email, string address)
        {
            try
            {
                var teacher = new Teacher();
                teacher.Name = name;
                teacher.Age = age;
                teacher.Gender = gender;
                teacher.Phone = phone;
                teacher.Email = email;
                teacher.Address = address;
                teacher.Status = 0;
                dbContext.Teachers.Add(teacher);
                dbContext.SaveChanges();
                return 0;
            }
            catch
            {
                return 2;
            }
        }

        public int EditTeacher(int id, string name, int age, int gender, string phone, string email, string address)
        {
            try
            {
                var teacher = GetTeacher(id);
                if (teacher == null)
                    return 1;
                teacher.Name = name;
                teacher.Age = age;
                teacher.Gender = gender;
                teacher.Phone = phone;
                teacher.Email = email;
                teacher.Address = address;
                teacher.Status = 0;
                dbContext.SaveChanges();
                return 0;
            }
            catch
            {
                return 2;
            }
        }

        public int DelTeacher(int id)
        {
            try
            {
                var teacher = GetTeacher(id);
                if (teacher == null)
                    return 1;
                dbContext.Teachers.Remove(teacher);
                dbContext.SaveChanges();
                return 0;
            }
            catch
            {
                return 2;
            }
        }

        public Teacher GetTeacher(int id)
        {
            return dbContext.Teachers.SingleOrDefault(x => x.ID == id);
        }

        public IEnumerable<Teacher> GetListAllTeachers()
        {
            return dbContext.Teachers.OrderBy(x => x.Name).ToList();
        }

        public IEnumerable<Teacher> GetListManyTeachers(string name)
        {
            throw new NotImplementedException();
        }
    }
}