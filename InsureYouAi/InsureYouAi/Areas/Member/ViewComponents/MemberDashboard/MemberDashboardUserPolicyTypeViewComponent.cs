using InsureYouAi.Entities;
using InsureYouAi.Models;
using InsureYouAi.Service.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace InsureYouAi.Areas.Member.ViewComponents.MemberDashboard
{
    public class MemberDashboardUserPolicyTypeViewComponent:ViewComponent
    {
        private readonly IPolicyService policyService;
        private readonly UserManager<AppUser> userManager;

        public MemberDashboardUserPolicyTypeViewComponent(IPolicyService policyService, UserManager<AppUser> userManager)
        {
            this.policyService = policyService;
            this.userManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var user = await userManager.GetUserAsync(HttpContext.User);
            if (user == null)
                return View();



            var policies = await policyService.GetUserPolicyList(user.Id);

            var activePolicies = policies.Where(x => x.Status == "Active").ToList();

            var grouped = activePolicies
                .GroupBy(x => x.PolicyType)
                .Select(g => new PolicyTypeCountModel
                {
                    PolicyType = g.Key,
                    Count = g.Count()
                })
                .OrderByDescending(x => x.Count)
                .ToList();

            ViewBag.PolicyTypeBreakdown = grouped;
            ViewBag.TotalCount = activePolicies.Count;

            return View();
        }
    }
}
