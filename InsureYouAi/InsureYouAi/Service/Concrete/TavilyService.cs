using Azure;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace InsureYouAi.Service.Concrete
{
    public class TavilyService
    {
        private readonly IHttpClientFactory httpClientFactory;
        private readonly IConfiguration configuration;

        public TavilyService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            this.httpClientFactory = httpClientFactory;
            this.configuration = configuration;
        }

       public async Task<string> AskWithSearchAsync(string userQuery, string searchContext)
       {
            var openApiKey = configuration.GetSection("ApiKey:OpenAi").Value;
            var client = httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", openApiKey);

            var prompt = "Aşağıdaki güncel web arama bilgisini kullanarak kullanıcının sorusunu yanıtla:\n\n" +
                      (string.IsNullOrEmpty(searchContext) ? "" : $"Güncel Bilgi: {searchContext}\n\n") +
                      $"Soru: {userQuery}";

            var requestData = new
            {
                model = "gpt-3.5-turbo",
                messages = new[]
                {
                    new { role = "system", content = "Sen yardımsever bir asistansın." },
                    new { role = "user", content = prompt }
                },
                temperature = 0.7
            };

            var content = new StringContent(JsonSerializer.Serialize(requestData), Encoding.UTF8, "application/json");
            var response = await client.PostAsync("https://api.openai.com/v1/chat/completions", content);
            var responseString = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"OpenAI API Hatası: {responseString}");
            }
            using var doc = JsonDocument.Parse(responseString);
            var answerText = doc.RootElement
                .GetProperty("choices")[0]
                .GetProperty("message")
                .GetProperty("content")
                .GetString();

            return answerText ?? "";

        }
    }
}
