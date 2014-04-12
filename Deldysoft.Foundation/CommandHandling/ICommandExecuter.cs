namespace Deldysoft.Foundation.CommandHandling
{
    public interface ICommandExecuter
    {
        void Execute<T>(T command);
    }
}