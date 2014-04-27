using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


using Geekhub.App.Core.Data;

namespace Geekhub.App.Modules.Meetings.CommandHandlers
{
    public class DeleteAllMeetingsCommandHandler
    {
        public void Execute()
        {
            DataContext.Current.Meetings.Purge();
        }
    }
}