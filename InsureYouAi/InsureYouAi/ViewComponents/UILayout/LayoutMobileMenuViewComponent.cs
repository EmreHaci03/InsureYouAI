using Microsoft.AspNetCore.Mvc;

namespace InsureYouAi.ViewComponents.UILayout
{
    public class LayoutMobileMenuViewComponent:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
