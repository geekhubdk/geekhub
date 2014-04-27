using System.Linq;

using Geekhub.App.Core.Adapters;

using Geekhub.App.Core.Data;
using Geekhub.App.Modules.Alerts.Adapters;
using Geekhub.App.Modules.Users.Models;
using Geekhub.App.Modules.Users.Support;

namespace Geekhub.App.Modules.Users.CommandHandlers
{
    public class SendUserLoginEmailCommandHandler
    {
        private readonly UserValidationCodeGenerator _userValidationCodeGenerator = new UserValidationCodeGenerator();
        private readonly IEmailAdapter _emailAdapter = Alerts.Config.AlertsContainerConfig.CreateEmailAdapter();

        public SendUserLoginEmailCommandHandler(string email)
        {
            var user = DataContext.Current.Users.Single(x => x.Email == email);

            var generatedCode = _userValidationCodeGenerator.GenerateCode(user.InvalidLoginAttempts);
            user.ValidationCode = generatedCode;
            DataContext.Current.Users.Update(user);

            SendValidationEmail(email, generatedCode);
        }
        
        private void SendValidationEmail(string email, string validationCode)
        {
            _emailAdapter.SendMail(email, "Din kode til Geekhub", "Din kode er: " + validationCode);
        }
    }
}