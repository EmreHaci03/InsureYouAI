using InsureYouAi.Context;
using InsureYouAi.Entities;
using InsureYouAi.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace InsureYouAi.Service.Concrete
{
    public class MessageService : GenericService<Message>, IMessageService
    {
        private readonly InsureAiContext _context;
        public MessageService(InsureAiContext context) : base(context)
        {
            this._context = context;
        }

        public async Task<bool> GetMessageStatusChangeToRead(int id)
        {
            var message = await _context.Messages.FirstOrDefaultAsync(x => x.MessageId == id);
            if (message == null)
                return false;

            message.IsRead = true;
            await _context.SaveChangesAsync();
            return true;

        }

        public async Task<bool> GetMessageStatusChangeToUnRead(int id)
        {
            var message = await _context.Messages.FirstOrDefaultAsync(x => x.MessageId == id);
            if (message == null)
                return false;

            message.IsRead = false;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Message>> Last5Message()
        {
            return await _context.Messages.OrderByDescending(x => x.MessageId).Take(5).ToListAsync();
        }

        public async Task<int> MessageCountAsync()
        {
            return await  _context.Messages.CountAsync();
        }
    }
}
