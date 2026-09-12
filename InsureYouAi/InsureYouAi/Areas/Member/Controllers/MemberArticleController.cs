using AutoMapper;
using InsureYouAi.Dtos.ArticleDtos;
using InsureYouAi.Entities;
using InsureYouAi.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace InsureYouAi.Areas.Member.Controllers
{
    [Area("Member")]
    [Authorize]
    public class MemberArticleController : Controller
    {
        private readonly UserManager<AppUser> userManager;
        private readonly IArticleService articleService;
        private readonly IMapper _mapper;

        public MemberArticleController(UserManager<AppUser> userManager, IMapper mapper, IArticleService articleService)
        {
            this.userManager = userManager;
            _mapper = mapper;
            this.articleService = articleService;
        }

        [HttpGet]

        public async Task<IActionResult> MemberArticleList()
        {
            var user = await userManager.GetUserAsync(User);
            if (user == null)
            {
                TempData["ErrorMessage"] = "Kullanıcı Bilgilerini Görmek İçin Giriş Yapınız";
                return View(new List<ResultArticleDto>());
            }

            var memberArticles = await articleService.GetUserArticleList(user.Id);
            var mapped = _mapper.Map<List<ResultArticleDto>>(memberArticles);

            return View(mapped);
        }

        [HttpGet]
        public async Task<IActionResult> ArticleDetail(int id)
        {
            var article = await articleService.GetArticleWithCategory(id);
            if (article == null)
            {
                TempData["ErrorMessage"] = "Blog Detayı Bulunamadı";
                return View();
            }

            var mapper = _mapper.Map<GetArticleByIdDto>(article);
            return View(mapper);
                
        }


    }
}