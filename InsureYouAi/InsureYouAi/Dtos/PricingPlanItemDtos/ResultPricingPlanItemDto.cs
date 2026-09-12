using InsureYouAi.Entities;

namespace InsureYouAi.Dtos.PricingPlanItemDtos
{
    public class ResultPricingPlanItemDto
    {
        public int PricingPlanItemId { get; set; }
        public string PlanItem { get; set; }
        public string PricingPlan { get; set; }
    }
}
