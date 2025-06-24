using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using NuGet.Common;
using StudentPlanner.BLL.Interfaces;
using StudentPlanner.BLL.Models;
using StudentPlanner.DAL.Database;
using StudentPlanner.DAL.Entities;
using StudentPlanner.DAL.Extends;
using System.Linq.Expressions;
using System.Runtime.InteropServices;

namespace StudentPlanner.PL.Controllers
{
    public class AccountController : Controller
    {
        #region DI
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IMapper mapper;
        private readonly IStudent data;
        private readonly IEmail email;
        public AccountController(UserManager<ApplicationUser> userManager, IMapper mapper, IStudent data, SignInManager<ApplicationUser> signInManager, IEmail email)
        {
            this.userManager = userManager;
            this.mapper = mapper;
            this.data = data;
            this.signInManager = signInManager;
            this.email = email;
        }
        #endregion
        //-----------------------------------------------------
        #region Login
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
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
                    ModelState.AddModelError("", "Email or Password is Incorrect");
                    return View(userLogin);
                }

                var result = await signInManager.PasswordSignInAsync(user, userLogin.Password, userLogin.RememberMe, false);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Course");
                }

                ModelState.AddModelError("", "Email or Password is Incorrect");
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
        [ValidateAntiForgeryToken]
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
        #endregion
        //-----------------------------------------------------
        #region Logout
        public async Task<IActionResult> LogOut()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }
        #endregion
        //-----------------------------------------------------
        #region ResetPassword
        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordVM model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = await userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                //ModelState.AddModelError("", "Email Not Found!");
                return View(model);
            }

            var token = await userManager.GeneratePasswordResetTokenAsync(user);

            var callbackUrl = Url.Action("ResetPassword", "Account",
                new { token = token, email = model.Email },
                protocol: Request.Scheme);

            //await email.SendEmailAsync(model.Email, "Reset Password",
            //    $"Reset your password by <a href='{callbackUrl}'>clicking here</a>.");

            string message = $@"
                <p>Please reset your password by clicking the button below:</p>
                <a href='{callbackUrl}' style='
                    display:inline-block;
                    padding:10px 20px;
                    font-size:16px;
                    color:#fff;
                    background-color:#0d6efd;
                    border-radius:5px;
                    text-decoration:none;
                '>Reset Password</a>
                <p>If the button doesn't work, copy and paste this URL into your browser:</p>
                <p>{callbackUrl}</p>
                ";

            await email.SendEmailAsync(model.Email, "إعادة تعيين كلمة المرور", message);

            return RedirectToAction("ResetPasswordConfirmation");
        }

        [HttpGet]
        public IActionResult ResetPasswordConfirmation()
        {
            return View();
        }
        //This action will be accessed via the link in the email sent to the user
        [HttpGet]
        public IActionResult ResetPassword(string token, string email)
        {
            return View(new ResetPasswordVM { Token = token, Email = email });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordVM model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = await userManager.FindByEmailAsync(model.Email);
            if (user == null)
                return RedirectToAction("ResetPasswordConfirmation");

            var result = await userManager.ResetPasswordAsync(user, model.Token, model.NewPassword);

            if (result.Succeeded)
                return RedirectToAction("ResetPasswordConfirmation");

            foreach (var error in result.Errors)
                ModelState.AddModelError(string.Empty, error.Description);

            return View(model);
        }
        #endregion
    }
}
