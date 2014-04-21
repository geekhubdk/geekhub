using System.Linq;
using Deldysoft.Foundation.CommandHandling;
using Geekhub.App.Core.Adapters;
using Geekhub.App.Core.CommandHandling;
using Geekhub.App.Core.Data;
using Geekhub.App.Modules.Alerts.Adapters;
using Geekhub.App.Modules.Users.Commands;
using Geekhub.App.Modules.Users.Models;
using Geekhub.App.Modules.Users.Support;

namespace Geekhub.App.Modules.Users.CommandHandlers
{
    public class SendUserLoginEmailCommandHandler : CommandHandlerBase, IHandleCommand<SendUserLoginEmailCommand>
    {
        private readonly UserValidationCodeGenerator _userValidationCodeGenerator;
        private readonly IEmailAdapter _emailAdapter;

        public SendUserLoginEmailCommandHandler(DataContext dataContext, UserValidationCodeGenerator userValidationCodeGenerator, IEmailAdapter emailAdapter)
            : base(dataContext)
        {
            _userValidationCodeGenerator = userValidationCodeGenerator;
            _emailAdapter = emailAdapter;
        }

        public void Execute(SendUserLoginEmailCommand command) {
            var user = DataContext.Users.Single(x => x.Email == command.UserEmail);

            var generatedCode = _userValidationCodeGenerator.GenerateCode(user.InvalidLoginAttempts);
            user.ValidationCode = generatedCode;
            DataContext.Users.Update(user);

            SendValidationEmail(command.UserEmail, generatedCode);
        }
        
        private void SendValidationEmail(string email, string validationCode)
        {
            _emailAdapter.SendMail(email, "Din kode til Geekhub", "Din kode er: " + validationCode);
        }
    }
}