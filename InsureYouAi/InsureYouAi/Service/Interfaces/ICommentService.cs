using InsureYouAi.Entities;
using InsureYouAi.Models;

namespace InsureYouAi.Service.Interfaces
{
    public interface ICommentService:IGenericService<Comment>
    {
        Task<List<Comment>> CommentListWithDetail();

        Task<Dictionary<int, int>> GetCommentCountsByArticleIds(List<int> articleIds);
        Task<List<CommentStatusViewModel>> GetCommentStatus();

        Task<List<TopCommenterViewModel>> GetTop5Commenters();
        Task<List<Comment>> CommentListByUserId(string id);

        Task<double> GetAverageApprovalHours();
        Task<int> CommentCountByUserId(string id);
        Task<int> ApprovedCommentCountByUser(string id);
        Task<List<Comment>> UnreadCommentList();
        Task<Comment> GetCommentWithDetail(int id);
        Task<int> GetCommentCountByArticle(int id);

        Task<List<Comment>> GetLast5Comments();

        Task<List<Comment>> CommentListByArticle(int id);

        Task<List<Comment>> ApprovedCommentListByArticle(int id);

        Task<bool> CommentStatusTypeChangeApproved(int id);
    }
}
