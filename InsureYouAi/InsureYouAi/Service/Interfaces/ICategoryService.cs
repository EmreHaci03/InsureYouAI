using InsureYouAi.Entities;
using InsureYouAi.Models;

namespace InsureYouAi.Service.Interfaces
{
    public interface ICategoryService : IGenericService<Category>
    {
        Task<List<CategoryArticleCountViewModel>> CategoryListWithArticleCount();
    }
}
