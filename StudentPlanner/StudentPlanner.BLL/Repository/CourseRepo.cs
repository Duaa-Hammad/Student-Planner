using Microsoft.EntityFrameworkCore;
using StudentPlanner.BLL.Interfaces;
using StudentPlanner.DAL.Database;
using StudentPlanner.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentPlanner.BLL.Repository
{
    public class CourseRepo : ICourse
    {
        private readonly StPlannerContext data;
        public CourseRepo(StPlannerContext data)
        {
            this.data = data;
        }
        public async Task<IEnumerable<Course>> GetStudentCoursesAsync(int Id)
        {
            return await data.Courses.Where(a => a.StudentId == Id).ToListAsync();
        }
        public async Task<Course> GetCourseByIdAsync(int Id)
        {
            //return await data.Courses.FindAsync(id);
            return await data.Courses.Where(a => a.Id == Id).FirstOrDefaultAsync();
        }
        public async Task AddCourseAsync(Course course)
        {
            data.Courses.Add(course);
            await data.SaveChangesAsync();
        }
        public async Task UpdateCourseAsync(Course course)
        {
            data.Courses.Update(course);
            await data.SaveChangesAsync();
        }
        public async Task DeleteCourseAsync(int Id)
        {
            var course = await GetCourseByIdAsync(Id);
            if (course != null)
            {
                data.Courses.Remove(course);
                await data.SaveChangesAsync();
            }
        }
    }
}
