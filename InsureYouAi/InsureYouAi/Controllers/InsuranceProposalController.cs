
using InsureYouAi.Dtos.RecommendationResultDto;
using InsureYouAi.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;

namespace InsureYouAi.Controllers
{
    [Authorize(Roles = "Admin")]
    public class InsuranceProposalController : Controller
    {
        private readonly IConfiguration configuration;
        private readonly IHttpClientFactory httpClientFactory;
        private readonly IPricingPlanService pricingPlanService;

        public InsuranceProposalController(
            IConfiguration configuration,
            IHttpClientFactory httpClientFactory,
            IPricingPlanService pricingPlanService)
        {
            this.configuration = configuration;
            this.httpClientFactory = httpClientFactory;
            this.pricingPlanService = pricingPlanService;
        }

        [HttpGet]
        public IActionResult InsuranceProposal()
        {
            return View(new CreateRecommendationResultDto());
        }

        [HttpPost]
        public async Task<IActionResult> InsuranceProposal(CreateRecommendationResultDto request)
        {
            if (!ModelState.IsValid)
                return View(request);

            var apiKey = configuration.GetSection("ApiKey:OpenAI").Value;
            var plans = await pricingPlanService.GetAllAsync();

            var plansText = string.Join("\n", plans.Select(p =>
                $"- {p.Title} (Fiyat: {p.Price:N0}₺/ay){(p.IsFeature ? " [Öne Çıkan Plan]" : "")}"));

            var systemPrompt = @"Sen InsureYouAi şirketinde çalışan uzman bir sigorta danışmanısın.
Kullanıcının bilgilerine göre, sana verilen mevcut planlar arasından EN UYGUN OLANI seçeceksin.
Sadece verilen plan listesindeki bir planı önerebilirsin, listede olmayan bir plan uydurma.
Cevabını SADECE aşağıdaki JSON formatında ver, başka hiçbir açıklama ekleme:
{
  ""recommendedPlanTitle"": ""<plan başlığı, listedekiyle birebir aynı>"",
  ""confidenceLevel"": ""Düşük | Orta | Yüksek"",
  ""reasoning"": ""<kullanıcıya hitaben 2-3 cümlelik açıklama>"",
  ""tips"": [""<kısa öneri 1>"", ""<kısa öneri 2>""]
}";

            var userPrompt = $@"Kullanıcı Bilgileri:
- Yaş: {request.Age}
- Şehir: {request.City}
- Aile üye sayısı: {request.FamilyMemberCount}
- Seyahat sıklığı: {request.TravelFrequency}
- Aylık bütçe: {request.MonthlyBudget:N0}₺
- Tercih edilen kapsam: {request.PreferredCoverageType}

Mevcut Planlar:
{plansText}";

            var client = httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

            var requestData = new
            {
                model = "gpt-4o-mini",
                messages = new[]
                {
                    new { role = "system", content = systemPrompt },
                    new { role = "user", content = userPrompt }
                },
                response_format = new { type = "json_object" },
                temperature = 0.4
            };

            var response = await client.PostAsJsonAsync("https://api.openai.com/v1/chat/completions", requestData);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                TempData["ErrorMessage"] = $"AI İçerik Üretiminde Hata Oluştu: {error} {response.StatusCode}";
                return View(request);
            }

            var responseContent = await response.Content.ReadAsStringAsync();

            using var jsonDocument = JsonDocument.Parse(responseContent);
            var aiRawJson = jsonDocument.RootElement
                .GetProperty("choices")[0]
                .GetProperty("message")
                .GetProperty("content")
                .GetString();

            var aiResult = JsonSerializer.Deserialize<JsonElement>(aiRawJson!);
            var recommendedTitle = aiResult.GetProperty("recommendedPlanTitle").GetString();
            var matchedPlan = plans.FirstOrDefault(p =>
                string.Equals(p.Title, recommendedTitle, StringComparison.OrdinalIgnoreCase));

            ViewBag.Result = new RecommendationResultDto
            {
                RecommendedPlanId = matchedPlan?.PricingPlanId,
                RecommendedPlanTitle = matchedPlan?.Title ?? recommendedTitle ?? "Belirlenemedi",
                RecommendedPlanPrice = matchedPlan?.Price ?? 0,
                ConfidenceLevel = aiResult.TryGetProperty("confidenceLevel", out var c) ? c.GetString() : "Orta",
                Reasoning = aiResult.TryGetProperty("reasoning", out var r) ? r.GetString() : "",
                Tips = aiResult.TryGetProperty("tips", out var t)
                    ? t.EnumerateArray().Select(x => x.GetString()!).ToList()
                    : new List<string>()
            };

            return View(request);
        }
    }
}