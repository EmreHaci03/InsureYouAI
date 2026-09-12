using InsureYouAi.Dtos.LoginDto;
using InsureYouAi.Dtos.RegisterDtos;
using InsureYouAi.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace InsureYouAi.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;
        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        [HttpGet]
        public async Task<IActionResult> Register()
        {
            return View(new RegisterDto());
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            var appUser = new AppUser()
            {
                Name = dto.Name,
                SurName = dto.Surname,
                UserName = dto.Username,
                Email = dto.Email,
            };
            if (dto.Password == dto.ConfirmPassword)
            {
                var result = await userManager.CreateAsync(appUser, dto.Password);
                if (result.Succeeded)
                    return RedirectToAction("Login");  
            else
            {
                foreach (var item in result.Errors)
                    ModelState.AddModelError(item.Code, item.Description);
            }
            }
            else
                    ModelState.AddModelError("", "Şifreler Eşleşmiyor");

                return View();
        }

        [HttpGet]
        public async Task<IActionResult> Login()
        {
            return View(new LoginDto());
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDto dto)
        {

            if (!ModelState.IsValid)
                return View(dto);

            var user = await userManager.FindByNameAsync(dto.Username);
            if (user==null)
            {
                ModelState.AddModelError("", "Kullanıcı Adı veya şifre hatalı.");
                return View(dto);
            }
            var result = await signInManager.PasswordSignInAsync(user, dto.Password, false, lockoutOnFailure: true);
            if (result.Succeeded)
            {
                var roles = await userManager.GetRolesAsync(user);
                if (roles.Contains("Admin"))
                {
                    return RedirectToAction("Index", "Dashboard");
                }

                return RedirectToAction("MemberArticleList", "MemberArticle", new {area="Member"});
            }
            else if (result.IsLockedOut)
            {
                ModelState.AddModelError("", "Hesabınız çok fazla hatalı denemeden dolayı kilitlendi. Lütfen daha sonra tekrar deneyin.");
            }
            else
            {
                ModelState.AddModelError("", "Kullanıcı Adı veya şifre hatalı.");
            }

                return View(dto);
        }

        [HttpGet]
        public async Task<IActionResult> LogOut()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }
    }
}
