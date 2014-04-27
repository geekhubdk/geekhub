using System.Linq;


using Geekhub.App.Core.Data;

namespace Geekhub.App.Modules.Users.CommandHandlers
{
    public class ExpireUserValidationCodeCommandHandler
    {
        public ExpireUserValidationCodeCommandHandler(string email)
        {
            var user = DataContext.Current.Users.Single(x => x.Email == email);
            user.ValidationCode = null;
            user.InvalidLoginAttempts = 0;
            DataContext.Current.Users.Update(user);
        }
    }
}