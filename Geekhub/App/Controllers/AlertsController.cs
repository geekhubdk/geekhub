using System;
using System.Web.Mvc;
using Geekhub.App.Modules.Alerts.Adapters;
using Geekhub.App.Modules.Alerts.Support;
using Geekhub.App.Modules.Alerts.Data;

namespace Geekhub.App.Controllers
{
    [Route("alerts/{action}")]
    public class AlertsController : Controller
    {
        private AlertsService _alertsService = new AlertsService();

        public ActionResult SendTweets()
        {
            _alertsService.SendTweetsForMeetings(TimeSpan.FromHours(0));
            
            return Content("Done");
        }

        public ActionResult SendNewMeetingsNewsletter()
        {
            _alertsService.SendNewMeetingsNewsletter();

            return Content("Done");
        }
	}
}