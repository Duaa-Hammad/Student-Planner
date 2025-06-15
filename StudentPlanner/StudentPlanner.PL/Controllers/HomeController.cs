using Microsoft.AspNetCore.Mvc;
using StudentPlanner.BLL.Models;

namespace StudentPlanner.PL.Controllers;

public class HomeController : Controller
{
      public IActionResult Index()
        {
        return View();
      }
}
