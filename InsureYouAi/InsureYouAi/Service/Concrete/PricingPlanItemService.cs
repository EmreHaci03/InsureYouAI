using InsureYouAi.Context;
using InsureYouAi.Entities;
using InsureYouAi.Service.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InsureYouAi.Service.Concrete
{
    public class PricingPlanItemService : GenericService<PricingPlanItem>, IPricingPlanItemService
    {
        private readonly InsureAiContext _context;
        public PricingPlanItemService(InsureAiContext context) : base(context)
        {
            this._context = context;
        }

        public async Task<List<PricingPlanItem>> GetPricingPlanItemListWithPlan(int id)
        {
            return await _context.PricingPlanItems.Include(x => x.PricingPlan).Where(x => x.PricingPlanId == id).ToListAsync();
        }

        public async Task<PricingPlanItem> GetPricingPlanItemWithPlan(int id)
        {
            return await _context.PricingPlanItems.Include(x => x.PricingPlan).FirstOrDefaultAsync(x=>x.PricingPlanItemId==id);
        }

        public async Task<List<PricingPlanItem>> PricingPlanListItemWithPlan()
        {
            return await _context.PricingPlanItems.Include(x => x.PricingPlan).ToListAsync();
        }
    }
}
