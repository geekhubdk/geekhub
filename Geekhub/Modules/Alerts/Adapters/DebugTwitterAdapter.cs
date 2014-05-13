using System.Diagnostics;

namespace Geekhub.Modules.Alerts.Adapters
{
    public class DebugTwitterAdapter : ITwitterAdapter
    {
        public void SendTweet(string message)
        {
            Debug.WriteLine("Tweet: " + message);
        }
    }
}