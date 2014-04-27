using System;
using System.Collections.Generic;
using System.Linq;

using Geekhub.App.Core.Adapters;

using Geekhub.App.Core.Data;
using Geekhub.App.Modules.Alerts.Adapters;
using Geekhub.App.Modules.Alerts.Models;
using Geekhub.App.Modules.Meetings.Models;
using Geekhub.App.Modules.Meetings.Queries;
using Geekhub.App.Modules.Alerts.Config;

namespace Geekhub.App.Modules.Alerts.CommandHandlers
{
    public class SendNewMeetingsNewsletterCommandHandler
    {
        private readonly IEmailAdapter _emailAdapter;

        public SendNewMeetingsNewsletterCommandHandler() : this(AlertsContainerConfig.CreateEmailAdapter())
        {

        }

        public SendNewMeetingsNewsletterCommandHandler(IEmailAdapter emailAdapter)
        {
            _emailAdapter = emailAdapter;
        }

        public void Execute()
        {
            // Find a list of meetings that is not yet alerted via newsletters
            var meetings = FindNewMeetingsToCreateNewsletterFrom();

            if(!meetings.Any())
            {
                return;
            }

            // Make a combined newsletter with the meetings
            var newsletter = GenerateNewsletterFromMeetings(meetings);
            
            // Get the subscriptions that wants to recieve new meetings
            var subscriptions = GetSubscriptions();

            // Send the newsletter to each reciever
            newsletter.SendToSubscribers(subscriptions);
        }

        private IEnumerable<NewsletterSubscription> GetSubscriptions()
        {
            return DataContext.Current.NewsletterSubscriptions.Where(x => x.SubscribedToNewMeetingUpdates);
        }

        private NewMeetingsNewsletter GenerateNewsletterFromMeetings(IEnumerable<Meeting> meetings)
        {
            return new NewMeetingsNewsletter(meetings, _emailAdapter);
        }

        private Meeting[] FindNewMeetingsToCreateNewsletterFrom()
        {
            var upcommingMeetings = new UpcommingMeetingsQuery().Meetings;
            var lastMail = DataContext.Current.NewsletterLogs.Where(x=>x.NewsletterType == typeof (NewMeetingsNewsletter).Name).OrderByDescending(x=>x.DateSent).FirstOrDefault();

            var fromTime = lastMail == null ? new DateTime(2000,1,1) : lastMail.DateSent;
            var age = TimeSpan.FromHours(1);

            // The created time is the time the last newsletter run minus the minium age
            var createdTimeFrom = fromTime - age;
            var createdTimeTo = DateTime.Now - age;

            var meetings = upcommingMeetings.Where(x => x.CreatedAt >= createdTimeFrom && x.CreatedAt <= createdTimeTo);

            return meetings.ToArray();
        }

    }
}