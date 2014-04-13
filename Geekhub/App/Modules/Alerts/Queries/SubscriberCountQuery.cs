using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Geekhub.App.Core.CommandHandling;
using Geekhub.App.Core.Data;

namespace Geekhub.App.Modules.Alerts.Queries
{
    public class SubscriberCountQuery : QueryBase
    {
        public SubscriberCountQuery(DataContext dataContext) : base(dataContext)
        {
        }

        public int Execute()
        {
            return DataContext.NewsletterSubscriptions.Count(x => x.SubscribedToNewMeetingUpdates);
        }
    }
}