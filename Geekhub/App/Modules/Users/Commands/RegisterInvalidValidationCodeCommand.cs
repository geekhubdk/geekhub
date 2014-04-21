using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Geekhub.App.Modules.Users.Commands
{
    public class RegisterInvalidValidationCodeCommand
    {
        public string Email { get; set; }

        public RegisterInvalidValidationCodeCommand(string email)
        {
            Email = email;
        }
    }
}