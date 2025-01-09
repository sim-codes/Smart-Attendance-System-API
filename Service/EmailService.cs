using Microsoft.Extensions.Configuration;
using Service.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class EmailService : IEmailService
    {
        private IConfiguration _configuration;

        public EmailService(IConfiguration configuration) => _configuration = configuration;
        public void SendEmail(string to, string subject, string body)
        {
            var emailConfig = _configuration.GetSection("MailSettings");

            // Set up SMPT client
            SmtpClient client = new SmtpClient(emailConfig["Host"], 587);
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential(emailConfig["Mail"], emailConfig["Password"]);

            // Create email
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(emailConfig["Mail"]);
            mailMessage.To.Add(to);
            mailMessage.Subject = subject;
            mailMessage.IsBodyHtml = true;
            mailMessage.Body = body;

            client.Send(mailMessage);
        }
    }
}
