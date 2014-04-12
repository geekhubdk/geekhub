using Deldysoft.Foundation.CommandHandling;
using Geekhub.App.Core.Data;

namespace Geekhub.App.Core.CommandHandling
{
    public class CommandLogger : ICommandLoggerAdapter
    {
        private readonly DataContext _dataContext;

        public CommandLogger(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public void Log(object command)
        {
            var commandLog = new CommandLog();
            commandLog.CommandName = command.GetType().Name;
            commandLog.Command = command;
            _dataContext.CommandLogs.Add(commandLog);
        }
    }
}