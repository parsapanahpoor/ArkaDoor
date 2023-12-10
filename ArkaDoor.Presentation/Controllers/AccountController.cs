#region Usings

using ArkaDoor.Application.Services.Interfaces;
using ArkaDoor.Domain.DTOs.SiteSide.Account;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using static System.Runtime.InteropServices.JavaScript.JSType;

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

	[HttpPost("Register") , ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(UserRegisterDTO register , CancellationToken cancellationToken = default)
    {
		if (ModelState.IsValid)
		{
            var result = await _userService.RegisterUserAsync(register, cancellationToken);
            switch (result)
            {
                case RegisterUserResponse.Success:
                    TempData["success"] = "ثبت نام شما باموفقیت انجام شده است .";
                    return RedirectToAction(nameof(Login));

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

    #region Active mobile user

    [HttpGet("Active-mobile/{Mobile}/{Resend?}")]
    public async Task<IActionResult> ActiveUserByMobileActivationCode(string Mobile , CancellationToken cancellationToken, bool Resend = false)
    {
        #region Send Verification Code

        var result = await _userService.SendActivationCodeForUser(Mobile , cancellationToken , Resend);
        switch (result.SendActivationCodeResult)
        {
            case SendActivationCodeResult.Success:
                break;

            case SendActivationCodeResult.UserNotFound:
                TempData["error"] = "کاربری با اطلاعات وارد شده یافت نشده است.";
                return RedirectToAction(nameof(Login));

            case SendActivationCodeResult.Faild:
                TempData["error"] = "اطلاعات وارد شده صحیح نمی باشد.";
                return RedirectToAction(nameof(Login));

            default:
                break;
        }

        #endregion

        #region Time Counter Initilize

        ViewBag.Time = result.Time;

        ViewBag.Mobile = Mobile;

        #endregion

        return View();
    }

    [HttpPost("Active-mobile/{Mobile}/{Resend?}"), ValidateAntiForgeryToken]
    public async Task<IActionResult> ActiveUserByMobileActivationCode(ActiveMobileByActivationCodeDTO activeMobileByActivationCodeViewModel , 
                                                                      CancellationToken cancellationToken = default)
    {
        #region Active User Mobile

        if (ModelState.IsValid)
        {
            var result = await _userService.ActiveUserMobile(activeMobileByActivationCodeViewModel , cancellationToken);
            switch (result)
            {
                case ActiveMobileByActivationCodeResult.Success:
                    TempData["success"] = "فعال سازی حساب کاربری شما با موفقیت انجام شده است.";
                    return RedirectToAction(nameof(Login));

                case ActiveMobileByActivationCodeResult.AccountNotFound:
                    ModelState.AddModelError("Error", "کاربر مورد نظر یافت نشده است.");
                    return RedirectToAction("ActiveUserByMobileActivationCode", new { Mobile = activeMobileByActivationCodeViewModel.Mobile, Resend = false });
            }
        }

        #endregion

        return View(activeMobileByActivationCodeViewModel);
    }

    #endregion

    #region Login

    [HttpGet("login")]
    public IActionResult Login(string? returnUrl = "/")
    {
        return View();
    }

    [HttpPost("login") , ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginUserDTO model, CancellationToken cancellationToken = default , string? returnUrl = "/")
    {
        if (ModelState.IsValid)
        {
            var result = await _userService.LoginUserAsync(model , cancellationToken);
                switch (result.LoginUserResponse)
                {
                    case LoginUserResponse.Success:
                        break;

                    case LoginUserResponse.UserNotFound:
                        ModelState.AddModelError("Email", "کاربری با اطلاعات وارد شده صحیح نمی باشد .");
                        return View(model);

                    case LoginUserResponse.UserNotActive:
                        ModelState.AddModelError("Error", "حساب کاربری شما فعال نمی باشد.");
                        return RedirectToAction("ActiveUserByMobileActivationCode", new { Mobile = model.Mobile, Resend = true });

                    case LoginUserResponse.WrongPassword:
                        ModelState.AddModelError("Password", "کلمه ی عبور وارد شده صحیح نمی باشد.");
                        return View(model);

                    default:
                        ModelState.AddModelError("Error", "اطلاعات وارد شده صحیح نمی باشد.");
                        return View(model);
            }

            var claims = new List<Claim>
            {
                new (ClaimTypes.NameIdentifier, result.User.Id.ToString()),
                new (ClaimTypes.MobilePhone, result.User.Mobile),
                new (ClaimTypes.Name, result.User.Username),
            };

            var claimIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(claimIdentity);

            var authProps = new AuthenticationProperties();
            authProps.IsPersistent = model.RememberMe;

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, authProps);
            return Redirect(Url.IsLocalUrl(returnUrl) ? returnUrl : "/");
        }

        return View(model);
    }

    #endregion

    #region Logout

    [HttpGet("logout")]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync();
        return Redirect("/");
    }

    #endregion
}
