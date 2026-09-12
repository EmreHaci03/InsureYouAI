using InsureYouAi.Entities;

namespace InsureYouAi.Dtos.ArticleDtos
{
    public class ResultArticleDto
    {
        public int ArticleId { get; set; }
        public string Title { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Content { get; set; }
        public string CoverImageUrl { get; set; }
        public string MainCoverImageUrl { get; set; }
        public string User { get; set; }
        public string Category { get; set; }
        public int CommentCount { get; set; }
    }
}
