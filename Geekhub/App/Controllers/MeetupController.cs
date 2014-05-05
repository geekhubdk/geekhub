using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Geekhub.App.Controllers
{
    public class MeetupController : Controller
    {
        [Route("meetup/pullgroup")]
        public ActionResult PullGroup(string group)
        {
            var url = "https://api.meetup.com/2/events?&group_urlname=" + group + "&key=61352153f41187a1d7565553101529";

            var client = new WebClient();
            client.Encoding = System.Text.Encoding.UTF8;
            dynamic json = JsonConvert.DeserializeObject(client.DownloadString(url));
            dynamic results = ((JArray)json.results).Select(x => new MeetupEvent((dynamic)x));
            return Json(results, JsonRequestBehavior.AllowGet);
        }

        public class MeetupEvent
        {
            public MeetupEvent(dynamic obj)
            {
                Description = obj.description;
                Name = obj.name;
                int offset = (int)((long)obj.utc_offset/1000/60/60);
                StartsAt = UnixTimeStampToDateTime((double)obj.time).ToString();
            }

            public string StartsAt { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }

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