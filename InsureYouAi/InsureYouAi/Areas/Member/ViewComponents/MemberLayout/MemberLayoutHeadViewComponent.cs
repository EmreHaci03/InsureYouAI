using Microsoft.AspNetCore.Mvc;

namespace InsureYouAi.Areas.Member.ViewComponents.MemberLayout
{
    public class MemberLayoutHeadViewComponent:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
