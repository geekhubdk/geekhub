using System;
using System.Globalization;
using Geekhub.App.Core.Support;
using Geekhub.App.Modules.Meetings.Models;

namespace Geekhub.App.Modules.Meetings.ViewModels
{
    public class MeetingFormModelBinder
    {
        public void FormModelToMeeting(MeetingFormModel formModel, Meeting meeting)
        {
            meeting.Url = formModel.Url;
            meeting.Title = formModel.Title;
            meeting.Address = formModel.Address;
            meeting.AddressFormatted = formModel.AddressFormatted;
            if (formModel.AddressLat.IsPresent() && formModel.AddressLng.IsPresent()) {
                meeting.Latitude = double.Parse(formModel.AddressLat, CultureInfo.InvariantCulture);
                meeting.Longtitude = double.Parse(formModel.AddressLng, CultureInfo.InvariantCulture);
            }
            meeting.StartsAt = DateTime.Parse(formModel.StartsAtDate + " " + formModel.StartsAtTime);
            
            if (formModel.City.IsPresent()) {
                meeting.City = new City() { Name = formModel.City };
            }

            AddOrganizersToMeeting(meeting, formModel.Organizers);
            AddTagsToMeeting(meeting, formModel.Tags);

            meeting.Description = formModel.Description;
        }

        private void AddOrganizersToMeeting(Meeting meeting, string organizersString)
        {
            meeting.Organizers.Clear();

            var organizers = TagsToString(organizersString);

            foreach (var organizer in organizers) {
                var organizerRecord = new Organizer() { Name = organizer };
                meeting.Organizers.Add(organizerRecord);
            }
        }

        private void AddTagsToMeeting(Meeting meeting, string tagsString)
        {
            meeting.Tags.Clear();

            var tags = TagsToString(tagsString);

            foreach (var tag in tags) {
                var tagRecord = new Tag() { Name = tag };
                meeting.Tags.Add(tagRecord);
            }
        }

        private string[] TagsToString(string organizersString)
        {
            return organizersString.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);
        }
    }
}