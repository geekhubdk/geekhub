using System.Linq;
using Geekhub.App.Core.CommandHandling;
using Geekhub.App.Core.Data;
using Geekhub.App.Modules.Meetings.Models;

namespace Geekhub.App.Modules.Meetings.Queries
{
    public class ShowMeetingQuery : QueryBase
    {
        public ShowMeetingQuery(DataContext dataContext) : base(dataContext)
        {
        }

        public Meeting Execute(int id)
        {
            return DataContext.Meetings.Single(x => x.Id == id);
        }
    }
}