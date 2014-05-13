using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using Geekhub.Modules.Meetings.Models;

namespace Geekhub.Modules.Meetings.ViewModels
{
    public class MeetingFormModel
    {
        public MeetingFormModel()
        {
            
        }

        public MeetingFormModel(Meeting meeting)
        {
            Url = meeting.Url;
            Title = meeting.Title;
            Address = meeting.Address;
            AddressFormatted = meeting.AddressFormatted;
            if (meeting.Latitude.HasValue) {
                AddressLat = meeting.Latitude.Value.ToString(CultureInfo.InvariantCulture);
            }
            if (meeting.Longtitude.HasValue) {
                AddressLng = meeting.Longtitude.Value.ToString(CultureInfo.InvariantCulture);
            }

            StartsAtDate = meeting.StartsAt.ToString("yyyy/MM/dd");
            StartsAtTime = meeting.StartsAt.ToString("HH:mm");
            Organizers = string.Join(",", meeting.Organizers.Select(x => x.Name));
            Tags = string.Join(",", meeting.Tags.Select(x => x.Name));
            Description = meeting.Description;

            if (meeting.City != null) {
                City = meeting.City.Name;
            }

        }

        [Required]
        public string Url { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string AddressFormatted { get; set; }

        public string AddressLat { get; set; }

        public string AddressLng { get; set; }

        [Required]
        public string StartsAtDate { get; set; }

        [Required]
        public string StartsAtTime { get; set; }

        [Required]
        public string Organizers { get; set; }

        public string Tags { get; set; }

        [Required]
        public string Description { get; set; }

        public string City { get; set; }
    }
}