using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StudentPlanner.BLL.Interfaces;
using StudentPlanner.BLL.Models;
using StudentPlanner.DAL.Entities;
using StudentPlanner.DAL.Extends;

namespace StudentPlanner.PL.Controllers;
[Authorize]
public class HomeController : Controller
{
    #region DI
    private readonly IReminder reminder;
    private readonly UserManager<ApplicationUser> userManager;
    private readonly IMapper mapper;
    private readonly IStudent student;
    public HomeController(IReminder reminder, UserManager<ApplicationUser> userManager, IMapper mapper, IStudent student)
    {
        this.reminder = reminder;
        this.userManager = userManager;
        this.mapper = mapper;
        this.student = student;
    }
    #endregion


    #region View-Upcomming-Assignments-&-Exams
      public async Task<IActionResult> Index()
      {
        var user = userManager.GetUserId(User);
        if (user == null)
        {
            return Unauthorized();
        }
        var st = await student.GetStudentByIdentityUserId(user);
        var reminders = await reminder.GetRemindersByUserId(st.Id);

        var remindersVM = mapper.Map<IEnumerable<ReminderVM>>(reminders);
        
        return View(remindersVM);
      }
    #endregion
}
