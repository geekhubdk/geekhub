using System.Linq;
using Geekhub.App.Core.CommandHandling;
using Geekhub.App.Core.Data;

namespace Geekhub.App.Modules.Meetings.Queries
{
    public class LoadMeetingFormDataQuery : QueryBase {
        
        public LoadMeetingFormDataQuery(DataContext dataContext) : base(dataContext)
        {
        }

        public LoadMeetingFormDataQueryResult Execute()
        {
            return new LoadMeetingFormDataQueryResult() {
                Organizers = DataContext.Meetings.SelectMany(x => x.Organizers.Select(y => y.Name)).Distinct().ToArray(),
                Tags = DataContext.Meetings.SelectMany(x => x.Tags.Select(y => y.Name)).Distinct().ToArray()
            };
        }

        public struct LoadMeetingFormDataQueryResult
        {
            public string[] Organizers;
            public string[] Tags;
        }
    }
}