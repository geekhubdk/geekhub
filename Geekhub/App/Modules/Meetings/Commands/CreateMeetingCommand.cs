using Geekhub.App.Modules.Meetings.ViewModels;

namespace Geekhub.App.Modules.Meetings.Commands
{
    public class CreateMeetingCommand
    {
        public CreateMeetingCommand(MeetingFormModel formModel, string by)
        {
            FormModel = formModel;
            By = @by;
        }

        public MeetingFormModel FormModel { get; private set; }
        public string By { get; set; }
    }
}