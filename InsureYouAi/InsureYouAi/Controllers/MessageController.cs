using AutoMapper;
using InsureYouAi.Dtos.MessageDtos;
using InsureYouAi.Entities;
using InsureYouAi.Service.Concrete;
using InsureYouAi.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace InsureYouAi.Controllers
{
    [Authorize(Roles = "Admin")]
    public class MessageController : Controller
    {
        private readonly IMessageService messageService;
        private readonly EmailService emailService;
        private readonly IHttpClientFactory httpClientFactory;
        private readonly IConfiguration configuration;
        private readonly IMessageReplyService messageReplyService;
        private readonly IMapper _mapper;

        public MessageController(IMessageService messageService, IMapper mapper, IConfiguration configuration = null, IHttpClientFactory httpClientFactory = null, EmailService emailService = null, IMessageReplyService messageReplyService = null)
        {
            this.messageService = messageService;
            _mapper = mapper;
            this.configuration = configuration;
            this.httpClientFactory = httpClientFactory;
            this.emailService = emailService;
            this.messageReplyService = messageReplyService;
        }
        [HttpGet]
        public async Task<IActionResult> MessageList()
        {
            var message = await messageService.GetAllAsync();
            var mapper = _mapper.Map<List<ResultMessageDto>>(message);
            return View(mapper);
        }

        [HttpGet]
        public async Task<IActionResult> GetMessage(int id)
        {
            var message = await messageService.GetByIdAsync(id);
            if (message == null)
                return NotFound();

            var mapper = _mapper.Map<GetMessageByIdDto>(message);
            return View(mapper);
        }

        [HttpGet]
        public async Task<IActionResult> ChangeMessageStatusToTrue(int id) 
        {
            await messageService.GetMessageStatusChangeToRead(id);
            return RedirectToAction("MessageList");
        }

        [HttpGet]
        public async Task<IActionResult> ChangeMessageStatusToFalse(int id)
        {
            await messageService.GetMessageStatusChangeToUnRead(id);
            return RedirectToAction("MessageList");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteMessage(int id)
        {
            var message = await messageService.GetByIdAsync(id);
            if (message is null)
                return NotFound();
            await messageService.DeleteAsync(message);
            TempData["SuccessMessage"] = "Mesaj Başarıyla Silindi";
            return RedirectToAction("MessageList");

        }

        [HttpGet]
        public async Task<IActionResult> AutoReplyMessage(int id)
        {
            var message = await messageService.GetByIdAsync(id);
            if (message == null)
                return NotFound();

            var messageDto = _mapper.Map<ResultMessageDto>(message);

            var apiKey = configuration.GetSection("ApiKey:Claude").Value;
            var client = httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Add("x-api-key", apiKey);
            client.DefaultRequestHeaders.Add("anthropic-version", "2023-06-01");

            var requestData = new
            {
                model = "claude-sonnet-4-6",
                max_tokens = 1000,
                messages = new[]
                {
            new
            {
                role = "user",
                content = $"Sen bir sigorta şirketinde çalışan, kibar ve yardımsever bir müşteri temsilcisisin. " +
                   $"Aşağıdaki müşteri mesajına, kısa ve profesyonel bir e-posta yanıtı yaz. Sadece yanıt metnini yaz, başka açıklama ekleme:\n\n" +
                   $"Gönderen: {message.NameSurname}\n" +
                   $"Konu: {message.Subject}\n" +
                   $"Mesaj: {message.MessageDetail}"
            }
        },
            };

            var content = new StringContent(JsonSerializer.Serialize(requestData), Encoding.UTF8, "application/json");
            var response = await client.PostAsync("https://api.anthropic.com/v1/messages", content);
            var responseString = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                TempData["ErrorMessage"] = $"Claude API Hatası: {responseString}";
                return RedirectToAction("MessageList");
            }

            using var doc = JsonDocument.Parse(responseString);
            var replyText = doc.RootElement
                .GetProperty("content")[0]
                .GetProperty("text")
                .GetString();

            ViewBag.ReplyText = replyText;
            return View(messageDto);
        }

        [HttpPost]
        public async Task<IActionResult> AutoReplyMessage(int id, string replyText)
        {
            var message = await messageService.GetByIdAsync(id);
            if (message == null)
                return NotFound();

            try
            {
                await emailService.SendEmailAsync(
                    message.Email,
                    subject: $"Re: {message.Subject}",
                    body: replyText
                );
                //message.IsRead = true;
                //await messageService.UpdateAsync(message);
                TempData["SuccessMessage"] = $"{message.Email} adresine otomatik yanıt gönderildi.";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Mail gönderilemedi: {ex.Message}";
            }

            var reply = new MessageReply
            {
                MessageId = message.MessageId,
                ReplyDetail = replyText,
                ReplyDate = DateTime.Now,
                IsSentSuccessfully = true
            };
            await messageReplyService.AddAsync(reply);
            return RedirectToAction("MessageList");
        }


    }
}
