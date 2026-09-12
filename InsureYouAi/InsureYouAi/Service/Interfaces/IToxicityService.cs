using InsureYouAi.Dtos.ToxicityDtos;

namespace InsureYouAi.Service.Interfaces
{
    public interface IToxicityService
    {
        public Task<ToxicityResultDto> AnalyzeAsync(string turkishText);
    }
}
