using AutoMapper;
using InsureYouAi.Dtos.AboutDtos;
using InsureYouAi.Dtos.ArticleDtos;
using InsureYouAi.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace InsureYouAi.ViewComponents.Default
{
    public class DefaultBlogViewComponent:ViewComponent
    {
        private readonly IArticleService articleService;
        private readonly IMapper _mapper;

        public DefaultBlogViewComponent(IArticleService articleService, IMapper mapper)
        {
            this.articleService = articleService;
            _mapper = mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var about = await articleService.GetAllWithAllDetail();
            var mapper = _mapper.Map<List<ResultArticleDto>>(about);
            return View(mapper);
        }
    }
}
