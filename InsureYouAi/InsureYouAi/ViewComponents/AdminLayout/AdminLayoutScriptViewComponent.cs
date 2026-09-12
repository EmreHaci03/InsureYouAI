using Microsoft.AspNetCore.Mvc;

namespace InsureYouAi.ViewComponents.AdminLayout
{
    public class AdminLayoutScriptViewComponent:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
