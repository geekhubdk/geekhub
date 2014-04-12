using Deldysoft.Foundation.CommandHandling;
using Geekhub.App.Core.CommandHandling;
using Geekhub.App.Core.Data;
using Geekhub.App.Modules.Users.Commands;
using Geekhub.App.Modules.Users.Models;

namespace Geekhub.App.Modules.Users.CommandHandlers
{
    public class CreateUserCommandHandler : CommandHandlerBase, IHandleCommand<CreateUserCommand>
    {
        public CreateUserCommandHandler(DataContext dataContext) : base(dataContext)
        {
        }

        public void Execute(CreateUserCommand command)
        {
            var user = new User {Email = command.Email.ToLower(), Name = command.Name};
            DataContext.Users.Add(user);
        }
    }
}