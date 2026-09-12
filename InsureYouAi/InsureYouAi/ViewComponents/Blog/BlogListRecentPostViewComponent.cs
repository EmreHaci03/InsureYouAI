using AutoMapper;
using InsureYouAi.Dtos.ArticleDtos;
using InsureYouAi.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace InsureYouAi.ViewComponents.Blog
{
    public class BlogListRecentPostViewComponent:ViewComponent
    {
        private readonly IArticleService articleService;
        private readonly IMapper _mapper;

        public BlogListRecentPostViewComponent(IArticleService articleService, IMapper mapper)
        {
            this.articleService = articleService;
            _mapper = mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var Article = await articleService.GetRecentPost();
            var mapper = _mapper.Map<List<ResultArticleDto>>(Article);
            return View(mapper);
        }
    }
}
