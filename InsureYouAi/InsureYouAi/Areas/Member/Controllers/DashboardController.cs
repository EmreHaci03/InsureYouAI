using InsureYouAi.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace InsureYouAi.Areas.Member.Controllers
{
    [Area("Member")]
    [Authorize]
    public class DashboardController : Controller
    {
        private readonly UserManager<AppUser> userManager;

        public DashboardController(UserManager<AppUser> userManager)
        {
            this.userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var user = await userManager.GetUserAsync(User);
            ViewBag.FullName = user != null ? $"{user.Name} {user.SurName}".Trim() : "Ziyaretçi";
            return View();
        }
    }
}
