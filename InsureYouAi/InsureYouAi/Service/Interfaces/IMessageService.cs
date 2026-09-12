using InsureYouAi.Entities;

namespace InsureYouAi.Service.Interfaces
{
    public interface IMessageService : IGenericService<Message>
    {
        Task<int> MessageCountAsync();

        Task<List<Message>> Last5Message();
        Task<bool> GetMessageStatusChangeToRead(int id);
        Task<bool> GetMessageStatusChangeToUnRead(int id);
    }
}
