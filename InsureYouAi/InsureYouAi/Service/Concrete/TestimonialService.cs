using InsureYouAi.Context;
using InsureYouAi.Entities;
using InsureYouAi.Service.Interfaces;

namespace InsureYouAi.Service.Concrete
{
    public class TestimonialService : GenericService<Testimonial>, ITestimonialService
    {
        public TestimonialService(InsureAiContext context) : base(context)
        {
        }
    }
}
