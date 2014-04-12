using System.Linq;
using Deldysoft.Foundation.CommandHandling;
using Geekhub.App.Core.CommandHandling;
using Geekhub.App.Core.Data;
using Geekhub.App.Modules.Meetings.Commands;
using Geekhub.App.Modules.Meetings.Models;
using Geekhub.App.Modules.Meetings.ViewModels;

namespace Geekhub.App.Modules.Meetings.CommandHandlers
{
    public class CreateMeetingCommandHandler : CommandHandlerBase, IHandleCommand<CreateMeetingCommand>
    {
        private readonly MeetingFormModelBinder _meetingFormModelBinder = new MeetingFormModelBinder();

        public CreateMeetingCommandHandler(DataContext dataContext) : base(dataContext)
        {
        }

        public void Execute(CreateMeetingCommand command)
        {
            var meeting = new Meeting();

            if (DataContext.Meetings.Any()) {
                meeting.Id = DataContext.Meetings.Max(x => x.Id) + 1;
            } else {
                meeting.Id = 1;
            }
            
            _meetingFormModelBinder.FormModelToMeeting(command.FormModel, meeting);

            DataContext.Meetings.Add(meeting);
        }
    }
}