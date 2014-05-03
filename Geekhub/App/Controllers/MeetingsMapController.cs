using Geekhub.App.Modules.Meetings.Data;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Geekhub.App.Controllers
{
    public class MeetingsMapController : Controller
    {
        [Route("Map")]
        public ActionResult Index()
        {
            var meetings = new MeetingsRepository().GetUpcommingMeetings();

            var markers = meetings.GroupBy(x => x.City.Name).Select(x => new { latLng = new[] { x.First().Latitude, x.First().Longtitude }, name = string.Format("{0} i {1}", Pluralize(x.Count(), "event", "events"), x.Key), href = Url.Action("Index", "Meetings", new { city = x.Key }) });
            ViewBag.Markers = new HtmlString(JsonConvert.SerializeObject(markers));

            return View();
        }

        private string Pluralize(int count, string singular, string plural)
        {
            if (count == 1)
                return count + " " + singular;
            return count + " " + plural;
        }
    }
}