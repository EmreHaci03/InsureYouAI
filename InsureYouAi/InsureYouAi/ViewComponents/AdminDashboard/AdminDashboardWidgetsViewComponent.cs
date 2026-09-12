using InsureYouAi.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InsureYouAi.Entities.Enums;

namespace InsureYouAi.ViewComponents.AdminDashboard
{
    public class AdminDashboardWidgetsViewComponent:ViewComponent
    {
        private readonly InsureAiContext _context;

        public AdminDashboardWidgetsViewComponent(InsureAiContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var articles = await _context.Articles.CountAsync();
            var Users=await _context.Users.CountAsync();
            var PendingComments = await _context.Comments.Where(x => x.CommentStatus == CommentStatusType.Pending).CountAsync();
            var UnReadMessages = await _context.Messages.Where(x =>!x.IsRead).CountAsync();
            ViewBag.TotalArticles = articles;
            ViewBag.TotalUsers = Users;
            ViewBag.PendingComments = PendingComments;
            ViewBag.UnreadMessages = UnReadMessages;

            int n1, n2, n3, n4;
            int r1, r2, r3, r4;

            Random random = new Random();
            n1 = random.Next(0, 10);
            r1 = random.Next(0, 30);

            n2 = random.Next(0, 10);
            r2 = random.Next(0, 30);

            n3 = random.Next(0, 10);
            r3 = random.Next(0, 30);

            n4 = random.Next(0, 10);
            r4 = random.Next(0, 30);

            ViewBag.n1 = n1 + "." + r1;
            ViewBag.n2 = n2 + "." + r2;
            ViewBag.n3 = n3 + "." + r3;
            ViewBag.n4 = n4 + "." + r4;
            return View();
        }
    }
}
