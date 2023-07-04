using Microsoft.AspNetCore.Mvc;

namespace Puzzles.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
