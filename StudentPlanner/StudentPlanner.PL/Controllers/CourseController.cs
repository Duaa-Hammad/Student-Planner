using Microsoft.AspNetCore.Mvc;
using StudentPlanner.BLL.Repository;
using StudentPlanner.BLL.Interfaces;
using AutoMapper;
using StudentPlanner.BLL.Models;
using StudentPlanner.DAL.Entities;

namespace StudentPlanner.PL.Controllers
{
    public class CourseController : Controller
    {
        //Dependency Injection for Course Repository
        private readonly ICourse data;
        //AutoMapping
        private readonly IMapper mapper;
        public CourseController(ICourse data, IMapper mapper)
        {
            this.data = data;
            this.mapper = mapper;
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
        public async Task<IActionResult> Create(CourseVM newCourseVM)
        {
            var newCourse = mapper.Map<Course>(newCourseVM);
            await data.AddCourseAsync(newCourse);
            return RedirectToAction("Index");
        }
        public IActionResult Delete()
        {
            return View();
        }
    }
}
