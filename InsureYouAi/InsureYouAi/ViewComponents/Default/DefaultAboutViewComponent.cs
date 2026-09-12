using AutoMapper;
using InsureYouAi.Dtos.AboutDtos;
using InsureYouAi.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace InsureYouAi.ViewComponents.Default
{
    public class DefaultAboutViewComponent:ViewComponent
    {
        private readonly IAboutService aboutService;
        private readonly IMapper _mapper;
        public DefaultAboutViewComponent(IAboutService aboutService, IMapper mapper)
        {
            this.aboutService = aboutService;
            _mapper = mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var about = await aboutService.GetAllAsync();
            var mapper = _mapper.Map<List<ResultAboutDto>>(about);
            return View(mapper);
        }
    }
}
