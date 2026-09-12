using MailKit.Net.Smtp;
using MimeKit;

namespace InsureYouAi.Service.Concrete
{
    public class EmailService
    {
        private readonly IHttpClientFactory httpClientFactory;
        private readonly IConfiguration configuration;

        public EmailService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            this.httpClientFactory = httpClientFactory;
            this.configuration = configuration;
        }


        public async Task SendEmailAsync(string toEmail,string subject,string body)
        {
            var mailSettings = configuration.GetSection("MailSettings");

            var email = new MimeMessage();
            email.From.Add(new MailboxAddress(mailSettings["SenderName"], mailSettings["SenderEmail"]));
            email.To.Add(MailboxAddress.Parse(toEmail));
            email.Subject = subject;
            email.Body = new TextPart("plain") { Text = body };


            using var smtpClient = new SmtpClient();
            await smtpClient.ConnectAsync(mailSettings["SmtpServer"], int.Parse(mailSettings["SmtpPort"]), MailKit.Security.SecureSocketOptions.StartTls);
            await smtpClient.AuthenticateAsync(mailSettings["SenderEmail"], mailSettings["SenderPassword"]);
            await smtpClient.SendAsync(email);
            await smtpClient.DisconnectAsync(true);

        }
    }
}
