using System;
using TweetSharp;

namespace Geekhub.Modules.Alerts.Adapters
{
    public class LiveTwitterAdapter : ITwitterAdapter
    {
        private const int TweetLength = 120;

        public static bool Initialized { get; protected set; }
        protected static string AccessTokenSecret { get; set; }
        protected static string AccessToken { get; set; }
        protected static string ConsumerSecret { get; set; }
        protected static string ConsumerKey { get; set; }

        public LiveTwitterAdapter(string consumerKey, string consumerSecret, string accessToken, string accessTokenSecret)
        {
            ConsumerKey = consumerKey;
            ConsumerSecret = consumerSecret;
            AccessToken = accessToken;
            AccessTokenSecret = accessTokenSecret;
            Initialized = true;
        }

        public void SendTweet(string message)
        {
            if (string.IsNullOrEmpty(message))
                throw new ArgumentNullException("message");

            if (message.Length > TweetLength)
                throw new ArgumentException("Message is too long. MaxLength is 120", "message");

            var service = new TwitterService(ConsumerKey, ConsumerSecret);
            service.AuthenticateWith(AccessToken, AccessTokenSecret);
            service.SendTweet(new SendTweetOptions {
                Status = message
            });
        }

    }
}