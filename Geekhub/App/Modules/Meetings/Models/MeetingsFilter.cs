using System.Collections.Specialized;
using System.Linq;
using Geekhub.App.Core.Support;

namespace Geekhub.App.Modules.Meetings.Models
{
    public class MeetingsFilter
    {
        public MeetingsFilter(NameValueCollection query)
        {
            Tags = query.GetValuesFrom("tag[]", "tag");
            Organizers = query.GetValuesFrom("organizer[]", "organizer");
            Locations = query.GetValuesFrom("location[]", "location", "city[]", "city");
        }

        public string[] Tags { get; set; }
        public string[] Organizers { get; set; }
        public string[] Locations { get; set; }

        public bool Any()
        {
            return Tags.Any() || Organizers.Any() || Locations.Any();
        }
    }
}