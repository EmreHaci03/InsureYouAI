using Microsoft.AspNetCore.Mvc;

namespace InsureYouAi.ViewComponents.AdminLayout
{
    public class AdminLayoutSwitcherViewComponent:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
