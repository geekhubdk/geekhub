using System;
using System.Collections.Generic;

namespace Geekhub.App.Modules.Meetings.Models
{
    public partial class Meeting
    {

        public Meeting()
        {
            Tags = new List<Tag>();
            Organizers = new List<Organizer>();
            CreatedAt = DateTime.Now;
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime StartsAt { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public bool? CostsMoney { get; set; }
        public virtual ICollection<Tag> Tags { get; set; }
        public virtual ICollection<Organizer> Organizers { get; set; }

        public string Address { get; set; }
        public string AddressFormatted { get; set; }
        public virtual City City { get; set; }
        public double? Latitude { get; set; }
        public double? Longtitude { get; set; }
        public DateTime? DeletedAt { get; set; }
        public bool IsNotDeleted { get { return !DeletedAt.HasValue; } }
    }
}