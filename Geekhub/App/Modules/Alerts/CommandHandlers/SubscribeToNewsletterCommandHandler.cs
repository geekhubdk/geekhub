using System;
using System.Linq;
using Deldysoft.Foundation.CommandHandling;
using Geekhub.App.Core.CommandHandling;
using Geekhub.App.Core.Data;
using Geekhub.App.Modules.Alerts.Commands;
using Geekhub.App.Modules.Alerts.Models;

namespace Geekhub.App.Modules.Alerts.CommandHandlers
{
    public class SubscribeToNewsletterCommandHandler : IHandleCommand<SubscribeToNewsletterCommand>
    {
        public void Execute(SubscribeToNewsletterCommand command)
        {
            var subscription = DataContext.Current.NewsletterSubscriptions.FirstOrDefault(
                x => x.Email.Equals(command.Email, StringComparison.CurrentCultureIgnoreCase));

            if (subscription == null) {
                subscription = new NewsletterSubscription {Email = command.Email};
                DataContext.Current.NewsletterSubscriptions.Add(subscription);
            }

            subscription.SubscribedToNewMeetingUpdates = true;
            DataContext.Current.NewsletterSubscriptions.Update(subscription);
        }
    }
}