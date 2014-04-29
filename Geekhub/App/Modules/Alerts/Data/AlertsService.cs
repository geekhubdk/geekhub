using Geekhub.App.Core.Adapters;
using Geekhub.App.Core.Data;
using Geekhub.App.Core.Support;
using Geekhub.App.Modules.Alerts.Adapters;
using Geekhub.App.Modules.Alerts.Models;
using Geekhub.App.Modules.Alerts.Support;
using Geekhub.App.Modules.Meetings.Data;
using Geekhub.App.Modules.Meetings.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace Geekhub.App.Modules.Alerts.Data
{
    public class AlertsService
    {
        private readonly IEmailAdapter _emailAdapter;
        private readonly MeetingsRepository _meetingsRepository;
        private readonly TwitterMeetingAlertGenerator _twitterMeetingAlertGenerator = new TwitterMeetingAlertGenerator();
        private readonly ITwitterAdapter _twitterAdapter;

        public AlertsService() : this(ObjectFactory.CreateEmailAdapter(), ObjectFactory.CreateTwitterAdapter())
        {

        }

        public AlertsService(IEmailAdapter emailAdapter, ITwitterAdapter twitterAdapter)
        {
            _emailAdapter = emailAdapter;
            _twitterAdapter = twitterAdapter;
            _meetingsRepository = new MeetingsRepository();
        }

        public void SendNewMeetingsNewsletter()
        {
            // Find a list of meetings that is not yet alerted via newsletters
            var meetings = FindNewMeetingsToCreateNewsletterFrom();

            if (!meetings.Any()) {
                return;
            }

            // Make a combined newsletter with the meetings
            var newsletter = GenerateNewsletterFromMeetings(meetings);

            // Get the subscriptions that wants to recieve new meetings
            var subscriptions = GetSubscriptions();

            // Send the newsletter to each reciever
            newsletter.SendToSubscribers(subscriptions);
        }

        public void SendTweetsForMeetings(TimeSpan waitTime)
        {
            var meetings = _meetingsRepository.GetUpcommingMeetings();
            foreach (var meeting in meetings) {

                // We will wait for 1 hour before sending tweets
                if (DateTime.Now - meeting.CreatedAt < waitTime) {
                    continue;
                }

                if (!DataContext.Current.MeetingTweetAlerts.Any(x => x.MeetingId == meeting.Id)) {
                    var alert = new MeetingTweetAlert();
                    alert.Message = _twitterMeetingAlertGenerator.GenerateMessage(meeting);
                    alert.DateSent = DateTime.Now;
                    alert.MeetingId = meeting.Id;

                    try {
                        _twitterAdapter.SendTweet(alert.Message);
                    } catch (Exception ex) {
                        alert.DateFailed = DateTime.Now;
                        alert.FailMessage = ex.Message;
                        Debug.WriteLine("Could not send twitter message for meeting {0}: {1}", meeting.Id, ex.Message);
                    }

                    DataContext.Current.MeetingTweetAlerts.Add(alert);
                }
            }
        }

        public void SubscribeToNewsletter(string email)
        {
            var subscription = DataContext.Current.NewsletterSubscriptions.FirstOrDefault(
                x => x.Email.Equals(email, StringComparison.CurrentCultureIgnoreCase));

            if (subscription == null) {
                subscription = new NewsletterSubscription {Email = email};
                DataContext.Current.NewsletterSubscriptions.Add(subscription);
            }

            subscription.SubscribedToNewMeetingUpdates = true;
            DataContext.Current.NewsletterSubscriptions.Update(subscription);
        }

        public void UnsubscribeFromNewsletter(string email)
        {
            var subscription = DataContext.Current.NewsletterSubscriptions.FirstOrDefault(
                x => x.Email.Equals(email, StringComparison.CurrentCultureIgnoreCase));

            if (subscription == null) {
                throw new SubscriptionNotFoundException();
            }

            subscription.SubscribedToNewMeetingUpdates = false;

            DataContext.Current.NewsletterSubscriptions.Update(subscription);
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
            var upcommingMeetings = _meetingsRepository.GetUpcommingMeetings();
            var lastMail = DataContext.Current.NewsletterLogs.Where(x => x.NewsletterType == typeof(NewMeetingsNewsletter).Name).OrderByDescending(x => x.DateSent).FirstOrDefault();

            var fromTime = lastMail == null ? new DateTime(2000, 1, 1) : lastMail.DateSent;
            var age = TimeSpan.FromHours(1);

            // The created time is the time the last newsletter run minus the minium age
            var createdTimeFrom = fromTime - age;
            var createdTimeTo = DateTime.Now - age;

            var meetings = upcommingMeetings.Where(x => x.CreatedAt >= createdTimeFrom && x.CreatedAt <= createdTimeTo);

            return meetings.ToArray();
        }

    }
}