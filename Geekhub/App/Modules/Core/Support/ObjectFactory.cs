using System;
using System.Configuration;
using Deldysoft.Foundation;
using Geekhub.App.Modules.Alerts.Adapters;
using Geekhub.App.Modules.Core.Adapters;

namespace Geekhub.App.Modules.Core.Support
{
    public class ObjectFactory
    {
        public static ITwitterAdapter CreateTwitterAdapter()
        {
            if (AppEnvironment.Current == EnvironmentType.Development) {
                return new DebugTwitterAdapter();
            }

            var consumerKey = GetApplicationSetting("Twitter.ConsumerKey");
            var consumerSecret = GetApplicationSetting("Twitter.ConsumerSecret");
            var accessToken = GetApplicationSetting("Twitter.AccessToken");
            var accessTokenSecret = GetApplicationSetting("Twitter.AccessTokenSecret");

            var twitterAdapter = new LiveTwitterAdapter(consumerKey, consumerSecret, accessToken, accessTokenSecret);
            return twitterAdapter;
        }

        public static IEmailAdapter CreateEmailAdapter()
        {
            return new SmtpEmailAdapter();
        }

        private static string GetApplicationSetting(string key)
        {
            var value = ConfigurationManager.AppSettings[key];
            if (string.IsNullOrWhiteSpace(value)) {
                throw new Exception("There is no valid for the required AppSettings key: " + key);
            }
            return value;
        }

    }
}