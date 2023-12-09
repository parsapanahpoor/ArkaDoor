using ArkaDoor.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc.Filters;
namespace ArkaDoor.Presentation.Areas.Admin.ActionFilterAttributes;
using ArkaDoor.Application.Utilities.Extensions;

public class CheckUserHasAnyRole : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var service = (IRoleService)context.HttpContext.RequestServices.GetService(typeof(IRoleService))!;

        base.OnActionExecuting(context);

        var hasUserAnyRole = service.IsUserAdmin(context.HttpContext.User.GetUserId() , default).Result;

        if (!hasUserAnyRole)
        {
            context.HttpContext.Response.Redirect("/");
        }
    }
}
