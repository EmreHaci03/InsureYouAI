using AutoMapper;
using InsureYouAi.Entities;
using InsureYouAi.Service.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Specialized;

namespace InsureYouAi.Areas.Member.ViewComponents.MemberDashboard
{
    public class MemberPolicyCountViewComponent:ViewComponent
    {
        private readonly IPolicyService policyService;
        private readonly UserManager<AppUser> userManager;
        private readonly IMapper _mapper;

        public MemberPolicyCountViewComponent(IPolicyService policyService, IMapper mapper, UserManager<AppUser> userManager)
        {
            this.policyService = policyService;
            _mapper = mapper;
            this.userManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var user = await userManager.GetUserAsync(HttpContext.User);
            if (user == null)
                return View();

            var Count = await policyService.GeActivePolicyByCount(user.Id);
            ViewBag.PolicyCount= Count;
            return View();
        }
    }
}
