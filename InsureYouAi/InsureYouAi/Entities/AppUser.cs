using Microsoft.AspNetCore.Identity;

namespace InsureYouAi.Entities
{
    public class AppUser:IdentityUser
    {
        public string Name { get; set; }
        public string SurName { get; set; }
        public string? ImageUrl { get; set; }
        public string? Description { get; set; }
        public List<Comment> Comments { get; set; }
        public List<Article> Articles { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public List<Policy> Policies { get; set; }
    }
}
