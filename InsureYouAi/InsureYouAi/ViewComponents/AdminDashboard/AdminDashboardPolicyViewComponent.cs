using InsureYouAi.Context;
using InsureYouAi.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace InsureYouAi.ViewComponents.AdminDashboard
{
    public class AdminDashboardPolicyViewComponent:ViewComponent
    {
        private readonly InsureAiContext _context;

        public AdminDashboardPolicyViewComponent(InsureAiContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var Year = DateTime.Now.Year;
            var Month = DateTime.Now.Month;

            var Policy = await _context.Policies
                .Where(x => x.CreatedDate.Year == Year && x.CreatedDate.Month == Month)
                .GroupBy(x => x.PolicyType)
                .Select(g => new
                {
                    Detail = g.Key,
                    Sum = g.Sum(x => x.PremiumAmount)
                }).ToListAsync();

            ViewBag.Detail = JsonSerializer.Serialize(Policy.Select(x => x.Detail));
            ViewBag.Sum = JsonSerializer.Serialize(Policy.Select(x => x.Sum));
            return View();
        }
    }
}
