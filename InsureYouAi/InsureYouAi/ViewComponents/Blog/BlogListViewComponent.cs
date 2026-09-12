using AutoMapper;
using InsureYouAi.Dtos.ArticleDtos;
using InsureYouAi.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace InsureYouAi.ViewComponents.Blog
{
    public class BlogListViewComponent:ViewComponent
    {
        private readonly IArticleService articleService;
        private readonly ICommentService commentService;
        private readonly IMapper _mapper;
        public BlogListViewComponent(IArticleService articleService, IMapper mapper, ICommentService commentService)
        {
            this.articleService = articleService;
            _mapper = mapper;
            this.commentService = commentService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var Blogs = await articleService.GetAllWithAllDetail();
            var mapper = _mapper.Map<List<ResultArticleDto>>(Blogs);
            foreach(var item in mapper)
            {
                item.CommentCount = await commentService.GetCommentCountByArticle(item.ArticleId);
            }
            return View(mapper);
        }
    }
}
