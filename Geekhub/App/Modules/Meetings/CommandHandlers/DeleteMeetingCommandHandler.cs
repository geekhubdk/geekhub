using System;
using System.Linq;
using Deldysoft.Foundation.CommandHandling;
using Geekhub.App.Core.CommandHandling;
using Geekhub.App.Core.Data;
using Geekhub.App.Modules.Meetings.Commands;

namespace Geekhub.App.Modules.Meetings.CommandHandlers
{
    public class DeleteMeetingCommandHandler : CommandHandlerBase, IHandleCommand<DeleteMeetingCommand>
    {
        public DeleteMeetingCommandHandler(DataContext dataContext) : base(dataContext)
        {
        }

        public void Execute(DeleteMeetingCommand command)
        {
            var meeting = DataContext.Meetings.First(x => x.Id == command.MeetingId);
            meeting.DeletedAt = DateTime.Now;
            DataContext.Meetings.Update(meeting);
        }
    }
}