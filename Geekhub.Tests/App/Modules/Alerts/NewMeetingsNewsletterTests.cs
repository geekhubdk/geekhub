using System;
using System.Collections.ObjectModel;
using System.Linq;
using Geekhub.App.Core.Data;
using Geekhub.App.Modules.Alerts.CommandHandlers;
using Geekhub.App.Modules.Alerts.Models;
using Geekhub.App.Modules.Meetings.Models;
using Xunit;

namespace Geekhub.Tests.App.Modules.Alerts
{
    public class NewMeetingsNewsletterTests
    {
        private readonly Meeting _meeting1 = new Meeting() {
            Id = 1,
            Title = "title1",
            City = new City() { Name = "city1" },
            CreatedAt = DateTime.Now.AddHours(-3),
            StartsAt = DateTime.Now.AddDays(15),
            Description = "description1",
            Organizers = new Collection<Organizer>() { new Organizer() { Name = "ONUG" }, new Organizer() { Name = "ANUG" } }
        };

        private readonly Meeting _meeting2 = new Meeting() {
            Id = 2,
            Title = "title2",
            City = new City() {Name = "city2"},
            CreatedAt = DateTime.Now.AddHours(-3),
            StartsAt = DateTime.Now.AddDays(14),
            Description = "description2",
            Organizers = new Collection<Organizer>() {new Organizer() {Name = "ONUG"}, new Organizer() {Name = "ANUG"}}
        };

        [Fact]
        public void CheckThatItWillGenerateAndSendCurrectly()
        {
            var context = new DataContext(null);
            DataContext.Current = context;
            context.Meetings.Add(_meeting1);
            context.Meetings.Add(_meeting2);
                        
            context.NewsletterSubscriptions.Add(new NewsletterSubscription() {Email ="deldy@deldysoft.dk", SubscribedToNewMeetingUpdates = true });
            context.NewsletterSubscriptions.Add(new NewsletterSubscription() {Email ="jesper@deldysoft.dk", SubscribedToNewMeetingUpdates = true });

            var emailAdapter = new EmailAdapterFake();
            
            var handler = new SendNewMeetingsNewsletterCommandHandler(emailAdapter);
            handler.Execute();

            Assert.Equal(2, emailAdapter.SentEmails.Count);

            var firstEmail = emailAdapter.SentEmails.First();
            var lastEmail = emailAdapter.SentEmails.Last();

            Assert.Equal(firstEmail.Email, "deldy@deldysoft.dk");
            Assert.Equal(lastEmail.Email, "jesper@deldysoft.dk");
            Assert.Contains("title1", firstEmail.Html);
            Assert.Contains("title2", firstEmail.Html);
            Assert.Contains("city1", firstEmail.Html);
            Assert.Contains("city2", firstEmail.Html);
            Assert.Contains("ONUG", firstEmail.Html);
            Assert.Contains("http://geekhub.dk/meetings/1/?utm_source=email", firstEmail.Html);
            Assert.Contains("http://geekhub.dk/meetings/2/?utm_source=email", firstEmail.Html);

            // The secound meeting should be the first, as its start date is first
            Assert.True(firstEmail.Html.IndexOf("title1", System.StringComparison.Ordinal) > firstEmail.Html.IndexOf("title2", System.StringComparison.Ordinal));

            Assert.Equal(1, context.NewsletterLogs.Count);
        }

        [Fact]
        public void DoesNotSendTheSameMeetingsAgain()
        {
            var context = new DataContext(null);
            DataContext.Current = context;
            context.Meetings.Add(_meeting1);
            context.Meetings.Add(_meeting2);

            context.NewsletterSubscriptions.Add(new NewsletterSubscription() { Email = "deldy@deldysoft.dk", SubscribedToNewMeetingUpdates = true });
            context.NewsletterSubscriptions.Add(new NewsletterSubscription() { Email = "jesper@deldysoft.dk", SubscribedToNewMeetingUpdates = true });

            context.NewsletterLogs.Add(new NewsletterLog() {
                DateSent = DateTime.Now,
                NewsletterType = typeof(NewMeetingsNewsletter).Name
                });

            var emailAdapter = new EmailAdapterFake();
            
            var handler = new SendNewMeetingsNewsletterCommandHandler();
            handler.Execute();

            Assert.Equal(0, emailAdapter.SentEmails.Count);
        }

    }
}
