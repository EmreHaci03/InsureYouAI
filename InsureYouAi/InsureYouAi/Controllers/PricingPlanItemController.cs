using AutoMapper;
using InsureYouAi.Dtos.PricingPlanDtos;
using InsureYouAi.Dtos.PricingPlanItemDtos;
using InsureYouAi.Entities;
using InsureYouAi.Service.Concrete;
using InsureYouAi.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InsureYouAi.Controllers
{
    [Authorize(Roles = "Admin")]
    public class PricingPlanItemController : Controller
    {
        private readonly IPricingPlanItemService pricingPlanItemService;
        private readonly IPricingPlanService pricingPlanService;
        private readonly IMapper _mapper;
        public PricingPlanItemController(IPricingPlanItemService pricingPlanItemService, IMapper mapper, IPricingPlanService pricingPlanService)
        {
            this.pricingPlanItemService = pricingPlanItemService;
            _mapper = mapper;
            this.pricingPlanService = pricingPlanService;
        }

        [HttpGet]
        public async Task<IActionResult> PricingPlanItemList()
        {
            var values = await pricingPlanItemService.PricingPlanListItemWithPlan();
            var mapped = _mapper.Map<List<ResultPricingPlanItemDto>>(values);
            return View(mapped);
        }

        [HttpGet]
        public async Task<IActionResult> CreatePricingPlanItem()
        {
            var pricingPlan = await pricingPlanService.GetAllAsync();
            var mapper = _mapper.Map<List<ResultPricingPlanDto>>(pricingPlan);
            ViewBag.Plans = mapper;
            return View(new CreatePricingPlanItemDto());
        }
        [HttpPost]
        public async Task<IActionResult> CreatePricingPlanItem(CreatePricingPlanItemDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            var PricingPlanItem = _mapper.Map<PricingPlanItem>(dto);
            await pricingPlanItemService.AddAsync(PricingPlanItem);
            TempData["SuccessMessage"] = "Ödeme Planı Elemanı başarıyla Eklendi.";
            return RedirectToAction("PricingPlanItemList");
        }

        [HttpPost]
        public async Task<IActionResult> DeletePricingPlanItem(int id)
        {
            var PricingPlan = await pricingPlanItemService.GetPricingPlanItemWithPlan(id);
            if (PricingPlan is null)
                return NotFound();

            await pricingPlanItemService.DeleteAsync(PricingPlan);
            TempData["SuccessMessage"] = "Ödeme Planı Elemanı başarıyla silindi.";
            return RedirectToAction("PricingPlanItemList");
        }


        [HttpGet]
        public async Task<IActionResult> UpdatePricingPlanItem(int id)
        {
            var pricingPlan = await pricingPlanService.GetAllAsync();
            var Pricingmapper = _mapper.Map<List<ResultPricingPlanDto>>(pricingPlan);
            ViewBag.Plans = Pricingmapper;
            var PricingPlanItem = await pricingPlanItemService.GetPricingPlanItemWithPlan(id);
            if (PricingPlanItem is null)
                return NotFound();

            var mapper = _mapper.Map<UpdatePricingPlanItemDto>(PricingPlanItem);
            return View(mapper);
        }

        [HttpPost]
        public async Task<IActionResult> UpdatePricingPlanItem(UpdatePricingPlanItemDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            var PricingPlanItem = _mapper.Map<PricingPlanItem>(dto);
            await pricingPlanItemService.UpdateAsync(PricingPlanItem);
            TempData["SuccessMessage"] = "Ödeme Planı Elemanı başarıyla Güncellendi.";
            return RedirectToAction("PricingPlanItemList");
        }
    }
}
