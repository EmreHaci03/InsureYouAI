using AutoMapper;
using InsureYouAi.Dtos.CommentDtos;
using InsureYouAi.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace InsureYouAi.ViewComponents.BlogDetail
{
    public class BlogDetailCommentListViewComponent:ViewComponent
    {
        private readonly ICommentService commentService;
        private readonly IMapper _mapper;

        public BlogDetailCommentListViewComponent(ICommentService commentService, IMapper mapper)
        {
            this.commentService = commentService;
            _mapper = mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            var blog = await commentService.ApprovedCommentListByArticle(id);
            var mapper = _mapper.Map<List<ResultCommentDto>>(blog);
            return View(mapper);
        }
    }
}
