using AutoMapper;
using InsureYouAi.Dtos.ArticleDtos;
using InsureYouAi.Dtos.CommentDtos;
using InsureYouAi.Entities;
using InsureYouAi.Entities.Enums;
using InsureYouAi.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace InsureYouAi.Controllers
{
   
    public class BlogController : Controller
    {
        private readonly IArticleService articleService;
        private readonly UserManager<AppUser> userManager;
        private readonly ICommentService commentService;
        private readonly IToxicityService toxicityService;
        private readonly IMapper _mapper;

        public BlogController(IArticleService articleService, IMapper mapper, ICommentService commentService, IToxicityService toxicityService, UserManager<AppUser> userManager)
        {
            this.articleService = articleService;
            _mapper = mapper;
            this.commentService = commentService;
            this.toxicityService = toxicityService;
            this.userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> BlogList()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> BlogDetail(int id)
        {
            var blog = await articleService.GetArticleWithCategory(id);
            if (blog == null)
            {
                return RedirectToAction("BlogList");
            }
            var mapper = _mapper.Map<GetArticleByIdDto>(blog);
            return View(mapper);
        }

        [HttpGet]
        public async Task<IActionResult> GetBlogByCategory(int id)
        {
            ViewBag.Id = id;
            return View();
        }

        [HttpGet]
        public async Task<PartialViewResult> GetBlog()
        {
            return PartialView();
        }
        [HttpPost]
        public async Task<PartialViewResult> GetBlog(string keyword)
        {
            return PartialView();
        }

        [HttpGet]
        public async Task<IActionResult> CreateComment()
        {
            return PartialView();
        }

        [HttpPost]
        public async Task<IActionResult> CreateComment(CreateCommentDto dto)
        {
            var toxicityResult = await toxicityService.AnalyzeAsync(dto.CommentDetail);

            var user = await userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }


            if (toxicityResult.IsToxic)
            {

                TempData["ErrorMessage"] = "Yorumunuz uygunsuz içerik içerdiği için yayınlanamadı.";
                return RedirectToAction("BlogDetail", new { id = dto.ArticleId });
            }

            dto.CommentDate = DateTime.Now;
            dto.AppUserId = user.Id;
            dto.CommentStatusType = CommentStatusType.Pending;
            var mapper = _mapper.Map<Comment>(dto);

            await commentService.AddAsync(mapper);
            TempData["SuccessMessage"] = "Yorumunuz Yönetici Onayı Sonrası Yayınlanacaktır";
            return RedirectToAction("BlogDetail", new {id=dto.ArticleId});
        }
    }
}
