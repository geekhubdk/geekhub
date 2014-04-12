using Geekhub.App.Core.Data;

namespace Geekhub.App.Core.CommandHandling
{
    public abstract class QueryBase
    {
        public DataContext DataContext { get; private set; }

        protected QueryBase(DataContext dataContext)
        {
            DataContext = dataContext;
        }
    }
}