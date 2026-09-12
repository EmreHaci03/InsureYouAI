using InsureYouAi.Entities;
using InsureYouAi.Models;

namespace InsureYouAi.Service.Interfaces
{
    public interface IPricingPlanService : IGenericService<PricingPlan>
    {
        Task<List<PricingPlan>> GetPricingPlanWithItem();

        Task<List<PlanFeatureCountViewModel>> GetPlanFeatureCounts();
        Task<List<PricingPlan>> GetAllNonFeaturedPlans();
        Task<PricingPlan> GetFirstFeaturePlan();
    }
}
