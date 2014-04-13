using System.Web.Mvc;
using Deldysoft.Foundation.CommandHandling;
using Geekhub.App.Modules.Alerts.Commands;
using Geekhub.App.Modules.Alerts.Queries;
using Geekhub.App.Modules.Alerts.Support;

namespace Geekhub.App.Controllers
{
    public class NewsletterSubscriptionsController : ControllerBase
    {
        private readonly ICommandExecuter _commandExecuter;
        private readonly SubscriberCountQuery _subscriberCountQuery;

        public NewsletterSubscriptionsController(ICommandExecuter commandExecuter, SubscriberCountQuery subscriberCountQuery)
        {
            _commandExecuter = commandExecuter;
            _subscriberCountQuery = subscriberCountQuery;
        }

        [Route("newsletter/subscribe")]
        public ActionResult Create()
        {
            IncludeSubscriberCount();
            return View();
        }

        [Route("newsletter/subscribe")]
        [HttpPost]
        public ActionResult Create(string email)
        {
            IncludeSubscriberCount();
            if (string.IsNullOrEmpty(email)) {
                return View();
            }

            _commandExecuter.Execute(new SubscribeToNewsletterCommand(email));

            Notice("Du er tilmeldt nyhedsbrevet");
            return Redirect("~/");
        }

        [Route("newsletter/unsubscribe")]
        public ActionResult Delete()
        {
            return View();
        }

        [Route("newsletter/unsubscribe")]
        [HttpPost]
        public ActionResult Delete(string email)
        {
            if (string.IsNullOrEmpty(email)) {
                return View();
            }

            try {
                _commandExecuter.Execute(new UnsubscribeToNewsletterCommand(email));
            } catch (SubscriptionNotFoundException) {
                Warn("Vi kunne ikke finde dig. Prøv en anden email");
                return View();
            }
            
            Notice("Du er nu afmeldt nyhedsbreve");

            return Redirect("~/");
        }
        
        private void IncludeSubscriberCount()
        {
            ViewBag.SubscriberCount = _subscriberCountQuery.Execute();
        }

	}
}