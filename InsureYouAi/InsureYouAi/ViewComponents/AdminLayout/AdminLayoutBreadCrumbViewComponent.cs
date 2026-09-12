using Microsoft.AspNetCore.Mvc;

namespace InsureYouAi.ViewComponents.AdminLayout
{
    public class AdminLayoutBreadCrumbViewComponent:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
