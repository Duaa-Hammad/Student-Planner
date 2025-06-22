using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StudentPlanner.BLL.Interfaces;
using StudentPlanner.BLL.Models;
using StudentPlanner.DAL.Extends;

namespace StudentPlanner.PL.Controllers
{
    public class StudentController : Controller
    {
        private readonly IStudent studentData;
        private readonly IMapper mapper;
        private readonly UserManager<ApplicationUser> userManager;
        public StudentController(IStudent studentData, IMapper mapper, UserManager<ApplicationUser> userManager)
        {
            this.studentData = studentData;
            this.mapper = mapper;
            this.userManager = userManager;
        }
        public async Task<IActionResult> Profile()
        {
            var userId = userManager.GetUserId(User);
            if(userId==null)
            {
                return Unauthorized();
            }
            var student = await studentData.GetStudentByIdentityUserId(userId);
            var studentVM = mapper.Map<StudentVM>(student);

            return View(studentVM);
        }
    }
}
