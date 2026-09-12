using Microsoft.AspNetCore.Mvc;

namespace InsureYouAi.ViewComponents.AdminLayout
{
    public class AdminLayoutSideBarViewComponent:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
