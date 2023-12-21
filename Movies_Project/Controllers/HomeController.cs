using Microsoft.AspNetCore.Mvc;

namespace Movies_Project.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
