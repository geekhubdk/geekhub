using System.Linq;
using Geekhub.Modules.Core.Data;

namespace Geekhub.Modules.Alerts.Data
{
    public class AlertsRepository
    {
        public int GetNewsletterSubscriberCount()
        {
            return DataContext.Current.NewsletterSubscriptions.Count(x => x.SubscribedToNewMeetingUpdates);
        }
    }
}