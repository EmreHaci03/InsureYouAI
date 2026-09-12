using Microsoft.AspNetCore.Mvc;

namespace InsureYouAi.Areas.Member.ViewComponents.MemberLayout
{
    public class MemberLayoutFooterViewComponent:ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}
