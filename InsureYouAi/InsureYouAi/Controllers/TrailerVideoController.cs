using AutoMapper;
using InsureYouAi.Dtos.TrailerVideoDtos;
using InsureYouAi.Dtos.CategoryDtos;
using InsureYouAi.Dtos.PricingPlanDtos;
using InsureYouAi.Entities;
using InsureYouAi.Service.Concrete;
using Microsoft.AspNetCore.Authorization;
using InsureYouAi.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace InsureYouAi.Controllers
{
    [Authorize(Roles = "Admin")]
    public class TrailerVideoController : Controller
    {
        private readonly ITrailerService trailerService;
        private readonly IMapper _mapper;
        public TrailerVideoController(IMapper mapper, ITrailerService trailerService)
        {
            _mapper = mapper;
            this.trailerService = trailerService;
        }
        [HttpGet]
        public async Task<IActionResult> TrailerVideoList()
        {
            var TrailerVideo = await trailerService.GetAllAsync();
            var mapper = _mapper.Map<List<ResultTrailerVideoDto>>(TrailerVideo);
            return View(mapper);
        }

        [HttpGet]
        public async Task<IActionResult> CreateTrailerVideo()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateTrailerVideo(CreateTrailerVideoDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);
            var TrailerVideo = _mapper.Map<TrailerVideo>(dto);
            await trailerService.AddAsync(TrailerVideo);
            TempData["SuccessMessage"] = "Tanıtım Videosu Başarıyla Eklendi.";
            return RedirectToAction("TrailerVideoList");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteTrailerVideo(int id)
        {
            var TrailerVideo = await trailerService.GetByIdAsync(id);
            if (TrailerVideo is null)
                return NotFound();

            await trailerService.DeleteAsync(TrailerVideo);
            TempData["SuccessMessage"] = "Tanıtım Videosu Başarıyla silindi.";
            return RedirectToAction("TrailerVideoList");
        }


        [HttpGet]
        public async Task<IActionResult> UpdateTrailerVideo(int id)
        {
            var TrailerVideo = await trailerService.GetByIdAsync(id);
            if (TrailerVideo is null)
                return NotFound();

            var mapper = _mapper.Map<UpdateTrailerVideoDto>(TrailerVideo);
            return View(mapper);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateTrailerVideo(UpdateTrailerVideoDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            var TrailerVideo = _mapper.Map<TrailerVideo>(dto);
            await trailerService.UpdateAsync(TrailerVideo);
            TempData["SuccessMessage"] = "Tanıtım Videosu başarıyla Güncellendi.";
            return RedirectToAction("TrailerVideoList");
        }
    }
}
