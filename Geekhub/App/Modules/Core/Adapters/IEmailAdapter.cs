namespace Geekhub.App.Modules.Core.Adapters
{
    public interface IEmailAdapter
    {
        void SendMail(string email, string subject, string html);
    }
}
