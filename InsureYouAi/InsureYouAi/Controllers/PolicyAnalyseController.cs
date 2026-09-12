using InsureYouAi.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Authorization;
using UglyToad.PdfPig;
namespace InsureYouAi.Controllers
{
    [Authorize(Roles = "Admin")]
    public class PolicyAnalyseController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _httpClientFactory;

        private const long MaxFileSizeBytes = 10 * 1024 * 1024; // 10 MB

        public PolicyAnalyseController(IConfiguration configuration, IHttpClientFactory httpClientFactory)
        {
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public IActionResult PdfAnalyze()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> PdfAnalyze(IFormFile pdfFile)
        {
            if (pdfFile == null || pdfFile.Length == 0)
            {
                ViewBag.Error = "Lütfen bir PDF dosyası seçin.";
                return View();
            }

            if (!pdfFile.ContentType.Equals("application/pdf", StringComparison.OrdinalIgnoreCase) &&
                !pdfFile.FileName.EndsWith(".pdf", StringComparison.OrdinalIgnoreCase))
            {
                ViewBag.Error = "Lütfen geçerli bir PDF dosyası yükleyin.";
                return View();
            }

            if (pdfFile.Length > MaxFileSizeBytes)
            {
                ViewBag.Error = "Dosya boyutu 10 MB'ı geçemez.";
                return View();
            }

            string policyText;
            try
            {
                policyText = await ExtractTextFromPdf(pdfFile);
            }
            catch (Exception ex)
            {
                ViewBag.Error = "PDF okunurken hata oluştu: " + ex.Message;
                return View();
            }

            if (string.IsNullOrWhiteSpace(policyText))
            {
                ViewBag.Error = "PDF içinden metin çıkarılamadı. Dosya taranmış görsel olabilir.";
                return View();
            }

            try
            {
                var analysisMarkdown = await AnalyzePolicyWithClaude(policyText);
                ViewBag.AnalysisResult = SimpleMarkdownToHtml(analysisMarkdown);
            }
            catch (Exception ex)
            {
                ViewBag.Error = "AI analizi sırasında hata oluştu: " + ex.Message;
            }

            return View();
        }

        // -----------------------------------------------------------
        // PDF → Text Extraction
        // -----------------------------------------------------------
        private async Task<string> ExtractTextFromPdf(IFormFile pdfFile)
        {
            using var ms = new MemoryStream();
            await pdfFile.CopyToAsync(ms);
            ms.Position = 0;

            var sb = new StringBuilder();

            using (var document = PdfDocument.Open(ms))
            {
                foreach (var page in document.GetPages())
                {
                    sb.AppendLine(page.Text);
                    sb.AppendLine();
                }
            }

            return sb.ToString();
        }

        // -----------------------------------------------------------
        // Claude AI – Poliçe Analizi
        // -----------------------------------------------------------
        private async Task<string> AnalyzePolicyWithClaude(string policyText)
        {
            var apiKey = _configuration["ApiKey:Claude"];
            var model = "claude-sonnet-5";

            if (string.IsNullOrWhiteSpace(apiKey))
                throw new Exception("Antrophic Claude API key yapılandırılmamış (appsettings.json > ApiKey:Claude).");

            const int maxCharsForPrompt = 40000;
            var trimmedText = policyText.Length > maxCharsForPrompt
                ? policyText[..maxCharsForPrompt]
                : policyText;

            var prompt = $@"
Aşağıdaki metin bir sigorta poliçesine aittir.

Görevlerin:
1) Poliçeyi 10 maddede özetle.
2) Neleri kapsar? (Madde madde yaz)
3) Neleri kapsamaz? (Madde madde yaz)
4) Müşteri için kritik uyarıları **kalın** yap.
5) Yanıtı markdown formatında üret.

--- POLİÇE METNİ ---
{trimmedText}
--- SON ---
";

            var body = new
            {
                model = model,
                max_tokens = 1500,
                messages = new[]
                {
                    new
                    {
                        role = "user",
                        content = new object[]
                        {
                            new { type = "text", text = prompt }
                        }
                    }
                }
            };

            var json = JsonSerializer.Serialize(body);

            var http = _httpClientFactory.CreateClient("ClaudeClient");
            var request = new HttpRequestMessage(HttpMethod.Post, "https://api.anthropic.com/v1/messages")
            {
                Content = new StringContent(json, Encoding.UTF8, "application/json")
            };
            request.Headers.Add("x-api-key", apiKey);
            request.Headers.Add("anthropic-version", "2023-06-01");

            var response = await http.SendAsync(request);
            var responseText = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
                throw new Exception($"Claude API hatası ({response.StatusCode}): {responseText}");

            using var doc = JsonDocument.Parse(responseText);
            var contentArray = doc.RootElement.GetProperty("content");
            var resultSb = new StringBuilder();

            foreach (var item in contentArray.EnumerateArray())
            {
                if (item.GetProperty("type").GetString() == "text")
                {
                    resultSb.AppendLine(item.GetProperty("text").GetString());
                }
            }

            return resultSb.ToString();
        }

        // -----------------------------------------------------------
        // Basit Markdown → HTML çevirici (paket gerektirmez)
        // -----------------------------------------------------------
        private string SimpleMarkdownToHtml(string markdown)
        {
            if (string.IsNullOrEmpty(markdown)) return "";

            // Önce HTML injection'a karşı encode et
            var html = System.Net.WebUtility.HtmlEncode(markdown);

            // Başlıklar
            html = Regex.Replace(html, @"^### (.+)$", "<h3>$1</h3>", RegexOptions.Multiline);
            html = Regex.Replace(html, @"^## (.+)$", "<h2>$1</h2>", RegexOptions.Multiline);
            html = Regex.Replace(html, @"^# (.+)$", "<h1>$1</h1>", RegexOptions.Multiline);

            // Kalın
            html = Regex.Replace(html, @"\*\*(.+?)\*\*", "<strong>$1</strong>");

            // Liste öğeleri (- veya * ile başlayanlar)
            html = Regex.Replace(html, @"^[\-\*] (.+)$", "<li>$1</li>", RegexOptions.Multiline);
            html = Regex.Replace(html, @"(<li>.*?</li>\n?)+", m => "<ul>" + m.Value + "</ul>", RegexOptions.Singleline);

            // Numaralı liste öğeleri (1. 2. gibi)
            html = Regex.Replace(html, @"^\d+\. (.+)$", "<li>$1</li>", RegexOptions.Multiline);

            // Satır sonlarını paragraf/br'a çevir
            var paragraphs = html.Split(new[] { "\n\n" }, StringSplitOptions.RemoveEmptyEntries);
            var sb = new StringBuilder();
            foreach (var p in paragraphs)
            {
                var trimmed = p.Trim();
                if (trimmed.StartsWith("<h") || trimmed.StartsWith("<ul") || trimmed.StartsWith("<li"))
                {
                    sb.AppendLine(trimmed);
                }
                else if (!string.IsNullOrWhiteSpace(trimmed))
                {
                    sb.AppendLine($"<p>{trimmed.Replace("\n", "<br/>")}</p>");
                }
            }

            return sb.ToString();
        }
    }
}