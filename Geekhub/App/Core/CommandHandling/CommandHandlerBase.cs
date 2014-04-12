using Geekhub.App.Core.Data;

namespace Geekhub.App.Core.CommandHandling
{
    public abstract class CommandHandlerBase
    {
        public DataContext DataContext { get; private set; }

        protected CommandHandlerBase(DataContext dataContext)
        {
            DataContext = dataContext;
        }
    }
}