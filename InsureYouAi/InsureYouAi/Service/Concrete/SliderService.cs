using InsureYouAi.Context;
using InsureYouAi.Entities;
using InsureYouAi.Service.Interfaces;

namespace InsureYouAi.Service.Concrete
{
    public class SliderService : GenericService<Slider>, ISliderService
    {
        public SliderService(InsureAiContext context) : base(context)
        {
        }
    }
}
