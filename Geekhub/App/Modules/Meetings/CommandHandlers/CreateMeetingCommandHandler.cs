using System.Linq;


using Geekhub.App.Core.Data;
using Geekhub.App.Core.Support;
using Geekhub.App.Modules.Meetings.Models;
using Geekhub.App.Modules.Meetings.ViewModels;

namespace Geekhub.App.Modules.Meetings.CommandHandlers
{
    public class CreateMeetingCommandHandler
    {
        private readonly MeetingFormModelBinder _meetingFormModelBinder = new MeetingFormModelBinder();

        public CreateMeetingCommandHandler(MeetingFormModel formModel)
        {
            var meeting = new Meeting();

            SetID(meeting);

            _meetingFormModelBinder.FormModelToMeeting(formModel, meeting);

            DataContext.Current.Meetings.Add(meeting);
        }

        public CreateMeetingCommandHandler(JsonViewModel.JsonMeetingViewModel formModel)
        {
            var meeting = new Meeting();

            SetID(meeting);

            meeting.Address = formModel.Address;
            meeting.AddressFormatted = formModel.Address;
            meeting.City = new City() { Name = formModel.City };
            meeting.CreatedAt = formModel.CreatedAt;
            meeting.Description = formModel.Description;
            meeting.Latitude = formModel.Latitude;
            meeting.Longtitude = formModel.Longtitude;
            meeting.Organizers.AddRange(formModel.Organizers.Select(x=>new Organizer() { Name = x}));
            meeting.StartsAt = formModel.StartsAt;
            meeting.Tags.AddRange(formModel.Tags.Select(x=>new Tag() { Name = x}));
            meeting.Title = formModel.Title;
            meeting.Url = formModel.Url;

            DataContext.Current.Meetings.Add(meeting);
        }

        private void SetID(Meeting meeting)
        {
            if (DataContext.Current.Meetings.Any()) {
                meeting.Id = DataContext.Current.Meetings.Max(x => x.Id) + 1;
            }
            else {
                meeting.Id = 1;
            }
        }
    }
}