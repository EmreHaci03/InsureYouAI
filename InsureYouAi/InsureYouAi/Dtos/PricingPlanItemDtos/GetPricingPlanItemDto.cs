using InsureYouAi.Entities;

namespace InsureYouAi.Dtos.PricingPlanItemDtos
{
    public class GetPricingPlanItemDto
    {
        public int PricingPlanItemId { get; set; }
        public string PlanItem { get; set; }

        public string PricingPlan { get; set; }
    }

}
