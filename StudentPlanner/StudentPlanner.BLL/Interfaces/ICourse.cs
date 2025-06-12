<<<<<<< HEAD
﻿using StudentPlanner.DAL.Entities;
using System;
=======
﻿using System;
>>>>>>> 75dd13d (Created Mapper and Ojbect Lifetime)
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentPlanner.BLL.Interfaces
{
    public interface ICourse
    {
<<<<<<< HEAD
        public Task<IEnumerable<Course>> GetStudentCoursesAsync(int Id);
        public Task<Course> GetCourseByIdAsync(int Id);
        public Task AddCourseAsync(Course course);
        public Task UpdateCourseAsync(Course course);
        public Task DeleteCourseAsync(int Id);
=======
>>>>>>> 75dd13d (Created Mapper and Ojbect Lifetime)
    }
}
