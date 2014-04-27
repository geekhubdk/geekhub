using System;
using System.Web.Mvc;


using Geekhub.App.Modules.Alerts.CommandHandlers;
using Geekhub.App.Modules.Alerts.Adapters;
using Geekhub.App.Modules.Alerts.Support;
using Geekhub.App.Modules.Alerts.Config;

namespace Geekhub.App.Controllers
{
    [Route("alerts/{action}")]
    public class AlertsController : Controller
    {
        public ActionResult SendTweets()
        {
            new SendTweetsForMeetingsCommandHandler(TimeSpan.FromHours(0));
            
            return Content("Done");
        }

        public ActionResult SendNewMeetingsNewsletter()
        {
            new SendNewMeetingsNewsletterCommandHandler();

            return Content("Done");
        }
	}
}