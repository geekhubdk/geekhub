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
            StartsAtDate = meeting.StartsAt.ToString("yyyy/MM/dd");
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
        public string StartsAtDate { get; set; }

        [Required]
        public string Organizers { get; set; }

        public string Tags { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string City { get; set; }
    }
}