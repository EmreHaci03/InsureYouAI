using AutoMapper;
using InsureYouAi.Dtos.PricingPlanDtos;
using InsureYouAi.Entities;
using InsureYouAi.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InsureYouAi.Controllers
{
    [Authorize(Roles = "Admin")]
    public class PricingPlanController : Controller
    {
        private readonly IPricingPlanService pricingPlanService;
        private readonly IMapper _mapper;
        public PricingPlanController(IPricingPlanService pricingPlanService, IMapper mapper)
        {
            this.pricingPlanService = pricingPlanService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> PricingPlanList()
        {
            var values = await pricingPlanService.GetAllAsync();
            var mapped = _mapper.Map<List<ResultPricingPlanDto>>(values);
            return View(mapped);
        }

        [HttpGet]
        public async Task<IActionResult> CreatePricingPlan()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreatePricingPlan(CreatePricingPlanDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);
            var PricingPlan = _mapper.Map<PricingPlan>(dto);
            await pricingPlanService.AddAsync(PricingPlan);
            TempData["SuccessMessage"] = "Ödeme Planı başarıyla Eklendi.";
            return RedirectToAction("PricingPlanList");
        }

        [HttpPost]
        public async Task<IActionResult> DeletePricingPlan(int id)
        {
            var PricingPlan = await pricingPlanService.GetByIdAsync(id);
            if (PricingPlan is null)
                return NotFound();

            await pricingPlanService.DeleteAsync(PricingPlan);
            TempData["SuccessMessage"] = "Ödeme Planı başarıyla silindi.";
            return RedirectToAction("PricingPlanList");
        }


        [HttpGet]
        public async Task<IActionResult> UpdatePricingPlan(int id)
        {
            var PricingPlan = await pricingPlanService.GetByIdAsync(id);
            if (PricingPlan is null)
                return NotFound();

            var mapper = _mapper.Map<UpdatePricingPlanDto>(PricingPlan);
            return View(mapper);
        }

        [HttpPost]
        public async Task<IActionResult> ChangePricingPlanStatusToFalse(int id)
        {
            var pricingPlan = await pricingPlanService.GetByIdAsync(id);
            if (pricingPlan is null)
                return NotFound();

            pricingPlan.IsFeature = false;
            await pricingPlanService.UpdateAsync(pricingPlan);

            TempData["SuccessMessage"] = "Plan öne çıkarılanlardan kaldırıldı.";
            return RedirectToAction("PricingPlanList");
        }

        [HttpPost]
        public async Task<IActionResult> ChangePricingPlanStatusToTrue(int id)
        {
            var pricingPlan = await pricingPlanService.GetByIdAsync(id);
            if (pricingPlan is null)
                return NotFound();

            pricingPlan.IsFeature = true;
            await pricingPlanService.UpdateAsync(pricingPlan);

            TempData["SuccessMessage"] = "Plan öne çıkanlara eklendi.";
            return RedirectToAction("PricingPlanList");
        }


        [HttpPost]
        public async Task<IActionResult> UpdatePricingPlan(UpdatePricingPlanDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            var PricingPlan = _mapper.Map<PricingPlan>(dto);
            await pricingPlanService.UpdateAsync(PricingPlan);
            TempData["SuccessMessage"] = "Ödeme Planı başarıyla Güncellendi.";
            return RedirectToAction("PricingPlanList");
        }
    }
}
