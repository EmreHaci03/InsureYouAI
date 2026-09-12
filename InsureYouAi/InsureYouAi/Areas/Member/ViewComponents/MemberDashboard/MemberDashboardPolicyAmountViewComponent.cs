using InsureYouAi.Areas.Member.ViewComponents.MemberDashboard;
using InsureYouAi.Entities;
using InsureYouAi.Service.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace InsureYouAi.Areas.Member.ViewComponents.MemberDashboard
{
    public class MemberDashboardPolicyAmountViewComponent : ViewComponent
    {
        private readonly IPolicyService policyService;
        private readonly UserManager<AppUser> userManager;

        public MemberDashboardPolicyAmountViewComponent(IPolicyService policyService, UserManager<AppUser> userManager)
        {
            this.policyService = policyService;
            this.userManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var user = await userManager.GetUserAsync(HttpContext.User);
            if (user == null)
            {
                ViewBag.TotalAnnualPremium = 0m;
                return View();
            }

            var allPolicies = await policyService.GetUserPolicyList(user.Id);

            var totalPremium = allPolicies
                .Where(x => x.Status == "Active")
                .Sum(x => x.PremiumAmount);

            ViewBag.TotalAnnualPremium = totalPremium;

            return View();
        }
    }
}