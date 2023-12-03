using Microsoft.AspNetCore.Mvc;

namespace ArkaDoor.Presentation.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}