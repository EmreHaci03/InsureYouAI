using InsureYouAi.Entities;

namespace InsureYouAi.Service.Interfaces
{
    public interface IPricingPlanItemService : IGenericService<PricingPlanItem>
    {
        Task<List<PricingPlanItem>> PricingPlanListItemWithPlan();
        Task<PricingPlanItem> GetPricingPlanItemWithPlan(int id);

        Task<List<PricingPlanItem>> GetPricingPlanItemListWithPlan(int id);
    }
}
