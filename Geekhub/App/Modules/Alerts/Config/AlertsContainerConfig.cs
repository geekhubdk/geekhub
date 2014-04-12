using System;
using System.Configuration;
using Autofac;
using Deldysoft.Foundation;
using Geekhub.App.Core.Config;
using Geekhub.App.Modules.Alerts.Adapters;

namespace Geekhub.App.Modules.Alerts.Config
{
    public class AlertsContainerConfig : ModuleContainerConfigBase
    {
        public override void RegisterDevelopment()
        {
            Container.Register(x => new DebugTwitterAdapter()).As<ITwitterAdapter>();
        }

        public override void RegisterLive()
        {
            Container.Register(CreateTwitterAdapter).As<ITwitterAdapter>();
        }

        private static ITwitterAdapter CreateTwitterAdapter(IComponentContext componentContext)
        {
            var consumerKey = GetApplicationSetting("Twitter.ConsumerKey");
            var consumerSecret = GetApplicationSetting("Twitter.ConsumerSecret");
            var accessToken = GetApplicationSetting("Twitter.AccessToken");
            var accessTokenSecret = GetApplicationSetting("Twitter.AccessTokenSecret");

            var twitterAdapter = new LiveTwitterAdapter(consumerKey, consumerSecret, accessToken, accessTokenSecret);
            return twitterAdapter;
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