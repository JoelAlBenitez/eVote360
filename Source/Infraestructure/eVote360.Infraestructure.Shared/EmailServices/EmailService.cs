using eVote360.Core.Application.Contracts.EmailService;
using eVote360.Core.Application.DTOs.Message;
using eVote360.Core.Domain.Settings.EmailService;
using MailKit.Security;
using Microsoft.Extensions.Logging;
using MimeKit;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;

namespace eVote360.Infraestructure.Shared.EmailServices
{
    public class EmailService : IEmailService
    {

        private readonly EmailSettings _emailSettings;

        private readonly ILogger<EmailService> _logger;

        public EmailService(IOptions<EmailSettings>  emailSettings, ILogger<EmailService> logger)
        {
            _emailSettings = emailSettings.Value;
            _logger = logger;
        }

        public async Task<bool> SendEmailAsync(MessageDto messageDto)
        {
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress(_emailSettings.SenderName, _emailSettings.SenderName));
            emailMessage.To.Add(new MailboxAddress("", messageDto.ToEmail));

            emailMessage.Subject = messageDto.Subject;

            var bodyBuilder = new BodyBuilder { HtmlBody = messageDto.Body };
            emailMessage.Body = bodyBuilder.ToMessageBody();
            using var client = new SmtpClient();  
            
             try
            {
                var secure = _emailSettings.EnableSsl ? SecureSocketOptions.StartTls : SecureSocketOptions.None;
                await client.ConnectAsync(_emailSettings.SmtpServer, _emailSettings.Port, secure);
                await client.AuthenticateAsync(_emailSettings.Username, _emailSettings.Password);
                await client.SendAsync(emailMessage);
                await client.DisconnectAsync(true);
                return true;

            }
            catch (Exception ex) {

                _logger.LogError(ex, "Error crítico al intentar enviar correo a {ToEmail}", messageDto.ToEmail);
                return false;
            }
           
        }
    }
}
