using System;
using System.Linq;
using Geekhub.Modules.Core.Data;
using Geekhub.Modules.Users.Models;

namespace Geekhub.Modules.Users.Data
{
    public class UsersRepository
    {
        public User GetUserByEmail(string email)
        {
            return DataContext.Current.Users.SingleOrDefault(x => x.Email.Equals(email, StringComparison.InvariantCultureIgnoreCase));
        }

        public bool IsUserValidationCodeValid(string email, string code)
        {
            var user = DataContext.Current.Users.Single(x => x.Email == email);

            if (code == user.ValidationCode) {
                return true;
            }

            return false;
        }
    }
}