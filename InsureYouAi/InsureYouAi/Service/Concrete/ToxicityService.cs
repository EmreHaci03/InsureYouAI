using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using InsureYouAi.Dtos.ToxicityDtos;
using InsureYouAi.Service.Interfaces;

namespace InsureYouAi.Service.Concrete
{
    public class ToxicityService : IToxicityService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        // Çeviri modelinin yakalayamadığı Türkçe argo/kısaltmalar için yerel güvenlik ağı
        private static readonly string[] LocalBannedWords = new[]
        {
            "oç", "amk", "aq", "mk", "s.g", "gtç", "orospu", "piç", "yavşak",
            "siktir", "göt", "amına", "ananı", "sikeyim"
        };

        public ToxicityService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        public async Task<ToxicityResultDto> AnalyzeAsync(string turkishText)
        {
            // 1) Önce yerel kelime listesiyle hızlı kontrol (kısaltmalar için garanti)
            if (ContainsLocalProfanity(turkishText))
            {
                return new ToxicityResultDto
                {
                    OriginalText = turkishText,
                    TranslatedText = null,
                    IsToxic = true,
                    ToxicScore = 1.0
                };
            }

            // 2) Yerel listede yakalanmadıysa, AI destekli analiz dene
            try
            {
                var apiKey = _configuration["ApiKey:HuggingFace"];
                var client = _httpClientFactory.CreateClient();
                client.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", apiKey);

                var translatedText = await TranslateAsync(client, turkishText);
                var (isToxic, score) = await CheckToxicityAsync(client, translatedText);

                return new ToxicityResultDto
                {
                    OriginalText = turkishText,
                    TranslatedText = translatedText,
                    IsToxic = isToxic,
                    ToxicScore = score
                };
            }
            catch (Exception)
            {
                // API çökerse (503, timeout vb.) yorumun akışını durdurma;
                // güvenli tarafta kal: yerel liste zaten üstte kontrol edildi,
                // bu noktaya geldiyse muhtemelen zararsızdır.
                return new ToxicityResultDto
                {
                    OriginalText = turkishText,
                    TranslatedText = null,
                    IsToxic = false,
                    ToxicScore = 0
                };
            }
        }

        private bool ContainsLocalProfanity(string text)
        {
            var normalized = text.ToLower()
                .Replace(".", "")
                .Replace("0", "o")
                .Replace("1", "i")
                .Replace("3", "e");

            foreach (var word in LocalBannedWords)
            {
                if (Regex.IsMatch(normalized, $@"\b{Regex.Escape(word)}\b"))
                    return true;
            }

            return false;
        }

        private async Task<string> TranslateAsync(HttpClient client, string text)
        {
            var requestBody = new { inputs = text };
            var content = new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json");

            var response = await client.PostAsync(
                "https://router.huggingface.co/hf-inference/models/Helsinki-NLP/opus-mt-tr-en",
                content
            );

            var responseString = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
                throw new Exception($"Çeviri hatası: {responseString}");

            using var doc = JsonDocument.Parse(responseString);
            var translatedText = doc.RootElement[0].GetProperty("translation_text").GetString();

            return translatedText ?? text;
        }

        private async Task<(bool IsToxic, double Score)> CheckToxicityAsync(HttpClient client, string englishText)
        {
            var requestBody = new { inputs = englishText };
            var content = new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json");

            var response = await client.PostAsync(
                "https://router.huggingface.co/hf-inference/models/unitary/toxic-bert",
                content
            );

            var responseString = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
                throw new Exception($"Toksisite analizi hatası: {responseString}");

            using var doc = JsonDocument.Parse(responseString);
            var labels = doc.RootElement[0];
            double toxicScore = 0;

            foreach (var item in labels.EnumerateArray())
            {
                var label = item.GetProperty("label").GetString();
                var score = item.GetProperty("score").GetDouble();

                if (label != null && label.ToLower().Contains("toxic") && !label.ToLower().Contains("not"))
                {
                    toxicScore = score;
                }
            }

            bool isToxic = toxicScore > 0.5;

            return (isToxic, toxicScore);
        }
    }
}