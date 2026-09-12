using InsureYouAi.Entities;
using InsureYouAi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace InsureYouAi.ViewComponents.AdminLayout
{
    public class AdminLayoutProfileViewComponent:ViewComponent
    {
        private readonly UserManager<AppUser> userManager;

        public AdminLayoutProfileViewComponent(UserManager<AppUser> userManager)
        {
            this.userManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var user = await userManager.GetUserAsync(HttpContext.User);
            if (user == null)
                return View(new ProfileViewModel());

            ProfileViewModel model = new ProfileViewModel()
            {
                Id=user.Id,
                ImageUrl=user.ImageUrl,
                Name=user.Name,
                Surname=user.SurName,
                Description=user.Description
            };

            return View(model);
        }
    }
}
