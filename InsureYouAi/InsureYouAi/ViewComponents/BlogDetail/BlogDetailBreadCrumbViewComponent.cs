using AutoMapper;
using InsureYouAi.Dtos.ArticleDtos;
using InsureYouAi.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace InsureYouAi.ViewComponents.BlogDetail
{
    public class BlogDetailBreadCrumbViewComponent:ViewComponent
    {
        private readonly IArticleService articleService;
        private readonly IMapper _mapper;

        public BlogDetailBreadCrumbViewComponent(IArticleService articleService, IMapper mapper)
        {
            this.articleService = articleService;
            _mapper = mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            var blog = await articleService.GetArticleWithCategory(id);
            var mapper = _mapper.Map<GetArticleByIdDto>(blog);
            return View(mapper);
        }
    }
}
