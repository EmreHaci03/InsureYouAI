using InsureYouAi.Entities;

namespace InsureYouAi.Service.Interfaces
{
    public interface IMessageReplyService : IGenericService<MessageReply>
    {
        Task<List<MessageReply>> GetAllWithMessage();
        Task<MessageReply> GetAllWithMessageById(int id);
    }
}
