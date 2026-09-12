using InsureYouAi.Context;
using InsureYouAi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InsureYouAi.ViewComponents.AdminDashboard
{
    public class AdminDashboardStatisticViewComponent : ViewComponent
    {
        private readonly InsureAiContext _context;

        public AdminDashboardStatisticViewComponent(InsureAiContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var policy = await _context.Policies
                .GroupBy(x => x.PolicyType)
                .Select(x => new PolicyTypeCountViewModel
                {
                    PolicyType = x.Key,
                    PolicyCount = x.Count()
                }).ToListAsync();

            ViewBag.PolicyTypeNames = System.Text.Json.JsonSerializer.Serialize(policy.Select(x => x.PolicyType));
            ViewBag.PolicyTypeCounts = System.Text.Json.JsonSerializer.Serialize(policy.Select(x => x.PolicyCount));

            var totalComments = await _context.Comments.CountAsync();
            var totalMessages = await _context.Messages.CountAsync();

            ViewBag.TotalComments = totalComments;
            ViewBag.TotalMessages = totalMessages;

            return View();
        }
    }
}