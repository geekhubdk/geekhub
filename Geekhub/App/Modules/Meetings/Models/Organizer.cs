using System.Collections.Generic;

namespace Geekhub.App.Modules.Meetings.Models
{
    public partial class Organizer
    {
        public string Name { get; set; }

        public virtual ICollection<Meeting> Meetings { get; set; }
    }
}