#region Usings

using Microsoft.AspNetCore.Mvc;
namespace ArkaDoor.Presentation.Controllers;

#endregion

public class AccountController : Controller
{
	#region Ctor



	#endregion

	#region Register

	[HttpGet("Register")]
	public IActionResult Register()
	{
		return View();
	}



	#endregion
}
