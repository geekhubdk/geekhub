namespace Geekhub.App.Modules.Alerts.Commands
{
    public class SubscribeToNewsletterCommand
    {
        public SubscribeToNewsletterCommand(string email)
        {
            Email = email;
        }

        public string Email { get; set; }
    }
}