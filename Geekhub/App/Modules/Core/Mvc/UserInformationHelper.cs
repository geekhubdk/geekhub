using System.Web;
using System.Web.Security;
using Geekhub.App.Modules.Core.Support;
using Geekhub.App.Modules.Users.Data;
using Geekhub.App.Modules.Users.Models;

namespace Geekhub.App.Modules.Core.Mvc
{
    public static class UserInformationHelper
    {
        private static UsersRepository _usersRepository = new UsersRepository();

        public static UserInformationViewModel GetUserInformation(this HttpContextBase context)
        {
            if (!context.User.Identity.IsAuthenticated)
                return null;

            if (context.Items["UserInformation"] == null) {
                var user = _usersRepository.GetUserByEmail(context.User.Identity.Name);

                if (user == null) {
                    FormsAuthentication.SignOut();
                    return null;
                }

                var userInformation = new UserInformationViewModel(user);
                
                context.Items["UserInformation"] = userInformation;
            }

            return context.Items["UserInformation"] as UserInformationViewModel;
        }

        public class UserInformationViewModel
        {
            public UserInformationViewModel(User user)
            {
                var hash = user.Email.Trim().ToLower().ToMD5();
                ImageUrl = "http://www.gravatar.com/avatar/" + hash + "?s=256&default=http://www.geekhub.dk/person.png";
                DisplayName = user.Name;
            }

            public string ImageUrl { get; set; }
            public string DisplayName { get; set; }
        }
    }
}