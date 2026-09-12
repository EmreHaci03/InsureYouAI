using Microsoft.AspNetCore.Mvc;

namespace InsureYouAi.ViewComponents.Default
{
    public class DefaultSkillViewComponent:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
