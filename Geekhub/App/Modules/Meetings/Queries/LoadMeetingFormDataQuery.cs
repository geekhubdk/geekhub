using System.Linq;
using Geekhub.App.Core.Data;

namespace Geekhub.App.Modules.Meetings.Queries
{
    public class LoadMeetingFormDataQuery {

        public LoadMeetingFormDataQuery()
        {
            Organizers = DataContext.Current.Meetings.SelectMany(x => x.Organizers.Select(y => y.Name)).Distinct().ToArray();
            Tags = DataContext.Current.Meetings.SelectMany(x => x.Tags.Select(y => y.Name)).Distinct().ToArray();
        }

        public string[] Organizers { get; private set; }

        public string[] Tags { get; private set; }
    }
}