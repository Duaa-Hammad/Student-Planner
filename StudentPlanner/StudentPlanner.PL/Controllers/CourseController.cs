using Microsoft.AspNetCore.Mvc;

namespace StudentPlanner.PL.Controllers
{
    public class CourseController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Create()
        {
            return View();
        }
        public IActionResult Delete()
        {
            return View();
        }
    }
}
