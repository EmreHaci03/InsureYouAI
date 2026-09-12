using AutoMapper;
using InsureYouAi.Dtos.ArticleDtos;
using InsureYouAi.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace InsureYouAi.ViewComponents.BlogDetail
{
    public class BlogDetailContentViewComponent:ViewComponent
    {
        private readonly IArticleService articleService;
        private readonly IMapper _mapper;

        public BlogDetailContentViewComponent(IArticleService articleService, IMapper mapper)
        {
            this.articleService = articleService;
            _mapper = mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            var Blog = await articleService.GetArticleWithCategory(id);
            var mapper=_mapper.Map<GetArticleByIdDto>(Blog);
            return View(mapper);
        }
    }
}
