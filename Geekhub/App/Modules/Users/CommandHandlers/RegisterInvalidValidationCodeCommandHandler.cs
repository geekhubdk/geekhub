using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


using Geekhub.App.Core.Data;

namespace Geekhub.App.Modules.Users.CommandHandlers
{
    public class RegisterInvalidValidationCodeCommandHandler
    {
        public RegisterInvalidValidationCodeCommandHandler(string email)
        {
            var user = DataContext.Current.Users.Single(x => x.Email.Equals(email, StringComparison.InvariantCultureIgnoreCase));
            user.InvalidLoginAttempts++;

            if (user.InvalidLoginAttempts % 3 == 0) {
                user.ValidationCode = null;
                DataContext.Current.Users.Update(user);
                // We will send a new email
                new SendUserLoginEmailCommandHandler(email);
            }
        }
    }
}