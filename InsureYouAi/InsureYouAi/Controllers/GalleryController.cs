using AutoMapper;
using InsureYouAi.Dtos.GalleryDtos;
using InsureYouAi.Entities;
using InsureYouAi.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InsureYouAi.Controllers
{
    [Authorize(Roles = "Admin")]
    public class GalleryController : Controller
    {
        private readonly IGalleryService GalleryService;
        private readonly IMapper _mapper;
        public GalleryController(IGalleryService GalleryService, IMapper mapper)
        {
            this.GalleryService = GalleryService;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GalleryList()
        {
            var values = await GalleryService.GetAllAsync();
            var mapped = _mapper.Map<List<ResultGalleryDto>>(values);
            return View(mapped);
        }

        [HttpGet]
        public async Task<IActionResult> CreateGallery()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateGallery(CreateGalleryDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);
            var Gallery = _mapper.Map<Gallery>(dto);
            await GalleryService.AddAsync(Gallery);
            TempData["SuccessMessage"] = "Resim başarıyla Eklendi.";
            return RedirectToAction("GalleryList");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteGallery(int id)
        {
            var Gallery = await GalleryService.GetByIdAsync(id);
            if (Gallery is null)
                return NotFound();

            await GalleryService.DeleteAsync(Gallery);
            TempData["SuccessMessage"] = "Resim başarıyla silindi.";
            return RedirectToAction("GalleryList");
        }


        [HttpGet]
        public async Task<IActionResult> UpdateGallery(int id)
        {
            var values = await GalleryService.GetByIdAsync(id);
            var mapper = _mapper.Map<UpdateGalleryDto>(values);
            return View(mapper);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateGallery(UpdateGalleryDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);
            var Gallery = _mapper.Map<Gallery>(dto);
            await GalleryService.UpdateAsync(Gallery);
            TempData["SuccessMessage"] = "Resim başarıyla Güncellendi.";
            return RedirectToAction("GalleryList");
        }
    }
}
