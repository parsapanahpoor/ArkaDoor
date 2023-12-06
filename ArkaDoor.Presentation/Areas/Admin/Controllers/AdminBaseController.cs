using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ArkaDoor.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    //[CheckUserHasAnyRole]
    public class AdminBaseController : Controller
    {
        public static string SuccessMessage = "SuccessMessage";
        public static string ErrorMessage = "ErrorMessage";
        public static string InfoMessage = "InfoMessage";
        public static string WarningMessage = "WarningMessage";
    }
}
