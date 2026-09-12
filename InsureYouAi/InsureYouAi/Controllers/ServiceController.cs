using AutoMapper;
using InsureYouAi.Dtos.ServiceDtos;
using InsureYouAi.Entities;
using InsureYouAi.Service.Concrete;
using InsureYouAi.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceEntity = InsureYouAi.Entities.Service;
namespace InsureYouAi.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ServiceController : Controller
    {
        private readonly IServiceService serviceService;
        private readonly IMapper _mapper;
        public ServiceController(IServiceService serviceService, IMapper mapper)
        {
            this.serviceService = serviceService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> ServiceList()
        {
            var values = await serviceService.GetAllAsync();
            var mapped = _mapper.Map<List<ResultServiceDto>>(values);
            return View(mapped);
        }

        [HttpGet]
        public async Task<IActionResult> CreateService()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateService(CreateServiceDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);
            var Service = _mapper.Map<ServiceEntity>(dto);
            await serviceService.AddAsync(Service);
            TempData["SuccessMessage"] = "Hizmet başarıyla Eklendi.";
            return RedirectToAction("ServiceList");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteService(int id)
        {
            var Service = await serviceService.GetByIdAsync(id);
            if (Service is null)
                return NotFound();

            await serviceService.DeleteAsync(Service);
            TempData["SuccessMessage"] = "Hizmet başarıyla silindi.";
            return RedirectToAction("ServiceList");
        }


        [HttpGet]
        public async Task<IActionResult> UpdateService(int id)
        {
            var Service = await serviceService.GetByIdAsync(id);
            if (Service is null)
                return NotFound();

            var mapper = _mapper.Map<UpdateServiceDto>(Service);
            return View(mapper);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateService(UpdateServiceDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            var Service = _mapper.Map<ServiceEntity>(dto);
            await serviceService.UpdateAsync(Service);
            TempData["SuccessMessage"] = "Hizmet başarıyla Güncellendi.";
            return RedirectToAction("ServiceList");
        }
    }
}
