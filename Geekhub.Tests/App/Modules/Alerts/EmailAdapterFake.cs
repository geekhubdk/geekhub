using System.Collections.Generic;
using Geekhub.App.Modules.Alerts.Adapters;
using Geekhub.App.Modules.Core.Adapters;

namespace Geekhub.Tests.App.Modules.Alerts
{
    public class EmailAdapterFake : IEmailAdapter
    {
        public List<SentEmail> SentEmails { get; set; }

        public EmailAdapterFake()
        {
            SentEmails = new List<SentEmail>();
        }

        public void SendMail(string email, string subject, string html)
        {
            SentEmails.Add(new SentEmail() { Email = email, Subject = subject, Html = html });
        }

        public class SentEmail
        {
            public string Email { get; set; }
            public string Subject { get; set; }
            public string Html { get; set; }
        }
    }
}