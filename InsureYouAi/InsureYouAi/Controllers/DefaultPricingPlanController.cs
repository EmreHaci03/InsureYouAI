using AutoMapper;
using InsureYouAi.Dtos.PricingPlanDtos;
using InsureYouAi.Service.Concrete;
using InsureYouAi.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace InsureYouAi.Controllers
{
    public class DefaultPricingPlanController : Controller
    {
        private readonly IPricingPlanService pricingPlanService;
        private readonly IMapper _mapper;

        public DefaultPricingPlanController(IPricingPlanService pricingPlanService, IMapper mapper)
        {
            this.pricingPlanService = pricingPlanService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> DefaultPricingPlanList()
        {
            var values = await pricingPlanService.GetPricingPlanWithItem();
            var mapped = _mapper.Map<List<ResultPricingPlanDto>>(values);
            return View(mapped);
        }
    }
}
