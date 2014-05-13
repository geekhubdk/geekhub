using System.Collections.Generic;
using System.Linq;
using Geekhub.App.Modules.Core.Data;
using Geekhub.App.Modules.Meetings.Models;

namespace Geekhub.App.Modules.Meetings.MeetingProviders
{
    public class MeetingProviderCoordinator
    {
        private MeetupMeetingProvider meetupProvider = new MeetupMeetingProvider();
        public MeetingSubject[] PullMeetings()
        {
            var meetings = meetupProvider.GetSubjects();
            var newMeetings = new List<MeetingSubject>();
            foreach (var meeting in meetings) {
                var storedSubject = DataContext.Current.MeetingSubjects.SingleOrDefault(x => x.IsSameAs(meeting));
                if (storedSubject == null) {
                    DataContext.Current.MeetingSubjects.Add(meeting);
                    newMeetings.Add(meeting);
                } else {
                    // Its the same meeting, we need to find out if we should insert an update
                    if (!meeting.IsEqual(storedSubject)) {
                        if (storedSubject.Handled) {
                            meeting.SubjectType = MeetingSubjectType.Updated;
                        } 
                        DataContext.Current.MeetingSubjects.Remove(storedSubject);
                        DataContext.Current.MeetingSubjects.Add(meeting);
                        newMeetings.Add(meeting);
                    }

                }
            }

            return newMeetings.ToArray();
        }
    }
}