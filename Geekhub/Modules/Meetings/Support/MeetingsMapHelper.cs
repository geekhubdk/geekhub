using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Geekhub.Modules.Meetings.Models;
using Newtonsoft.Json;

namespace Geekhub.Modules.Meetings.Support
{
    public class MeetingsMapHelper
    {
        public static dynamic CreateMeetingMapModel(IEnumerable<Meeting> meetings, UrlHelper urlHelper)
        {
            var markers =
                meetings.GroupBy(x => x.City.Name)
                    .Select(
                        x =>
                            new {
                                latLng = new[] {x.First().Latitude, x.First().Longtitude},
                                name = string.Format("{0} i {1}", Pluralize(x.Count(), "event", "events"), x.Key),
                                href = urlHelper.Action("Index", "Meetings", new {city = x.Key})
                            });
            return new HtmlString(JsonConvert.SerializeObject(markers));
        }

        private static string Pluralize(int count, string singular, string plural)
        {
            if (count == 1)
                return count + " " + singular;
            return count + " " + plural;
        }
    }
}