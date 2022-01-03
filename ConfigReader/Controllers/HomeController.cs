using Microsoft.AspNetCore.Mvc;

namespace ConfigReader.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
