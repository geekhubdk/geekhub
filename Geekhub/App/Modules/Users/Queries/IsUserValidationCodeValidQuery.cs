using System.Linq;
using Geekhub.App.Core.CommandHandling;
using Geekhub.App.Core.Data;

namespace Geekhub.App.Modules.Users.Queries
{
    public class IsUserValidationCodeValidQuery : QueryBase
    {
        public IsUserValidationCodeValidQuery(DataContext dataContext) : base(dataContext)
        {
        }

        public bool Execute(string email, string code)
        {
            var user = DataContext.Users.Single(x => x.Email == email);

            if (code == user.ValidationCode) {
                return true;
            }

            DataContext.Users.Update(user);

            return false;
        }
    }
}