using System;
using System.Collections.Generic;

namespace Geekhub.App.Modules.Alerts.Models
{
    public class NewsletterLog
    {
        public string Subject { get; set; }
        public IEnumerable<string> Recipients { get; set; }
        public string NewsletterType { get; set; }
        public string Parameters { get; set; }
        public DateTime DateSent { get; set; }
    }
}