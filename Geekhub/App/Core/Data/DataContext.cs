using System;
using System.Web;
using Biggy;
using Geekhub.App.Modules.Alerts.Models;
using Geekhub.App.Modules.Meetings.Models;
using Geekhub.App.Modules.Users.Models;

namespace Geekhub.App.Core.Data
{
    public class DataContext
    {
        public InMemoryList<Meeting> Meetings;
        public InMemoryList<User> Users;
        public InMemoryList<MeetingTweetAlert> MeetingTweetAlerts;
        public InMemoryList<NewsletterLog> NewsletterLogs;
        public InMemoryList<NewsletterSubscription> NewsletterSubscriptions;
 
        public static DataContext Current { get; set; }

        static DataContext()
        {
            if (HttpRuntime.AppDomainAppId != null) {
                var path = HttpRuntime.AppDomainAppPath + "App_Data";
                Current = new DataContext(path);
            }
        }

        public DataContext(string path)
        {
            Meetings = CreateList<Meeting>(path);
            Users = CreateList<User>(path);
            MeetingTweetAlerts = CreateList<MeetingTweetAlert>(path);
            NewsletterLogs = CreateList<NewsletterLog>(path);
            NewsletterSubscriptions = CreateList<NewsletterSubscription>(path);
        }

        private static InMemoryList<T> CreateList<T>(string path) where T : new()
        {
            if (string.IsNullOrEmpty(path)) {
                return new InMemoryList<T>();
            } else {
                try {
                    return new BiggyListFixed<T>(dbPath: path);
                }
                catch (Exception ex) {
                    var message = string.Format("Error loading: {0}, Message: {1}", path, ex.Message);
                    throw new Exception(message, ex);
                }
            }
        }
    }


}