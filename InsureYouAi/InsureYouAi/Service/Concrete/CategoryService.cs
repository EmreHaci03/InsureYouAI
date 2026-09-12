using InsureYouAi.Context;
using InsureYouAi.Entities;
using InsureYouAi.Models;
using InsureYouAi.Service.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InsureYouAi.Service.Concrete
{
    public class CategoryService : GenericService<Category>, ICategoryService
    {
        private readonly InsureAiContext _context;
        public CategoryService(InsureAiContext context) : base(context)
        {
            this._context = context;
        }

        public async Task<List<CategoryArticleCountViewModel>> CategoryListWithArticleCount()
        {
            return await _context.Categories
                .Select(x => new CategoryArticleCountViewModel
                {
                    CategoryId = x.CategoryId,
                    CategoryName=x.CategoryName,
                    ArticleCount = x.Articles.Count()
                })
                .OrderBy(x => x.CategoryId)
                .ToListAsync();
        }
    }
}
