using InsureYouAi.Context;
using InsureYouAi.Service.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InsureYouAi.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ForecastController : Controller
    {
        private readonly InsureAiContext _context;
        private readonly ForecastService forecastService;

        public ForecastController(InsureAiContext context)
        {
            _context = context;
            this.forecastService = new ForecastService();
        }

        public async Task<IActionResult> Index()
        {
            var rawData = await _context.Policies
                .GroupBy(p => new { p.StartDate.Year, p.StartDate.Month })
                .Select(g => new
                {
                    Year = g.Key.Year,
                    Month = g.Key.Month,
                    Count = g.Count()
                })
                .OrderBy(x => x.Year).ThenBy(x => x.Month)
                .ToListAsync();

            var salesData = rawData.Select(x => new PolicySalesData
            {
                Date = new DateTime(x.Year, x.Month, 1),
                SaleCount = (float)x.Count
            }).OrderBy(x => x.Date).ToList();

            var forecast = forecastService.GetPolicySalesForecast(salesData, Horizon:24);
            for (int i = 0; i < forecast.ForecastedValues.Length; i++)
            {
                forecast.ForecastedValues[i] = Math.Max(0, forecast.ForecastedValues[i]);
                forecast.LowerBoundValues[i] = Math.Max(0, forecast.LowerBoundValues[i]);
                forecast.UpperBoundValues[i] = Math.Max(0, forecast.UpperBoundValues[i]);
            }

            ViewBag.Forecast = forecast;
            return View(salesData);
        }
    }
}