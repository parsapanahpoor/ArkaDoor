using Microsoft.AspNetCore.Mvc;

namespace ArkaDoor.Presentation.Areas.Admin.ViewComponents
{
    public class AdminSideBarViewComponent : ViewComponent
    {
        #region Ctor

        public AdminSideBarViewComponent()
        {

        }

        #endregion

        public async Task<IViewComponentResult> InvokeAsync()
        {


            return View("AdminSideBar");
        }
    }
}
