using Microsoft.AspNetCore.Mvc;
using StudentPlanner.BLL.Interfaces;
using AutoMapper;
using StudentPlanner.BLL.Models;
using StudentPlanner.DAL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace StudentPlanner.PL.Controllers
{
    [Authorize]
    public class CourseController : Controller
    {
        //Dependency Injection for Course Repository
        private readonly ICourse data;
        //AutoMapping
        private readonly IMapper mapper;
        private readonly IStudent studentData;
        public CourseController(ICourse data, IMapper mapper, IStudent studentData)
        {
            this.data = data;
            this.mapper = mapper;
            this.studentData = studentData;
        }
        public async Task<IActionResult> Index()
        {
            var courses = await data.GetAllCoursesAsync();
            var stCourses = mapper.Map<IEnumerable<CourseVM>>(courses);
                return View(stCourses);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(CourseVM newCourseVM)
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
            {
                return Unauthorized();
            }

            var student = await studentData.GetStudentByIdentityUserId(userId);
            if (student == null)
            {
                return Unauthorized();
            }

            var newCourse = mapper.Map<Course>(newCourseVM);
            newCourse.StudentId = student.Id;

            await data.AddCourseAsync(newCourse);
            return RedirectToAction("Index");
        }

        public IActionResult Delete()
        {
            return View();
        }
    }
}
