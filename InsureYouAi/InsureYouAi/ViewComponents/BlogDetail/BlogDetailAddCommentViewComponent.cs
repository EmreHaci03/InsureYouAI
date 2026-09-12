using Microsoft.AspNetCore.Mvc;

namespace InsureYouAi.ViewComponents.BlogDetail
{
    public class BlogDetailAddCommentViewComponent:ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            ViewBag.ArticleId = id;
            return View();
        }
    }
}
