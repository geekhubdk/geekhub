using System.Linq;
using Deldysoft.Foundation.CommandHandling;
using Geekhub.App.Core.CommandHandling;
using Geekhub.App.Core.Data;
using Geekhub.App.Core.Support;
using Geekhub.App.Modules.Meetings.Commands;
using Geekhub.App.Modules.Meetings.Models;
using Geekhub.App.Modules.Meetings.ViewModels;

namespace Geekhub.App.Modules.Meetings.CommandHandlers
{
    public class CreateMeetingCommandHandler : CommandHandlerBase, IHandleCommand<CreateMeetingCommand>, IHandleCommand<CreateMeetingFromJsonCommand>
    {
        private readonly MeetingFormModelBinder _meetingFormModelBinder = new MeetingFormModelBinder();

        public CreateMeetingCommandHandler(DataContext dataContext) : base(dataContext)
        {
        }

        public void Execute(CreateMeetingCommand command)
        {
            var meeting = new Meeting();

            SetID(meeting);
            
            _meetingFormModelBinder.FormModelToMeeting(command.FormModel, meeting);

            DataContext.Meetings.Add(meeting);
        }

        public void Execute(CreateMeetingFromJsonCommand command)
        {
            var meeting = new Meeting();

            SetID(meeting);

            meeting.Address = command.FormModel.Address;
            meeting.AddressFormatted = command.FormModel.Address;
            meeting.City = new City() { Name = command.FormModel.City };
            meeting.CreatedAt = command.FormModel.CreatedAt;
            meeting.Description = command.FormModel.Description;
            meeting.Latitude = command.FormModel.Latitude;
            meeting.Longtitude = command.FormModel.Longtitude;
            meeting.Organizers.AddRange(command.FormModel.Organizers.Select(x=>new Organizer() { Name = x}));
            meeting.StartsAt = command.FormModel.StartsAt;
            meeting.Tags.AddRange(command.FormModel.Tags.Select(x=>new Tag() { Name = x}));
            meeting.Title = command.FormModel.Title;
            meeting.Url = command.FormModel.Url;

            DataContext.Meetings.Add(meeting);
        }

        private void SetID(Meeting meeting)
        {
            if (DataContext.Meetings.Any()) {
                meeting.Id = DataContext.Meetings.Max(x => x.Id) + 1;
            }
            else {
                meeting.Id = 1;
            }
        }
    }
}