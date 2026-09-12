using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using System.Text.Json;

namespace InsureYouAi.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ImageAIController : Controller
    {
        private readonly IHttpClientFactory httpClientFactory;
        private readonly IConfiguration configuration;

        public ImageAIController(
            IHttpClientFactory httpClientFactory,
            IConfiguration configuration)
        {
            this.httpClientFactory = httpClientFactory;
            this.configuration = configuration;
        }

        [HttpGet]
        public IActionResult CreateImageWithOpenAI()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateImageWithOpenAI(string prompt)
        {
            try
            {
                var apiKey = configuration["ApiKey:OpenAi"];

                if (string.IsNullOrWhiteSpace(apiKey))
                {
                    TempData["ErrorMessage"] = "OpenAI API Key bulunamadı.";
                    return View((string)null);
                }

                if (string.IsNullOrWhiteSpace(prompt))
                {
                    TempData["ErrorMessage"] = "Prompt boş olamaz.";
                    return View((string)null);
                }

                var client = httpClientFactory.CreateClient();

                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", apiKey);

                var requestData = new
                {
                    model = "gpt-image-1",
                    prompt = prompt,
                    n = 1,
                    size = "1024x1024"
                };

                var jsonData = JsonSerializer.Serialize(requestData);

                // TEST AMAÇLI
                Console.WriteLine("OPENAI REQUEST:");
                Console.WriteLine(jsonData);

                var content = new StringContent(
                    jsonData,
                    Encoding.UTF8,
                    "application/json"
                );

                var responseMessage = await client.PostAsync(
                    "https://api.openai.com/v1/images/generations",
                    content
                );

                var responseString =
                    await responseMessage.Content.ReadAsStringAsync();

                // HATA
                if (!responseMessage.IsSuccessStatusCode)
                {
                    Console.WriteLine("OPENAI ERROR:");
                    Console.WriteLine(responseString);

                    TempData["ErrorMessage"] =
                        $"OpenAI Hatası: {responseString}";

                    return View((string)null);
                }

                Console.WriteLine("OPENAI RESPONSE:");
                Console.WriteLine(responseString);

                using var doc =
                    JsonDocument.Parse(responseString);

                var data = doc.RootElement
                    .GetProperty("data")[0];

                // Base64
                if (data.TryGetProperty(
                    "b64_json",
                    out var b64Element))
                {
                    var base64 =
                        b64Element.GetString();

                    return View(base64);
                }

                // URL
                if (data.TryGetProperty(
                    "url",
                    out var urlElement))
                {
                    var imageUrl =
                        urlElement.GetString();

                    return View(imageUrl);
                }

                TempData["ErrorMessage"] =
                    "OpenAI başarılı cevap verdi fakat görsel verisi bulunamadı.";

                return View((string)null);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] =
                    $"Beklenmeyen hata: {ex.Message}";

                return View((string)null);
            }
        }
    }
}