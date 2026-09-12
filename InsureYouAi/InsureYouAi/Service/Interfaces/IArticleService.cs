using InsureYouAi.Entities;
using InsureYouAi.Models;

namespace InsureYouAi.Service.Interfaces
{
    public interface IArticleService:IGenericService<Article>
    {
        Task<List<Article>> GetAllWithAllDetail();
        Task<List<Article>> GetArticleByCategory(int id);

        Task<List<Article>> GetUserArticleList(string id);

        Task<List<ArticleCommentCountViewModel>> GetTop5ArticlesByCommentCount();

        Task<List<TopCategoriesByArticleCountViewModel>> CategoryListByArticleCount();
        Task<int> ArticleCountByUser(string id);
        Task<List<Article>> GetRecentPost();
        Task<List<string>> GetAllArticleContentByUser(string id);
        Task<Article> GetPreviousPost(int id);

        Task<List<Article>> GetRecent2PostArticle();
        Task<Article> GetNextPost(int id);
        Task<List<Article>> GetAllWithCategory();
        Task<Article> GetArticleWithCategory(int id);
    }
}
