namespace Deldysoft.Foundation.CommandHandling
{
    public class CommandExecuter : ICommandExecuter
    {
        public CommandBus CommandBus;

        public void Execute<T>(T command)
        {
            CommandBus.Execute(command);
        }
    }
}