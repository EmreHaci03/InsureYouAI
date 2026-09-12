using InsureYouAi.Context;
using InsureYouAi.Entities;
using InsureYouAi.Models;
using InsureYouAi.Service.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InsureYouAi.Service.Concrete
{
    public class RevenueService : GenericService<Revenue>, IRevenueService
    {
        private readonly InsureAiContext _context;
        public RevenueService(InsureAiContext context) : base(context)
        {
            this._context = context;
        }

        public async Task<List<MonthlyRevenueViewModel>> GetMonthlyRevenue()
        {
            var grouped = await _context.Revenues
                 .GroupBy(x => new { x.ProcessDate.Year, x.ProcessDate.Month })
                 .Select(g => new
                 {
                     g.Key.Year,
                     g.Key.Month,
                     Total = g.Sum(x => x.Amount)
                 })
                 .OrderBy(x => x.Year).ThenBy(x => x.Month)
                 .ToListAsync();

            string[] monthNames = { "Oca", "Şub", "Mar", "Nis", "May", "Haz", "Tem", "Ağu", "Eyl", "Eki", "Kas", "Ara" };

            return grouped.Select(x => new MonthlyRevenueViewModel
            {
                Month=x.Month,
                Year=x.Year,
                MonthName = monthNames[x.Month-1],
                TotalAmount=x.Total
            }).ToList();
        }

        public async Task<decimal> GetTotalRevenue()
        {
            var values= await _context.Revenues.SumAsync(x=>x.Amount);
            return values;
        }
    }
}
