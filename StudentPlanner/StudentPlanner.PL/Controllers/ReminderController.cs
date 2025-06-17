using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using StudentPlanner.BLL.Interfaces;
using StudentPlanner.BLL.Models;
using StudentPlanner.DAL.Entities;
using StudentPlanner.DAL.Extends;
using static Microsoft.AspNetCore.Razor.Language.TagHelperMetadata;
using System.Security.Policy;
using System;

namespace StudentPlanner.PL.Controllers
{
    public class ReminderController : Controller
    {
        private readonly IReminder reminderData;
        private readonly IStudent studentData;
        private readonly IAssignment assignmentData;
        private readonly IExam examData;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IMapper mapper;
        public ReminderController(IReminder reminderData, IMapper mapper, UserManager<ApplicationUser> userManager, IStudent studentData, IAssignment assignmentData, IExam examData)
        {
            this.reminderData = reminderData;
            this.studentData = studentData;
            this.userManager = userManager;
            this.mapper = mapper;
            this.assignmentData = assignmentData;
            this.examData = examData;
        }
        public IActionResult Create(int courseId)
        {
            ViewBag.DepList = new SelectList(Enum.GetValues(typeof(BLL.Models.ReminderType)).Cast<BLL.Models.ReminderType>()
               .Select(e => new { Id = (int)e, Name = e.ToString() }),"Id", "Name");

            var model = new ReminderVM
            {
                CourseId = courseId,
                Deadline = DateTime.Now.AddDays(7), // ممكن تحطي تاريخ افتراضي بعد أسبوع
                ReminderOffsetDays = 1 //1 day before (default)
            };
            return PartialView("_CreateReminderPartial");
        }

        [HttpPost]
//      It’s an attribute that helps protect your application from a common web security attack called:
//      CSRF — Cross-Site Request Forgery
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ReminderVM model)
        {
            if (!ModelState.IsValid)
            {
                return PartialView("_CreateReminderPartial", model);
            }

            // Get current logged in user Id
            var userId = userManager.GetUserId(User);
            if (userId == null) {
                return Unauthorized();
            }

            // Get student linked to that user
            var student = await studentData.GetStudentByIdentityUserId(userId);

            model.ReminderDate = model.Deadline.AddDays(-model.ReminderOffsetDays);

            Reminder reminderEntity = new Reminder()
            {
                Deadline = model.Deadline,
                StudentId = student.Id,
                ReminderDate = model.ReminderDate,
                Note = model.Note,
                Type = (DAL.Entities.ReminderType)model.Type,
                CourseId = model.CourseId
            };
            if (model.Type == BLL.Models.ReminderType.Assignment)
            {
                Assignment assingEntity = new Assignment()
                {
                    CourseId = model.CourseId,
                    StudentId = student.Id,
                    DueDate = model.Deadline,
                    Title = model.Note,
                };
                await assignmentData.AddAssignment(assingEntity);

                reminderEntity.AssignmentId = assingEntity.Id;

            }
            else if (model.Type == BLL.Models.ReminderType.Exam)
            {
                Exam examEntity = new Exam()
                {
                    CourseId = model.CourseId,
                    StudentId = student.Id,
                    Date = model.Deadline,
                    Note = model.Note,
                };
                await examData.AddExam(examEntity);

                reminderEntity.ExamId = examEntity.Id;

            }

            await reminderData.AddReminder(reminderEntity);

            return RedirectToAction("Index", "Course", new { id = model.CourseId });
        }


    }
}
