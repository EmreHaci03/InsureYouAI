using InsureYouAi.Entities;
using InsureYouAi.Service.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace InsureYouAi.Areas.Member.ViewComponents.MemberDashboard
{
    public class MemberAllPolicyCountViewComponent:ViewComponent
    {
        private readonly IPolicyService policyService;
        private readonly UserManager<AppUser> userManager;
        public MemberAllPolicyCountViewComponent(IPolicyService policyService, UserManager<AppUser> userManager)
        {
            this.policyService = policyService;
            this.userManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var user = await userManager.GetUserAsync(HttpContext.User);
            if (user == null)
                return View();

            var policy = await policyService.GetAllPolicyCount(user.Id);
            ViewBag.PolicyCount = policy;
            return View();
        }
    }
}
