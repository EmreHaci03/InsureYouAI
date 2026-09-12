using InsureYouAi.Context;
using InsureYouAi.Service.Interfaces;
using ServiceEntity = InsureYouAi.Entities.Service;

namespace InsureYouAi.Service.Concrete
{
    public class ServiceService : GenericService<ServiceEntity>, IServiceService
    {
        public ServiceService(InsureAiContext context) : base(context)
        {
        }
    }
}
