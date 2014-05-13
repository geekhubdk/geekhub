using System;

namespace Geekhub.Modules.Users.Support
{
    public class UserValidationCodeGenerator
    {
        public string GenerateCode(int invalidLoginAttempts)
        {
            int codeLength = 4 + invalidLoginAttempts / 10;

            int max = (int)Math.Pow(10, codeLength) + 1;
            var pattern = "".PadLeft(codeLength, '0');
            var random = new Random();
            return random.Next(0, max).ToString(pattern);
        }
    }
}