using AutoMapper;
using InsureYouAi.Dtos.PricingPlanDtos;
using InsureYouAi.Dtos.PricingPlanItemDtos;
using InsureYouAi.Service.Concrete;
using InsureYouAi.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Numerics;

namespace InsureYouAi.ViewComponents.Default
{
    public class DefaultFeaturePricingViewComponent:ViewComponent
    {
        private readonly IPricingPlanService pricingPlanService;
        private readonly IPricingPlanItemService pricingPlanItemService;
        private readonly IMapper _mapper;
        public DefaultFeaturePricingViewComponent(IPricingPlanService pricingPlanService, IMapper mapper, IPricingPlanItemService pricingPlanItemService)
        {
            this.pricingPlanService = pricingPlanService;
            _mapper = mapper;
            this.pricingPlanItemService = pricingPlanItemService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var values=await pricingPlanService.GetFirstFeaturePlan();
            ViewBag.planItem = await pricingPlanItemService.GetPricingPlanItemListWithPlan(values.PricingPlanId);
            var mapper = _mapper.Map<GetPricingPlanByIdDto>(values);
            return View(mapper);
        }
    }
}
