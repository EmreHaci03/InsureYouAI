using AutoMapper;
using InsureYouAi.Dtos.ContactDtos;
using InsureYouAi.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace InsureYouAi.ViewComponents.Default
{
    public class DefaultContactViewComponent:ViewComponent
    {
        private readonly IContactService contactService;
        private readonly IMapper _mapper;

        public DefaultContactViewComponent(IContactService contactService, IMapper mapper)
        {
            this.contactService = contactService;
            _mapper = mapper;
        }

        public async  Task<IViewComponentResult> InvokeAsync()
        {
            var values = await contactService.GetContact();
            var mapper = _mapper.Map<GetContactByIdDto>(values);
            return View(mapper);
        }
    }
}
