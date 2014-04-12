using System;
using System.Collections.Generic;
using System.Web.Mvc;
using DDay.iCal;
using DDay.iCal.Serialization.iCalendar;

namespace Geekhub.App.Core.Mvc
{
    public class CalendarResult : ActionResult
    {
        private readonly string _name;
        private readonly IEnumerable<Event> _events;

        public CalendarResult(string name, IEnumerable<Event> events)
        {
            _name = name;
            _events = events;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            var calendar = new iCalendar();
            calendar.AddProperty("X-WR-CALDESC", _name);
            calendar.AddProperty("X-WR-CALNAME", _name);

            foreach (var e in _events) {
                var calendarEvent = new DDay.iCal.Event {
                    Description = e.Description,
                    DTEnd = new iCalDateTime(e.DateEnd),
                    DTStamp = new iCalDateTime(e.DateStart),
                    DTStart = new iCalDateTime(e.DateStart),
                    Location = e.Location,
                    Summary = e.Summary,
                    Url = new Uri(e.Url)
                };

                calendar.Events.Add(calendarEvent);
            }

            var s = new iCalendarSerializer();
            context.HttpContext.Response.ContentType = "text/calendar";
            context.HttpContext.Response.Write(s.SerializeToString(calendar));
            context.HttpContext.Response.End();
        }

        public class Event
        {
            public string Description;
            public DateTime DateStart;
            public DateTime DateEnd;
            public string Location;
            public string Summary;
            public string Url;
        }
    }
}