using AutoMapper;
using InsureYouAi.Dtos.ArticleDtos;
using InsureYouAi.Dtos.CategoryDtos;
using InsureYouAi.Dtos.PricingPlanDtos;
using InsureYouAi.Entities;
using InsureYouAi.Service.Concrete;
using InsureYouAi.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Net.Http.Headers;
using System.Text.Json;

namespace InsureYouAi.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ArticleController : Controller
    {
        private readonly IArticleService articleService;
        private readonly ICategoryService categoryService;
        private readonly IConfiguration configuration;
        private readonly IMapper _mapper;
        private readonly IHttpClientFactory httpClientFactory;
        public ArticleController(IArticleService articleService, IMapper mapper, ICategoryService categoryService, IConfiguration configuration, IHttpClientFactory httpClientFactory)
        {
            this.articleService = articleService;
            _mapper = mapper;
            this.categoryService = categoryService;
            this.configuration = configuration;
            this.httpClientFactory = httpClientFactory;
        }
        [HttpGet]
        public async Task<IActionResult> ArticleList()
        {
            var Article = await articleService.GetAllWithCategory();
            var mapper = _mapper.Map<List<ResultArticleDto>>(Article);
            return View(mapper);
        }

        [HttpGet]
        public async Task<IActionResult> CreateArticle()
        {
            var Category = await categoryService.GetAllAsync();
            var mapper = _mapper.Map<List<ResultCategoryDto>>(Category);
            ViewBag.Categories = mapper;
            return View(new CreateArticleDto());
        }
        [HttpPost]
        public async Task<IActionResult> CreateArticle(CreateArticleDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);
            dto.CreatedDate = DateTime.UtcNow;
            var PricingPlan = _mapper.Map<Article>(dto);
            await articleService.AddAsync(PricingPlan);
            TempData["SuccessMessage"] = "Makale Başarıyla Eklendi.";
            return RedirectToAction("ArticleList");
        }

        [HttpGet]
        public async Task<IActionResult> ArticleDetail(int id)
        {
            var Article = await articleService.GetByIdAsync(id);
            if (Article is null)
                return NotFound();
            var mapper = _mapper.Map<GetArticleByIdDto>(Article);
            return View(mapper);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteArticle(int id)
        {
            var Article = await articleService.GetByIdAsync(id);
            if (Article is null)
                return NotFound();

            await articleService.DeleteAsync(Article);
            TempData["SuccessMessage"] = "Ödeme Planı başarıyla silindi.";
            return RedirectToAction("ArticleList");
        }


        [HttpGet]
        public async Task<IActionResult> UpdateArticle(int id)
        {
            var Category = await categoryService.GetAllAsync();
            var Categorymapper = _mapper.Map<List<ResultCategoryDto>>(Category);
            ViewBag.Categories = Categorymapper;

            var Article = await articleService.GetArticleWithCategory(id);
            if (Article is null)
                return NotFound();

            var mapper = _mapper.Map<UpdateArticleDto>(Article);
            return View(mapper);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateArticle(UpdateArticleDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            var Article = _mapper.Map<Article>(dto);
            await articleService.UpdateAsync(Article);
            TempData["SuccessMessage"] = "Makale başarıyla Güncellendi.";
            return RedirectToAction("ArticleList");
        }

        [HttpGet]
        public async Task<IActionResult> CreateArticleWithOpenAi()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> CreateArticleWithOpenAi(string prompt)
        {
            var apiKey = configuration.GetSection("ApiKey:OpenAi").Value;

            var client = httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization=new AuthenticationHeaderValue("Bearer", apiKey);
            var SystemPrompt = @"Sen bir sigorta şirketi için profesyonel içerik üreten bir blog yazarısın.
Görevin, kullanıcının verdiği konu başlığına göre bilgilendirici, SEO uyumlu ve okunabilir bir makale yazmak.

Kurallar:
- Türkçe, akıcı ve profesyonel bir dil kullan.
- İçerik en az 400, en fazla 800 kelime olsun.
- Başlıkları <h2> ve <h3> HTML etiketleriyle biçimlendir.
- Paragrafları <p> etiketiyle sar.
- Karmaşık sigorta terimlerini sade bir dille açıkla.
- Reklam veya abartılı pazarlama dili kullanma, bilgilendirici ve güven verici bir ton benimse.
- Yanıtı sadece HTML içerik olarak ver, başka açıklama veya yorum ekleme.
- Kaynak uydurma, sadece genel bilgi ver.";


            var requestData = new
            {
                model = "gpt-3.5-turbo",
                messages = new[] 
                {
                    new {role="system",content=SystemPrompt},
                    new {role="user",content=prompt}
                },
                temperature = 0.7
            };

            var response = await client.PostAsJsonAsync("https://api.openai.com/v1/chat/completions", requestData);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                TempData["ErrorMessage"] = $"AI İçerik Üretiminde Hata Oluştu{error} {response.StatusCode}";
                return View();
            }

            var result = await response.Content.ReadFromJsonAsync<OpenAiResponse>();
            var generateContent = result?.Choices?.FirstOrDefault().Message?.content ?? string.Empty;

            ViewBag.Content = generateContent;


            var category = await categoryService.GetAllAsync();
            ViewBag.Categories = _mapper.Map<List<ResultCategoryDto>>(category);
            return View();

        }
    }
}


public class OpenAiResponse
{
    public List<Choice> Choices { get; set; }
}

public class Choice
{
    public OpenAiMessage Message {  get; set; }
}
public class OpenAiMessage
{
    public string role { get; set; }
    public string content  { get; set; }
}
