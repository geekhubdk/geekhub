using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using Geekhub.App.Core.CommandHandling;
using Geekhub.App.Core.Data;
using Geekhub.App.Modules.Meetings.Models;

namespace Geekhub.App.Modules.Meetings.Queries
{
    public class UpcommingMeetingsQuery : QueryBase
    {
        public UpcommingMeetingsQuery(DataContext dataContext) : base(dataContext)
        {
        }

        public IEnumerable<Meeting> Execute(NameValueCollection filter)
        {
            return DataContext.Meetings.Where(x => x.StartsAt > DateTime.Now.Date && x.IsNotDeleted).OrderBy(x => x.StartsAt).Filter(filter);
        }

        public virtual IEnumerable<Meeting> Execute()
        {
            return Execute(new NameValueCollection());
        }
    }
}