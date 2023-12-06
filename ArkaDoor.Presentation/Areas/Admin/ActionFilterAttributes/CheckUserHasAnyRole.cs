//using Academy.Application.Extensions;
//using Academy.Application.Services.Interfaces;
//using Microsoft.AspNetCore.Mvc.Filters;

//namespace Academy.Web.Areas.Admin.ActionFilterAttributes
//{
//    public class CheckUserHasAnyRole : ActionFilterAttribute
//    {
//        public override void OnActionExecuting(ActionExecutingContext context)
//        {
//            var service = (IPermissionService)context.HttpContext.RequestServices.GetService(typeof(IPermissionService))!;

//            base.OnActionExecuting(context);

//            var hasUserAnyRole = service.CheckUserHasAnyRole(context.HttpContext.User.GetUserId()).Result;

//            if (!hasUserAnyRole)
//            {
//                context.HttpContext.Response.Redirect("/");
//            }
//        }
//    }
//}
