
﻿using StudentPlanner.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentPlanner.BLL.Interfaces
{
    public interface ICourse
    {
        public Task<IEnumerable<Course>> GetStudentCoursesAsync(int Id);
        public Task<Course> GetCourseByIdAsync(int Id);
        public Task AddCourseAsync(Course course);
        public Task UpdateCourseAsync(Course course);
        public Task DeleteCourseAsync(int Id);
    }
}
