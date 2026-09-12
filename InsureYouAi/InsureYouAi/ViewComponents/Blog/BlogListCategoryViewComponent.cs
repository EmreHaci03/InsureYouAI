using AutoMapper;
using InsureYouAi.Dtos.CategoryDtos;
using InsureYouAi.Models;
using InsureYouAi.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace InsureYouAi.ViewComponents.Blog
{
    public class BlogListCategoryViewComponent:ViewComponent
    {
        private readonly ICategoryService categoryService;
        private readonly IMapper _mapper;
        public BlogListCategoryViewComponent(ICategoryService categoryService, IMapper mapper)
        {
            this.categoryService = categoryService;
            _mapper = mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var category = await categoryService.CategoryListWithArticleCount();    
            var mapper = _mapper.Map<List<CategoryArticleCountViewModel>>(category);
            return View(mapper);
        }
    }
}
