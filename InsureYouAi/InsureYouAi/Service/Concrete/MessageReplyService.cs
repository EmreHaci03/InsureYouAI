using InsureYouAi.Context;
using InsureYouAi.Entities;
using InsureYouAi.Service.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InsureYouAi.Service.Concrete
{
    public class MessageReplyService : GenericService<MessageReply> ,IMessageReplyService
    {
        private readonly InsureAiContext _context;
        public MessageReplyService(InsureAiContext context) : base(context)
        {
            this._context = context;
        }

        public async Task<List<MessageReply>> GetAllWithMessage()
        {
            return await _context.MessageReplies.Include(x => x.Message).OrderByDescending(x=>x.ReplyDate).ToListAsync();
        }

        public async Task<MessageReply> GetAllWithMessageById(int id)
        {
            return await _context.MessageReplies.Include(x => x.Message).FirstOrDefaultAsync(x=>x.MessageReplyId==id);
        }
    }
}
