using AutoMapper;
using InsureYouAi.Dtos.ArticleDtos;
using InsureYouAi.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace InsureYouAi.ViewComponents.BlogDetail
{
    public class BlogDetailNavigationNextPostViewComponent:ViewComponent
    {
        private readonly IArticleService articleService;
        private readonly IMapper _mapper;

        public BlogDetailNavigationNextPostViewComponent(IArticleService articleService, IMapper mapper)
        {
            this.articleService = articleService;
            _mapper = mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            var article = await articleService.GetNextPost(id);
            var mapper = _mapper.Map<GetArticleByIdDto>(article);
            return View(mapper);
        }
    }
}
