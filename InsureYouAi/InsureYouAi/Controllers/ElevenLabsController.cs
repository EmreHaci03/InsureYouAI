using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace InsureYouAi.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ElevenLabsController : Controller
    {
        private readonly IConfiguration configuration;
        private readonly IHttpClientFactory httpClientFactory;

        public ElevenLabsController(IConfiguration configuration, IHttpClientFactory httpClientFactory)
        {
            this.configuration = configuration;
            this.httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public IActionResult SpeakInsuranceAnswer()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SpeakInsuranceAnswer(string speechText)
        {
            if (string.IsNullOrWhiteSpace(speechText))
            {
                TempData["ErrorMessage"] = "Seslendirilecek metin bulunamadı.";
                ViewBag.SpeechText = speechText;
                return View();
            }

            var apiKey = configuration["ElevenLabs:ApiKey"];
            var VoiceKey = configuration["ElevenLabs:VoiceId"];

            if (string.IsNullOrWhiteSpace(apiKey) || string.IsNullOrWhiteSpace(VoiceKey))
            {
                TempData["ErrorMessage"] = "ElevenLabs yapılandırması eksik (appsettings.json > ElevenLabs).";
                ViewBag.SpeechText = speechText;
                return View();
            }

            var client = httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Add("xi-api-key", apiKey);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("audio/mpeg"));

            var requestBody = new
            {
                text = speechText,
                model_id = "eleven_multilingual_v2",
                voice_settings = new
                {
                    stability = 0.5,
                    similarity_boost = 0.75
                }
            };

            var url = $"https://api.elevenlabs.io/v1/text-to-speech/{VoiceKey}/stream";

            var content = new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync(url, content);

            if (!responseMessage.IsSuccessStatusCode)
            {
                var error = await responseMessage.Content.ReadAsStringAsync();
                TempData["ErrorMessage"] = $"ElevenLabs API hatası ({responseMessage.StatusCode}): {error}";
                ViewBag.SpeechText = speechText;
                return View();
            }

            var audioBytes = await responseMessage.Content.ReadAsByteArrayAsync();

            var fileName = $"eleven_{Guid.NewGuid()}.mp3";
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "voices", fileName);

            Directory.CreateDirectory(Path.GetDirectoryName(filePath)!);
            await System.IO.File.WriteAllBytesAsync(filePath, audioBytes);

            ViewBag.AudioUrl = "/voices/" + fileName;
            ViewBag.SpeechText = speechText;

            return View();
        }
    }
}