using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;

namespace Geekhub.App.Modules.Meetings.Models
{
    public static class MeetingExtensions
    {
        public static IEnumerable<Meeting> Filter(this IEnumerable<Meeting> meetings, NameValueCollection query)
        {
            query = query ?? new NameValueCollection();
            var filter = new MeetingsFilter(query);

            meetings = Filter(meetings, filter.Tags, x => x.Tags.Select(y=>y.Name));
            meetings = Filter(meetings, filter.Organizers, x => x.Organizers.Select(y => y.Name));
            meetings = Filter(meetings, filter.Locations, x => new[]{x.City.Name});
  
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