using System.Linq;
using Deldysoft.Foundation.CommandHandling;
using Geekhub.App.Core.CommandHandling;
using Geekhub.App.Core.Data;
using Geekhub.App.Modules.Users.Commands;

namespace Geekhub.App.Modules.Users.CommandHandlers
{
    public class ExpireUserValidationCodeCommandHandler : CommandHandlerBase, IHandleCommand<ExpireUserValidationCodeCommand>
    {
        public ExpireUserValidationCodeCommandHandler(DataContext dataContext) : base(dataContext)
        {
        }

        public void Execute(ExpireUserValidationCodeCommand command)
        {
            var user = DataContext.Users.Single(x => x.Email == command.UserEmail);
            user.ValidationCode = null;
            user.InvalidLoginAttempts = 0;
            DataContext.Users.Update(user);
        }
    }
}