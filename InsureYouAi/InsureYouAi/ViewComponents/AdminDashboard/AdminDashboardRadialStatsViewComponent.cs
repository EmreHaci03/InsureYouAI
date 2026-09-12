using InsureYouAi.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace InsureYouAi.ViewComponents.AdminDashboard
{
    public class AdminDashboardRadialStatsViewComponent:ViewComponent
    {
        private readonly IArticleService articleService;
        private readonly ICommentService commentService;
        private readonly IPricingPlanService pricingPlanService;
        public AdminDashboardRadialStatsViewComponent(IArticleService articleService, ICommentService commentService, IPricingPlanService pricingPlanService)
        {
            this.articleService = articleService;
            this.commentService = commentService;
            this.pricingPlanService = pricingPlanService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var topArticles = await articleService.GetTop5ArticlesByCommentCount();


            ViewBag.ArticleTitles = JsonSerializer.Serialize(topArticles.Select(x => x.Title));
            ViewBag.ArticleCounts = JsonSerializer.Serialize(topArticles.Select(x => x.CommentCount));

            var TopComments = await commentService.GetTop5Commenters();
            ViewBag.UserName = JsonSerializer.Serialize(TopComments.Select(x => x.UserName));
            ViewBag.CommentCount = JsonSerializer.Serialize(TopComments.Select(x => x.CommentCount));


            var TopPlanItem = await pricingPlanService.GetPlanFeatureCounts();
            ViewBag.Plan = JsonSerializer.Serialize(TopPlanItem.Select(x =>x.PlanFeature));
            ViewBag.FeatureCount = JsonSerializer.Serialize(TopPlanItem.Select(x =>x.PlanItemCount));


            var CategoryNameByArticleCount = await articleService.CategoryListByArticleCount();

            ViewBag.CategoryName = JsonSerializer.Serialize(CategoryNameByArticleCount.Select(x => x.CategoryName));
            ViewBag.CategoryArticleCount = JsonSerializer.Serialize(CategoryNameByArticleCount.Select(x => x.ArticleCount));

            return View();
        }
    }
}
