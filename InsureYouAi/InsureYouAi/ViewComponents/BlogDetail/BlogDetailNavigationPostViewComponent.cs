using AutoMapper;
using InsureYouAi.Dtos.ArticleDtos;
using InsureYouAi.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace InsureYouAi.ViewComponents.BlogDetail
{
    public class BlogDetailNavigationPostViewComponent:ViewComponent
    {
        private readonly IArticleService articleService;
        private readonly IMapper _mapper;

        public BlogDetailNavigationPostViewComponent(IArticleService articleService, IMapper mapper)
        {
            this.articleService = articleService;
            _mapper = mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            ViewBag.Id = id;
            var article = await articleService.GetPreviousPost(id);
            var mapper=_mapper.Map<GetArticleByIdDto>(article);
            return View(mapper);
        }
    }
}
