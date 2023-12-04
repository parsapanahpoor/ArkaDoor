#region Usings

using ArkaDoor.Application.Services.Interfaces;
using ArkaDoor.Domain.DTOs.SiteSide.Account;
using Microsoft.AspNetCore.Mvc;

namespace ArkaDoor.Presentation.Controllers;

#endregion

public class AccountController : SiteBaseController
{
	#region Ctor

    private readonly IUserService _userService;

    public AccountController(IUserService userService)
    {
            _userService = userService;
    }

    #endregion

    #region Register

    [HttpGet("Register")]
	public IActionResult Register()
	{
		return View();
	}

	[HttpPost , ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(UserRegisterDTO register , CancellationToken cancellationToken = default)
    {
		if (ModelState.IsValid)
		{
            var result = await _userService.RegisterUserAsync(register, cancellationToken);
            switch (result)
            {
                case RegisterUserResponse.Success:
                    TempData["success"] = "ثبت نام شما باموفقیت انجام شده است .";
                    return RedirectToAction("ActiveUserByMobileActivationCode", new { Mobile = register.Mobile });

                case RegisterUserResponse.MobileExist:
                    TempData["error"] = "کاربری با موبایل وارد شده در سایت موجود است.";
                    break;

                default:
                    TempData["error"] = "اطلاعات وارد شده صحیح نمی باشد.";
                    break;
            }
        }

        return View(register);
    }

    #endregion
}
