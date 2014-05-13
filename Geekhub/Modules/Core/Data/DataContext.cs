using System;
using System.Web;
using Biggy;
using Geekhub.Modules.Alerts.Models;
using Geekhub.Modules.Meetings.Models;
using Geekhub.Modules.Users.Models;

namespace Geekhub.Modules.Core.Data
{
    public class DataContext
    {
        public BiggyList<Meeting> Meetings;
        public BiggyList<MeetingSubject> MeetingSubjects;
        public BiggyList<User> Users;
        public BiggyList<MeetingTweetAlert> MeetingTweetAlerts;
        public BiggyList<NewsletterLog> NewsletterLogs;
        public BiggyList<NewsletterSubscription> NewsletterSubscriptions;
 
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
            MeetingSubjects = CreateList<MeetingSubject>(path);
            Users = CreateList<User>(path);
            MeetingTweetAlerts = CreateList<MeetingTweetAlert>(path);
            NewsletterLogs = CreateList<NewsletterLog>(path);
            NewsletterSubscriptions = CreateList<NewsletterSubscription>(path);
        }

        private static BiggyList<T> CreateList<T>(string path) where T : new()
        {
            if (string.IsNullOrEmpty(path)) {
                return new BiggyList<T>();
            } else {
                try {
                    return new BiggyList<T>(new Biggy.JSON.JsonStore<T>(dbPath: path));
                }
                catch (Exception ex) {
                    var message = string.Format("Error loading: {0}, Message: {1}", path, ex.Message);
                    throw new Exception(message, ex);
                }
            }
        }
    }


}