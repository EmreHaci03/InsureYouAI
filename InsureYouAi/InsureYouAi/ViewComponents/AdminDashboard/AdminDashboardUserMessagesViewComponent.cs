using AutoMapper;
using InsureYouAi.Dtos.MessageDtos;
using InsureYouAi.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace InsureYouAi.ViewComponents.AdminDashboard
{
    public class AdminDashboardUserMessagesViewComponent:ViewComponent
    {
        private readonly IMessageService messageService;
        private readonly IMapper _mapper;
        public AdminDashboardUserMessagesViewComponent(IMessageService messageService, IMapper mapper)
        {
            this.messageService = messageService;
            _mapper = mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var messages = await messageService.Last5Message();
            var mapper = _mapper.Map<List<ResultMessageDto>>(messages);
            return View(mapper);
        }
    }
}
