using System.Collections.Generic;
using System.Linq;
using System.Web;
using Geekhub.Modules.Meetings.Models;
using Geekhub.Modules.Meetings.Support;

namespace Geekhub.Modules.Meetings.ViewModels
{
    public class RssViewModel
    {
        public string Title = "geekhub - kommende events";
        public string Link = "http://www.geekhub.dk/meetings";
        public string Description = "Kommende events i Danmark";
        public IEnumerable<RssViewItemModel> Items;

        public RssViewModel(IEnumerable<Meeting> meetings)
        {
            Items = meetings.Select(meeting => new RssViewItemModel(meeting));
        }

        public class RssViewItemModel
        {
            public string Title;
            public string Link;
            public string Guid;
            public HtmlString Description;

            public RssViewItemModel(Meeting meeting)
            {
                Title = string.Format("{0} - {1}", meeting.Title, meeting.StartsAt.ToString("dd. MMM yyyy"));
                Link = MeetingUrlGenerator.CreateFullMeetingUrl(meeting.Id, "rss");
                Guid = Link;
                var description = string.Format("<strong>{0} - {1}</strong><br/>{2}",
                    meeting.City.Name,
                    string.Join(" & ", meeting.Organizers.Select(x => x.Name)),
                    HttpUtility.HtmlEncode(meeting.Description));
                Description = new HtmlString(description);
            }
        }
    }
}