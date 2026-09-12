using Microsoft.AspNetCore.Mvc;

namespace InsureYouAi.ViewComponents.Blog
{
    public class BlogListBreadCrumbViewComponent:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
