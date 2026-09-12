using AutoMapper;
using InsureYouAi.Dtos.SliderDtos;
using InsureYouAi.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace InsureYouAi.ViewComponents.Default
{
    public class DefaultSliderViewComponent:ViewComponent
    {
        private readonly ISliderService sliderService;
        private readonly IMapper _mapper;

        public DefaultSliderViewComponent(IMapper mapper, ISliderService sliderService)
        {
            _mapper = mapper;
            this.sliderService = sliderService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var Slider = await sliderService.GetAllAsync();
            var mapper = _mapper.Map<List<ResultSliderDto>>(Slider);
            return View(mapper);
        }
    }
}
