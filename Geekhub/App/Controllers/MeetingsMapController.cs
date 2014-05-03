using Geekhub.App.Modules.Meetings.Data;
using Geekhub.App.Modules.Meetings.Models;
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

            ViewData.Model = MeetingsMapHelper.CreateMeetingMapModel(meetings, Url);

            return View();
        }
    }
}