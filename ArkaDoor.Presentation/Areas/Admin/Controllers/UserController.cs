using AngleSharp.Io;
using ArkaDoor.Application.Services.Interfaces;
using ArkaDoor.Areas.Admin.Controllers;
using ArkaDoor.Domain.DTOs.Admin.User;
using ArkaDoor.Presentation.HttpManager;
using Microsoft.AspNetCore.Mvc;
namespace ArkaDoor.Presentation.Areas.Admin.Controllers;

public class UserController : AdminBaseController
{
    #region Ctor

    private readonly IUserService _userService;
    private readonly IRoleService _roleService;

    public UserController(IUserService userService, 
                          IRoleService roleService)
    {
        _userService = userService;
        _roleService = roleService;
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

    #region edit user

    public async Task<IActionResult> EditUser(ulong id , CancellationToken cancellation = default)
    {
        var user = await _userService.GetUserForEdit(id , cancellation);
        if (user == null)return NotFound();

        #region Page Data

        ViewData["Roles"] = await _roleService.GetSelectRolesList(cancellation);

        #endregion

        return View(user);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> EditUser(EditUserDTO userDTO, IFormFile UserAvatar, 
                                              CancellationToken cancellation = default)
    {
        #region Page Data

        ViewData["Roles"] = await _roleService.GetSelectRolesList(cancellation);

        #endregion

        var res = await _userService.EditUser(userDTO, UserAvatar, cancellation);
        switch (res)
        {
            case EditUserResult.DuplicateEmail:
                TempData[ErrorMessage] = "ایمیل وارد شده صحیح نمی باشد .";
                break;

            case EditUserResult.DuplicateMobileNumber:
                TempData[ErrorMessage] = "موبایل وارد شده تکراری می باشد.";
                break;

            case EditUserResult.Success:
                return RedirectToAction("Index");
        }

        return View();
    }

    #endregion

    #region remove user

    public async Task<IActionResult> RemoveUser(ulong userId , CancellationToken cancellation = default)
    {
        var res = await _userService.RemoveUserById(userId , default);
        if (res) return JsonResponseStatus.Success();

        return JsonResponseStatus.Error();
    }

    #endregion
}
