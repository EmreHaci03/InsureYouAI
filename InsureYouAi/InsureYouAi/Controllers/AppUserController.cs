using AutoMapper;
using InsureYouAi.Dtos.AppUserDtos;
using InsureYouAi.Dtos.CommentDtos;
using InsureYouAi.Entities;
using InsureYouAi.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Headers;
using System.Text.Json;

namespace InsureYouAi.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AppUserController : Controller
    {
        private readonly UserManager<AppUser> userManager;
        private readonly IArticleService articleService;
        private readonly IHttpClientFactory httpClientFactory;
        private readonly IConfiguration configuration;
        private readonly ICommentService commentService;
        private readonly IMapper _mapper;

        public AppUserController(UserManager<AppUser> userManager, IMapper mapper, ICommentService commentService, IConfiguration configuration, IHttpClientFactory httpClientFactory, IArticleService articleService)
        {
            this.userManager = userManager;
            _mapper = mapper;
            this.commentService = commentService;
            this.configuration = configuration;
            this.httpClientFactory = httpClientFactory;
            this.articleService = articleService;
        }

        [HttpGet]
        public async Task<IActionResult> UserList()
        {
            var user = await userManager.Users.ToListAsync();
            var mapper = _mapper.Map<List<ResultAppUserDto>>(user);
            return View(mapper);
        }

        [HttpGet]
        public async Task<IActionResult> UserProfile(string id)
        {
            var user = await userManager.Users.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (user == null)
                return NotFound();

            ViewBag.UserComments=await commentService.CommentCountByUserId(id);

            ViewBag.UserArticle = await articleService.ArticleCountByUser(id);

            ViewBag.ApprovedComments = await commentService.ApprovedCommentCountByUser(id);

            var mapper = _mapper.Map<GetAppUserByIdDto>(user);

            return View(mapper);
        }

        [HttpGet]
        public async Task<IActionResult> UserAnalyse(string id)
        {
            var user = await userManager.Users.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (user == null)
                return NotFound();

            var mapper = _mapper.Map<GetAppUserByIdDto>(user);

            var articles = await articleService.GetAllArticleContentByUser(id);
            if ( articles==null &&articles.Count == 0)
            {
                TempData["ErrorMessage"] = "Analiz Yapılacak Kullanıcı Makalesi Bulunamadı";
                return View(mapper);
            }

            var allArticles = string.Join("\n\n---\n\n", articles);

            var apiKey = configuration.GetSection("ApiKey:OpenAi").Value;
            var client = httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

            var prompt = "Aşağıda bir sigorta platformu kullanıcısının yazdığı makalelerin içerikleri verilmiştir.\n\n" +
                "Bu verilere dayanarak aşağıdaki başlıklar altında kısa ve öz bir analiz raporu hazırla:\n" +
                "1. Genel Ton: Makaleler genel olarak olumlu mu, bilgilendirici mi, teknik mi?\n" +
                "2. İlgi Alanları: Kullanıcı hangi sigorta türlerine (sağlık, konut, araç, hayat vb.) daha çok içerik üretmiş?\n" +
                "3. Yazım Sıklığı ve Kapsamı: Makale sayısı ve içerik derinliği nasıl?\n" +
                "4. Öne Çıkan Temalar: Makalelerde tekrar eden konu veya vurgular var mı?\n" +
                "5. Genel Değerlendirme: Bu kullanıcı hakkında 2-3 cümlelik bir özet.\n\n" +
                "Yanıtı sade, madde işaretli ve Türkçe olarak ver.\n\n" +
                "Makale İçerikleri:\n" + allArticles;

            var requestData = new
            {
                model = "gpt-3.5-turbo",
                messages = new[]
                {
                    new { role = "system", content = "Sen Sigorta Sektöründe İçerik Analizi Yapan Bir Uzmansın" },
                    new { role = "user", content = prompt }
                },
                temperature = 0.3
            };

            var responseMessage = await client.PostAsJsonAsync("https://api.openai.com/v1/chat/completions", requestData);

            if (!responseMessage.IsSuccessStatusCode)
            {
                var error = await responseMessage.Content.ReadAsStringAsync();
                TempData["ErrorMessage"] = $"AI İçerik Üretiminde Hata Oluştu: {error} ({responseMessage.StatusCode})";
                return View(mapper);
            }

            var responseString = await responseMessage.Content.ReadAsStringAsync();

            try
            {
                using var doc = JsonDocument.Parse(responseString);
                var analysisText = doc.RootElement
                    .GetProperty("choices")[0]
                    .GetProperty("message")
                    .GetProperty("content")
                    .GetString();

                ViewBag.AnalysisText = analysisText;
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Analiz sonucu işlenemedi: {ex.Message}";
                return View(mapper);
            }

            return View(mapper);
        }

        [HttpGet]
        public async Task<IActionResult> UserAnalyseByComment(string id)
        {
            var user = await userManager.Users.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (user == null)
                return NotFound();

            var mapper = _mapper.Map<GetAppUserByIdDto>(user);

            var comments = await commentService.CommentListByUserId(id);
            if (comments == null || comments.Count == 0)
            {
                TempData["ErrorMessage"] = "Analiz Yapılacak Kullanıcı Yorumu Bulunamadı";
                return View(mapper);
            }

            var allComments = string.Join("\n\n---\n\n", comments);

            var apiKey = configuration.GetSection("ApiKey:OpenAi").Value;
            var client = httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

            var prompt = "Aşağıda bir sigorta platformu kullanıcısının çeşitli makalelere yazdığı yorumlar verilmiştir.\n\n" +
                "Bu yorumlara dayanarak aşağıdaki başlıklar altında kısa ve öz bir karakter/ton analizi hazırla:\n" +
                "1. Genel Karakter: Kullanıcının yazım tarzı nasıl (samimi, resmi, eleştirel, meraklı vb.)?\n" +
                "2. Duygu Tonu: Yorumlar genel olarak olumlu mu, olumsuz mu, nötr mü?\n" +
                "3. İletişim Tarzı: Kullanıcı kısa ve öz mü yazıyor, yoksa detaylı açıklamalar mı yapıyor?\n" +
                "4. Öne Çıkan Kaygılar: Yorumlarda tekrar eden bir soru, şikayet ya da beklenti var mı?\n" +
                "5. Genel Değerlendirme: Bu kullanıcı hakkında 2-3 cümlelik bir özet.\n\n" +
                "Yanıtı sade, madde işaretli ve Türkçe olarak ver.\n\n" +
                "Kullanıcı Yorumları:\n" + allComments;

            var requestData = new
            {
                model = "gpt-3.5-turbo",
                messages = new[]
                {
            new { role = "system", content = "Sen Sigorta Sektöründe Kullanıcı Yorumlarını Analiz Eden Bir Uzmansın" },
            new { role = "user", content = prompt }
        },
                temperature = 0.3
            };

            var responseMessage = await client.PostAsJsonAsync("https://api.openai.com/v1/chat/completions", requestData);

            if (!responseMessage.IsSuccessStatusCode)
            {
                var error = await responseMessage.Content.ReadAsStringAsync();
                TempData["ErrorMessage"] = $"AI İçerik Üretiminde Hata Oluştu: {error} ({responseMessage.StatusCode})";
                return View(mapper);
            }

            var responseString = await responseMessage.Content.ReadAsStringAsync();

            try
            {
                using var doc = JsonDocument.Parse(responseString);
                var analysisText = doc.RootElement
                    .GetProperty("choices")[0]
                    .GetProperty("message")
                    .GetProperty("content")
                    .GetString();

                ViewBag.AnalysisText = analysisText;
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Analiz sonucu işlenemedi: {ex.Message}";
                return View(mapper);
            }

            return View(mapper);
        }


        [HttpGet]
        public async Task<IActionResult> UserComments(string id)
        {
            var comments = await commentService.CommentListByUserId(id);
            if (comments == null || !comments.Any())
                return NotFound();

            var mapper = _mapper.Map<List<GetCommentByIdDto>>(comments);
            return View(mapper);
        }

    }
}
