using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using Geekhub.App.Core.CommandHandling;
using Geekhub.App.Core.Data;
using Geekhub.App.Modules.Meetings.Models;

namespace Geekhub.App.Modules.Meetings.Queries
{
    public class UpcommingMeetingsQuery
    {
        public UpcommingMeetingsQuery(NameValueCollection filter = null)
        {
            filter = filter ?? new NameValueCollection();
            Meetings = DataContext.Current.Meetings.Where(x => x.StartsAt > DateTime.Now.Date && x.IsNotDeleted).OrderBy(x => x.StartsAt).Filter(filter);
        }

        public IEnumerable<Meeting> Meetings { get; private set; }
    }
}