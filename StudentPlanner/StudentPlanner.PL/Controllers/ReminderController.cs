using Microsoft.AspNetCore.Mvc;

namespace StudentPlanner.PL.Controllers
{
    public class ReminderController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
