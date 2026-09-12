using InsureYouAi.Context;
using InsureYouAi.Entities;
using InsureYouAi.Models;
using InsureYouAi.Service.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InsureYouAi.Service.Concrete
{
    public class ArticleService : GenericService<Article>, IArticleService
    {
        private readonly InsureAiContext _context;
        public ArticleService(InsureAiContext context) : base(context)
        {
            this._context = context;
        }

        public async Task<int> ArticleCountByUser(string id)
        {
            return await _context.Articles.Where(x => x.AppUserId == id).CountAsync();
        }

        public async Task<List<TopCategoriesByArticleCountViewModel>> CategoryListByArticleCount()
        {
            return await _context.Articles
                .Include(x => x.Category)
                .GroupBy(x => x.CategoryId)
                .Select(g => new TopCategoriesByArticleCountViewModel
                {
                    CategoryId=g.Key,
                    CategoryName = g.First().Category.CategoryName,
                    ArticleCount = g.Count()
                })
            .OrderByDescending(x => x.ArticleCount)
            .Take(5)
            .ToListAsync();
        }



        public async Task<List<string>> GetAllArticleContentByUser(string id)
        {
            return await _context.Articles
                .Include(x=>x.Category)
                .Where(x => x.AppUserId == id)
                .Select(x => x.Content)
                .ToListAsync();
        }

        public async Task<List<Article>> GetAllWithAllDetail()
        {
            return await _context.Articles.Include(x => x.Category).Include(x=>x.AppUser).ToListAsync();
        }

        public async Task<List<Article>> GetAllWithCategory()
        {
            return await _context.Articles.Include(x => x.Category).ToListAsync();
        }

        public async Task<List<Article>> GetArticleByCategory(int id)
        {
            return await _context.Articles.Include(x => x.Category).Include(x => x.AppUser).Where(x => x.CategoryId == id).ToListAsync();
        }

        public async Task<Article> GetArticleWithCategory(int id)
        {
            return await _context.Articles.Include(x => x.Category).Include(x=>x.AppUser).FirstOrDefaultAsync(x => x.ArticleId == id);
        }



        public async Task<Article> GetNextPost(int id)
        {
            return await _context.Articles.Include(x=>x.Category).Include(x=>x.AppUser).FirstOrDefaultAsync(x => x.ArticleId == id + 1);
        }

        public async Task<Article> GetPreviousPost(int id)
        {
            return await _context.Articles.Include(x => x.Category).Include(x => x.AppUser).FirstOrDefaultAsync(x => x.ArticleId == id - 1);
        }

        public async Task<List<Article>> GetRecent2PostArticle()
        {
            return await _context.Articles.Include(x => x.Category).OrderByDescending(x => x.ArticleId).Take(2).ToListAsync();
        }

        public async Task<List<Article>> GetRecentPost()
        {
            return await _context.Articles.OrderByDescending(x => x.ArticleId).ToListAsync();
        }

      

        public async Task<List<ArticleCommentCountViewModel>> GetTop5ArticlesByCommentCount()
        {
            return await _context.Articles
                .Select(x => new ArticleCommentCountViewModel
                {
                    Title = x.Title,
                    CommentCount = x.Comments.Count()
                })
                .OrderByDescending(x => x.CommentCount)
                .Take(5)
                .ToListAsync();
        }

        public async Task<List<Article>> GetUserArticleList(string id)
        {
            return await _context.Articles.Where(x => x.AppUserId == id).ToListAsync();
        }
    }
}
