using System;

namespace Geekhub.Modules.Alerts.Models
{
    public class MeetingTweetAlert
    {
        public int MeetingId { get; set; }
        public DateTime DateSent { get; set; }
        public string Message { get; set; }
        public DateTime? DateFailed { get; set; }
        public string FailMessage { get; set; }
    }
}