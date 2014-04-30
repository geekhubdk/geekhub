﻿using System;
using System.Web;
using Biggy;
using Geekhub.App.Modules.Alerts.Models;
using Geekhub.App.Modules.Meetings.Models;
using Geekhub.App.Modules.Users.Models;
using System.IO;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Geekhub.App.Core.Data
{
    public class DataContext
    {
        public BiggyList<Meeting> Meetings;
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
                    var p = path + "\\Data\\" + typeof(T).Name + "s.json";
                    var needsUpdate = File.Exists(p) && File.ReadAllText(p).StartsWith("[");

                    if (needsUpdate) {
                        var items = JsonConvert.DeserializeObject<List<T>>(File.ReadAllText(p));
                        var tmpList = new BiggyList<T>(new Biggy.JSON.JsonStore<T>(dbPath: path + "/_tmp"));
                        foreach (var i in items) {
                            tmpList.Add(i);
                        }
                        File.Delete(p);
                        File.Move(path + "/_tmp/Data/" + typeof(T).Name + "s.json", p);
                    }

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