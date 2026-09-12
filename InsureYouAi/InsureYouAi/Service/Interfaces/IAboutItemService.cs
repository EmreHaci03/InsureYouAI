using InsureYouAi.Entities;

namespace InsureYouAi.Service.Interfaces
{
    public interface IAboutItemService:IGenericService<AboutItem>
    {
        Task<List<AboutItem>> GetAllWithAbout();

        Task<List<AboutItem>> GetAllWithAboutId(int id);
        Task<AboutItem> GetItemWithAbout(int id);
    }
}
