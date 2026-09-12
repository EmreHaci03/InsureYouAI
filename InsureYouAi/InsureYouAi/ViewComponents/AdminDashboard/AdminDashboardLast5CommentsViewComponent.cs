using AutoMapper;
using InsureYouAi.Dtos.CommentDtos;
using InsureYouAi.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace InsureYouAi.ViewComponents.AdminDashboard
{
    public class AdminDashboardLast5CommentsViewComponent:ViewComponent
    {
        private readonly ICommentService commentService;
        private readonly IMapper _mapper;
        public AdminDashboardLast5CommentsViewComponent(ICommentService commentService, IMapper mapper)
        {
            this.commentService = commentService;
            _mapper = mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var comments = await commentService.GetLast5Comments();
            var mapper = _mapper.Map<List<ResultCommentDto>>(comments);
            return View(mapper);
        }
    }
}
