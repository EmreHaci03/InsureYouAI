using AutoMapper;
using InsureYouAi.Dtos.ArticleDtos;
using InsureYouAi.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace InsureYouAi.ViewComponents.Blog
{
    public class BlogListByCategoryViewComponent : ViewComponent
    {
        private readonly IArticleService articleService;
        private readonly IMapper _mapper;

        public BlogListByCategoryViewComponent(IArticleService articleService, IMapper mapper)
        {
            this.articleService = articleService;
            _mapper = mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            var articles = await articleService.GetArticleByCategory(id);

            if (articles == null || !articles.Any())
            {
                ViewBag.ErrorMessage = "Belirtilen kategoriye ait blog yazısı bulunmamaktadır.";
                return View(new List<ResultArticleDto>());
            }

            var mapped = _mapper.Map<List<ResultArticleDto>>(articles);
            return View(mapped);
        }
    }
}