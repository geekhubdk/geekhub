using System;
using System.Linq;
using Geekhub.App.Core.CommandHandling;
using Geekhub.App.Core.Data;
using Geekhub.App.Modules.Users.Models;

namespace Geekhub.App.Modules.Users.Queries
{
    public class FetchUserByEmailQuery
    {
        public FetchUserByEmailQuery(string email)
        {
            User = DataContext.Current.Users.SingleOrDefault(x => x.Email.Equals(email, StringComparison.InvariantCultureIgnoreCase));
        }

        public User User { get; private set; }
    }
}