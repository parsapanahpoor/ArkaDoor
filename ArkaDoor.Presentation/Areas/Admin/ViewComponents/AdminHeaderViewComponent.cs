using Microsoft.AspNetCore.Mvc;

namespace ArkaDoor.Presentation.Areas.Admin.ViewComponents
{
    public class AdminHeaderViewComponent : ViewComponent
    {
        #region Ctor

    

        #endregion

        public async Task<IViewComponentResult> InvokeAsync()
        {

            return View("AdminHeader");
        }
    }
}
