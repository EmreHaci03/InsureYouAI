using InsureYouAi.Entities;

namespace InsureYouAi.Service.Interfaces
{
    public interface IContactService : IGenericService<Contact>
    {
        Task<Contact> GetContact();
    }
}
