using Geekhub.App.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Geekhub.App.Modules.Alerts.Data
{
    public class AlertsRepository
    {
        public int GetNewsletterSubscriberCount()
        {
            return DataContext.Current.NewsletterSubscriptions.Count(x => x.SubscribedToNewMeetingUpdates);
        }
    }
}