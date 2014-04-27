using System;
using System.Linq;


using Geekhub.App.Core.Data;

namespace Geekhub.App.Modules.Meetings.CommandHandlers
{
    public class DeleteMeetingCommandHandler
    {
        public DeleteMeetingCommandHandler(int id)
        {
            var meeting = DataContext.Current.Meetings.First(x => x.Id == id);
            meeting.DeletedAt = DateTime.Now;
            DataContext.Current.Meetings.Update(meeting);
        }
    }
}