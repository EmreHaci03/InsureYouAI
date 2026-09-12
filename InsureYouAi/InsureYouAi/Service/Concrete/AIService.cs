using Azure;
using System.Text;
using System.Text.Json;

namespace InsureYouAi.Service.Concrete
{
    public class AIService
    {
        private readonly IConfiguration configuration;
        private readonly IHttpClientFactory httpClientFactory;
        public AIService(IConfiguration configuration, IHttpClientFactory httpClientFactory)
        {
            this.configuration = configuration;
            this.httpClientFactory = httpClientFactory;
        }

        public async Task<string> PredictCategoryAsync(string messageText)
        {
            var apiKey = configuration.GetSection("ApiKey:Gemini").Value;
            var client = httpClientFactory.CreateClient();

            var prompt = "Aşağıdaki müşteri mesajını oku ve sadece şu kategorilerden birini seç: " +
                       "Kasko, Trafik, Sağlık, Hayat, Konut, İşyeri, Genel. " +
                       "Başka hiçbir açıklama yazma, sadece kategori adını yaz.\n\n" +
                       $"Mesaj: {messageText}";

            var requestBody = new
            {
                contents = new[]
                {
                    new
                    {
                        parts = new[]
                        {
                            new{text=prompt}
                        }
                    }
                }
            };

            var content = new StringContent(JsonSerializer.Serialize(requestBody),Encoding.UTF8,"application/json");
            var url = $"https://generativelanguage.googleapis.com/v1beta/models/gemini-3.6-flash:generateContent?key={apiKey}";
            var responseMessage = await client.PostAsync(url, content);
            var responseString = await responseMessage.Content.ReadAsStringAsync();

            if (!responseMessage.IsSuccessStatusCode)
            {
                throw new Exception($"Gemini API Hatası: {responseString}");
            }

            using var doc = JsonDocument.Parse(responseString);
            var category = doc.RootElement
                 .GetProperty("candidates")[0]
                 .GetProperty("content")
                 .GetProperty("parts")[0]
                 .GetProperty("text")
                 .GetString();

            return category?.Trim() ?? "Genel"; //Trim Baştaki Ve Sondaki Boşluk Karakterlerini Temizler.


        }

        public async Task<string> PredictPriorityAsync(string messageText)
        {
            var apiKey = configuration.GetSection("ApiKey:Gemini").Value;
            var client = httpClientFactory.CreateClient();

            var prompt = "Aşağıdaki müşteri mesajının aciliyetini değerlendir ve sadece şunlardan birini yaz: " +
                  "Düşük, Orta, Yüksek. Başka hiçbir şey yazma.\n\n" +
                  $"Mesaj: {messageText}";

            var requestBody = new
            {
                contents = new[]
                {
                    new
                    {
                        parts = new[]
                        {
                            new { text = prompt }
                        }
                    }
                }
            };

            var content=new StringContent(JsonSerializer.Serialize(requestBody),Encoding.UTF8,"application/json");
            var url = $"https://generativelanguage.googleapis.com/v1beta/models/gemini-3.6-flash:generateContent?key={apiKey}";
            var responseMessage = await client.PostAsync(url, content);
            var responseString = await responseMessage.Content.ReadAsStringAsync();

            if (!responseMessage.IsSuccessStatusCode)
            {
                throw new Exception($"Gemini API Hatası: {responseString}");
            }

            using var doc = JsonDocument.Parse(responseString);
            var priority = doc.RootElement
                 .GetProperty("candidates")[0]
                 .GetProperty("content")
                 .GetProperty("parts")[0]
                 .GetProperty("text")
                 .GetString();

            return priority?.Trim() ?? "Genel"; //Trim Baştaki Ve Sondaki Boşluk Karakterlerini Temizler.



        }
    }
}
