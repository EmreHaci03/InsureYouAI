using AutoMapper;
using InsureYouAi.Dtos.ServiceDtos;
using InsureYouAi.Entities;
using InsureYouAi.Service.Concrete;
using InsureYouAi.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using DefaultServiceEntity = InsureYouAi.Entities.Service;
namespace InsureYouAi.Controllers
{
    public class DefaultServiceController : Controller
    {
        private readonly IServiceService serviceService;
        private readonly IMapper _mapper;

        public DefaultServiceController(IServiceService serviceService, IMapper mapper)
        {
            this.serviceService = serviceService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> DefaultServiceList()
        {
            var values = await serviceService.GetAllAsync();
            var mapped = _mapper.Map<List<ResultServiceDto>>(values);
            return View(mapped);
        }
    }
}
