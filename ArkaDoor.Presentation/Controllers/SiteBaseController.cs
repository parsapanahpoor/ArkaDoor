using Microsoft.AspNetCore.Mvc;

namespace ArkaDoor.Presentation.Controllers;

public class SiteBaseController : Controller
{
    public static string SuccessMessage = "SuccessMessage";
    public static string ErrorMessage = "ErrorMessage";
    public static string InfoMessage = "InfoMessage";
    public static string WarningMessage = "WarningMessage";

    // Swal Messages Temp Data Key
    public static string SwalSuccess = "success";
    public static string SwalError = "error";
    public static string SwalInfo = "info";
}
