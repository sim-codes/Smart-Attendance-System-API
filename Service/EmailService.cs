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
        public void SendEmail(string to, string subject, string body)
        {
            // Set up SMPT client
            
            // Create email
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress("robert68@ethereal.email");
            mailMessage.To.Add(to);
            mailMessage.Subject = subject;
            mailMessage.IsBodyHtml = true;
            mailMessage.Body = body;

            client.Send(mailMessage);
        }
    }
}
