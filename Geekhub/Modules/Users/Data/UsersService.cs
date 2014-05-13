using System;
using System.Linq;
using Geekhub.Modules.Core.Adapters;
using Geekhub.Modules.Core.Data;
using Geekhub.Modules.Core.Support;
using Geekhub.Modules.Users.Models;
using Geekhub.Modules.Users.Support;

namespace Geekhub.Modules.Users.Data
{
    public class UsersService
    {
        private readonly UserValidationCodeGenerator _userValidationCodeGenerator = new UserValidationCodeGenerator();
        private readonly IEmailAdapter _emailAdapter = ObjectFactory.CreateEmailAdapter();

        public void CreateUser(string email, string name)
        {
            var user = new User {Email = email.ToLower(), Name = name};
            DataContext.Current.Users.Add(user);
        }

        public void ExpireUserValidationCode(string email)
        {
            var user = DataContext.Current.Users.Single(x => x.Email == email);
            user.ValidationCode = null;
            user.InvalidLoginAttempts = 0;
            DataContext.Current.Users.Update(user);
        }
                
        public void RegisterInvalidValidationCode(string email)
        {
            var user = DataContext.Current.Users.Single(x => x.Email.Equals(email, StringComparison.InvariantCultureIgnoreCase));
            user.InvalidLoginAttempts++;

            if (user.InvalidLoginAttempts % 3 == 0) {
                user.ValidationCode = null;
                DataContext.Current.Users.Update(user);
                // We will send a new email
                 SendUserLoginEmail(email);
            }
        }

        public void SendUserLoginEmail(string email)
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