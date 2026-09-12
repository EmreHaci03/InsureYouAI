using AutoMapper;
using InsureYouAi.Dtos.AboutItemDtos;
using InsureYouAi.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace InsureYouAi.ViewComponents.Default
{
    public class DefaultAboutItemViewComponent:ViewComponent
    {
        private readonly IAboutItemService aboutItemService;
        private readonly IMapper _mapper;
        public DefaultAboutItemViewComponent(IAboutItemService aboutItemService, IMapper mapper)
        {
            this.aboutItemService = aboutItemService;
            _mapper = mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            var aboutItem = await aboutItemService.GetAllWithAboutId(id);
            var mapper = _mapper.Map<List<ResultAboutItemDto>>(aboutItem);
            return View(mapper);
        }
    }
}
