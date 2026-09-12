using Microsoft.AspNetCore.Mvc;

namespace InsureYouAi.ViewComponents.UILayout
{
    public class LayoutHeaderNavBarViewComponent:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
