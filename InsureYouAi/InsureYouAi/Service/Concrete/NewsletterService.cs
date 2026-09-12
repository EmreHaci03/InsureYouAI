using InsureYouAi.Context;
using InsureYouAi.Entities;
using InsureYouAi.Service.Interfaces;

namespace InsureYouAi.Service.Concrete
{
    public class NewsletterService : GenericService<Newsletter>, INewsletterService
    {
        public NewsletterService(InsureAiContext context) : base(context)
        {
        }
    }
}
