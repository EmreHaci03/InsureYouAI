using InsureYouAi.Context;
using InsureYouAi.Service.Concrete;
using InsureYouAi.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace InsureYouAi.ViewComponents.AdminDashboard
{
    public class AdminDashboardMainChartViewComponent:ViewComponent
    {
        private readonly InsureAiContext _context;

        public AdminDashboardMainChartViewComponent(InsureAiContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var currentYear = DateTime.Now.Year;
            var currentMonth = DateTime.Now.Month;

            var expenseData = await _context.Expenses
                .Where(x => x.ProcessDate.Year == currentYear && x.ProcessDate.Month == currentMonth)
                .GroupBy(x => x.Detail)
                .Select(g => new
                {
                    Category = g.Key,
                    TotalAmount = g.Sum(x => x.Amount)
                })
                .ToListAsync();

            var totalExpense = expenseData.Sum(x => x.TotalAmount);

            ViewBag.ExpenseLabel = JsonSerializer.Serialize(expenseData.Select(x => x.Category).ToList());
            ViewBag.ExpenseValues = JsonSerializer.Serialize(expenseData.Select(x => x.TotalAmount).ToList());
            ViewBag.TotalExpense = totalExpense;
            return View();
        }
    }
}
