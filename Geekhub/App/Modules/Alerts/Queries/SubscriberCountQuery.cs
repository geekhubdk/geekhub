using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Geekhub.App.Core.CommandHandling;
using Geekhub.App.Core.Data;

namespace Geekhub.App.Modules.Alerts.Queries
{
    public class SubscriberCountQuery
    {
        public SubscriberCountQuery()
        {
            NumberOfSubscribers = DataContext.Current.NewsletterSubscriptions.Count(x => x.SubscribedToNewMeetingUpdates);
        }

        public int NumberOfSubscribers { get; private set; }
    }
}