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
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IMapper mapper;
        //Dependency Injection for Course Repository
        private readonly IStudent data;
        public AccountController (UserManager<ApplicationUser> userManager, IMapper mapper, IStudent data, SignInManager<ApplicationUser> signInManager)
        {
            this.userManager = userManager;
            this.mapper = mapper;
            this.data = data;
            this.signInManager = signInManager;
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginVM userLogin)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(userLogin);
                }

                var user = await userManager.FindByEmailAsync(userLogin.Email);

                if (user == null)
                {
                    ModelState.AddModelError("", "Email Not Found!");
                    return View(userLogin);
                }

                var result = await signInManager.PasswordSignInAsync(user, userLogin.Password, userLogin.RememberMe, false);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError("", "Incorrect Password!");
                return View(userLogin);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "An Error has Occured!");
                return View(userLogin);
            }
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

        public async Task<IActionResult> LogOut()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Login");
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
