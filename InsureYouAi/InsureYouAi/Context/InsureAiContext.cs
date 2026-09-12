using InsureYouAi.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using ServiceEntity = InsureYouAi.Entities.Service;

namespace InsureYouAi.Context
{
    public class InsureAiContext:IdentityDbContext<AppUser,AppRole,string>
    {
        public InsureAiContext(DbContextOptions<InsureAiContext>options):base(options)
        {
                
        }

        public DbSet<About> Abouts { get; set; }
        public DbSet<AboutItem> AboutItems { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Newsletter> Newsletters { get; set; }
        public DbSet<PricingPlan> PricingPlans { get; set; }
        public DbSet<PricingPlanItem> PricingPlanItems { get; set; }
        public DbSet<ServiceEntity> Services { get; set; }
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<Testimonial> Testimonials { get; set; }
        public DbSet<TrailerVideo> TrailerVideos { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Gallery> Galleries { get; set; }
        public DbSet<MessageReply> MessageReplies { get; set; }
        public DbSet<Revenue> Revenues { get; set; }
        public DbSet<Policy> Policies { get; set; }
        public DbSet<Expense> Expenses { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Comment>()
                .HasOne(x => x.AppUser)
                .WithMany(x => x.Comments)
                .HasForeignKey(x => x.AppUserId);


            builder.Entity<Comment>()
                .HasOne(c => c.Article)
                .WithMany(a => a.Comments)
                .HasForeignKey(c => c.ArticleId);
        }
    }
}
