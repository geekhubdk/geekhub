using System.Diagnostics;

namespace Geekhub.App.Modules.Alerts.Adapters
{
    public class DebugTwitterAdapter : ITwitterAdapter
    {
        public void SendTweet(string message)
        {
            Debug.WriteLine("Tweet: " + message);
        }
    }
}