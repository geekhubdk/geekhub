using System;
using System.Collections.Generic;
using System.Linq;
using Geekhub.App.Core.CommandHandling;
using Geekhub.App.Core.Data;
using Geekhub.App.Modules.Meetings.Models;

namespace Geekhub.App.Modules.Meetings.Queries
{
    public class ArchivedMeetingsQuery
    {
        public ArchivedMeetingsQuery()
        {
            Meetings = DataContext.Current.Meetings.Where(x => x.StartsAt.Date < DateTime.Now.Date).OrderByDescending(x => x.StartsAt);
        }

        public IOrderedEnumerable<Meeting> Meetings { get; private set; }
    }
}