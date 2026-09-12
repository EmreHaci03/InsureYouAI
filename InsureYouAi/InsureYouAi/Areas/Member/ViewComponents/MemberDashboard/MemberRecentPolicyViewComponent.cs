using AutoMapper;
using InsureYouAi.Dtos.PolicyDtos;
using InsureYouAi.Entities;
using InsureYouAi.Service.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace InsureYouAi.Areas.Member.ViewComponents.MemberDashboard
{
    public class MemberRecentPolicyViewComponent : ViewComponent
    {
        private readonly UserManager<AppUser> userManager;
        private readonly IPolicyService policyService;
        private readonly IMapper _mapper;

        public MemberRecentPolicyViewComponent(UserManager<AppUser> userManager, IPolicyService policyService, IMapper mapper)
        {
            this.userManager = userManager;
            this.policyService = policyService;
            _mapper = mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var user = await userManager.GetUserAsync(HttpContext.User);
            if (user == null)
                return View(new List<ResultPolicyDto>());

            var policies = await policyService.GetUserPolicyList(user.Id);

            var recentPolicies = policies
                .OrderByDescending(x => x.StartDate)
                .Take(10)
                .ToList();

            var mapped = _mapper.Map<List<ResultPolicyDto>>(recentPolicies);

            return View(mapped);
        }
    }
}