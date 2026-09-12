namespace InsureYouAi.Entities
{
    public class PricingPlanItem
    {
        public int PricingPlanItemId { get; set; }
        public string PlanItem {  get; set; }

        public int PricingPlanId { get; set; }
        public PricingPlan PricingPlan { get; set; }
    }

}
