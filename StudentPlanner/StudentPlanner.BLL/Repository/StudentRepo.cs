using Microsoft.EntityFrameworkCore;
using StudentPlanner.BLL.Interfaces;
using StudentPlanner.BLL.Models;
using StudentPlanner.DAL.Database;
using StudentPlanner.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace StudentPlanner.BLL.Repository
{
    public class StudentRepo : IStudent
    {
        private readonly StPlannerContext data;
        public StudentRepo(StPlannerContext data)
        {
            this.data = data;
        }
        public async Task CreateStudentAsync(Student student)
        {
            data.Students.Add(student);
            await data.SaveChangesAsync();
        }
        public async Task<Student> GetStudentByIdentityUserId(string Id)
        {
            var student = await data.Students.FirstOrDefaultAsync(s => s.IdentityUserId == Id);
            return student;
        }

    }
}
