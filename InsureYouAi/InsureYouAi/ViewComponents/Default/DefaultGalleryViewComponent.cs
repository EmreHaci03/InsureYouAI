using AutoMapper;
using InsureYouAi.Dtos.GalleryDtos;
using InsureYouAi.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace InsureYouAi.ViewComponents.Default
{
    public class DefaultGalleryViewComponent:ViewComponent
    {
        private readonly IGalleryService? galleryService;
        private readonly IMapper _mapper;
        public DefaultGalleryViewComponent(IGalleryService? galleryService, IMapper mapper)
        {
            this.galleryService = galleryService;
            _mapper = mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var values = await galleryService.GetAllAsync();
            var mapper = _mapper.Map<List<ResultGalleryDto>>(values);
            return View(mapper);
        }
    }
}
