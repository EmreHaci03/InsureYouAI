using InsureYouAi.Context;
using InsureYouAi.Entities;
using InsureYouAi.Service.Interfaces;

namespace InsureYouAi.Service.Concrete
{
    public class AboutService : GenericService<About>, IAboutService
    {
        private readonly InsureAiContext _context;
        public AboutService(InsureAiContext context) : base(context)
        {
            this._context = context;
        }

    }
}
