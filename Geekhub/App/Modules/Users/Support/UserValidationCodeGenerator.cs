using System;

namespace Geekhub.App.Modules.Users.Support
{
    public class UserValidationCodeGenerator
    {
        public string GenerateCode()
        {
            var random = new Random();
            return random.Next(0, 1001).ToString("0000");
        }
    }
}