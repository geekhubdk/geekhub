using System;

namespace Geekhub.App.Modules.Alerts.Commands
{
    public class SendTweetsForMeetingsCommand
    {
        public SendTweetsForMeetingsCommand(TimeSpan waitTime)
        {
            WaitTime = waitTime;
        }

        public TimeSpan WaitTime { get; private set; }
    }
}