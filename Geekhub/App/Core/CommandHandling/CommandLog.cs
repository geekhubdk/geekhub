using System;

namespace Geekhub.App.Core.CommandHandling
{
    public class CommandLog
    {
        public CommandLog()
        {
            ExecutedAt = DateTime.Now;
        }

        public string CommandName { get; set; }
        public DateTime ExecutedAt { get; set; }
        public object Command { get; set; }
    }
}