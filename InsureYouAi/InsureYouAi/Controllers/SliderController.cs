using AutoMapper;
using InsureYouAi.Dtos.SliderDtos;
using InsureYouAi.Entities;
using InsureYouAi.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InsureYouAi.Controllers
{
    [Authorize(Roles = "Admin")]
    public class SliderController : Controller
    {
        private readonly ISliderService sliderService;
        private readonly IMapper _mapper;

        public SliderController(IMapper mapper, ISliderService sliderService)
        {
            _mapper = mapper;
            this.sliderService = sliderService;
        }

        [HttpGet]
        public async Task<IActionResult> SliderList()
        {
            var values = await sliderService.GetAllAsync();
            var mapped = _mapper.Map<List<ResultSliderDto>>(values);
            return View(mapped);
        }

        [HttpGet]
        public IActionResult CreateSlider()
        {
            return View(new CreateSliderDto());
        }

        [HttpPost]
        public async Task<IActionResult> CreateSlider(CreateSliderDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            var slider = _mapper.Map<Slider>(dto);
            await sliderService.AddAsync(slider);
            TempData["SuccessMessage"] = "Slider başarıyla eklendi.";
            return RedirectToAction("SliderList");
        }
        [HttpPost]
        public async Task<IActionResult> DeleteSlider(int id)
        {
            var slider = await sliderService.GetByIdAsync(id);
            if (slider is null) return NotFound();

            await sliderService.DeleteAsync(slider);
            TempData["SuccessMessage"] = "Slider başarıyla silindi.";
            return RedirectToAction("SliderList");
        }

        [HttpGet]
        public async Task<IActionResult> UpdateSlider(int id)
        {
            var slider = await sliderService.GetByIdAsync(id);
            if (slider is null) return NotFound();

            var dto = _mapper.Map<UpdateSliderDto>(slider);
            return View(dto);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateSlider(UpdateSliderDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            var slider = _mapper.Map<Slider>(dto);
            await sliderService.UpdateAsync(slider);
            TempData["SuccessMessage"] = "Slider başarıyla güncellendi.";
            return RedirectToAction("SliderList");
        }

    }
}