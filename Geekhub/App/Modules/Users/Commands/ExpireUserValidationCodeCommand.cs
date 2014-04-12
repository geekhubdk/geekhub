namespace Geekhub.App.Modules.Users.Commands
{
    public class ExpireUserValidationCodeCommand
    {
        public ExpireUserValidationCodeCommand(string userEmail)
        {
            UserEmail = userEmail;
        }

        public string UserEmail { get; private set; }
    }
}