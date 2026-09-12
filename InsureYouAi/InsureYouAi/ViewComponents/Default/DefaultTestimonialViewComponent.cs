using AutoMapper;
using InsureYouAi.Dtos.TestimonialDtos;
using InsureYouAi.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace InsureYouAi.ViewComponents.Default
{
    public class DefaultTestimonialViewComponent : ViewComponent
    {
        private readonly ITestimonialService _testimonialService;
        private readonly IMapper _mapper;
        public DefaultTestimonialViewComponent(ITestimonialService testimonialService, IMapper mapper)
        {
            _testimonialService = testimonialService;
            _mapper = mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var testimonials = await _testimonialService.GetAllAsync();
            var mapper = _mapper.Map<List<ResultTestimonialDto>>(testimonials);
            return View(mapper);
        }
    }
}