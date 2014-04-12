namespace Geekhub.App.Modules.Alerts.Commands
{
    public class UnsubscribeToNewsletterCommand
    {
        public UnsubscribeToNewsletterCommand(string email)
        {
            Email = email;
        }

        public string Email { get; set; }
    }
}