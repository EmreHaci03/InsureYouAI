using InsureYouAi.Entities;

namespace InsureYouAi.Service.Interfaces
{
    public interface IPolicyService:IGenericService<Policy>
    {
        Task<int> GeActivePolicyByCount(string id);
        Task<int> GetAllPolicyCount(string id);
        Task<List<Policy>> GetUserPolicyList(string id);
    }
}
