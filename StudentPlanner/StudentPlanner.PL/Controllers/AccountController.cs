using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StudentPlanner.BLL.Interfaces;
using StudentPlanner.BLL.Models;
using StudentPlanner.DAL.Database;
using StudentPlanner.DAL.Entities;
using StudentPlanner.DAL.Extends;
using System.Linq.Expressions;

namespace StudentPlanner.PL.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IMapper mapper;
        //Dependency Injection for Course Repository
        private readonly IStudent data;
        public AccountController (UserManager<ApplicationUser> userManager, IMapper mapper, IStudent data)
        {
            this.userManager = userManager;
            this.mapper = mapper;
            this.data = data;
        }
        public IActionResult Login()
        {
            return View();
        }
        public IActionResult Registration()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Registration(RegistrationVM newUser)
        {
            if (!ModelState.IsValid)
                return View(newUser);
            try
            {
                var user = mapper.Map<ApplicationUser>(newUser);

                var result = await userManager.CreateAsync(user, newUser.Password);

                if (result.Succeeded)
                {
                    //Mapping
                    var student = new Student
                    {
                        Name = newUser.Name,
                        Email = newUser.Email,
                        IdentityUserId = user.Id,
                        CreatedAt = DateTime.UtcNow,
                        Semester = newUser.Semester
                    };

                    await data.CreateStudentAsync(student);

                    return RedirectToAction("Login");
                }

                // View Errors 
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }

                return View(newUser);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", "An Error has Occured!");
                return View(newUser);
            }
        }


        public IActionResult ForgotPassword()
        {
            return View();
        }
        public IActionResult Test ()
        {
            return View();
        }
        public IActionResult MyTest()
        {
            return View();
        }
    }
}
