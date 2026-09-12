using InsureYouAi.Context;
using InsureYouAi.Dtos.AppUserDtos;
using InsureYouAi.Entities;
using InsureYouAi.Service.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InsureYouAi.Service.Concrete
{
    public class UserService : GenericService<AppUser>, IUserService
    {
        private readonly InsureAiContext _context;
        public UserService(InsureAiContext context) : base(context)
        {
            this._context = context;
        }

        public async Task<List<AppUser>> GetLast5User()
        {
            return await _context.Users.OrderByDescending(x => x.CreatedDate).Take(5).ToListAsync();
        }

        public async Task<AppUser> GetUserByIdAsync(string id)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
