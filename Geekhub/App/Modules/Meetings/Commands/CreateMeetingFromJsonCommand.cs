using Geekhub.App.Modules.Meetings.ViewModels;

namespace Geekhub.App.Modules.Meetings.Commands
{
    public class CreateMeetingFromJsonCommand
    {
        public CreateMeetingFromJsonCommand(JsonViewModel.JsonMeetingViewModel formModel, string by)
        {
            FormModel = formModel;
            By = @by;
        }

        public JsonViewModel.JsonMeetingViewModel FormModel { get; private set; }
        public string By { get; set; }
    }
}