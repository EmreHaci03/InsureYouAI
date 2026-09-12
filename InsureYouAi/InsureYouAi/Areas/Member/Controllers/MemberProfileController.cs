using InsureYouAi.Entities;
using InsureYouAi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace InsureYouAi.Areas.Member.Controllers
{
    [Area("Member")]
    [Authorize]
    public class MemberProfileController : Controller
    {
        private readonly UserManager<AppUser> userManager;

        public MemberProfileController(UserManager<AppUser> userManager)
        {
            this.userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var user = await userManager.GetUserAsync(User);
            if (user == null)
            {
                TempData["ErrorMessage"] = "Profilinizi görmek için giriş yapınız.";
                return RedirectToAction("Index", "Dashboard");
            }

            var model = new MemberProfileViewModel
            {
                FullName = $"{user.Name} {user.SurName}".Trim(),
                Email = user.Email ?? "",
                PhoneNumber = user.PhoneNumber ?? "",
                ImageUrl = user.ImageUrl,
                Description = user.Description
            };

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> ChangePassword()
        {
            var user = await userManager.GetUserAsync(User);
            if (user == null)
                return RedirectToAction("Index", "Dashboard");

            return View(new ChangePasswordViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = await userManager.GetUserAsync(User);
            if (user == null)
                return RedirectToAction("Index", "Dashboard");

            if (model.NewPassword != model.ConfirmPassword)
            {
                ModelState.AddModelError("", "Yeni şifreler eşleşmiyor.");
                return View(model);
            }

            var result = await userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);

            if (result.Succeeded)
            {
                TempData["SuccessMessage"] = "Şifreniz başarıyla güncellendi.";
                return RedirectToAction("Index", "MemberProfile");
            }

            foreach (var error in result.Errors)
                ModelState.AddModelError("", error.Description);

            return View(model);
        }
    }
}