using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using Geekhub.App.Controllers;
using Geekhub.App.Core.Support;
using Geekhub.App.Modules.Meetings.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Geekhub.App.Modules.MeetingProviders
{
    public class MeetupMeetingProvider
    {
        public MeetingSubject[] GetSubjects()
        {
            var groups = new [] { 
                new MeetupGroupSettings() { Identifier = "umbraco-odense", DefaultCity = "Odense", DefaultTags = new [] {"umbraco", "web", "c#" } }, 
                new MeetupGroupSettings() { Identifier = "dwodense", DefaultCity = "Odense", DefaultTags = new [] {"web", "design", "javascript" } }, 
            };

            return groups.SelectMany(GetSubject).ToArray();
        }

        public MeetingSubject[] GetSubject(MeetupGroupSettings group)
        {
            var url = "https://api.meetup.com/2/events?&status=upcoming&group_urlname=" + group.Identifier + "&key=" + Secrets.Get("Meetup.ApiKey");

            var client = new WebClient();
            client.Encoding = System.Text.Encoding.UTF8;
            dynamic json = JsonConvert.DeserializeObject(client.DownloadString(url));
            var results = ((JArray)json.results).Select(x => new MeetupEvent(group, (dynamic)x).ToMeetingSubject()).ToArray();

            return results.Where(x => (x.StartsAt - DateTime.Now).TotalDays < 90).ToArray();
        }

        public class MeetupGroupSettings
        {
            public string Identifier { get; set; }
            public string DefaultCity { get; set; }
            public string[] DefaultTags { get; set; }
        }

        public class MeetupEvent
        {
            private readonly MeetupGroupSettings _settings;
            private dynamic _inner;
            public MeetupEvent(MeetupGroupSettings settings, dynamic obj)
            {
                _settings = settings;
                _inner = obj;
            }

            public string Id
            {
                get
                {
                    return _inner.id;
                }
            }

            public DateTime StartsAt
            {
                get
                {
                    return UnixTimeStampToDateTime((double)_inner.time);
                }
            }
            public string Name
            {
                get
                {
                    return _inner.name;
                }
            }
            public string Description
            {
                get
                {
                    return _inner.description;
                }
            }
            public string Url
            {
                get
                {
                    return _inner.event_url;
                }
            }
            public string Address
            {
                get
                {
                    if (_inner.venue == null)
                        return null;

                    return _inner.venue.address_1;
                }
            }

            public string City
            {
                get
                {
                    if (_inner.venue == null)
                        return _settings.DefaultCity;

                    return _inner.venue.city;
                }
            }

            public string Organizer
            {
                get
                {
                    return _inner.group.name;
                }
            }

            public string[] Tags {
                get
                {
                    return _settings.DefaultTags;
                }
            }

            public MeetingSubject ToMeetingSubject()
            {
                var m = new MeetingSubject();

                m.Address = (Address + " " + City).Trim();
                m.Description = HtmlToPlainConverter.Convert(Description).Trim('\r', '\n');
                m.Organizers.Add(Organizer);
                m.Url = Url;
                m.Title = Name;
                m.Provider = "Meetup";
                m.ProviderIdentifier = Id;
                m.StartsAt = StartsAt;
                m.Tags.AddRange(Tags);

                return m;
            }

            public static DateTime UnixTimeStampToDateTime(double unixTimeStamp)
            {
                // Unix timestamp is seconds past epoch
                System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
                dtDateTime = dtDateTime.AddMilliseconds(unixTimeStamp).ToLocalTime();
                return dtDateTime;
            }
        }

    }
}