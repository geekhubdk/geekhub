using System.Linq;
using Deldysoft.Foundation.CommandHandling;
using Geekhub.App.Core.CommandHandling;
using Geekhub.App.Core.Data;
using Geekhub.App.Modules.Meetings.Commands;
using Geekhub.App.Modules.Meetings.ViewModels;

namespace Geekhub.App.Modules.Meetings.CommandHandlers
{
    public class SaveMeetingCommandHandler : CommandHandlerBase, IHandleCommand<SaveMeetingCommand>
    {
        private readonly MeetingFormModelBinder _meetingFormModelBinder = new MeetingFormModelBinder();

        public SaveMeetingCommandHandler(DataContext dataContext) : base(dataContext)
        {
        }

        public void Execute(SaveMeetingCommand command)
        {
            var meeting = DataContext.Meetings.FirstOrDefault(x => x.Id == command.MeetingId);

            _meetingFormModelBinder.FormModelToMeeting(command.FormModel, meeting);

            DataContext.Meetings.Update(meeting);
        }
    }
}