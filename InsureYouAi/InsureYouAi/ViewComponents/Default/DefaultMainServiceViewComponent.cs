using AutoMapper;
using InsureYouAi.Dtos.ServiceDtos;
using InsureYouAi.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace InsureYouAi.ViewComponents.Default
{
    public class DefaultMainServiceViewComponent:ViewComponent
    {
        private readonly IServiceService serviceService;
        private readonly IMapper _mapper;
        public DefaultMainServiceViewComponent(IServiceService serviceService, IMapper mapper)
        {
            this.serviceService = serviceService;
            _mapper = mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var Service = await serviceService.GetAllAsync();
            var mapper = _mapper.Map<List<ResultServiceDto>>(Service);
            return View(mapper);
        }
    }
}
