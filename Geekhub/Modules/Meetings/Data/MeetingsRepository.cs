using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using Geekhub.Modules.Core.Data;
using Geekhub.Modules.Meetings.Models;

namespace Geekhub.Modules.Meetings.Data
{
    public class MeetingsRepository
    {
        public IEnumerable<Meeting> GetArchivedMeetings(NameValueCollection filter = null)
        {
            return GetPublicMeetings().Where(x => x.StartsAt.Date < DateTime.Now.Date).OrderByDescending(x => x.StartsAt).Filter(filter);
        }

        public IEnumerable<Meeting> GetUpcommingMeetings(NameValueCollection filter = null)
        {
            return GetPublicMeetings().Where(x => x.StartsAt > DateTime.Now.Date).OrderBy(x => x.StartsAt).Filter(filter);
        }

        public IEnumerable<string> GetMeetingOrganizers()
        {
            return DataContext.Current.Meetings.SelectMany(x => x.Organizers.Select(y => y.Name)).Distinct();
        }

        public IEnumerable<string> GetMeetingsTags()
        {
            return DataContext.Current.Meetings.SelectMany(x => x.Tags.Select(y => y.Name)).Distinct();
        }

        public Meeting GetMeeting(int meetingId)
        {
            return DataContext.Current.Meetings.Single(x => x.Id == meetingId);
        }

        private IEnumerable<Meeting> GetPublicMeetings()
        {
            return DataContext.Current.Meetings.Where(x => x.IsNotDeleted);
        }
    }
}