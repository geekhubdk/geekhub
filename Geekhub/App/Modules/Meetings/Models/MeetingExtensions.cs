using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using Geekhub.App.Core.Support;

namespace Geekhub.App.Modules.Meetings.Models
{
    public static class MeetingExtensions
    {
        public static IEnumerable<Meeting> Filter(this IEnumerable<Meeting> meetings, NameValueCollection query)
        {
            var tags = query.GetValuesFrom("tag[]", "tag");
            var organizers = query.GetValuesFrom("organizer[]","organizer");
            var locations = query.GetValuesFrom("location[]", "location");
            
            meetings = Filter(meetings, tags, x => x.Tags.Select(y=>y.Name));
            meetings = Filter(meetings, organizers, x => x.Organizers.Select(y => y.Name));
            meetings = Filter(meetings, locations, x => new[]{x.City.Name});
  
            return meetings;
        }

        private static IEnumerable<Meeting> Filter(IEnumerable<Meeting> meetings, string[] values, Func<Meeting,IEnumerable<string>> selector)
        {
            if (values.Any()) {
               return meetings.Where(x => values.Any(y => selector(x).Any(z=>z.Equals(y, StringComparison.CurrentCultureIgnoreCase))));
            }

            return meetings;
        }
    }
}