using ArkaDoor.Areas.Admin.Controllers;
using Microsoft.AspNetCore.Mvc;
namespace ArkaDoor.Presentation.Areas.Admin.Controllers;

public class HomeController : AdminBaseController
{
    public IActionResult Index()
    {
        return View();
    }
}
