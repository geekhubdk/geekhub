using System;
using System.Collections.Generic;
using System.Linq;

namespace Geekhub.Modules.Meetings.Models
{
    public partial class MeetingSubject
    {
        public MeetingSubject()
        {
            Tags = new List<string>();
            Organizers = new List<string>();
            CreatedAt = DateTime.Now;
            SubjectType = MeetingSubjectType.Created;
        }

        public string Title { get; set; }
        public DateTime StartsAt { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public bool? CostsMoney { get; set; }
        public virtual List<string> Tags { get; set; }
        public virtual List<string> Organizers { get; set; }

        public string Address { get; set; }
        public DateTime? DeletedAt { get; set; }
        public string Provider { get; set; }
        public string ProviderIdentifier { get; set; }

        public bool Handled { get; set; }
        public MeetingSubjectType SubjectType { get; set; }

        public bool IsSameAs(MeetingSubject meeting)
        {
            return (meeting.Provider == Provider && meeting.ProviderIdentifier == ProviderIdentifier);
        }

        public bool IsEqual(object obj)
        {
            if (!(obj is MeetingSubject))
                return false;
            var b = (MeetingSubject)obj;

            var checks = new bool[] {
                Title == b.Title,
                StartsAt == b.StartsAt,
                Description == b.Description,
                Url == b.Url,
                CostsMoney == b.CostsMoney,
                Tags.SequenceEqual(b.Tags),
                Organizers.SequenceEqual(b.Organizers),
                Address == b.Address,
                DeletedAt == b.DeletedAt,
                Provider == b.Provider,
                ProviderIdentifier == b.ProviderIdentifier
            };

            return checks.All(x => x);
        }
    }
}