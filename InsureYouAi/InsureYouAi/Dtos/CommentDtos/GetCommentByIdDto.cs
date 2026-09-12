using InsureYouAi.Entities;

namespace InsureYouAi.Dtos.CommentDtos
{
    public class GetCommentByIdDto
    {
        public int CommentId { get; set; }
        public string CommentDetail { get; set; }
        public DateTime CommentDate { get; set; }
        public string AppUser { get; set; }
        public string Article { get; set; }
        public string CommentStatusType { get; set; }

    }
}
