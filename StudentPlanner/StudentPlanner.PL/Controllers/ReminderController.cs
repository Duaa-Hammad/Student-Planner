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
using StudentPlanner.BLL.Repository;
using Microsoft.AspNetCore.Authorization;
using StudentPlanner.BLL.TimeHelper;

namespace StudentPlanner.PL.Controllers
{
    [Authorize]
    public class ReminderController : Controller
    {
        #region DI
        private readonly IReminder reminderData;
        private readonly IStudent studentData;
        private readonly ICourse course;
        private readonly IAssignment assignmentData;
        private readonly IExam examData;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IMapper mapper;
        //------------------------------------------------------------------
        private readonly IEmail email;
        public ReminderController(IReminder reminderData, IMapper mapper, UserManager<ApplicationUser> userManager, IStudent studentData, IAssignment assignmentData, IExam examData, IEmail email, ICourse course)
        {
            this.reminderData = reminderData;
            this.studentData = studentData;
            this.userManager = userManager;
            this.mapper = mapper;
            this.assignmentData = assignmentData;
            this.examData = examData;
            this.email = email;
            this.course = course;
        }
        #endregion
        //-----------------------------------------------------
        #region ViewAllReminders
        public async Task<IActionResult> Index()
        {
            var userId = userManager.GetUserId(User);
            if(userId == null)
            {
                return Unauthorized();
            }
            var st = await studentData.GetStudentByIdentityUserId(userId);
            var stReminders = await reminderData.GetRemindersByUserId(st.Id);
            var stRemindersVM = mapper.Map<IEnumerable<ReminderVM>>(stReminders);

            return View(stRemindersVM);
        }
        #endregion
        //-----------------------------------------------------
        #region Create
        public IActionResult Create(int courseId)
        {
            ViewBag.DepList = new SelectList(Enum.GetValues(typeof(BLL.Models.ReminderType)).Cast<BLL.Models.ReminderType>()
               .Select(e => new { Id = (int)e, Name = e.ToString() }),"Id", "Name");

            //var courseEntity = await course.GetCourseByIdAsync(courseId);
            //var courseVM = mapper.Map<CourseVM>(courseEntity);

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
                TempData["ReminderFaildMessage"] = "Faild Reminder set. All fields need to be filled";
                return RedirectToAction("Index","Course");
             }
            // Get current logged in user Id
            var userId = userManager.GetUserId(User);
            if (userId == null) {
                return Unauthorized();
            }

            // Get student linked to that user
            var student = await studentData.GetStudentByIdentityUserId(userId);

            var deadlineLibya = DateTime.SpecifyKind(model.Deadline, DateTimeKind.Unspecified);
            var reminderDateLibya = deadlineLibya.AddDays(-model.ReminderOffsetDays);

            Reminder reminderEntity = new Reminder()
            {
                Deadline = ConvertTime.ToUtcFromLibya(deadlineLibya),
                ReminderDate = ConvertTime.ToUtcFromLibya(reminderDateLibya),
                StudentId = student.Id,
                Note = model.Note,
                Type = (DAL.Entities.ReminderType)model.Type,
                CourseId = model.CourseId
            };

            #region Assignment
            if (model.Type == BLL.Models.ReminderType.Assignment)
            {
                Assignment assingEntity = new Assignment()
                {
                    CourseId = model.CourseId,
                    StudentId = student.Id,
                    DueDate = ConvertTime.ToUtcFromLibya(deadlineLibya),
                    //Title = model.Note,
                    Note = model.Note
                };
                await assignmentData.AddAssignment(assingEntity);

                reminderEntity.AssignmentId = assingEntity.Id;

            }
            #endregion
            #region Exam
            else if (model.Type == BLL.Models.ReminderType.Exam)
            {
                Exam examEntity = new Exam()
                {
                    CourseId = model.CourseId,
                    StudentId = student.Id,
                    Date = ConvertTime.ToUtcFromLibya(deadlineLibya),
                    Note = model.Note,
                };
                await examData.AddExam(examEntity);

                reminderEntity.ExamId = examEntity.Id;

            }
            #endregion
            await reminderData.AddReminder(reminderEntity);

            //return RedirectToAction("Index");
            return RedirectToAction("Index", "Home");
        }
        #endregion
        //-----------------------------------------------------
        #region Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int Id)
        {
            var reminder = await reminderData.GetReminderById(Id);
            int? assignmentId = reminder.AssignmentId;
            int? examId = reminder.ExamId;

            //Here we cannot delete assignment or exam if there's reminder related to it.
            //so we delete the reminder first, then assignment or exam if exists.
            await reminderData.DeleteReminderAsync(reminder);

            if(reminder.Type.Equals(BLL.Models.ReminderType.Assignment) && assignmentId.HasValue)
            {
                await assignmentData.DeleteAssignmentById(reminder.AssignmentId);
            }

            else if (reminder.Type.Equals(BLL.Models.ReminderType.Exam) && examId.HasValue)
            {
                await examData.DeleteExamById(reminder.ExamId);
            }

            return RedirectToAction("Index");
        }
        #endregion
        //-----------------------------------------------------
        #region Update
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Update(ReminderVM newReminder)
        {
            // نحدد إن الوقت جاينا من المستخدم وبدون timezone، نعامله كأنه بتوقيت ليبيا
            var deadlineLibya = DateTime.SpecifyKind(newReminder.Deadline, DateTimeKind.Unspecified);

            // نحسب وقت التذكير حسب الـ Offset
            var reminderDateLibya = deadlineLibya.AddDays(-newReminder.ReminderOffsetDays);

            // نحول للتوقيت العالمي للتخزين
            var reminderEntity = mapper.Map<Reminder>(newReminder);
            reminderEntity.Deadline = ConvertTime.ToUtcFromLibya(deadlineLibya);
            reminderEntity.ReminderDate = ConvertTime.ToUtcFromLibya(reminderDateLibya);

            await reminderData.UpdateReminderAsync(reminderEntity);
            return RedirectToAction("Index");
        }
        #endregion

        //-----------------------------------------------------
        #region Manage
        public async Task<IActionResult> Manage(int Id)
        {
            var reminder = await reminderData.GetReminderById(Id);

            ViewBag.DepList = new SelectList(Enum.GetValues(typeof(BLL.Models.ReminderType)).Cast<BLL.Models.ReminderType>()
            .Select(e => new { Id = (int)e, Name = e.ToString() }), "Id", "Name");

           

            var reminderVM = mapper.Map<ReminderVM>(reminder);

            reminderVM.ReminderOffsetDays = (reminder.Deadline - reminder.ReminderDate).Days;

            reminderVM.Deadline = ConvertTime.ToLibyaTime(reminder.Deadline);
            reminderVM.ReminderDate = ConvertTime.ToLibyaTime(reminder.ReminderDate);

            return View(reminderVM);
        }
        #endregion
        //-----------------------------------------------------
        #region TestEmail
        [HttpGet]
        public async Task<IActionResult> TestEmail()
        {
            var userId = userManager.GetUserId(User);
            if (userId == null)
            {
                return Unauthorized();
            }
            var st = await studentData.GetStudentByIdentityUserId(userId);
            string studentName = st.Name;
            string type = "assignment";
            int daysLeft = 3;
            string userEmail = st.Email;

            string timeFrame = daysLeft switch
            {
                1 => "tomorrow",
                3 => "in 3 days",
                7 => "in a week",
                30 => "in a month",
                _ => $"in {daysLeft} days"
            };

            string message = $"Hey {studentName}, you have {type} {timeFrame}, be ready for it!\nBreak a leg buddy <3";

            await email.SendEmailAsync(userEmail, "Reminder", message);

            //return Content("Email Sent Successfully!");

            TempData["SuccessMessage"] = "Email Sent Successfully!";
            return RedirectToAction("Index", "Course");
        }
        #endregion
    }
}
