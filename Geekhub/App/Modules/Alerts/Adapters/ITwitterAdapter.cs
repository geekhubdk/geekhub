namespace Geekhub.App.Modules.Alerts.Adapters
{
    public interface ITwitterAdapter
    {
        void SendTweet(string message);
    }
}