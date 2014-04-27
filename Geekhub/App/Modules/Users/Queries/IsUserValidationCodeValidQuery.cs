using System.Linq;
using Geekhub.App.Core.CommandHandling;
using Geekhub.App.Core.Data;

namespace Geekhub.App.Modules.Users.Queries
{
    public class IsUserValidationCodeValidQuery
    {
        public IsUserValidationCodeValidQuery(string email, string code)
        {
            var user = DataContext.Current.Users.Single(x => x.Email == email);

            if (code == user.ValidationCode) {
                IsValid = true;
                return;
            }

            IsValid = false;
        }

        public bool IsValid { get; private set; }

    }
}