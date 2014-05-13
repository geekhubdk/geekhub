using System.Net.Mail;

namespace Geekhub.Modules.Core.Adapters
{
    public class SmtpEmailAdapter : IEmailAdapter
    {
        public void SendMail(string email, string subject, string html)
        {
            var from = new MailAddress("hello@geekhub.dk", "Geekhub");
            var to = new MailAddress(email);
            var mailMessage = new MailMessage(from, to) {
                IsBodyHtml = true,
                Body = html,
                BodyEncoding = System.Text.Encoding.UTF8,
                Subject = subject
            };
            var client = new System.Net.Mail.SmtpClient();
            client.Send(mailMessage);
        }
    }
}