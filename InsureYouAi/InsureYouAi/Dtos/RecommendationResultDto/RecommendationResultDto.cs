namespace InsureYouAi.Dtos.RecommendationResultDto
{
    public class RecommendationResultDto
    {
        public int? RecommendedPlanId { get; set; }
        public string RecommendedPlanTitle { get; set; }
        public decimal RecommendedPlanPrice { get; set; }
        public string ConfidenceLevel { get; set; }   // "Düşük", "Orta", "Yüksek"
        public string Reasoning { get; set; }
        public List<string> Tips { get; set; } = new();
    }
}
