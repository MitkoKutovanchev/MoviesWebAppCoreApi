using Data.Entity.Entities.LogService;
using LogService;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace NotificationService
{
    public class NotificationManager 
    {
        private ILog _logger = Logger.GetInstance;
       
        private async Task SendEmailAsync(string email, string subject, string message)
        {
            try
            {
                SmtpClient client = new SmtpClient("smtp.gmail.com", 587)
                {
                    UseDefaultCredentials = false,
                    //Credentials = new NetworkCredential("your-name@gmail.com", "your-pass")
                    Credentials = new NetworkCredential("mitkosrandomemail@gmail.com", "12354")
                };

                MailMessage mailMessage = new MailMessage
                {
                    From = new MailAddress("mitkosrandomemail@gmail.com")
                };
                mailMessage.To.Add(email);
                mailMessage.Body = message;
                mailMessage.IsBodyHtml = true;
                mailMessage.Subject = subject;
                client.EnableSsl = true;
                await client.SendMailAsync(mailMessage);
            }
            catch (Exception ex)
            {
                await _logger.LogCustomExceptionAsync(ex, null);
                throw new ApplicationException($"Unable to load : '{ex.Message}'.");
            }
        }
    }
}
