namespace Geekhub.Modules.Alerts.Adapters
{
    public interface ITwitterAdapter
    {
        void SendTweet(string message);
    }
}