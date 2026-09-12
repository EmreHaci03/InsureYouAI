using InsureYouAi.Dtos.AppUserDtos;
using InsureYouAi.Entities;

namespace InsureYouAi.Service.Interfaces
{
    public interface IUserService:IGenericService<AppUser>
    {
        Task<AppUser> GetUserByIdAsync(string id);

        Task<List<AppUser>> GetLast5User();
    }
}
