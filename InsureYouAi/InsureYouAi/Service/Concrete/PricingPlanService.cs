using InsureYouAi.Context;
using InsureYouAi.Entities;
using InsureYouAi.Models;
using InsureYouAi.Service.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InsureYouAi.Service.Concrete
{
    public class PricingPlanService : GenericService<PricingPlan>, IPricingPlanService
    {
        private readonly InsureAiContext _context;
        public PricingPlanService(InsureAiContext context) : base(context)
        {
            this._context = context;
        }

        public async Task<PricingPlan> GetFirstFeaturePlan()
        {
            return await _context.PricingPlans.Include(x => x.PricingPlanItems).FirstOrDefaultAsync(x => x.IsFeature);
        }

        public async Task<List<PricingPlan>> GetPricingPlanWithItem()
        {
            return await _context.PricingPlans.Include(x => x.PricingPlanItems).ToListAsync();
        }

        public async Task<List<PricingPlan>> GetAllNonFeaturedPlans()
        {
            return await _context.PricingPlans
                .Include(x => x.PricingPlanItems)
                .Where(x => !x.IsFeature)
                .ToListAsync();
        }

        public async Task<List<PlanFeatureCountViewModel>> GetPlanFeatureCounts()
        {
            return await _context.PricingPlans
                .Select(x => new PlanFeatureCountViewModel
                {
                    PlanFeature = x.Title,
                    PlanItemCount = x.PricingPlanItems.Count()
                }).ToListAsync();
        }
    }
}
