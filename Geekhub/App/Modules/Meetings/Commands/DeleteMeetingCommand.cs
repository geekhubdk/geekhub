namespace Geekhub.App.Modules.Meetings.Commands
{
    public class DeleteMeetingCommand
    {
        public DeleteMeetingCommand(int meetingId, string by)
        {
            MeetingId = meetingId;
            By = by;
        }

        public int MeetingId { get; private set; }
        public string By { get; set; }
    }
}