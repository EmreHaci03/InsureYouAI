using InsureYouAi.Entities;

namespace InsureYouAi.Dtos.PricingPlanItemDtos
{
    public class UpdatePricingPlanItemDto
    {
        public int PricingPlanItemId { get; set; }
        public string PlanItem { get; set; }

        public int PricingPlanId { get; set; }
    }
}
