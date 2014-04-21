using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Deldysoft.Foundation.CommandHandling;
using Geekhub.App.Core.CommandHandling;
using Geekhub.App.Core.Data;
using Geekhub.App.Modules.Users.Commands;

namespace Geekhub.App.Modules.Users.CommandHandlers
{
    public class RegisterInvalidValidationCodeCommandHandler : CommandHandlerBase, IHandleCommand<RegisterInvalidValidationCodeCommand>
    {
        private readonly ICommandExecuter _commandExecuter;

        public RegisterInvalidValidationCodeCommandHandler(DataContext dataContext, ICommandExecuter commandExecuter) : base(dataContext)
        {
            _commandExecuter = commandExecuter;
        }

        public void Execute(RegisterInvalidValidationCodeCommand command)
        {
            var user = DataContext.Users.Single(x => x.Email.Equals(command.Email, StringComparison.InvariantCultureIgnoreCase));
            user.InvalidLoginAttempts++;

            if (user.InvalidLoginAttempts > 3) {
                user.ValidationCode = null;
                DataContext.Users.Update(user);
                // We will send a new email
                _commandExecuter.Execute(new SendUserLoginEmailCommand(command.Email));
            }
        }
    }
}