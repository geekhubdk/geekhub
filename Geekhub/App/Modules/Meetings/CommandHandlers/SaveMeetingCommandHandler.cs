using System.Linq;


using Geekhub.App.Core.Data;
using Geekhub.App.Modules.Meetings.ViewModels;

namespace Geekhub.App.Modules.Meetings.CommandHandlers
{
    public class SaveMeetingCommandHandler
    {
        private readonly MeetingFormModelBinder _meetingFormModelBinder = new MeetingFormModelBinder();

        public SaveMeetingCommandHandler(int meetingId, MeetingFormModel formModel)
        {
            var meeting = DataContext.Current.Meetings.FirstOrDefault(x => x.Id == meetingId);

            _meetingFormModelBinder.FormModelToMeeting(formModel, meeting);

            DataContext.Current.Meetings.Update(meeting);
        }

    }
}