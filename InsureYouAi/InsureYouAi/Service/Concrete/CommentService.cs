using InsureYouAi.Context;
using InsureYouAi.Entities;
using InsureYouAi.Entities.Enums;
using InsureYouAi.Models;
using InsureYouAi.Service.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InsureYouAi.Service.Concrete
{
    public class CommentService : GenericService<Comment>, ICommentService
    {
        private readonly InsureAiContext _context;
        public CommentService(InsureAiContext context) : base(context)
        {
            this._context = context;
        }

        public async Task<int> ApprovedCommentCountByUser(string id)
        {
           return await _context.Comments.Where(x=>x.AppUserId == id && x.CommentStatus==CommentStatusType.Approved).CountAsync();
        }

        public async Task<List<Comment>> ApprovedCommentListByArticle(int id)
        {
            return await _context.Comments.Include(x => x.AppUser).Include(x => x.Article).Where(x => x.ArticleId == id && x.CommentStatus==CommentStatusType.Approved).ToListAsync();
        }

        public async Task<int> CommentCountByUserId(string id)
        {
            return await _context.Comments.Where(x=>x.AppUserId == id).CountAsync();
        }

        public async Task<List<Comment>> CommentListByArticle(int id)
        {
            return await _context.Comments.Include(x=>x.AppUser).Include(x => x.Article).Where(x => x.ArticleId == id ).ToListAsync();
        }

        public async Task<List<Comment>> CommentListByUserId(string id)
        {
            return await _context.Comments.Include(x => x.AppUser).Include(x => x.Article).Where(x => x.AppUserId == id).ToListAsync();
        }

        public async Task<List<Comment>> CommentListWithDetail()
        {
            return await _context.Comments.Include(x => x.Article).Include(x => x.AppUser).ToListAsync();
        }

        public async Task<bool> CommentStatusTypeChangeApproved(int id)
        {
            var comment = await _context.Comments.FirstOrDefaultAsync(x => x.CommentId == id);
            if (comment == null)
            {
                return false;
            }
            comment.ApprovedDate = DateTime.Now;
            comment.CommentStatus = CommentStatusType.Approved;
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<double> GetAverageApprovalHours()
        {
            var approvedComments = await _context.Comments
                .Where(x => x.ApprovedDate != null)
                .ToListAsync();

            if (!approvedComments.Any())
                return 0;

            return approvedComments.Average(x => (x.ApprovedDate.Value - x.CommentDate).TotalHours);
        }

        public async Task<int> GetCommentCountByArticle(int id)
        {
            return await _context.Comments.Where(x => x.ArticleId == id).CountAsync();
        }

        public async Task<Dictionary<int, int>> GetCommentCountsByArticleIds(List<int> articleIds)
        {
            return await _context.Comments
                .Where(x => articleIds.Contains(x.ArticleId))
                .GroupBy(x => x.ArticleId)
                .Select(g => new { ArticleId = g.Key, Count = g.Count() })
                .ToDictionaryAsync(x => x.ArticleId, x => x.Count);
        }

        public async Task<List<CommentStatusViewModel>> GetCommentStatus()
        {
            var grouped=await _context.Comments
                .GroupBy(x=>x.CommentStatus)
                .Select(g =>new CommentStatusViewModel
                {
                  TypeName=g.Key.ToString(),
                  StatusCount=g.Count()
                }).ToListAsync();

            return grouped;
        }

        public async Task<Comment> GetCommentWithDetail(int id)
        {
            return await _context.Comments.Include(x => x.Article).Include(x => x.AppUser).FirstOrDefaultAsync(x => x.CommentId == id);
        }

        public async Task<List<Comment>> GetLast5Comments()
        {
            return await _context.Comments.Include(x => x.Article).Include(x => x.AppUser).OrderByDescending(x => x.CommentId).Take(5).ToListAsync();
        }

        public async Task<List<TopCommenterViewModel>> GetTop5Commenters()
        {
            return await _context.Comments
                .Include(x=>x.AppUser)
                .GroupBy(x => x.AppUserId)
                .Select(g => new TopCommenterViewModel
                {
                    UserId = g.Key,
                    UserName = g.First().AppUser.Name + " " + g.First().AppUser.SurName,
                    CommentCount = g.Count()
                })
                .OrderByDescending(x => x.CommentCount)
                .Take(5)
                .ToListAsync();
        }

        public async Task<List<Comment>> UnreadCommentList()
        {
            return await _context.Comments.Include(x => x.Article).Include(x => x.AppUser).Where(x => x.CommentStatus == CommentStatusType.Pending).OrderByDescending(x=>x.CommentDate).ToListAsync();
        }
    }
}
