using InsureYouAi.Context;
using InsureYouAi.Entities;
using InsureYouAi.Service.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InsureYouAi.Service.Concrete
{
    public class ContactService : GenericService<Contact>, IContactService
    {
        private readonly InsureAiContext _context;
        public ContactService(InsureAiContext context) : base(context)
        {
            this._context = context;
        }

        public async Task<Contact> GetContact()
        {
            return await _context.Contacts.FirstOrDefaultAsync();
        }
    }
}
