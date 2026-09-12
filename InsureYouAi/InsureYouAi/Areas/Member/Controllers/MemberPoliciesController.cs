using AutoMapper;
using InsureYouAi.Context;
using InsureYouAi.Dtos.PolicyDtos;
using InsureYouAi.Entities;
using InsureYouAi.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace InsureYouAi.Areas.Member.Controllers
{
    [Area("Member")]
    [Authorize]
    public class MemberPoliciesController : Controller
    {
        private readonly IPolicyService policyService;
        private readonly UserManager<AppUser> userManager;
        private readonly IMapper _mapper;

        public MemberPoliciesController(IPolicyService policyService, IMapper mapper, UserManager<AppUser> userManager)
        {
            this.policyService = policyService;
            _mapper = mapper;
            this.userManager = userManager;
        }

        public async Task<IActionResult> MemberPolicyList()
        {
            var user = await userManager.GetUserAsync(User);
            if (user == null)
            {
                TempData["ErrorMessage"] = "Poliçelerinizi görmek için giriş yapınız.";
                return View(new List<ResultPolicyDto>());
            }

            var memberPolicies = await policyService.GetUserPolicyList(user.Id);
            var mapped = _mapper.Map<List<ResultPolicyDto>>(memberPolicies);
            return View(mapped);
        }
    }
}
