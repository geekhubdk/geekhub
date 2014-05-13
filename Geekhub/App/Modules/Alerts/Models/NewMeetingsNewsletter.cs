using System;
using System.Collections.Generic;
using System.Linq;
using Geekhub.App.Modules.Alerts.Adapters;
using Geekhub.App.Modules.Core.Adapters;
using Geekhub.App.Modules.Core.Data;
using Geekhub.App.Modules.Meetings.Models;
using RazorEngine;

namespace Geekhub.App.Modules.Alerts.Models
{
    public class NewMeetingsNewsletter
    {
        private readonly IEmailAdapter _emailAdapter;
        public IEnumerable<Meeting> Meetings { get; private set; }

        public NewMeetingsNewsletter(IEnumerable<Meeting> meetings, IEmailAdapter emailAdapter)
        {
            _emailAdapter = emailAdapter;
            Meetings = meetings;
        }

        public void SendToSubscribers(IEnumerable<NewsletterSubscription> subscriptions)
        {
            var subscribers = subscriptions.Select(x => x.Email).ToArray();

            var subject = GenerateSubject();
            
            var meetingIds = string.Join(",", Meetings.Select(x => x.Id));
            var parameters = "ids=" + meetingIds;
            
            LogNewsletterDispatch(subject, subscribers, parameters);

            foreach (var email in subscribers) {
                var html = GenerateBodyFor(email);
                _emailAdapter.SendMail(email, subject, html);
            }
        }

        private void LogNewsletterDispatch(string subject, IEnumerable<string> subscribers, string parameters)
        {
            var log = new NewsletterLog() {
                Subject = subject,
                DateSent = DateTime.Now,
                NewsletterType = GetType().Name,
                Recipients = subscribers,
                Parameters = parameters
            };
            DataContext.Current.NewsletterLogs.Add(log);
        }

        private string GenerateBodyFor(string email)
        {
            var html = RenderView(email);
            return html;
        }

        private string RenderView(string email)
        {
            string template = @"
@foreach(var m in Model.Meetings) {
  <h1><a href=""@m.Url"">@m.Title</a></h1>
  <strong>@m.City, @m.StartsAt - @m.Organizers</strong><br/>
  <p>@Raw(m.Description)</p>
}
<br/>
<hr/>
<br/>
Træt af mailen? <a href=""http://www.geekhub.dk/newsletter/unsubscribe"">Afmeld dig her</a>.";
            var viewModel = Meetings.Select(x=>new MeetingViewModel(x));
            return Razor.Parse<ViewModel>(template, new ViewModel { Meetings = viewModel });
        }

        protected string GenerateSubject()
        {
            return "Nye events på Geekhub.dk";
        }

        public class ViewModel
        {
            public IEnumerable<MeetingViewModel> Meetings { get; set; }
        }

        public class MeetingViewModel
        {
            public string Url { get; set; }
            public string Title { get; set; }
            public string City { get; set; }
            public string StartsAt { get; set; }
            public string Organizers { get; set; }
            public string Description { get; set; }

            public MeetingViewModel(Meeting meeting)
            {
                Url = "http://geekhub.dk/meetings/" + meeting.Id + "/?utm_source=email";
                Title = meeting.Title;
                City = meeting.City.Name;
                StartsAt = meeting.StartsAt.ToString("dd. MMM HH:mm");
                Organizers = string.Join(" & ", meeting.Organizers.Select(x => x.Name));
                Description = meeting.Description.Replace(Environment.NewLine, "<br/>");
            }

        }

    }
}