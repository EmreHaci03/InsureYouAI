using InsureYouAi.Service.Concrete;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using System.Text.Json;

namespace InsureYouAi.Controllers
{
    [Authorize(Roles = "Admin")]
    public class TavilyController : Controller
    {
        private readonly IHttpClientFactory httpClientFactory;
        private readonly TavilyService tavilyService;
        private readonly IConfiguration configuration;
        public TavilyController(IHttpClientFactory httpClientFactory, IConfiguration configuration, TavilyService tavilyService)
        {
            this.httpClientFactory = httpClientFactory;
            this.configuration = configuration;
            this.tavilyService = tavilyService;
        }

        [HttpGet]
        public IActionResult SearchAndAnswer()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> SearchAndAnswer(string userQuery)
        {
            if (string.IsNullOrWhiteSpace(userQuery))
            {
                TempData["ErrorMessage"] = "Lütfen bir soru girin.";
                return View();
            }


            var tavilyApiKey = configuration.GetSection("ApiKey:TavilyAi").Value;
            var tavilyClient = httpClientFactory.CreateClient();
            tavilyClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tavilyApiKey);

            var tavilyRequest = new
            {
                query=userQuery,
                search_depth = "basic",
                max_results = 5,
                include_answer = true
            };

            var tavilyContent = new StringContent(JsonSerializer.Serialize(tavilyRequest), Encoding.UTF8, "application/json");
            var tavilyResponse = await tavilyClient.PostAsync("https://api.tavily.com/search", tavilyContent);
            var tavilyResponseString = await tavilyResponse.Content.ReadAsStringAsync();

            string searchContext = "";
            if (tavilyResponse.IsSuccessStatusCode)
            {
                using var TavilyDoc = JsonDocument.Parse(tavilyResponseString);

                if (TavilyDoc.RootElement.TryGetProperty("answer", out var answerProp))
                {
                    searchContext = answerProp.GetString();
                }

            }
            try
            {
                var answer = await tavilyService.AskWithSearchAsync(userQuery, searchContext);
                ViewBag.Query = userQuery;
                ViewBag.Answer = answer;
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Cevap oluşturulurken hata oluştu: {ex.Message}";
            }

            return View();


        }
    }
}
