namespace Geekhub.App.Modules.Users.Commands
{
    public class SendUserLoginEmailCommand
    {
        public SendUserLoginEmailCommand(string userEmail)
        {
            UserEmail = userEmail;
        }

        public string UserEmail { get; set; }
    }
}