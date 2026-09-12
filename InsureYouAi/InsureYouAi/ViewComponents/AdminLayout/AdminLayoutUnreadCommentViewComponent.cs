using AutoMapper;
using InsureYouAi.Dtos.CommentDtos;
using InsureYouAi.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace InsureYouAi.ViewComponents.AdminLayout
{
    public class AdminLayoutUnreadCommentViewComponent : ViewComponent
    {
        private readonly ICommentService commentService;
        private readonly IMapper _mapper;

        public AdminLayoutUnreadCommentViewComponent(ICommentService commentService, IMapper mapper)
        {
            this.commentService = commentService;
            _mapper = mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var unreadComments = await commentService.UnreadCommentList();
            var mapper = _mapper.Map<List<ResultCommentDto>>(unreadComments);
            return View(mapper);
        }
    }
}