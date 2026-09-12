using AutoMapper;
using InsureYouAi.Dtos.AboutDtos;
using InsureYouAi.Dtos.AboutItemDtos;
using InsureYouAi.Entities;
using InsureYouAi.Service.Concrete;
using InsureYouAi.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InsureYouAi.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AboutItemController : Controller
    {
        private readonly IAboutItemService aboutItemService;
        private readonly IAboutService aboutService;
        private readonly IMapper _mapper;
        public AboutItemController(IAboutItemService aboutItemService, IMapper mapper, IAboutService aboutService)
        {
            this.aboutItemService = aboutItemService;
            _mapper = mapper;
            this.aboutService = aboutService;
        }
        [HttpGet]
        public async Task<IActionResult> AboutItemList()
        {
            var values = await aboutItemService.GetAllWithAbout();
            var mapper = _mapper.Map<List<ResultAboutItemDto>>(values);
            return View(mapper);
        }


        [HttpGet]
        public async Task<IActionResult> CreateAboutItem()
        {
            var values = await aboutService.GetAllAsync();
            var mapper = _mapper.Map<List<ResultAboutDto>>(values);
            ViewBag.Abouts = mapper;
            return View(new CreateAboutItemDto());
        }

        [HttpPost]
        public async Task<IActionResult> CreateAboutItem(CreateAboutItemDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            var mapper = _mapper.Map<AboutItem>(dto);
            await aboutItemService.AddAsync(mapper);
            TempData["SuccessMessage"] = "Hakkımda Elemanı Başarıyla Eklendi";
            return RedirectToAction("AboutItemList");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteAboutItem(int id)
        {
            var aboutItem = await aboutItemService.GetByIdAsync(id);
            if (aboutItem is null)
                return NotFound();

            await aboutItemService.DeleteAsync(aboutItem);
            TempData["SuccessMessage"] = "Hakkımda Elemanı Başarıyla Silindi";
            return RedirectToAction("AboutItemList");
        }


        [HttpGet]
        public async Task<IActionResult> UpdateAboutItem(int id)
        {
            var values = await aboutService.GetAllAsync();
            var Aboutmapper = _mapper.Map<List<ResultAboutDto>>(values);
            ViewBag.Abouts = Aboutmapper;

            var aboutItem=await aboutItemService.GetByIdAsync(id);
            if (aboutItem is null)
                return NotFound();

            var mapper = _mapper.Map<UpdateAboutItemDto>(aboutItem);
            return View(mapper);
        }


        [HttpPost]
        public async Task<IActionResult> UpdateAboutItem(UpdateAboutItemDto dto)
        {

            if (!ModelState.IsValid)
                return View(dto);
            var AboutItem = _mapper.Map<AboutItem>(dto);
            await aboutItemService.UpdateAsync(AboutItem);
            TempData["SuccessMessage"] = "Hakkımda Elemanı Başarıyla Güncellendi";
            return RedirectToAction("AboutItemList");
        }

    }
}
