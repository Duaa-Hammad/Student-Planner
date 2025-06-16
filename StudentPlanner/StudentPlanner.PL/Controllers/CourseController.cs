using Microsoft.AspNetCore.Mvc;
using StudentPlanner.BLL.Interfaces;
using AutoMapper;
using StudentPlanner.BLL.Models;
using StudentPlanner.DAL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using StudentPlanner.DAL.Extends;

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
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        public CourseController(ICourse data, IMapper mapper, IStudent studentData, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            this.data = data;
            this.mapper = mapper;
            this.studentData = studentData;
            this.signInManager = signInManager;
            this.userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            //Getting the user's ID from the current session if the user is logged in
            var userId = userManager.GetUserId(User);
            if (userId == null)
            {
                return Unauthorized();
            }
            else
            {
                var student = await studentData.GetStudentByIdentityUserId(userId);
                var courses = await data.GetStudentCoursesAsync(student.Id);
                var stCourses = mapper.Map<IEnumerable<CourseVM>>(courses);
                return View(stCourses);
            }
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(CourseVM newCourseVM)
        {
            //var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;


            // Getting the user's ID from the current session if the user is logged in


            //The User here is a built-in property in ASP.NET Core.
            //It represents the currently logged -in user.
            var userId = userManager.GetUserId(User);

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
