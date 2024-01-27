using Domain.Entities.Common;
using MailKit.Security;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using MimeKit;
using Service.Interfaces;
using Service.ViewModels;

namespace Service.Account
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _config;
        private readonly UserManager<AppUser> _userManager;

        public EmailService(IConfiguration config, UserManager<AppUser> userManager)
        {
            _config = config;
            _userManager = userManager;
        }
        public async Task SendEmailAsync(string email, string link)
        {
            var emailConfig = _config.GetSection("EmailConfiguration").Get<EmailConfiguration>();

            AppUser user = await _userManager.FindByEmailAsync(email);

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(emailConfig.Title, emailConfig.From));
            message.To.Add(new MailboxAddress(user.Name, user.Email));
            message.Subject = emailConfig.Subject;
            string emailbody = link;
            message.Body = new TextPart() { Text = emailbody };
            using var smtp = new MailKit.Net.Smtp.SmtpClient();
            smtp.Connect(emailConfig.SmtpServer, emailConfig.Port, SecureSocketOptions.StartTls);
            smtp.Authenticate(emailConfig.Username, emailConfig.Password);
            smtp.Send(message);
            smtp.Disconnect(true);
        }
    }
}
