namespace Deldysoft.Foundation.CommandHandling
{
    public interface IHandleCommand<in T>
    {
        void Execute(T command);
    }
}