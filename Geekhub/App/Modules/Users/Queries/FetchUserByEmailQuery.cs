using System;
using System.Linq;
using Geekhub.App.Core.CommandHandling;
using Geekhub.App.Core.Data;
using Geekhub.App.Modules.Users.Models;

namespace Geekhub.App.Modules.Users.Queries
{
    public class FetchUserByEmailQuery : QueryBase
    {
        public FetchUserByEmailQuery(DataContext dataContext) : base(dataContext)
        {
        }

        public User Execute(string email)
        {
            return
                DataContext.Users.SingleOrDefault(x => x.Email.Equals(email, StringComparison.InvariantCultureIgnoreCase));
        }
    }
}