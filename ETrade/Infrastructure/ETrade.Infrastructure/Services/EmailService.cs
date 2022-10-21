using System.Net;
using System.Net.Mail;
using ETrade.Application.Model;
using ETrade.Application.Services;
using Microsoft.Extensions.Options;

namespace ETrade.Infrastructure.Services;
public class EmailService:IEmailService
    {
        private readonly MailSettings _mailSettings;
        
        public EmailService(IOptions<MailSettings> options)
        {
            _mailSettings = options.Value;
        }
        public bool SendEmail(MailRequest mailRequest)
        {
            
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(_mailSettings.Mail,mailRequest.DisplayName, System.Text.Encoding.UTF8);
            mailMessage.To.Add(new MailAddress(mailRequest.ToMail));

            mailMessage.Subject = mailRequest.MailSubject;
            mailMessage.IsBodyHtml = mailRequest.IsBodyHtml;
            mailMessage.Body = $"<a target=\"_blank\" href=\"{mailRequest.ConfirmationLink}\">{mailRequest.MailLinkTitle}</a>";

            SmtpClient client = new SmtpClient();
            client.Credentials = new NetworkCredential(_mailSettings.Mail, _mailSettings.Password);
            client.Port = _mailSettings.Port;
            client.Host = _mailSettings.Host;
            client.EnableSsl = _mailSettings.UseSsl;
 
            try
            {
                client.Send(mailMessage);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }
    }