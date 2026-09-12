using InsureYouAi.Service;
using InsureYouAi.Service.Concrete;
using InsureYouAi.Service.Interfaces;
using System;

namespace InsureYouAi.Extensions
{
    public static class ServiceRegistiration
    {

        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IAboutItemService, AboutItemService>();
            services.AddScoped<IAboutService, AboutService>();
            services.AddScoped<IArticleService, ArticleService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IContactService, ContactService>();
            services.AddScoped<IMessageService, MessageService>();
            services.AddScoped<INewsletterService, NewsletterService>();
            services.AddScoped<IPricingPlanItemService, PricingPlanItemService>();
            services.AddScoped<IPricingPlanService, PricingPlanService>();
            services.AddScoped<IServiceService, ServiceService>();
            services.AddScoped<ISliderService, SliderService>();
            services.AddScoped<ITestimonialService, TestimonialService>();
            services.AddScoped<ITrailerService, TrailerVideoService>();
            services.AddScoped<IGalleryService, GalleryService>();
            services.AddScoped<ICommentService, CommentService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IToxicityService, ToxicityService>();
            services.AddScoped<EmailService>();
            services.AddScoped<IMessageReplyService,MessageReplyService>();
            services.AddScoped<IRevenueService,RevenueService>();
            services.AddScoped<TavilyService>();
            services.AddScoped<ForecastService>();
            services.AddScoped<AIService>();
            services.AddScoped<IPolicyService,PolicyService>();
        }


        public static void AddRepositoryServices(this IServiceCollection services)
        {
            services.AddScoped(typeof(IGenericService<>), typeof(GenericService<>));
        }
    }
}
