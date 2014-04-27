using System;
using System.Linq;


using Geekhub.App.Core.Data;
using Geekhub.App.Modules.Alerts.Models;

namespace Geekhub.App.Modules.Alerts.CommandHandlers
{
    public class SubscribeToNewsletterCommandHandler
    {
        public SubscribeToNewsletterCommandHandler(string email)
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
    }
}