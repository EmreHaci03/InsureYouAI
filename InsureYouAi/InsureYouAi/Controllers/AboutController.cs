using AutoMapper;
using InsureYouAi.Dtos.AboutDtos;
using InsureYouAi.Entities;
using InsureYouAi.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace InsureYouAi.Controllers
{
    [Authorize(Roles ="Admin")]
    public class AboutController : Controller
    {
        private readonly IAboutService AboutService;
        private readonly IMapper _mapper;
        private readonly IHttpClientFactory httpClientFactory;
        private readonly IConfiguration configuration;
        public AboutController(IAboutService AboutService, IMapper mapper, IConfiguration configuration, IHttpClientFactory httpClientFactory)
        {
            this.AboutService = AboutService;
            _mapper = mapper;
            this.configuration = configuration;
            this.httpClientFactory = httpClientFactory;
        }
        [HttpGet]
        public async Task<IActionResult> AboutList()
        {
            var values = await AboutService.GetAllAsync();
            var mapped = _mapper.Map<List<ResultAboutDto>>(values);
            return View(mapped);
        }

        [HttpGet]
        public async Task<IActionResult> CreateAbout()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateAbout(CreateAboutDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            var About = _mapper.Map<About>(dto);
            await AboutService.AddAsync(About);
            TempData["SuccessMessage"] = "İçerik başarıyla Eklendi.";
            return RedirectToAction("AboutList");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteAbout(int id)
        {
            var about = await AboutService.GetByIdAsync(id);
            if (about is null)
                return NotFound();

            await AboutService.DeleteAsync(about);
            TempData["SuccessMessage"] = "İçerik başarıyla silindi.";
            return RedirectToAction("AboutList");
        }

        [HttpGet]
        public async Task<IActionResult> AboutDetail(int id)
        {
            var about = await AboutService.GetByIdAsync(id);
            if (about is null)
                return NotFound();

            var mapped = _mapper.Map<GetAboutByIdDto>(about);
            return View(mapped);
        }



        [HttpGet]
        public async Task<IActionResult> UpdateAbout(int id)
        {
            var about = await AboutService.GetByIdAsync(id);
            if (about is null)
                return NotFound();

            var mapped = _mapper.Map<UpdateAboutDto>(about);
            return View(mapped);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateAbout(UpdateAboutDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            var About = _mapper.Map<About>(dto);
            await AboutService.UpdateAsync(About);
            TempData["SuccessMessage"] = "İçerik başarıyla güncellendi.";
            return RedirectToAction("AboutList");
        }

        [HttpGet]
        public async Task<IActionResult> CreateAboutWithGemini()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateAboutWithGemini(string prompt)
        {
            var apiKey = configuration["ApiKey:Gemini"];   // appsettings.json'daki gerçek key adına göre ayarla

            var client = httpClientFactory.CreateClient();

            var systemPrompt = @"Sen bir sigorta şirketi için ""Hakkımızda"" içeriği üreten profesyonel bir metin yazarısın.
Görevin, kullanıcının verdiği konuya göre kısa, güven verici ve kurumsal bir açıklama metni yazmak.
Kurallar:
- Türkçe, akıcı ve profesyonel bir dil kullan.
- İçerik 100-250 kelime arasında olsun.
- Düz metin olarak yaz, HTML etiketi kullanma.
- Abartılı pazarlama dili kullanma, güven veren ve samimi bir ton benimse.
- Yanıtı sadece açıklama metni olarak ver, başka yorum ekleme.";

            var requestData = new
            {
                system_instruction = new
                {
                    parts = new[] { new { text = systemPrompt } }
                },
                contents = new[]
                {
                    new
                    {
                        role = "user",
                        parts = new[] { new { text = prompt } }
                    }
                },
            };

            var url = $"https://generativelanguage.googleapis.com/v1beta/models/gemini-3.6-flash:generateContent?key={apiKey}";

            var response = await client.PostAsJsonAsync(url, requestData);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                TempData["ErrorMessage"] = $"Hata Kodu: {response.StatusCode} - Detay: {error}";
                return View();
            }

            var json = await response.Content.ReadAsStringAsync();
            using var doc = JsonDocument.Parse(json);
            var generatedContent = doc.RootElement
                .GetProperty("candidates")[0]
                .GetProperty("content")
                .GetProperty("parts")[0]
                .GetProperty("text")
                .GetString();

            ViewBag.Description = generatedContent ?? string.Empty;
            return View();
        }
    }
}
