using System.Collections.Generic;
using System.Linq;
using Geekhub.App.Modules.Meetings.Models;
using Geekhub.App.Modules.Meetings.Support;

namespace Geekhub.App.Modules.Meetings.ViewModels
{
    public class WidgetViewModel
    {
        public string Header;
        public IEnumerable<WidgetMeetingViewModel> Meetings;

        public WidgetViewModel(string header, IEnumerable<Meeting> meetings)
        {
            Header = header;
            Meetings = meetings.Select(x => new WidgetMeetingViewModel(x));
        }

        public class WidgetMeetingViewModel
        {
            public string Tooltip;
            public string Day;
            public string Month;
            public string Link;
            public string Title;
            public string Location;
            public IEnumerable<string> Tags;
            public string Organizers;

            public WidgetMeetingViewModel(Meeting meeting)
            {
                Tooltip = string.Format("{0} - {1} - {2}", meeting.City.Name, meeting.StartsAt.ToString("d. MMM YYYY, HH:mm"), meeting.Title);
                Day = meeting.StartsAt.ToString("dd");
                Month = meeting.StartsAt.ToString("MMM");
                Link = MeetingUrlGenerator.CreateFullMeetingUrl(meeting.Id, null);
                Tags = meeting.Tags.Select(x => x.Name);
                Organizers = string.Join(" & ", meeting.Organizers.Select(x => x.Name));
                Title = meeting.Title;
                Location = meeting.City.Name;
            }
        }
    }
}