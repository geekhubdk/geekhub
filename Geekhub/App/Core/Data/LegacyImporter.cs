using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Geekhub.App.Core.Support;
using Geekhub.App.Modules.Meetings.Models;
using Newtonsoft.Json;

namespace Geekhub.App.Core.Data
{
    public static class LegacyImporter
    {
        public static void Import()
        {
            var meetings = new List<Meeting>();
            var c = new WebClient();
            c.Encoding = System.Text.Encoding.UTF8;
            dynamic json = JsonConvert.DeserializeObject(c.DownloadString("http://www.geekhub.dk/api/v1/meetings?all=1&show_hidden=true"));

            var cities = new Dictionary<string, City>();
            var organizer = new Dictionary<string, Organizer>();
            var tags = new Dictionary<string, Tag>();
            foreach (var m in json) {
                meetings.Add(CreateFromJson(m, cities, organizer, tags));
            }

            foreach (var m in meetings) {
                DataContext.Current.Meetings.Add(m);
            }
        }

        private static Meeting CreateFromJson(dynamic o, Dictionary<string, City> cities, Dictionary<string, Organizer> organizers, Dictionary<string, Tag> tags)
        {
            string location = o.location;
            
            var city = new City { Name = o.location };
            if (cities.ContainsKey(location.ToLower())) {
                city = cities[location.ToLower()];
            } else {
                cities.Add(city.Name.ToLower(), city);
            }

            var m = new Meeting() {
                Title = o.title,
                StartsAt = o.starts_at,
                Description = o.description,
                Address = o.address,
                City = city,
                Latitude = (double?)o.latitude,
                Longtitude = (double?)o.longtitude,
                Url = o.url,
                CostsMoney = false,
                CreatedAt = o.created_at,
                Id = o.id
            };

            if (string.IsNullOrWhiteSpace(m.Address)) {
                m.Address = m.City.Name;
            }

            m.Organizers.AddRange(((string)o.organizer).Split(new[] {"&", "og"}, StringSplitOptions.RemoveEmptyEntries).Select(x => {
                var organizer =new Organizer() { Name = x.Trim() };

                if (organizers.ContainsKey(organizer.Name.ToLower())) {
                    organizer = organizers[organizer.Name.ToLower()];
                }
                else {
                    organizers.Add(organizer.Name.ToLower(), organizer);
                }

                return organizer;
            }));

            foreach (var tag in o.tags) {
                var t = new Tag() { Name = ((string)tag.name).Trim() };

                if (tags.ContainsKey(t.Name.ToLower())) {
                    t = tags[t.Name.ToLower()];
                } else {
                    tags.Add(t.Name.ToLower(), t);
                }
                m.Tags.Add(t);
            }

            return m;
        }

    }
}