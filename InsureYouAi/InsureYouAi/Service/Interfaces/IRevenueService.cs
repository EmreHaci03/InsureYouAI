using InsureYouAi.Entities;
using InsureYouAi.Models;

namespace InsureYouAi.Service.Interfaces
{
    public interface IRevenueService:IGenericService<Revenue>
    {
        Task<List<MonthlyRevenueViewModel>> GetMonthlyRevenue();
        Task<decimal> GetTotalRevenue();
    }
}
