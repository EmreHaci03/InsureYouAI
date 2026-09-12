using AutoMapper;
using InsureYouAi.Dtos.AppUserDtos;
using InsureYouAi.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace InsureYouAi.ViewComponents.BlogDetail
{
    public class BlogDetailAvatarViewComponent:ViewComponent
    {
        private readonly IArticleService articleService;
        private readonly IUserService userService;
        private readonly IMapper _mapper;

        public BlogDetailAvatarViewComponent(IArticleService articleService, IUserService userService, IMapper mapper)
        {
            this.articleService = articleService;
            this.userService = userService;
            _mapper = mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            var blog = await articleService.GetByIdAsync(id);
            var userId = blog.AppUserId;
            var user = await userService.GetUserByIdAsync(userId);
            var mapper = _mapper.Map<GetAppUserByIdDto>(user);
            return View(mapper);
        }
    }
}
