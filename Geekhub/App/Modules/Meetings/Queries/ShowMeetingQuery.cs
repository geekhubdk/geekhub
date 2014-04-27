using System.Linq;

using Geekhub.App.Core.Data;
using Geekhub.App.Modules.Meetings.Models;

namespace Geekhub.App.Modules.Meetings.Queries
{
    public class ShowMeetingQuery
    {
        public ShowMeetingQuery(int id)
        {
            Meeting = DataContext.Current.Meetings.Single(x => x.Id == id);
        }

        public Meeting Meeting { get; private set; }
    }
}