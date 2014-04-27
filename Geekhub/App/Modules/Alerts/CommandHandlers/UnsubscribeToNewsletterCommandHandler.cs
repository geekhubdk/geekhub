using System;
using System.Linq;


using Geekhub.App.Core.Data;
using Geekhub.App.Modules.Alerts.Support;

namespace Geekhub.App.Modules.Alerts.CommandHandlers
{
    public class UnsubscribeToNewsletterCommandHandler
    {
        public UnsubscribeToNewsletterCommandHandler(string email)
        {
            var subscription = DataContext.Current.NewsletterSubscriptions.FirstOrDefault(
                x => x.Email.Equals(email, StringComparison.CurrentCultureIgnoreCase));

            if (subscription == null) {
                throw new SubscriptionNotFoundException();
            }

            subscription.SubscribedToNewMeetingUpdates = false;

            DataContext.Current.NewsletterSubscriptions.Update(subscription);
        }
    }
}