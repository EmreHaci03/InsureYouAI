using InsureYouAi.Context;
using InsureYouAi.Entities;
using InsureYouAi.Service.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InsureYouAi.Service.Concrete
{
    public class PolicyService : GenericService<Policy>, IPolicyService
    {
        private readonly InsureAiContext _context;
        public PolicyService(InsureAiContext context) : base(context)
        {
            this._context = context;
        }

        public async Task<int> GeActivePolicyByCount(string id)
        {
            return await _context.Policies
                .Where(x => x.AppUserId == id)
                .CountAsync(x => x.Status == "Active");

        }

        public async Task<int> GetAllPolicyCount(string id)
        {
            return await _context.Policies
                 .Where(x => x.AppUserId == id)
                 .CountAsync();
        }

        public async Task<List<Policy>> GetUserPolicyList(string id)
        {
            return await _context.Policies.Where(x => x.AppUserId == id).ToListAsync();
        }
    }
}
