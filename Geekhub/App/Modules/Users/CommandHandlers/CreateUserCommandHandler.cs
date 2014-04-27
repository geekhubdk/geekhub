using Geekhub.App.Core.Data;
using Geekhub.App.Modules.Users.Models;

namespace Geekhub.App.Modules.Users.CommandHandlers
{
    public class CreateUserCommandHandler
    {
        public CreateUserCommandHandler(string email, string name)
        {
            var user = new User {Email = email.ToLower(), Name = name};
            DataContext.Current.Users.Add(user);
        }
    }
}