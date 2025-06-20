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
        private readonly IReminder reminderData;
        private readonly IAssignment assignmentData;
        private readonly IExam examData;
        private readonly UserManager<ApplicationUser> userManager;
        public CourseController(ICourse data, IMapper mapper, IStudent studentData, UserManager<ApplicationUser> userManager, IReminder reminderData, IAssignment assignmentData, IExam examData)
        {
            this.data = data;
            this.mapper = mapper;
            this.studentData = studentData;
            this.userManager = userManager;
            this.reminderData = reminderData;
            this.assignmentData = assignmentData;
            this.examData = examData;
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

                var userReminders = await reminderData.GetRemindersByUserId(student.Id);

                var remindersVM = mapper.Map<List<ReminderVM>>(userReminders);

                // بناء قائمة الكورسات مع التذكيرات المرتبطة بها
                var result = stCourses.Select(course => new CourseWithRemindersVM
                {
                    Course = course,
                    Reminders = remindersVM.Where(r => r.CourseId == course.Id).ToList()
                }).ToList();

                return View(result);
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
        [HttpPost]
        public async Task<IActionResult> Delete(int Id)
        {
            await reminderData.DeleteReminderByCourseId(Id);
            await examData.DeleteExamByCourseId(Id);
            await assignmentData.DeleteAssignmentByCourseId(Id);

            await data.DeleteCourseAsync(Id);

            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Manage(int Id)
        {
            var courseEntity = await data.GetCourseByIdAsync(Id);

            var courseVM = mapper.Map<CourseVM>(courseEntity);
            return View(courseVM);
        }

        [HttpPost]
        public async Task<IActionResult> Update(CourseVM Updatedcourse)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Manage", new { Id = Updatedcourse.Id });
            }

            var courseEntity = mapper.Map<Course>(Updatedcourse);
            await data.UpdateCourseAsync(courseEntity);
            return RedirectToAction("Index");
        }
    }
}
