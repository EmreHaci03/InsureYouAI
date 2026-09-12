using Microsoft.AspNetCore.SignalR;
using System.Collections.Concurrent;
using System.Text;
using System.Text.Json;

namespace InsureYouAi.Models
{
    public class ChatHub : Hub
    {
        private readonly IHttpClientFactory httpClientFactory;
        private readonly IConfiguration configuration;

        public ChatHub(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            this.httpClientFactory = httpClientFactory;
            this.configuration = configuration;
        }

       
        private static readonly ConcurrentDictionary<string, List<Dictionary<string, string>>> _history = new();

        public override Task OnConnectedAsync()
        {
            _history[Context.ConnectionId] =
                new List<Dictionary<string, string>>
                {
                    new()
                    {
                        ["role"] = "system",
                        ["content"] = "You are a helpful assistant. Keep answers concise.",
                    }
                };

            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            _history.TryRemove(Context.ConnectionId, out _);
            return base.OnDisconnectedAsync(exception);
        }

        public async Task SendMessage(string usermessage)
        {
            await Clients.Caller.SendAsync("ReceiveUserEcho", usermessage);

            if (!_history.TryGetValue(Context.ConnectionId, out var history))
            {
                // Bağlantı geçmişi bulunamadıysa (örn. OnConnectedAsync tetiklenmediyse) yeni oluştur
                history = new List<Dictionary<string, string>>
                {
                    new() { ["role"] = "system", ["content"] = "You are a helpful assistant. Keep answers concise." }
                };
                _history[Context.ConnectionId] = history;
            }

            history.Add(new() { ["role"] = "user", ["content"] = usermessage });

            try
            {
                await StreamOpenAI(history, Context.ConnectionAborted);
            }
            catch (OperationCanceledException)
            {
                // Kullanıcı bağlantıyı kestiyse sessizce çık
            }
            catch (Exception ex)
            {
                await Clients.Caller.SendAsync("ReceiveError", ex.Message);
            }
        }

        private async Task StreamOpenAI(List<Dictionary<string, string>> history, CancellationToken cancellationToken)
        {
            var apiKey = configuration["ApiKey:OpenAI"];
            var client = httpClientFactory.CreateClient();

  
            if (!client.DefaultRequestHeaders.Contains("Authorization"))
            {
                client.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", apiKey);
            }

            var payload = new
            {
                model = "gpt-4o-mini", 
                messages = history,
                stream = true,
                temperature = 0.2
            };

            using var req = new HttpRequestMessage(HttpMethod.Post, "https://api.openai.com/v1/chat/completions");
            req.Content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");

            using var response = await client.SendAsync(req, HttpCompletionOption.ResponseHeadersRead, cancellationToken);
            response.EnsureSuccessStatusCode();

            await using var stream = await response.Content.ReadAsStreamAsync(cancellationToken);
            using var reader = new StreamReader(stream);

            var fullResponse = new StringBuilder();

            while (!reader.EndOfStream && !cancellationToken.IsCancellationRequested)
            {
                var line = await reader.ReadLineAsync();

                if (string.IsNullOrWhiteSpace(line))
                    continue;

                if (!line.StartsWith("data:"))
                    continue;

                var data = line["data:".Length..].Trim();

                if (data == "[DONE]")
                    break;

                try
                {
                    using var doc = JsonDocument.Parse(data);
                    var delta = doc.RootElement
                        .GetProperty("choices")[0]
                        .GetProperty("delta");

                    if (delta.TryGetProperty("content", out var contentProp))
                    {
                        var chunk = contentProp.GetString();
                        if (!string.IsNullOrEmpty(chunk))
                        {
                            fullResponse.Append(chunk);
                            await Clients.Caller.SendAsync("ReceiveChunk", chunk, cancellationToken: cancellationToken);
                        }
                    }
                }
                catch (JsonException)
                {
                    
                }
            }

            
            history.Add(new() { ["role"] = "assistant", ["content"] = fullResponse.ToString() });

            await Clients.Caller.SendAsync("StreamCompleted", cancellationToken: cancellationToken);
        }
    }
}