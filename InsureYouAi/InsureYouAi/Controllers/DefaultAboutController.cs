using AutoMapper;
using InsureYouAi.Dtos.AboutDtos;
using InsureYouAi.Entities;
using InsureYouAi.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace InsureYouAi.Controllers
{
    public class DefaultAboutController : Controller
    {
        private readonly IAboutService aboutService;
        private readonly IMapper _mapper;

        public DefaultAboutController(IAboutService aboutService, IMapper mapper)
        {
            this.aboutService = aboutService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> DefaultAboutList()
        {
            var values = await aboutService.GetAllAsync();
            var mapped = _mapper.Map<List<ResultAboutDto>>(values);
            return View(mapped);
        }
    }
}
