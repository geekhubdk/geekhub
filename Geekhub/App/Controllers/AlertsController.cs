using System;
using System.Web.Mvc;
using Deldysoft.Foundation.CommandHandling;
using Geekhub.App.Core.CommandHandling;
using Geekhub.App.Modules.Alerts.Commands;

namespace Geekhub.App.Controllers
{
    [Route("alerts/{action}")]
    public class AlertsController : Controller
    {
        private readonly CommandBus _commandBus;

        public AlertsController(CommandBus commandBus)
        {
            _commandBus = commandBus;
        }

        public ActionResult SendTweets()
        {
            _commandBus.Execute(new SendTweetsForMeetingsCommand(TimeSpan.FromHours(0)));
            return Content("Done");
        }

        public ActionResult SendNewMeetingsNewsletter()
        {
            _commandBus.Execute(new SendNewMeetingsNewsletterCommand());
            return Content("Done");
        }
	}
}