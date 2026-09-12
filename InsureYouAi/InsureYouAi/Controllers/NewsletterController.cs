using AutoMapper;
using InsureYouAi.Dtos.NewsletterDtos;
using InsureYouAi.Entities;
using InsureYouAi.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InsureYouAi.Controllers
{
    [Authorize(Roles = "Admin")]
    public class NewsletterController : Controller
    {
        private readonly INewsletterService newsletterService;
        private readonly IMapper _mapper;
        public NewsletterController(INewsletterService newsletterService, IMapper mapper)
        {
            this.newsletterService = newsletterService;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> NewsletterList()
        {
            var newsletter = await newsletterService.GetAllAsync();
            var mapper=_mapper.Map<List<ResultNewsletterDto>>(newsletter);
            return View(mapper);
        }


        [HttpGet]
        public async Task<IActionResult> CreateNewsletter()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateNewsletter(CreateNewsletterDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            var mapper = _mapper.Map<Newsletter>(dto);
            await newsletterService.AddAsync(mapper);
            TempData["Success"] = "Bültene Başarıyla Abone Oldunuz";
            return RedirectToAction("Index", "Default");
        }


        [HttpPost]
        public async Task<IActionResult> DeleteNewsletter(int id)
        {
            var newsletter = await newsletterService.GetByIdAsync(id);
            if (newsletter is null)
                return NotFound();
            await newsletterService.DeleteAsync(newsletter);
            return RedirectToAction("NewsletterList");
        }
    }
}
