using Microsoft.AspNetCore.Mvc;

namespace InsureYouAi.ViewComponents.AdminDashboard
{
    public class AdminDashboardQuickLinksViewComponent:ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}
