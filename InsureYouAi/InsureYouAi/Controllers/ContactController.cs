using AutoMapper;
using InsureYouAi.Dtos.ContactDtos;
using InsureYouAi.Entities;
using InsureYouAi.Service.Concrete;
using InsureYouAi.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InsureYouAi.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ContactController : Controller
    {
        private readonly IContactService contactService;
        private readonly IMapper _mapper;
        public ContactController(IContactService contactService, IMapper mapper)
        {
            this.contactService = contactService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> ContactList()
        {
            var values = await contactService.GetAllAsync();
            var mapped = _mapper.Map<List<ResultContactDto>>(values);
            return View(mapped);
        }

        [HttpGet]
        public async Task<IActionResult> CreateContact()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateContact(CreateContactDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);
            var Contact = _mapper.Map<Contact>(dto);
            await contactService.AddAsync(Contact);
            TempData["SuccessMessage"] = "İletişim başarıyla Eklendi.";
            return RedirectToAction("ContactList");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteContact(int id)
        {
            var Contact = await contactService.GetByIdAsync(id);
            if (Contact is null)
                return NotFound();

            await contactService.DeleteAsync(Contact);
            TempData["SuccessMessage"] = "İletişim başarıyla silindi.";
            return RedirectToAction("ContactList");
        }


        [HttpGet]
        public async Task<IActionResult> UpdateContact(int id)
        {
            var contact = await contactService.GetByIdAsync(id);
            if (contact is null)
                return NotFound();
            var mapper = _mapper.Map<UpdateContactDto>(contact);
            return View(mapper);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateContact(UpdateContactDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);
            var Contact = _mapper.Map<Contact>(dto);
            await contactService.UpdateAsync(Contact);
            TempData["SuccessMessage"] = "İletişim başarıyla Güncellendi.";
            return RedirectToAction("ContactList");
        }
    }
}
