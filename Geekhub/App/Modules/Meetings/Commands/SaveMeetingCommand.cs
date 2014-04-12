using Geekhub.App.Modules.Meetings.ViewModels;

namespace Geekhub.App.Modules.Meetings.Commands
{
    public class SaveMeetingCommand
    {
        public SaveMeetingCommand(int meetingId, string by, MeetingFormModel formModel)
        {
            MeetingId = meetingId;
            By = by;
            FormModel = formModel;
        }

        public string By { get; set; }
        public int MeetingId { get; private set; }
        public MeetingFormModel FormModel { get; private set; }
    }
}