using AutoMapper;
using InsureYouAi.Dtos.PricingPlanDtos;
using InsureYouAi.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace InsureYouAi.ViewComponents.Default
{
    public class DefaultPricingViewComponent : ViewComponent
    {
        private readonly IPricingPlanService pricingPlanService;
        private readonly IPricingPlanItemService pricingPlanItemService;
        private readonly IMapper _mapper;

        public DefaultPricingViewComponent(IPricingPlanService pricingPlanService, IMapper mapper, IPricingPlanItemService pricingPlanItemService)
        {
            this.pricingPlanService = pricingPlanService;
            _mapper = mapper;
            this.pricingPlanItemService = pricingPlanItemService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var plan = await pricingPlanService.GetAllNonFeaturedPlans();
            var mapper = _mapper.Map<List<ResultPricingPlanDto>>(plan);
            return View(mapper);
        }
    }
}