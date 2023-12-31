﻿using ArkaDoor.Application.Services.Interfaces;
using ArkaDoor.Areas.Admin.Controllers;
using ArkaDoor.Domain.DTOs.Admin;
using ArkaDoor.Presentation.HttpManager;
using Microsoft.AspNetCore.Mvc;

namespace ArkaDoor.Presentation.Areas.Admin.Controllers;

public class RoleController : AdminBaseController
{
    #region Ctor

    private readonly IRoleService _roleService;

    public RoleController(IRoleService roleService)
    {
        _roleService = roleService;
    }

    #endregion

    #region Filter Roles

    public async Task<IActionResult> FilterRoles(FilterRolesDTO filter, CancellationToken cancellationToken = default)
    {
        var result = await _roleService.FilterRoles(filter, cancellationToken);

        return View(result);
    }

    #endregion

    #region Create Role

    public async Task<IActionResult> CreateRole(ulong? parentId, CancellationToken cancellationToken = default)
    {
        ViewBag.parentId = parentId;

        if (parentId != null)
        {
            ViewBag.parentRole = await _roleService.GetRoleById(parentId.Value, cancellationToken);
        }

        return View();
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateRole(CreateRoleDTO create, CancellationToken cancellationToken = default)
    {
        if (!ModelState.IsValid)
        {
            TempData[ErrorMessage] = "اطلاعات وارد شده صحیح نمی باشد.";
            return View(create);
        }

        var result = await _roleService.CreateRole(create, cancellationToken);

        if (result)
        {
            TempData[SuccessMessage] = "عملیات باموفقیت انجام شده است.";
            return RedirectToAction("FilterRoles", "Role", new { area = "Admin" });
        }

        TempData[WarningMessage] = "عنوان یکتا موجود است.";
        return View(create);
    }

    #endregion

    #region Edit Role

    public async Task<IActionResult> EditRole(ulong id , CancellationToken cancellationToken = default)
    {
        var result = await _roleService.FillEditRoleViewModel(id , cancellationToken);
        if (result == null)return NotFound();

        return View(result);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> EditRole(EditRoleDTO edit, CancellationToken cancellationToken = default)
    {
        if (!ModelState.IsValid)
        {
            TempData[ErrorMessage] = "اطلاعات وارد شده صحیح نمی باشد.";
            return View(edit);
        }

        var result = await _roleService.EditRole(edit, cancellationToken) ;
        switch (result)
        {
            case EditRoleResult.Success:
                TempData[SuccessMessage] = "عملیات باموفقیت انجام شده است.";
                return RedirectToAction("FilterRoles", "Role", new { area = "Admin" });
            case EditRoleResult.RoleNotFound:
                TempData[ErrorMessage] = "نقش مورد نظر یافت نشده است.";
                return RedirectToAction("FilterRoles", "Role", new { area = "Admin" });
            case EditRoleResult.UniqueNameExists:
                TempData[WarningMessage] = "عنوان یکتا موجود است.";
                break;
        }

        return View(edit);
    }

    #endregion

    #region Delete Role

    public async Task<IActionResult> DeleteRole(ulong roleId , CancellationToken cancellation = default)
    {
        var result = await _roleService.DeleteRole(roleId , cancellation);
        if (result) return JsonResponseStatus.Success();

        return JsonResponseStatus.Error();
    }

    #endregion

}
