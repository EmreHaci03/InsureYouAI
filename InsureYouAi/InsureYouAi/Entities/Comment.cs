using InsureYouAi.Entities.Enums;

namespace InsureYouAi.Entities
{
    public class Comment
    {
        public int CommentId { get; set; }
        public string CommentDetail { get; set; }
        public DateTime CommentDate { get; set; }
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public int ArticleId { get; set; }
        public Article Article { get; set; }
        public CommentStatusType CommentStatus { get; set; }
        public DateTime? ApprovedDate { get; set; }
    }
}
