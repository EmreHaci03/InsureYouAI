using InsureYouAi.Entities;
using InsureYouAi.Service.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace InsureYouAi.Areas.Member.ViewComponents.MemberDashboard
{
    public class MemberDashboardUserArticleViewComponent : ViewComponent
    {
        private readonly UserManager<AppUser> userManager;
        private readonly IArticleService articleService;
        private readonly ICommentService commentService;

        public MemberDashboardUserArticleViewComponent(
            UserManager<AppUser> userManager,
            IArticleService articleService,
            ICommentService commentService)
        {
            this.userManager = userManager;
            this.articleService = articleService;
            this.commentService = commentService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var user = await userManager.GetUserAsync(HttpContext.User);
            if (user == null)
            {
                ViewBag.TopArticle = null;
                ViewBag.TopArticleCommentCount = 0;
                return View();
            }

            var userArticles = await articleService.GetUserArticleList(user.Id);

            if (!userArticles.Any())
            {
                ViewBag.TopArticle = null;
                ViewBag.TopArticleCommentCount = 0;
                return View();
            }

            var articleIds = userArticles.Select(x => x.ArticleId).ToList();
            var commentCounts = await commentService.GetCommentCountsByArticleIds(articleIds);

            Article? topArticle = null;
            int topCount = -1;

            foreach (var article in userArticles)
            {
                var count = commentCounts.TryGetValue(article.ArticleId, out var c) ? c : 0;
                if (count > topCount)
                {
                    topCount = count;
                    topArticle = article;
                }
            }

            ViewBag.TopArticle = topArticle;
            ViewBag.TopArticleCommentCount = topCount < 0 ? 0 : topCount;

            return View();
        }
    }
}