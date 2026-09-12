using AutoMapper;
using InsureYouAi.Dtos.ArticleDtos;
using InsureYouAi.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace InsureYouAi.ViewComponents.UILayout
{
    public class LayoutFooterRecent2PostViewComponent:ViewComponent
    {
        private readonly IArticleService articleService;
        private readonly IMapper _mapper;
        public LayoutFooterRecent2PostViewComponent(IArticleService articleService, IMapper mapper)
        {
            this.articleService = articleService;
            _mapper = mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var recentPosts = await articleService.GetRecent2PostArticle();
            var mapper = _mapper.Map<List<ResultArticleDto>>(recentPosts);
            return View(mapper);
        }
    }
}
