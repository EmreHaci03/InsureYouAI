using InsureYouAi.Entities;

namespace InsureYouAi.Dtos.PolicyDtos
{
    public class GetPolicyByIdDto
    {
        public int PolicyId { get; set; }
        public string PolicyNumber { get; set; } 
        public string PolicyType { get; set; }
        public decimal PremiumAmount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Status { get; set; } 
        public string User { get; set; } 
    }
}
