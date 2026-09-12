using InsureYouAi.Context;
using InsureYouAi.Entities;
using InsureYouAi.Service.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InsureYouAi.Service.Concrete
{
    public class AboutItemService : GenericService<AboutItem>, IAboutItemService
    {
        private readonly InsureAiContext _context;
        public AboutItemService(InsureAiContext context) : base(context)
        {
            this._context = context;
        }

        public async Task<List<AboutItem>> GetAllWithAbout()
        {
            return await _context.AboutItems.Include(x => x.About).ToListAsync();
        }

        public async Task<List<AboutItem>> GetAllWithAboutId(int id)
        {
            return await _context.AboutItems.Include(x => x.About).Where(x => x.AboutId == id).ToListAsync();
        }

        public async Task<AboutItem> GetItemWithAbout(int id)
        {
            return await _context.AboutItems.Include(x => x.About).FirstOrDefaultAsync(x => x.AboutItemId == id);
        }
    }
}
