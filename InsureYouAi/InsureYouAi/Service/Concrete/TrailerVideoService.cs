using InsureYouAi.Context;
using InsureYouAi.Entities;
using InsureYouAi.Service.Interfaces;

namespace InsureYouAi.Service.Concrete
{
    public class TrailerVideoService : GenericService<TrailerVideo>, ITrailerService
    {
        public TrailerVideoService(InsureAiContext context) : base(context)
        {
        }
    }
}
