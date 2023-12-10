using ArkaDoor.Application.Services.Interfaces;
using ArkaDoor.Areas.Admin.Controllers;
using ArkaDoor.Domain.DTOs.Admin.User;
using Microsoft.AspNetCore.Mvc;
namespace ArkaDoor.Presentation.Areas.Admin.Controllers;

public class UserController : AdminBaseController
{
    #region Ctor

    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
            _userService = userService;
    }

    #endregion

    #region User List

    public async Task<IActionResult> Index(FilterUserDTO filter)
    {
        return View(await _userService.FilterUsers(filter));
    }

    #endregion

    #region User Profile

    public async Task<IActionResult> Profile(ulong id , CancellationToken cancellationToken)
    {
        var user = await _userService.GetByIdAsync(cancellationToken , id);
        if (user == null)
        {
            TempData[ErrorMessage] = "کاربری یافت نشده.";
            return RedirectToAction("Index");
        }

        return View(user);
    }

    #endregion
}
