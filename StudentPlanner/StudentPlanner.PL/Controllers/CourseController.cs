using Microsoft.AspNetCore.Mvc;
using StudentPlanner.BLL.Repository;
using StudentPlanner.BLL.Interfaces;

namespace StudentPlanner.PL.Controllers
{
    public class CourseController : Controller
    {
        //Dependency Injection for Course Repository
        private readonly ICourse data;
        public CourseController(ICourse data)
        {
            this.data = data;
        }
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
