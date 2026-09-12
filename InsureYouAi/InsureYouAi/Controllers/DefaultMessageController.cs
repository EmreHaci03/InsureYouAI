using AutoMapper;
using InsureYouAi.Dtos.ContactDtos;
using InsureYouAi.Dtos.MessageDtos;
using InsureYouAi.Entities;
using InsureYouAi.Service.Concrete;
using InsureYouAi.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace InsureYouAi.Controllers
{
    public class DefaultMessageController : Controller
    {
        private readonly IMessageService messageService;
        private readonly AIService aIService;
        private readonly IMapper _mapper;

        public DefaultMessageController(IMessageService messageService, IMapper mapper, AIService aIService)
        {
            this.messageService = messageService;
            _mapper = mapper;
            this.aIService = aIService;
        }

        [HttpGet]
        public async Task<IActionResult> DefaultMessage()
        {
            return View();
        }


        [HttpGet]
        public async Task<IActionResult> DefaultCreateMessage()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> DefaultCreateMessage(CreateMessageDto dto)
        {
            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Mesaj Gönderilemedi Lütfen Tekrar Deneyiniz.";
                return View(dto);
            }
     
            var mapper = _mapper.Map<Message>(dto);
            var Category = await aIService.PredictCategoryAsync(mapper.MessageDetail);
            var priority = await aIService.PredictPriorityAsync(mapper.MessageDetail);
            mapper.Priority = priority;
            mapper.AICategory = Category;
            mapper.SendDate = DateTime.Now;
            mapper.IsRead = false;
            await messageService.AddAsync(mapper);
            TempData["SuccessMessage"] = "Mesaj Başarıyla Gönderildi";
            return RedirectToAction("DefaultMessage");
        }


    }
}
