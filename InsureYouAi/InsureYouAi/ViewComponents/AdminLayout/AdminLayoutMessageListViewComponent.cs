using AutoMapper;
using InsureYouAi.Dtos.MessageDtos;
using InsureYouAi.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace InsureYouAi.ViewComponents.AdminLayout
{
    public class AdminLayoutMessageListViewComponent:ViewComponent
    {
        private readonly IMessageService messageService;
        private readonly IMapper _mapper;

        public AdminLayoutMessageListViewComponent(IMessageService messageService, IMapper mapper)
        {
            this.messageService = messageService;
            _mapper = mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            ViewBag.Count = await messageService.MessageCountAsync();
            var message = await messageService.GetAllAsync();
            var mapper = _mapper.Map<List<ResultMessageDto>>(message);
            return View(mapper);
        }
    }
}
