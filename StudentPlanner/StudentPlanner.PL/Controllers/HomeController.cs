using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentPlanner.BLL.Models;

namespace StudentPlanner.PL.Controllers;
[Authorize]
public class HomeController : Controller
{
    [Authorize]
      public IActionResult Index()
        {
        return View();
      }
}
