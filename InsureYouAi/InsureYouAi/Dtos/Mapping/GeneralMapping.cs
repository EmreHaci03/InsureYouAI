using AutoMapper;
using InsureYouAi.Dtos.AboutDtos;
using InsureYouAi.Dtos.AboutItemDtos;
using InsureYouAi.Dtos.AppRoleDtos;
using InsureYouAi.Dtos.AppUserDtos;
using InsureYouAi.Dtos.ArticleDtos;
using InsureYouAi.Dtos.CategoryDtos;
using InsureYouAi.Dtos.CommentDtos;
using InsureYouAi.Dtos.ContactDtos;
using InsureYouAi.Dtos.GalleryDtos;
using InsureYouAi.Dtos.MessageDtos;
using InsureYouAi.Dtos.MessageReplyDtos;
using InsureYouAi.Dtos.NewsletterDtos;
using InsureYouAi.Dtos.PolicyDtos;
using InsureYouAi.Dtos.PricingPlanDtos;
using InsureYouAi.Dtos.PricingPlanItemDtos;
using InsureYouAi.Dtos.ServiceDtos;
using InsureYouAi.Dtos.SliderDtos;
using InsureYouAi.Dtos.TestimonialDtos;
using InsureYouAi.Dtos.TrailerVideoDtos;
using InsureYouAi.Entities;
using ServiceEntity = InsureYouAi.Entities.Service;

namespace InsureYouAi.Dtos.Mapping
{
    public class GeneralMapping:Profile
    {
        public GeneralMapping()
        {
            CreateMap<AppUser, ResultAppUserDto>().ReverseMap();
            CreateMap<AppUser, GetAppUserByIdDto>().ReverseMap();


            CreateMap<Policy, ResultPolicyDto>()
                .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User.Name))
                .ReverseMap();
            CreateMap<Policy, GetPolicyByIdDto>()
                .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User.Name))
                .ReverseMap();



            CreateMap<AppRole, ResultAppRoleDto>()
           .ForMember(dest => dest.RoleId, opt => opt.MapFrom(src => src.Id))
           .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.Name));


            CreateMap<AppRole, UpdateAppRoleDto>()
                .ForMember(dest => dest.RoleId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.Name));
            



            CreateMap<Category, ResultCategoryDto>().ReverseMap();
            CreateMap<Category, CreateCategoryDto>().ReverseMap();
            CreateMap<Category, UpdateCategoryDto>().ReverseMap();

            CreateMap<About, ResultAboutDto>().ReverseMap();
            CreateMap<About, CreateAboutDto>().ReverseMap();
            CreateMap<About, UpdateAboutDto>().ReverseMap();
            CreateMap<About, GetAboutByIdDto>().ReverseMap();

            CreateMap<Comment, ResultCommentDto>()
                .ForMember(dest => dest.AppUser, opt => opt.MapFrom(src => src.AppUser.Name + " " + src.AppUser.Name))
                .ForMember(dest => dest.Article, opt => opt.MapFrom(src => src.Article.Title))
                .ForMember(dest => dest.CommentStatusType, opt => opt.MapFrom(src => src.CommentStatus.ToString()))
                .ReverseMap();
            CreateMap<Comment, CreateCommentDto>().ReverseMap();
            CreateMap<Comment, GetCommentByIdDto>()
                .ForMember(dest => dest.AppUser, opt => opt.MapFrom(src => src.AppUser.Name + " " + src.AppUser.Name))
                .ForMember(dest => dest.Article, opt => opt.MapFrom(src => src.Article.Title))
                 .ForMember(dest => dest.CommentStatusType, opt => opt.MapFrom(src => src.CommentStatus.ToString()))
                  .ReverseMap();

            CreateMap<Gallery, ResultGalleryDto>().ReverseMap();
            CreateMap<Gallery, CreateGalleryDto>().ReverseMap();
            CreateMap<Gallery, UpdateGalleryDto>().ReverseMap();
            CreateMap<Gallery, GetGalleryByIdDto>().ReverseMap();


            CreateMap<Slider, ResultSliderDto>().ReverseMap();
            CreateMap<Slider, CreateSliderDto>().ReverseMap();
            CreateMap<Slider, UpdateSliderDto>().ReverseMap();


            CreateMap<MessageReply, ResultMessageReplyDto>()
                .ForMember(dest => dest.Message, opt => opt.MapFrom(src => src.Message.NameSurname))
                .ReverseMap();
            CreateMap<MessageReply, GetMessageReplyByIdDto>()
                .ForMember(dest => dest.Message, opt => opt.MapFrom(src => src.Message.NameSurname))
                .ReverseMap();
            CreateMap<MessageReply, CreateMessageReplyDto>().ReverseMap();
            CreateMap<MessageReply, UpdateMessageReplyDto>().ReverseMap();



            CreateMap<Message, ResultMessageDto>().ReverseMap();
            CreateMap<Message, UpdateMessageDto>().ReverseMap();
            CreateMap<Message, GetMessageByIdDto>().ReverseMap();
            CreateMap<Message, CreateMessageDto>().ReverseMap();

            CreateMap<Newsletter, CreateNewsletterDto>().ReverseMap();
            CreateMap<Newsletter, UpdateNewsletterDto>().ReverseMap();
            CreateMap<Newsletter, ResultNewsletterDto>().ReverseMap();


            CreateMap<Testimonial, ResultTestimonialDto>().ReverseMap();
            CreateMap<Testimonial, CreateTestimonialDto>().ReverseMap();
            CreateMap<Testimonial, UpdateTestimonialDto>().ReverseMap();
            CreateMap<Testimonial, GetTestimonialByIdDto>().ReverseMap();

            CreateMap<TrailerVideo, ResultTrailerVideoDto>().ReverseMap();
            CreateMap<TrailerVideo, CreateTrailerVideoDto>().ReverseMap();
            CreateMap<TrailerVideo, UpdateTrailerVideoDto>().ReverseMap();
            CreateMap<TrailerVideo, UpdateTrailerVideoDto>().ReverseMap();


            CreateMap<AboutItem, ResultAboutItemDto>()
                .ForMember(dest => dest.About, opt => opt.MapFrom(src => src.About.Title))
                .ReverseMap();
            CreateMap<AboutItem, CreateAboutItemDto>().ReverseMap();
            CreateMap<AboutItem, UpdateAboutItemDto>().ReverseMap();
            CreateMap<AboutItem, GetAboutItemByIdDto>()
                 .ForMember(dest => dest.About, opt => opt.MapFrom(src => src.About.Title))
                .ReverseMap();

            CreateMap<Contact, ResultContactDto>().ReverseMap();
            CreateMap<Contact, CreateContactDto>().ReverseMap();
            CreateMap<Contact, UpdateContactDto>().ReverseMap();
            CreateMap<Contact, GetContactByIdDto>().ReverseMap();



            CreateMap<Article, ResultArticleDto>()
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category.CategoryName))
                  .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.AppUser.Name + " " + src.AppUser.SurName))
                .ReverseMap();
            CreateMap<Article, CreateArticleDto>().ReverseMap();
            CreateMap<Article, UpdateArticleDto>().ReverseMap();
            CreateMap<Article, GetArticleByIdDto>()
                 .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category.CategoryName))
                 .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.AppUser.Name + " " + src.AppUser.SurName))
                .ReverseMap();


            CreateMap<PricingPlan, ResultPricingPlanDto>()
                .ReverseMap();
            CreateMap<CreatePricingPlanDto, PricingPlan>()
                .ReverseMap();
            CreateMap<PricingPlan, UpdatePricingPlanDto>()
                 .ReverseMap();
            CreateMap<PricingPlan, GetPricingPlanByIdDto>()
                 .ReverseMap();


            CreateMap<PricingPlanItem, ResultPricingPlanItemDto>()
                .ForMember(dest => dest.PricingPlan, opt => opt.MapFrom(src => src.PricingPlan.Title))
                .ReverseMap();
            CreateMap<PricingPlanItem, CreatePricingPlanItemDto>().ReverseMap();
            CreateMap<PricingPlanItem, UpdatePricingPlanItemDto>().ReverseMap();
            CreateMap<PricingPlanItem, GetPricingPlanItemDto>()
                 .ForMember(dest => dest.PricingPlan, opt => opt.MapFrom(src => src.PricingPlan.Title))
                .ReverseMap();


            CreateMap<ServiceEntity, ResultServiceDto>().ReverseMap();
            CreateMap<ServiceEntity, CreateServiceDto>().ReverseMap();
            CreateMap<ServiceEntity, UpdateServiceDto>().ReverseMap();
            CreateMap<ServiceEntity, GetServiceByIdDto>().ReverseMap();
        }
    }
}
