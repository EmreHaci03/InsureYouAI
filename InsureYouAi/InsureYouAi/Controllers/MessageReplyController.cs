using AutoMapper;
using InsureYouAi.Dtos.MessageReplyDtos;
using InsureYouAi.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InsureYouAi.Controllers
{
    [Authorize(Roles = "Admin")]
    public class MessageReplyController : Controller
    {
        private readonly IMessageReplyService messageReplyService;
        private readonly IMapper _mapper;

        public MessageReplyController(IMessageReplyService messageReplyService, IMapper mapper)
        {
            this.messageReplyService = messageReplyService;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> MessageReplyList()
        {
            var messageReply = await messageReplyService.GetAllWithMessage();
            var mapper = _mapper.Map<List<ResultMessageReplyDto>>(messageReply);
            return View(mapper);
        }


        [HttpGet]
        public async Task<IActionResult> MessageReplyDetail(int id)
        {
            var messageReply = await messageReplyService.GetAllWithMessageById(id);
            var mapper = _mapper.Map<GetMessageReplyByIdDto>(messageReply);
            return View(mapper);
        }
    }
}
