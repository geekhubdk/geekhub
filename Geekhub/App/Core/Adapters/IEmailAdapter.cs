namespace Geekhub.App.Core.Adapters
{
    public interface IEmailAdapter
    {
        void SendMail(string email, string subject, string html);
    }
}
