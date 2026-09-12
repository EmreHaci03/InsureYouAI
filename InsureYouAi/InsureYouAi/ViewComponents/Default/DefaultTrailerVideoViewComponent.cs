using AutoMapper;
using InsureYouAi.Dtos.TrailerVideoDtos;
using InsureYouAi.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace InsureYouAi.ViewComponents.Default
{
    public class DefaultTrailerVideoViewComponent:ViewComponent
    {
        private readonly ITrailerService trailerService;
        private readonly IMapper _mapper;
        public DefaultTrailerVideoViewComponent(ITrailerService trailerService, IMapper mapper)
        {
            this.trailerService = trailerService;
            _mapper = mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var trailerVideo = await trailerService.GetAllAsync();
            var mapper = _mapper.Map<List<ResultTrailerVideoDto>>(trailerVideo);
            return View(mapper);
        }
    }
}
