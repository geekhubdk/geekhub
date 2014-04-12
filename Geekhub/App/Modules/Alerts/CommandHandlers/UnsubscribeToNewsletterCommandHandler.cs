using System;
using System.Linq;
using Deldysoft.Foundation.CommandHandling;
using Geekhub.App.Core.CommandHandling;
using Geekhub.App.Core.Data;
using Geekhub.App.Modules.Alerts.Commands;
using Geekhub.App.Modules.Alerts.Support;

namespace Geekhub.App.Modules.Alerts.CommandHandlers
{
    public class UnsubscribeToNewsletterCommandHandler : IHandleCommand<UnsubscribeToNewsletterCommand>
    {
        public void Execute(UnsubscribeToNewsletterCommand command)
        {
            var subscription = DataContext.Current.NewsletterSubscriptions.FirstOrDefault(
                x => x.Email.Equals(command.Email, StringComparison.CurrentCultureIgnoreCase));

            if (subscription == null) {
                throw new SubscriptionNotFoundException();
            }

            subscription.SubscribedToNewMeetingUpdates = false;

            DataContext.Current.NewsletterSubscriptions.Update(subscription);
        }
    }
}