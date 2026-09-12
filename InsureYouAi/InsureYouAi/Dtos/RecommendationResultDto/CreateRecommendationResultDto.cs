namespace InsureYouAi.Dtos.RecommendationResultDto
{
    public class CreateRecommendationResultDto
    {

        public string? FullName { get; set; }
        public int Age { get; set; }
        public string City { get; set; }
        public int FamilyMemberCount { get; set; }
        public string MaritalStatus { get; set; }           // "Evli" / "Bekar"
        public string TravelFrequency { get; set; }          // "Hiç", "Yılda 1-2 kez", "Yılda 3-5 kez", "Sık sık"
        public decimal MonthlyBudget { get; set; }
        public bool HasChronicIllness { get; set; }
        public bool IsSmoker { get; set; }
        public string OccupationRiskLevel { get; set; }       // "Düşük", "Orta", "Yüksek"
        public string PreferredCoverageType { get; set; }     // "Sağlık", "Seyahat", "Hayat", "Kombine", "Bilmiyorum"
        public string? AdditionalNotes { get; set; }
    }
}
