using AutoMapper;
using InsureYouAi.Dtos.AppUserDtos;
using InsureYouAi.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace InsureYouAi.ViewComponents.AdminDashboard
{
    public class AdminDashboardUserListViewComponent:ViewComponent
    {
        private readonly IUserService userService;
        private readonly IMapper _mapper;
        public AdminDashboardUserListViewComponent(IUserService userService, IMapper mapper)
        {
            this.userService = userService;
            _mapper = mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var UserList = await userService.GetLast5User();
            var mapping = _mapper.Map<List<ResultAppUserDto>>(UserList);
            return View(mapping);
        }
    }
}
