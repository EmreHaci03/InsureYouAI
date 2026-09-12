using InsureYouAi.Context;
using InsureYouAi.Entities;
using InsureYouAi.Service.Interfaces;

namespace InsureYouAi.Service.Concrete
{
    public class GalleryService : GenericService<Gallery>, IGalleryService
    {
        private readonly InsureAiContext _context;
        public GalleryService(InsureAiContext context) : base(context)
        {
            this._context = context;
        }
    }
}
