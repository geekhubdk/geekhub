using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Deldysoft.Foundation.CommandHandling;
using Geekhub.App.Core.CommandHandling;
using Geekhub.App.Core.Data;
using Geekhub.App.Modules.Meetings.Commands;

namespace Geekhub.App.Modules.Meetings.CommandHandlers
{
    public class DeleteAllMeetingsCommandHandler : CommandHandlerBase, IHandleCommand<DeleteAllMeetingsCommand>
    {
        public DeleteAllMeetingsCommandHandler(DataContext dataContext) : base(dataContext)
        {
        }

        public void Execute(DeleteAllMeetingsCommand command)
        {
            DataContext.Meetings.Purge();
        }
    }
}