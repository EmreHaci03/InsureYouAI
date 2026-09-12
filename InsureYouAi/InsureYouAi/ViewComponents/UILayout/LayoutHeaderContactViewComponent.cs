using Microsoft.AspNetCore.Mvc;

namespace InsureYouAi.ViewComponents.UILayout
{
    public class LayoutHeaderContactViewComponent:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
