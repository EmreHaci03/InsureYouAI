using InsureYouAi.Entities;
using InsureYouAi.Entities.Enums;

namespace InsureYouAi.Dtos.CommentDtos
{
    public class CreateCommentDto
    {
        public string CommentDetail { get; set; }
        public DateTime CommentDate { get; set; }
        public string AppUserId { get; set; }
        public int ArticleId { get; set; }
        public CommentStatusType CommentStatusType { get; set; }
    }
}
